<template>
  <div :class="['pmqa', !isCollapsed ? 'extended' : 'collapsed']">
    <div :class="['pmqa__notifications', !isCollapsed ? 'extended' : 'collapsed', !newNotifications ? 'no-new-notifications' : 'new-notifications']">
      <div :class="['pmqa__notifications__text', !isCollapsed ? 'extended' : 'collapsed']">
        Notifications
      </div>
      <div :class="['pmqa__notifications__count', !newNotifications ? 'no-new-notifications' : 'new-notifications']">
        <div class="pmqa__notifications__count__value">
          {{ this.notifications.length }}
        </div>
      </div>
    </div>
    <div :class="['pmqa__tools', !isCollapsed ? 'extended' : 'collapsed']">
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
    </div>
    <FaIcon :class="['pmqa__btn', !isCollapsed ? 'extended' : 'collapsed']"
            @click="this.toggleCollapse"
            icon="chevron-circle-right"/>
  </div>
</template>

<script>
import dayjs from 'dayjs'

export default {
  name: 'PMQuickAccess',
  props: {
    collapsed: {
      type: Boolean,
      default: false
    },
    notifications: {
      type: Array,
      default: function () { return [5, 6, 2, 8] }
    }
  },
  data: function () {
    return {
      isCollapsed: this.collapsed,
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
    width: 60px;
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
  width: 36px;
  height: 36px;
  margin: 12px;
  text-align: center;
  color: #425B72; /* The contrast is not enough, figure something out */
  border-radius: 18px;
  /* border: 1px solid #607589; */
  background-color: #EFEFF4;
  /* box-shadow: 0px 0px 4px 1px rgba(66,91,114,0.25),
              0px 0px 16px 2px rgba(66,91,114,0.5); */
  &.new-notifications {
    color: #EFEFF4;
    // background-color: hsl(2, 56%, 42%);
    background-color: #B03930;
    box-shadow: 0 0 6px rgba(255,255,255,0.125) inset;
  }
}
.pmqa__notifications__count__value {
  width: 100%;
  line-height: 36px;
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
.pmqa__btn {
  position: absolute;
  width: 26px;
  height: 26px;
  left: -12px;
  top: calc(50% - 15px);
  transition: transform 0.5s, color 0.2s;
  color: $primary-dark;
  background-color: $primary;
  border: 2px solid $primary;
  border-radius: 15px;
  &.collapsed {
    transform: rotate(-180deg);
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

