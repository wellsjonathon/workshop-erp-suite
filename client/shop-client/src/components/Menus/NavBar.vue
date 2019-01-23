<template>
  <div :class="['nav', !isCollapsed ? 'extended' : 'collapsed']">
    <router-link to="/account" :class="['nav__account', !isCollapsed ? 'extended' : 'collapsed']">
      <FaIcon class="nav__account__icon" icon="user"/>
      <div :class="['nav__account__link', !isCollapsed ? 'extended' : 'collapsed']">
        Account
      </div>
    </router-link>
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
  position: relative;
  font-size: 1.6rem;
  transition-duration: 0.5s;
  background-color: #425B72;
  /* box-shadow: 2px 0px 4px 1px rgba(1,1,1,0.175),
              1px 0px 16px 1px rgba(1,1,1,0.075); */
}
.nav.extended {
  width: 280px;
}
.nav.collapsed {
  width: 60px;
}
.nav__links {
  margin: auto 0;
  position: relative;
}
.nav__account {
  position: absolute;
  display: flex;
  width: 100%;
  margin: 0;
  top: -2px;
  color: #EFEFF4;
  transition-duration: 0.2s;
  background-color: #8796A4;
  box-shadow: 0px -3px 4px -2px rgba(1,1,1,0.175) inset,
              0px -2px 16px -2px rgba(1,1,1,0.075) inset;
}
.nav__account:hover {
  background-color: #607589;
  cursor: pointer;
}
.nav__account:active {
  width: calc(100% + 2px);
  transition-duration: 0s;
  background-color: #8796A4;
  transform: translate(-2px, 2px);
}
.nav__account__icon {
  width: 36px;
  height: 36px;
  padding: 12px;
}
.nav__account__link {
  width: 220px; /* Upgrade to Sass, use extended - collapsed */
  margin: auto 0;
  overflow: hidden;
  white-space: nowrap;
  transition: width 0.49s;
}
.nav__account__link.extended {
  display: block;
}
.nav__account__link.collapsed {
  width: 0px;
}
a {
  text-decoration: none;
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
/* .nav__btn.extended {
  filter: drop-shadow(4px 0 8px rgba(1,1,1,0.175));
} */
.nav__btn.collapsed {
  transform: rotate(180deg);
  /* filter: drop-shadow(-4px 0 8px rgba(1,1,1,0.175)); */
}
.nav__btn:hover {
  color: rgba(25, 52, 75, 0.7);
  cursor: pointer;
}
.nav__btn:active {
  color: rgba(25, 52, 75, 0.45);
}
</style>

