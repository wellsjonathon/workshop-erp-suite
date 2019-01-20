<template>
  <div :class="['nav', !isCollapsed ? 'extended' : 'collapsed']">
    <div class="nav__links">
      <NavBarItem :collapsed="isCollapsed"
                  link="/dashboard"
                  linkName="Dashboard"
                  linkIcon="home"/>
      <NavBarItem :collapsed="isCollapsed"
                  link="/workorders"
                  linkName="Workorders"
                  linkIcon="file"/>
      <NavBarItem :collapsed="isCollapsed"
                  link="/inventory"
                  linkName="Inventory"
                  linkIcon="warehouse"/>
      <NavBarItem :collapsed="isCollapsed"
                  link="/project-management"
                  linkName="Project Management"
                  linkIcon="calendar-alt"/>
      <NavBarItem :collapsed="isCollapsed"
                  link="/reports"
                  linkName="Reports"
                  linkIcon="chart-pie"/>
      <NavBarItem :collapsed="isCollapsed"
                  link="/settings"
                  linkName="Settings"
                  linkIcon="cog"/>
    </div>
    <FaIcon :class="['nav__btn', !isCollapsed ? 'extended' : 'collapsed']"
            @click="this.toggleCollapse"
            icon="chevron-circle-left"/>
  </div>
</template>

<script>
import NavBarItem from './NavBarItem.vue'

export default {
  name: 'NavBar',
  components: {
    NavBarItem
  },
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
  display: flex;
  transition-duration: 0.5s;
  background-color: #425B72;
  box-shadow: 2px 0px 4px 1px rgba(1,1,1,0.175),
              1px 0px 16px 1px rgba(1,1,1,0.075);
}
.nav.extended {
  width: 300px;
}
.nav.collapsed {
  width: 60px;
}
.nav__links {
  margin: auto 0;
}
.nav__btn {
  position: absolute;
  width: 26px;
  height: 26px;
  right: -12px;
  top: calc(50% - 15px);
  transition: transform 0.5s, color 0.2s;
  color: #19344B;
  background-color: #42586E;
  border: 2px solid #42586E;
  border-radius: 15px;
}
.nav__btn.extended {
  filter: drop-shadow(4px 0 8px rgba(1,1,1,0.175));
}
.nav__btn.collapsed {
  transform: rotate(180deg);
  filter: drop-shadow(-4px 0 8px rgba(1,1,1,0.175));
}
.nav__btn:hover {
  color: rgba(25, 52, 75, 0.7);
  cursor: pointer;
}
.nav__btn:active {
  color: rgba(25, 52, 75, 0.45);
}
</style>

