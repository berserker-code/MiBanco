<script setup>
import { reactive } from "vue";

const cliente = reactive({
  nombre: "",
  apellido: "",
  edad: null,
  email: "",
  direccion: "",
});

async function registrarCliente() {
  const respuesta = await fetch("https://localhost:7083/api/clientes", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(cliente),
  });
  const datos = await respuesta.json();
  console.log(datos);
}
</script>

<template>
  <form @submit.prevent="registrarCliente">
    <input v-model="cliente.nombre" type="text" placeholder="Nombre" />
    <input v-model="cliente.apellido" type="text" placeholder="Apellido" />
    <input v-model.number="cliente.edad" type="number" placeholder="Edad" />
    <input v-model="cliente.email" type="email" placeholder="Email" />
    <input v-model="cliente.direccion" type="text" placeholder="Direccion" />
    <button type="submit">Registrar</button>
  </form>
</template>
