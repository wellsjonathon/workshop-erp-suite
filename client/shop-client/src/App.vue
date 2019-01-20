<template>
  <div id="app">
    <NavBar class="nav"/>
    <div id="content">
      <div id="content-area">

      </div>
    </div>
    <ProjectManagementQuickAccess class="pmqa"/>
  </div>
</template>

<script>
import NavBar from './components/Menus/Nav.vue'
import ProjectManagementQuickAccess from './components/Menus/PMQuickAccess.vue'

export default {
  name: 'app',
  components: {
    NavBar,
    ProjectManagementQuickAccess
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
  height: 100%;
  /* width: 100%; */
  /*width: 300px;*/
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
#content-area {
  width: 95%;
  height: 97%;
  margin: 14px auto;
  border-radius: 7px;
  box-sizing: border-box;
  background-color: #FFFFFF;
}
.pmqa {
  grid-row: 1 / 2;
  grid-column: 3 / 4;
  height: 100%;
}
</style>
