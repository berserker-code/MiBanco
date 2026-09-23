<script setup>
import { watch, ref } from "vue";

const props = defineProps({
  idCliente: {
    type: Number,
    required: true,
  },
  trigger: {
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
  () => [props.idCliente, props.trigger],
  ([nuevoId]) => {
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

<style scoped>
div {
  border: 1px solid #e2e8f0;
  border-radius: 8px;
  padding: 1rem;
  margin: 1rem 0;
  background: #f8fafc;
  color: #1e293b;
}
</style>
