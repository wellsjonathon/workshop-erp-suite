<template>
  <div class="d-flex flex-column" :class="['pmqa', !isCollapsed ? 'extended' : 'collapsed']">
    <div class="d-flex" :class="['pmqa__tabs', !isCollapsed ? 'extended' : 'collapsed']">
      <div class="pmqa__tab__container" :class="[isNotificationsActive ? 'active' : '']"
            @click="this.toggleNotifications">
        <FaIcon icon="bell" class="icon pmqa__tab"/>
      </div>
      <div class="pmqa__tab__container" :class="[isPMActive ? 'active' : '']"
            @click="this.togglePM">
        <FaIcon icon="stopwatch" class="icon pmqa__tab"/>
      </div>
    </div>
    <div class="d-flex flex-column flex-grow-1" :class="['pmqa__content-area', !isCollapsed ? 'extended' : 'collapsed']">

    </div>
    <!-- <div :class="['pmqa__tools', !isCollapsed ? 'extended' : 'collapsed']">
      <div class="pmqa__datepicker">
        <FaIcon class="pmqa__datepicker__decr"  @click="this.decrSelectedDate" icon="chevron-left"/>
        <div class="pmqa__datepicker__date">
          {{ this.selectedDateString }}
        </div>
        <FaIcon class="pmqa__datepicker__incr" @click="this.incrSelectedDate" icon="chevron-right"/>
      </div>
      <div class="pmqa__times">
        <span class="pmqa__times__separator"/>
        Stuff
        <span class="pmqa__times__separator"/>
      </div>
    </div> -->
    <div class="pmqa__toggle-area d-flex align-items-center justify-content-center">
      <FaIcon :class="['pmqa__btn', !isCollapsed ? 'extended' : 'collapsed']"
              @click="this.toggleCollapse"
              icon="chevron-circle-right"/>
    </div>
  </div>
</template>

<script>
import dayjs from 'dayjs'

export default {
  name: 'PMQuickAccess',
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
      isNotificationsActive: false,
      isPMActive: true,
      newNotifications: this.notifications.length > 0,
      date: {
        current: dayjs(Date()),
        selected: dayjs(Date())
      }
    }
  },
  computed: {
    selectedDateString: function () {
      return (this.date.selected.isSame(this.date.current)
        ? 'Today'
        : this.date.selected.format('ddd, MMM. D'))
    }
  },
  mounted: function () {
    this.date.current = dayjs(new Date())
    this.date.selected = dayjs(this.date.current)
  },
  methods: {
    toggleCollapse() {
      this.isCollapsed = !this.isCollapsed
    },
    toggleNotifications() {
      if (this.isCollapsed) {
        this.isCollapsed = !this.isCollapsed
      }
      this.isPMActive = false
      this.isNotificationsActive = true
    },
    togglePM() {
      if (this.isCollapsed) {
        this.isCollapsed = !this.isCollapsed
      }
      this.isNotificationsActive = false
      this.isPMActive = true
    },
    decrSelectedDate() {
      this.date.selected = this.date.selected.subtract(1, 'days')
    },
    incrSelectedDate() {
      this.date.selected = this.date.selected.add(1, 'days')
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
  background-color: $primary-lighter;
  flex-direction: row;
  flex-wrap: wrap;
  transition: border-color 0.49s ease 0.18s;
  border-color: $primary;
  &.collapsed {
    border-bottom: 2px solid $offwhite;
    transition: border-color 0.49s ease 0.18s;
    &>.pmqa__tab__container.active {
      transition-delay: 0.18s;
      background-color: $primary-lighter;
    }
  }
  &>.pmqa__tab__container {
    display: flex;
    align-items: center;
    justify-content: center;
    height: $width-collapsed - 12px;
    width: $width-collapsed;
    transition: 0.2s;
    color: $offwhite;
    &.active {
      background-color: $primary;
      color: $offwhite;
      &:hover {
        color: $primary;
      }
    }
    &:hover {
      background-color: $primary-lightest;
      cursor: pointer;
    }
    &:active {
      padding: 2px 0;
      transition-duration: 0s;
      background-color: $primary-lightest;
      // transform: translate(0, 2px);
    }
  }
}
.pmqa__notifications {
  position: absolute;
  right: 0;
  display: flex;
  width: 100%;
  margin: 0;
  top: -2px;
  color: #EFEFF4;
  transition: 0.2s, width 0.42s, border-radius 0.5s;
  background-color: $primary-lightest;
  box-sizing: border-box;
  box-shadow: 0px -3px 4px -2px rgba(0,0,0,0.175) inset,
              0px -2px 16px -2px rgba(0,0,0,0.075) inset;
}
.pmqa__notifications__count {
  width: $nav-icon-size;
  height: $nav-icon-size;
  margin: 12px;
  text-align: center;
  color: #425B72; /* The contrast is not enough, figure something out */
  border-radius: $nav-icon-size / 2;
  /* border: 1px solid #607589; */
  background-color: #EFEFF4;
  /* box-shadow: 0px 0px 4px 1px rgba(66,91,114,0.25),
              0px 0px 16px 2px rgba(66,91,114,0.5); */
  &.new-notifications {
    color: #EFEFF4;
    // background-color: hsl(2, 56%, 42%);
    background-color: #B03930;
    // box-shadow: 0 0 6px rgba(255,255,255,0.125) inset;
  }
}
.pmqa__notifications__count__value {
  width: 100%;
  line-height: $nav-icon-size;
}
.pmqa__notifications__text {
  width: 220px; /* Upgrade to Sass, use extended - collapsed */
  margin: auto 0;
  overflow: hidden;
  white-space: nowrap;
  transition: width 0.5s;
  text-align: center;
  &.collapsed {
    width: 0px;
  }
}
.pmqa__tools {
  margin-top: 60px;
  width: 100%;
}
.pmqa__datepicker {
  display: flex;
  flex-direction: row;
  width: 100%;
  padding: 20px 16px;
  font-size: 2rem;
  line-height: 30px;
  color: #EFEFF4;
  box-sizing: border-box;
}
.pmqa__datepicker__decr,
.pmqa__datepicker__incr {
  width: 28px;
  height: 28px;
  padding: 2px;
  &:hover {
    cursor: pointer;
  }
}
.pmqa__datepicker__date {
  width: calc(100% - 60px);
  text-align: center;
}
.pmqa__times {
  display: flex;
  flex-direction: column;
  align-items: center;
  width: 100%;
}
.pmqa__times__separator {
  display: block;
  width: 92%;
  height: 1px;
  border: none;
  background-image: linear-gradient(90deg, rgba(0,0,0,0), #EFEFF4, rgba(0,0,0,0));
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

