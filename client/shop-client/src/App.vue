<template>
  <div id="app" class="d-flex flex-column h-100">
    <div id="app-main" class="d-flex flex-row align-items-stretch flex-grow-1 w-100">
      <NavBar id="nav"/>
      <div id="content" class="d-flex flex-column flex-grow-1">
        <router-view></router-view>
        <!-- <PMQuickAccess id="pmqa"/> -->
      </div>
    </div>
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

<style lang="scss">
@import "./styles/variables.scss";
@import "../node_modules/bootstrap/scss/bootstrap.scss";

html {
  font-size: 62.5%;
  /* font-family: $body-font-stack */
  font-family: $heading-font-stack
}
body {
  padding: 0;
  margin: 0;
  height: 100%;
}
a {
  text-decoration: none !important;
}
h1 {
  font-size: 2.6rem;
  font-weight: 400;
  padding: 0;
  margin: 0;
}

#app {
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  margin: 0;
  padding: 0;
  height: 100%;
  width: 100%;
}
#app-main {
  // height: 100%;
}
#nav, #pmqa {
  height: 100%;
  margin: 0;
  padding: 0;
}
#content {
  height: 100%;
  background-color: $offwhite;
}
.icon {
  font-size: $nav-icon-size;
  height: $nav-icon-size;
  padding: 4px;
}
.icon-button {
  width: 40px;
  text-align: center;
  &:hover {
    // background-color: ;
    cursor: pointer;
  }
}
</style>
