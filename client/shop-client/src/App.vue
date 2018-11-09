<template>
  <div id="app">
    <NavTop id="nav__top"/>
    <NavSide id="nav__side"/>
    <div id="content">
      <img alt="Vue logo" src="./assets/logo.png">
      <HelloWorld msg="Welcome to Your Vue.js App"/>
    </div>
  </div>
</template>

<script>
import HelloWorld from './components/HelloWorld.vue'
import NavTop from './components/Nav/Top.vue'
import NavSide from './components/Nav/Side.vue'

export default {
  name: 'app',
  components: {
    HelloWorld,
    NavTop,
    NavSide
  }
}
</script>

<style>
body {
  padding: 0;
  margin: 0;
}
#app {
  font-family: 'Avenir', Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin: 0;
  padding: 0;
  height: 100%;
  width: 100%;
}
#nav__top {
  height: 50px;
  width: 100%;
  margin: 0;
  padding: 0;
  position: fixed;
  top: 0;
  left: 0;
  z-index: 100;
}
#nav__side {
  height: 100%;
  width: 60px;
  margin: 0;
  padding: 0;
  position: fixed;
  top: 50px;
  left: 0;
  z-index: 99;
}
#content {
  height: 100%;
  width: 100%;
  top: 50px;
  left: 60px;
  position: fixed;
  background-color: aqua;
}
</style>
