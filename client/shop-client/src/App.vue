<template>
  <div id="app">
    <Nav id="nav"/>
    <div id="content">
      <div id="content-area">

      </div>
    </div>
    <Pmqa id="pmqa"/>
  </div>
</template>

<script>
import Nav from './components/Menus/Nav.vue'
import Pmqa from './components/Menus/PMQuickAccess.vue'

export default {
  name: 'app',
  components: {
    Pmqa,
    Nav
  }
}
</script>

<style>
body {
  padding: 0;
  margin: 0;
  height: 100%;
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
  display: grid;
  grid-template-columns: 300px auto 300px;
  grid-template-rows: auto;
}
#nav {
  grid-row: 1 / 2;
  grid-column: 1 / 2;
  height: 100%;
  width: 100%;
  /*width: 300px;*/
  margin: 0;
  padding: 0;
  z-index: 99;
  background-color: #425B72;
}
#content {
  grid-row: 1 / 2;
  grid-column: 2 / 3;
  height: 100%;
  width: 100%;
  background-color: #EFEFF4;
}
#content-area {
  /* margin: 28px; */
  width: 97%;
  height: 97%;
  margin: 1.5%;
  border-radius: 7px;
  background-color: #FFFFFF;
}
#pmqa {
  grid-row: 1 / 2;
  grid-column: 3 / 4;
  height: 100%;
  width: 100%;
  /* width: 300px; */
  background-color: #425B72;
}
</style>
