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
    </div>
    <div class="nav__account-area">
      <NavBarItem :collapsed="isCollapsed"
                  link="/account"
                  linkName="Chris Yung"
                  linkIcon="user"
                  :isAccount="true"/>
    </div>
    <div class="nav__toggle-area d-flex align-items-center justify-content-center"
        @click="this.toggleCollapse">
      <FaIcon :class="['nav__btn', !isCollapsed ? 'extended' : 'collapsed']"
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
  font-size: 1.4rem;
  transition-duration: 0.5s;
  background-color: $primary;
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
.nav__toggle-area {
  height: $width-collapsed - 12px;
  border-top: 2px solid $primary-lighter;
  &:hover {
    background-color: $primary-lighter;
    cursor: pointer;
    & .nav__btn {
      color: $primary-lightest;
    }
  }
  &:active {
    background-color: $primary-lightest;
    & .nav__btn {
      color: lighten($primary-lightest, 8%);
    }
  }
  & .nav__btn {
    width: $nav-icon-size - 4px;
    height: $nav-icon-size - 4px;
    transition: transform 0.5s;
    color: $primary-lighter;
    border-radius: $nav-icon-size / 2;
    &.collapsed {
      transform: rotate(180deg);
    }
  }
}
</style>

