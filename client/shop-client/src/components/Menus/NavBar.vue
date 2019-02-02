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

<style lang="scss" scoped>
@import "../../styles/variables.scss";

.nav {
  display: flex;
  position: relative;
  font-size: 1.6rem;
  transition-duration: 0.5s;
  background-color: $primary;
  /* box-shadow: 2px 0px 4px 1px rgba(1,1,1,0.175),
              1px 0px 16px 1px rgba(1,1,1,0.075); */
  &.extended {
    width: $width-extended;
  }
  &.collapsed {
    width: $width-collapsed;
  }
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
  color: $offwhite;
  transition-duration: 0.2s;
  background-color: $primary-lightest;
  box-shadow: 0px -3px 4px -2px rgba(1,1,1,0.175) inset,
              0px -2px 16px -2px rgba(1,1,1,0.075) inset;
  &:hover {
    background-color: $primary-lighter;
    cursor: pointer;
  }
  &:active {
    width: calc(100% + 2px);
    transition-duration: 0s;
    background-color: $primary-lightest;
    transform: translate(-2px, 2px);
  }
}
.nav__account__icon {
  width: 36px;
  height: 36px;
  padding: 12px;
}
.nav__account__link {
  width: $width-extended - $width-collapsed; /* Upgrade to Sass, use extended - collapsed */
  margin: auto 0;
  overflow: hidden;
  white-space: nowrap;
  transition: width 0.49s;
  &.extended {
    display: block;
  }
  &.collapsed {
    width: 0px;
  }
}
.nav__btn {
  position: absolute;
  width: 26px;
  height: 26px;
  right: -12px;
  top: calc(50% - 15px);
  transition: transform 0.5s, color 0.2s;
  color: $primary-dark;
  background-color: $primary;
  border: 2px solid $primary;
  border-radius: 15px;
  &.collapsed {
    transform: rotate(180deg);
  }
  &:hover {
    color: $primary-darker;
    cursor: pointer;
  }
  &:active {
    color: $primary-darkest;
  }
}
</style>

