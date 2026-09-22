<script setup>
import HelloWorld from "./components/HelloWorld.vue";
import RegistrarCliente from "./components/RegistrarCliente.vue";
import ConsultarCuenta from "./components/ConsultarCuenta.vue";
import RealizarTransaccion from "./components/RealizarTransaccion.vue";
import HistorialTransaccion from "./components/HistorialTransaccion.vue";
import { ref } from "vue";

const idClienteActual = ref(null);
const idCuentaActual = ref(null);
const refrescarTrigger = ref(0);

function onCLienteResgistrado(datos) {
  idClienteActual.value = datos.idCliente;
  idCuentaActual.value = datos.idCuenta;
}

function onTransaccionRealizada() {
  refrescarTrigger.value++;
}
</script>

<template>
  <header>
    <img alt="Vue logo" class="logo" src="./assets/logo.svg" width="125" height="125" />

    <div class="wrapper">
      <HelloWorld msg="You did it!" />
    </div>
  </header>

  <main>
    <RegistrarCliente @cliente-registrado="onCLienteResgistrado" />
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
