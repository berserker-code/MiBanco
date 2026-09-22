<script setup>
import { ref, reactive } from "vue";

const emit = defineEmits(["cliente-registrado"]);

const cliente = reactive({
  nombre: "",
  apellido: "",
  edad: null,
  email: "",
  direccion: "",
});

const cargando = ref(false);
const mensajeExito = ref(null);
const mensajeError = ref(null);

async function registrarCliente() {
  cargando.value = true;
  mensajeExito.value = null;
  mensajeError.value = null;

  try {
    const respuesta = await fetch("https://localhost:7083/api/clientes", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(cliente),
    });

    if (!respuesta.ok) {
      throw new Error("Error al registrar el cliente");
    }
    const datos = await respuesta.json();
    mensajeExito.value = `Cliente ${datos.cliente.nombre} registrado con cuenta N° ${datos.cuenta.idCuenta}`;
    emit("cliente-registrado", datos.cliente.id);
  } catch (error) {
    mensajeError.value = error.message;
  } finally {
    cargando.value = false;
  }
}
</script>

<template>
  <form @submit.prevent="registrarCliente">
    <input v-model="cliente.nombre" type="text" placeholder="Nombre" />
    <input v-model="cliente.apellido" type="text" placeholder="Apellido" />
    <input v-model.number="cliente.edad" type="number" placeholder="Edad" />
    <input v-model="cliente.email" type="email" placeholder="Email" />
    <input v-model="cliente.direccion" type="text" placeholder="Direccion" />
    <button type="submit" :disabled="cargando">
      {{ cargando ? "Registrando..." : "Registrar" }}
    </button>
  </form>

  <p v-if="mensajeExito" style="color: green">{{ mensajeExito }}</p>
  <p v-if="mensajeError" style="color: red">{{ mensajeError }}</p>
</template>
