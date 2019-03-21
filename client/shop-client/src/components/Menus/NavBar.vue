<template>
  <div class="d-flex flex-column" :class="['nav', !isCollapsed ? 'extended' : 'collapsed']">
    <div class="nav__logo-area d-flex align-items-center justify-content-center w-100">
      <!-- <img src="../../assets/ur-logo-small.svg" alt="" class="w-100"> -->
      <img src="../../assets/ur-emblem-small.svg" alt="" class="logo__emblem">
      <img src="../../assets/ur-title-small.svg" alt="" :class="['logo__title', !isCollapsed ? 'extended' : 'collapsed']">
    </div>
    <div class="nav__links-area pt-5 flex-grow-1">
      <div class="nav__links">
        <NavBarItem :collapsed="isCollapsed"
                    link="/home"
                    linkName="Dashboard"
                    linkIcon="home"/>
        <NavBarItem :collapsed="isCollapsed"
                    link="/workorders"
                    linkName="Workorders"
                    linkIcon="file"/>
        <NavBarItem :collapsed="isCollapsed"
                    link="/materials"
                    linkName="Materials"
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
      <!-- <FaIcon :class="['nav__btn', !isCollapsed ? 'extended' : 'collapsed']"
              @click="this.toggleCollapse"
              icon="chevron-circle-left"/> -->
    </div>
    <div class="nav__account-area">
      <NavBarItem :collapsed="isCollapsed"
                  link="/account"
                  linkName="Chris Yung"
                  linkIcon="user"
                  isAccount="true"/>
    </div>
    <div class="nav__toggle-area d-flex align-items-center justify-content-center">
      <FaIcon :class="['nav__btn', !isCollapsed ? 'extended' : 'collapsed']"
              @click="this.toggleCollapse"
              icon="chevron-circle-left"/>
    </div>
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
  font-size: 1.5rem;
  transition-duration: 0.5s;
  background-color: $primary;
  /* box-shadow: 2px 0px 4px 1px rgba(1,1,1,0.175),
              1px 0px 16px 1px rgba(1,1,1,0.075); */
  &.extended {
    width: $width-extended;
    flex-basis: $width-extended;
    min-width: $width-extended;
  }
  &.collapsed {
    width: $width-collapsed;
    flex-basis: $width-collapsed;
    min-width: $width-collapsed;
  }
  & .nav__logo-area {
    height: 100px;
    background-color: $primary-lighter;
    border-bottom: 2px solid $offwhite;
    // box-shadow: 0px -1px 2px 1px rgba(1,1,1,0.125) inset;
    & img {
      color: $white;
      height: 100%;
      padding: 8px;
    }
    & .logo__emblem {
      width: $width-collapsed;
      transition: justify-content 0.49s;
    }
    & .logo__title {
      width: $width-extended * 0.52;
      padding: 11px 0;
      overflow: hidden;
      object-fit: cover;
      object-position: 0% 0;
      transition: width 0.49s;
      &.collapsed {
        width: 0;
      }
    }
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
  width: $nav-icon-size;
  height: $nav-icon-size;
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
.nav__toggle-area {
  height: $width-collapsed - 12px;
  // background-color: $primary-lighter;
  border-top: 2px solid $primary-lighter;
}
.nav__btn {
  // position: absolute;
  width: $nav-icon-size - 4px;
  height: $nav-icon-size - 4px;
  // right: -12px;
  // top: calc(50% - 15px);
  transition: transform 0.5s, color 0.2s;
  color: $primary-lighter;
  // background-color: $primary;
  // border: 2px solid $primary;
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

