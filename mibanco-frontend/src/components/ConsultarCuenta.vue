<script setup>
import { watch, ref } from "vue";

const props = defineProps({
  idCliente: {
    type: Number,
    required: true,
  },
});

const cargando = ref(false);
const cuenta = ref(null);
const mensajeError = ref(null);

async function consultarCuenta(idCliente) {
  cargando.value = true;
  cuenta.value = null;
  mensajeError.value = null;

  try {
    const respuesta = await fetch(`https://localhost:7083/api/cuentas/${idCliente}`);
    if (!respuesta.ok) {
      throw new Error("Error al consultar la cuenta");
    }
    cuenta.value = await respuesta.json();
  } catch (error) {
    mensajeError.value = error.message;
  } finally {
    cargando.value = false;
  }
}

watch(
  () => props.idCliente,
  (nuevoId) => {
    if (nuevoId) {
      consultarCuenta(nuevoId);
    }
  },
  { immediate: true },
);
</script>

<template>
  <div v-if="cuenta" style="margin-top: 1rem">
    <p><strong>Cuenta N°:</strong> {{ cuenta.idCuenta }}</p>
    <p><strong>Saldo:</strong> {{ cuenta.saldo }}</p>
  </div>
  <p v-if="mensajeError" style="color: red">{{ mensajeError }}</p>
</template>
