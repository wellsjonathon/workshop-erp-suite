<template>
  <div :class="['nav', !isCollapsed ? 'extended' : 'collapsed']">
    <FaIcon :class="['nav__btn', !isCollapsed ? 'extended' : 'collapsed']"
            @click="this.toggleCollapse" 
            icon="chevron-circle-left"/>
  </div>
</template>

<script>
export default {
  name: 'NavBar',
  props: {
    collapsed: {
      type: Boolean,
      default: false // Will need to change this based on session state
    }
  },
  data: function () {
    return {
      isCollapsed: this.collapsed
    }
  },
  methods: {
    toggleCollapse() {
      this.isCollapsed = !this.isCollapsed
    }
  }
}
</script>

<style scoped>
.nav {
  /* width: 300px; */
  transition-duration: 0.5s;
  background-color: #425B72;
  box-shadow: 2px 2px 4px rgba(1,1,1,0.1),
              1px 1px 16px rgba(1,1,1,0.075);
}
.nav.extended {
  width: 300px;
}
.nav.collapsed {
  width: 50px;
}
.nav__btn {
  position: relative;
  width: 26px;
  height: 26px;
  right: calc(-100% + 20px);
  /* right: calc(-100% + 16px); */
  top: calc(50% - 13px);
  transition-duration: 0.5s;
  color: #19344B;
  background-color: #42586E;
  border: 2px solid #42586E;
  border-radius: 14px;
}
.nav__btn.collapsed {
  transform: rotate(180deg);
}
.nav__btn:hover {
  color: rgba(25, 52, 75, 0.7);
  cursor: pointer;
}
.nav__btn:active {
  color: rgba(25, 52, 75, 0.45);
}
</style>

