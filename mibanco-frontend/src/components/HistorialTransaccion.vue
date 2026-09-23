<script setup>
import { ref, watch } from "vue";

const props = defineProps({
  idCuenta: {
    type: Number,
    required: true,
  },
  trigger: {
    type: Number,
    required: true,
  },
});

const historial = ref([]);
const cargando = ref(false);
const mensajeError = ref(null);

async function obtenerHistorial() {
  historial.value = [];
  cargando.value = true;
  mensajeError.value = null;

  try {
    const respuesta = await fetch(`https://localhost:7083/api/transacciones/${props.idCuenta}`);
    if (!respuesta.ok) {
      throw new Error("Error al obtener el historial de transacciones");
    }
    historial.value = await respuesta.json();
  } catch (error) {
    mensajeError.value = error.message;
  } finally {
    cargando.value = false;
  }
}

watch(
  () => [props.idCuenta, props.trigger],
  ([nuevoId]) => {
    if (nuevoId) {
      obtenerHistorial();
    }
  },
  { immediate: true },
);
</script>

<template>
  <ul>
    <li v-for="t in historial" :key="t.idTransaccion">
      {{ t.formaTransaccion }} - ${{ t.movimiento }} - saldo resultante: $ {{ t.saldoTotal }}
    </li>
  </ul>
  <p v-if="mensajeError" style="color: red">{{ mensajeError }}</p>
</template>
