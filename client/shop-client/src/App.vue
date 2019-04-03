<template>
  <div id="app" class="d-flex flex-column h-100">
    <div id="app-main" class="d-flex flex-row align-items-stretch flex-grow-1 w-100">
      <NavBar id="nav"/>
      <div class="content-area d-flex flex-column flex-grow-1">
        <!-- <div id="header">
          <b-container fluid class="d-flex flex-column h-100">
            <b-row>
              <b-col>
                <b-breadcrumb :items="breadcrumbs" />
              </b-col>
            </b-row>
            <b-row class="flex-grow-1 mb-4" align-v="end">
              <b-col>
                <h1>New Workorder</h1>
              </b-col>
            </b-row>
          </b-container>
        </div> -->
        <div id="content" class="d-flex flex-column flex-grow-1">
          <router-view></router-view>
          <!-- <PMQuickAccess id="pmqa"/> -->
        </div>
      </div>
      <PMQuickAccess id="pmqa"/>
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
  },
  data: function () {
    return {
      breadcrumbs: [
        {
          text: 'Home',
          to: { name: 'home' }
        },
        {
          text: 'Workorders',
          to: { name: 'workorders' }
        },
        {
          text: 'New',
          to: { name: 'new_workorder' }
        }
      ]
    }
  }
}
</script>

<style lang="scss">
@import "./styles/variables.scss";
// @import "../node_modules/bootstrap/scss/bootstrap.scss";
// @import '../node_modules/bootstrap-vue/src/index.scss';

html {
  font-size: 62.5%;
  /* font-family: $body-font-stack */
  font-family: $heading-font-stack;
}
body {
  padding: 0;
  margin: 0;
  height: 100%;
  overflow: hidden;
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
  height: 100vh;
  width: 100%;
}
#app-main {
  // height: 100%;
}
#nav, #pmqa {
  // position: -webkit-sticky;
  // position: sticky;
  // top: 0;
  height: 100%;
  margin: 0;
  padding: 0;
}
#content {
  // height: 100%;
  background-color: $offwhite;
}
#header {
  // height: 100px;
  border-bottom: 2px solid hsl(0, 0, 90);
  & h1 {
    margin: 0 0 10px;
  }
}
.content-area {
  overflow-y: scroll;
  &::-webkit-scrollbar {
    width: 0.75em;
  }
  &::-webkit-scrollbar-thumb {
    border-radius: 0.325rem;
    background-color: $primary-lightest
  }
  &::-webkit-scrollbar-track {
    background: $offwhite;
  }
}
.breadcrumb {
  font-size: 1.3rem;
  color: $gray-700;
  padding-left: 0 !important;
  margin: 0 !important;
  & a {
    color: $gray-800;
    font-weight: 400;
    &:hover {
      color: $gray-600;
    }
  }
}
.btn {
  & a {
    color: $offwhite
  }
}
.pagination {
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
