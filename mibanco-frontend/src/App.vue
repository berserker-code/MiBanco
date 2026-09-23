<script setup>
import HelloWorld from "./components/HelloWorld.vue";
import RegistrarCliente from "./components/RegistrarCliente.vue";
import ConsultarCuenta from "./components/ConsultarCuenta.vue";
import RealizarTransaccion from "./components/RealizarTransaccion.vue";
import HistorialTransaccion from "./components/HistorialTransaccion.vue";
import { ref } from "vue";
import BuscarCliente from "./components/BuscarCliente.vue";

const pestañaActiva = ref("registrar");
const idClienteActual = ref(null);
const idCuentaActual = ref(null);
const refrescarTrigger = ref(0);

function onCLienteResgistrado(datos) {
  idClienteActual.value = datos.idCliente;
  idCuentaActual.value = datos.idCuenta;
  pestañaActiva.value = "operaciones";
}

function onTransaccionRealizada() {
  refrescarTrigger.value++;
}
</script>

<template>
  <header>
    <img alt="Vue logo" class="logo" src="./assets/logo.svg" width="125" height="125" />

    <nav>
      <button
        @click="pestañaActiva = 'registrar'"
        :class="{ activa: pestañaActiva === 'registrar' }"
      >
        registrar
      </button>
      <button @click="pestañaActiva = 'buscar'" :class="{ activa: pestañaActiva === 'buscar' }">
        buscar cliente
      </button>
      <button
        @click="pestañaActiva = 'operaciones'"
        :class="{ activa: pestañaActiva === 'operaciones' }"
      >
        operaciones
      </button>
    </nav>
    <div class="wrapper">
      <HelloWorld msg="You did it!" />
    </div>
  </header>

  <main>
    <RegistrarCliente
      v-if="pestañaActiva === 'registrar'"
      @cliente-registrado="onCLienteResgistrado"
    />
    <BuscarCliente v-if="pestañaActiva === 'buscar'" @cliente-registrado="onCLienteResgistrado" />

    <div v-if="pestañaActiva === 'operaciones'">
      <ConsultarCuenta
        v-if="idClienteActual"
        :idCliente="idClienteActual"
        :trigger="refrescarTrigger"
      />
      <RealizarTransaccion
        v-if="idCuentaActual"
        :idCuenta="idCuentaActual"
        @transaccion-realizada="onTransaccionRealizada"
      />
      <HistorialTransaccion
        v-if="idCuentaActual"
        :idCuenta="idCuentaActual"
        :trigger="refrescarTrigger"
      />
    </div>
  </main>
</template>

<style scoped>
header {
  line-height: 1.5;
}

.logo {
  display: block;
  margin: 0 auto 2rem;
}

@media (min-width: 1024px) {
  header {
    display: flex;
    place-items: center;
    padding-right: calc(var(--section-gap) / 2);
  }

  .logo {
    margin: 0 2rem 0 0;
  }

  header .wrapper {
    display: flex;
    place-items: flex-start;
    flex-wrap: wrap;
  }
}
</style>
