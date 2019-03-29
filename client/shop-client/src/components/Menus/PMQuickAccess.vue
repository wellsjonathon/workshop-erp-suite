<template>
  <div class="d-flex flex-column" :class="['pmqa', !isCollapsed ? 'extended' : 'collapsed']">
    <div class="d-flex flex-column w-100" :class="['pmqa__tabs', !isCollapsed ? 'extended' : 'collapsed']">

      <div class="d-flex flex-row align-items-center justify-content-end w-100"
          :class="['pmqa__tab', isNotificationsActive ? 'active' : '', !isCollapsed ? 'extended' : 'collapsed']"
          @click="this.toggleNotifications">
        <div :class="['pmqa__tab__link', !isCollapsed ? 'extended' : 'collapsed', ]">
          Notifications
        </div>
        <div class="pmqa__tab__icon-container d-flex align-items-center justify-content-center">
          <FaIcon icon="bell" class="pmqa__tab__icon icon"/>
        </div>
      </div>

      <div class="d-flex flex-row align-items-center justify-content-end w-100"
          :class="['pmqa__tab', isPMActive ? 'active' : '', !isCollapsed ? 'extended' : 'collapsed']"
          @click="this.togglePM">
        <div :class="['pmqa__tab__link', !isCollapsed ? 'extended' : 'collapsed']">
          Time Tracking
        </div>
        <div class="pmqa__tab__icon-container d-flex align-items-center justify-content-center">
          <FaIcon icon="stopwatch" class="pmqa__tab__icon icon"/>
        </div>
      </div>

    </div>
    <div class="d-flex flex-column flex-grow-1" :class="['pmqa__content-area', !isCollapsed ? 'extended' : 'collapsed']">
      <NotificationDrawer v-if="isNotificationsActive && !isCollapsed && !isBusy"/>
      <ProjectManagementDrawer v-if="isPMActive && !isCollapsed && !isBusy"/>
      <div class="pmqa__spinner-container d-flex justify-content-center py-5" v-if="isBusy">
        <b-spinner class="pmqa__spinner"/>
      </div>
    </div>
    <div class="pmqa__toggle-area d-flex align-items-center justify-content-center">
      <FaIcon :class="['pmqa__btn', !isCollapsed ? 'extended' : 'collapsed']"
              @click="this.toggleCollapse"
              icon="chevron-circle-right"/>
    </div>
  </div>
</template>

<script>
import NotificationDrawer from './PM Content/NotificationDrawer'
import ProjectManagementDrawer from './PM Content/ProjectManagementDrawer'

export default {
  name: 'PMQuickAccess',
  components: {
    NotificationDrawer,
    ProjectManagementDrawer
  },
  props: {
    collapsed: {
      type: Boolean,
      default: true
    },
    notifications: {
      type: Array,
      default: function () { return [5, 6, 2, 8] }
    }
  },
  data: function () {
    return {
      isCollapsed: this.collapsed,
      isBusy: false,
      isNotificationsActive: false,
      isPMActive: true,
      newNotifications: this.notifications.length > 0
    }
  },
  methods: {
    toggleCollapse() {
      this.isCollapsed = !this.isCollapsed
      this.toggleContent()
    },
    toggleContent() {
      this.isBusy = true
      setTimeout(() => { this.isBusy = false }, 500)
    },
    toggleNotifications() {
      if (this.isCollapsed) {
        this.isCollapsed = !this.isCollapsed
      }
      this.isPMActive = false
      this.isNotificationsActive = true
      this.toggleContent()
    },
    togglePM() {
      if (this.isCollapsed) {
        this.isCollapsed = !this.isCollapsed
      }
      this.isNotificationsActive = false
      this.isPMActive = true
      this.toggleContent()
    }
  }
}
</script>

<style lang="scss" scoped>
@import "../../styles/variables.scss";

.pmqa {
  display: flex;
  position: relative;
  font-size: 1.6rem;
  transition: 0.5s;
  background-color: $primary;
  /* box-shadow: -2px 0 4px 1px rgba(1,1,1,0.175),
              -1px 0 16px 1px rgba(1,1,1,0.1); */
  &.extended {
    width: 280px;
  }
  &.collapsed {
    width: $width-collapsed;
  }
}
.pmqa__tabs {
  color: $offwhite;
  border-bottom: 2px solid $offwhite;
  background-color: $primary-lighter;
  &>.pmqa__tab {
    height: $width-collapsed - 12px;
    transition: 0.2s;
    & .pmqa__tab__icon-container {
      width: $width-collapsed;
    }
    & .pmqa__tab__link {
      text-align: right;
      overflow: hidden;
      white-space: nowrap;
      transition: width 0.49s;
      &.extended {
        width: $width-extended - $width-collapsed - 20px;
      }
      &.collapsed {
        width: 0px;
      }
    }
    &.extended {
      padding-right: $width-collapsed - 12px;
    }
    &.collapsed {
      padding: 0;
    }
    &:hover {
      color: $primary;
      background-color: $primary-lightest;
      cursor: pointer;
    }
    &:active {
      transition-duration: 0s;
      background-color: lighten($primary-lightest, 5%);
    }
    &.active {
      background-color: $primary-lightest;
      color: $offwhite;
      &:hover {
        color: $primary;
      }
    }
  }
  // * Used to override normal active styling when the menu is collapsed
  &.collapsed>.pmqa__tab.active {
    background-color: $primary-lighter;
    &:hover {
      background-color: $primary-lightest;
    }
  }
}
.pmqa__spinner {
  color: $offwhite;
}
.pmqa__toggle-area {
  height: $width-collapsed - 12px;
  // background-color: $primary-lighter;
  border-top: 2px solid $primary-lighter;
}
.pmqa__btn {
  // position: absolute;
  width: $nav-icon-size - 4px;
  height: $nav-icon-size - 4px;
  // left: -12px;
  // top: calc(50% - 15px);
  transition: transform 0.5s, color 0.2s;
  color: $primary-lighter;
  // background-color: $primary;
  // border: 2px solid $primary;
  border-radius: $nav-icon-size / 2;
  &.collapsed {
    transform: rotate(-180deg);
  }
  &:hover {
    color: $primary-lightest;
    cursor: pointer;
  }
  &:active {
    color: lighten($primary-lightest, 8%);
  }
}
</style>

