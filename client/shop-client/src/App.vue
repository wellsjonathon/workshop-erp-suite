<template>
  <div id="app">
    <NavBar class="nav"/>
    <div id="content">
      <router-view></router-view>
    </div>
    <PMQuickAccess class="pmqa"/>
  </div>
</template>

<script>
import NavBar from './components/Menus/NavBar.vue'
import PMQuickAccess from './components/Menus/PMQuickAccess.vue'

export default {
  name: 'app',
  components: {
    NavBar,
    PMQuickAccess
  }
}
</script>

<style>
html {
  font-size: 62.5%;
  /* font-family: 'Roboto', sans-serif; */
  font-family: 'Montserrat', sans-serif
}
body {
  padding: 0;
  margin: 0;
  height: 100%;
}
#app {
  /* font-family: 'Avenir', Helvetica, Arial, sans-serif; */
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  /* text-align: center;
  color: #2c3e50; */
  margin: 0;
  padding: 0;
  height: 100%;
  width: 100%;
  display: grid;
  grid-template-columns: auto 1fr auto;
  grid-template-rows: auto;
}
.nav {
  grid-row: 1 / 2;
  grid-column: 1 / 2;
  position: relative;
  height: 100%;
  margin: 0;
  padding: 0;
  z-index: 99;
}
#content {
  grid-row: 1 / 2;
  grid-column: 2 / 3;
  height: 100%;
  width: 100%;
  background-color: #EFEFF4;
}
/* .page {
  width: 95%;
  height: 97%;
  margin: 14px auto;
  border-radius: 7px;
  box-sizing: border-box;
  background-color: #FFFFFF;
  box-shadow: 2px 2px 8px rgba(1,1,1,0.1),
              -2px 2px 8px rgba(1,1,1,0.1);
} */
.pmqa {
  grid-row: 1 / 2;
  grid-column: 3 / 4;
  position: relative;
  height: 100%;
}
</style>
