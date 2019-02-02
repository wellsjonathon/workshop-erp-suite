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

<style lang="scss">
@import "./styles/variables.scss";

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
#app {
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
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
  background-color: $offwhite;
}
.pmqa {
  grid-row: 1 / 2;
  grid-column: 3 / 4;
  position: relative;
  height: 100%;
}
button { /* Just default values, change where needed */
  padding: 7px 14px;
  color: $offwhite;
  background-color: $primary;
  border: none;
  border-radius: 7px;
  &:hover {
    background-color: $primary-lighter;
    cursor: pointer;
  }
  &:active {
    background-color: $primary-lightest;
    outline: none;
  }
}
a {
  text-decoration: none;
}
</style>
