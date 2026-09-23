<script setup>
import { ref } from "vue";

const emit = defineEmits(["cliente-registrado"]);

const clientes = ref([]);
const idBuscado = ref(null);
const clienteEncontrado = ref(null);
const cargando = ref(false);
const mensajeError = ref(null);

async function listarTodos() {
  cargando.value = true;
  mensajeError.value = null;

  try {
    const respuestas = await fetch(`https://localhost:7083/api/clientes`);
    if (!respuestas.ok) {
      throw new Error("Error al listar clientes");
    }
    clientes.value = await respuestas.json();
  } catch (error) {
    mensajeError.value = error.message;
  } finally {
    cargando.value = false;
  }
}

async function buscarPorId() {
  cargando.value = true;
  mensajeError.value = null;

  try {
    const respuesta = await fetch(`https://localhost:7083/api/clientes/${idBuscado.value}`);
    if (!respuesta.ok) {
      throw new Error("id no encontrado");
    }
    clienteEncontrado.value = await respuesta.json();
  } catch (error) {
    mensajeError.value = error.message;
  } finally {
    cargando.value = false;
  }
}

async function seleccionarCliente(idCliente) {
  cargando.value = true;
  mensajeError.value = null;

  try {
    const respuesta = await fetch(`https://localhost:7083/api/cuentas/${idCliente}`);
    if (!respuesta.ok) {
      throw new Error("Error en la busqueda");
    }
    const datos = await respuesta.json();
    emit("cliente-registrado", { idCliente, idCuenta: datos.idCuenta });
  } catch (error) {
    mensajeError.value = error.message;
  } finally {
    cargando.value = false;
  }
}
</script>

<template>
  <div>
    <button @click="listarTodos" :disabled="cargando">ver todos los clientes</button>
    <ul>
      <li v-for="c in clientes" :key="c.id">
        {{ c.nombre }} {{ c.apellido }} (Id: {{ c.id }})
        <button @click="seleccionarCliente(c.id)">seleccionar</button>
      </li>
    </ul>

    <form @submit.prevent="buscarPorId">
      <input v-model.number="idBuscado" type="number" placeholder="buscar por id" />
      <button type="submit" :disabled="cargando">buscar</button>
    </form>

    <div v-if="clienteEncontrado">
      <p>{{ clienteEncontrado.nombre }} {{ clienteEncontrado.apellido }}</p>
      <button @click="seleccionarCliente(clienteEncontrado.id)">seleccionar cliente</button>
    </div>

    <p v-if="mensajeError" style="color: red">{{ mensajeError }}</p>
  </div>
</template>
