<script setup>
import { ref, reactive } from "vue";

const emit = defineEmits(["transaccion-realizada"]);

const props = defineProps({
  idCuenta: {
    type: Number,
    required: true,
  },
});

const transaccion = reactive({
  tipoTransaccion: "",
  movimiento: null,
});

const cargando = ref(false);
const mensajeExito = ref(null);
const mensajeError = ref(null);

async function realizarTransaccion() {
  cargando.value = true;
  mensajeExito.value = null;
  mensajeError.value = null;

  try {
    const cuerpo = {
      idCuenta: props.idCuenta,
      formaTransaccion: transaccion.tipoTransaccion,
      movimiento: transaccion.movimiento,
    };
    const respuesta = await fetch(`https://localhost:7083/api/transacciones`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(cuerpo),
    });

    if (!respuesta.ok) {
      const textoError = await respuesta.text();
      throw new Error(textoError);
    }

    const datos = await respuesta.json();
    mensajeExito.value = `Transaccion realizada con exito. Nuevo saldo: ${datos.saldoTotal}`;
    emit("transaccion-realizada", datos.saldoTotal);
  } catch (error) {
    mensajeError.value = error.message;
  } finally {
    cargando.value = false;
  }
}
</script>

<template>
  <form @submit.prevent="realizarTransaccion">
    <select v-model="transaccion.tipoTransaccion" required>
      <option value="" disabled>selecccione el tipo de trasaccion</option>
      <option value="Consignacion">Consignacion</option>
      <option value="Retiro">Retiro</option>
    </select>

    <input
      v-model.number="transaccion.movimiento"
      type="number"
      placeholder="monto"
      required
      min="1"
    />
    <button type="submit" :disabled="cargando">
      {{ cargando ? "Procesando..." : "Realizar Transaccion" }}
    </button>
  </form>
  <p v-if="mensajeExito" style="color: green">{{ mensajeExito }}</p>
  <p v-if="mensajeError" style="color: red">{{ mensajeError }}</p>
</template>
