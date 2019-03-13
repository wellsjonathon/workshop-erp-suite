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
.breadcrumbs {
  font-size: 1.6rem;
  margin: 10px;
  & ul {
    list-style: none;
    padding: 0;
    margin: 0;
  }
  & li {
    display: inline;
  }
  & li+li:before {
    padding: 8px;
    color: $grey;
    content: "/\00a0";
  }
}
.container {
  display: flex;
  flex-direction: column;
  padding: 15px;
  & .container__row {
    display: flex;
    flex-direction: row;
    // margin: 10px;
    & .card {
      margin: 10px;
      padding: 10px;
      border-radius: $radius;
      background-color: $white;
      box-shadow: 0 1px 2px hsla(0, 0, 0, 0.05);
      flex-basis: 0;
      flex-grow: 1;
      & .card__row {
        display: flex;
        margin: 15px;
        flex-direction: row;
        justify-content: space-between;
        &.card__header {
          padding: 0 0 10px;
        }
      }
    }
  }
}
button { /* Just default values, change where needed */
  padding: 7px 14px;
  font-size: 1.2rem;
  color: $offwhite;
  background-color: $primary;
  border: none;
  border-radius: $radius;
  // transition-duration: $btn-transition-duration;
  &:hover {
    background-color: $primary-lighter;
    cursor: pointer;
  }
  &:active {
    background-color: $primary-lightest;
    outline: none;
  }
}
.btn-group, .floating-tab-group {
  display: flex;
  flex-direction: row;
  & button, div {
    padding: 7px 14px;
    text-align: center;
    color: $grey;
    border: 1px solid $grey;
    background-color: $offwhite-med;
    &:first-child {
      border-radius: $radius 0 0 $radius;
    }
    &:last-child {
      border-radius: 0 $radius $radius 0;
    }
    &:not(:last-child) {
      border-right: none;
    }
    & FaIcon {
      height: 100%;
      width: 100%;
    }
  }
}
.btn-group--unattached {
  & button, div {
    &:not(:last-child) {
      margin: 0 10px 0 0;
    }
  }
}
a {
  text-decoration: none;
}
h1 {
  font-size: 2.6rem;
  font-weight: 400;
  padding: 0;
  margin: 0;
}

.data-table {
  margin: 15px;
  // border-radius: $radius;
  overflow: hidden;
  & table {
    width: 100%;
    text-align: left;
    // border-spacing: 0;
    // border-radius: $radius;
    border: 1px solid $grey;
    border-collapse: collapse;
  }
  & thead, tfoot {
    font-weight: bold;
    background-color: $offwhite;
    border-bottom: 1px solid $grey;
    // & th:first-child {
    //   border-radius: $radius 0 0 0;
    // }
    // & th:last-child {
    //   border-radius: 0 $radius 0 0;
    // }
  }
  & tr {
    border: 1px solid $grey;
    &:hover {
      background-color: darken($offwhite-med, 5%);
    }
    // &:last-child td:first-child {
    //   border-radius: 0 0 0 $radius;
    // }
    // &:last-child td:last-child {
    //   border-radius: 0 0 $radius 0;
    // }
  }
  & td, th {
    padding: 15px 10px;
  }
  & .status-row {
    display: inline-flex;
    flex-direction: row;
    align-items: baseline;
    & .status__indicator {
      margin: 0 5px 0 0;
      width: 1rem;
      height: 1rem;
      border-radius: 0.5rem;
      background-color: $green;
    }
  }
}
.card-header {
  background-color: $card-cap-bg;
}
</style>
