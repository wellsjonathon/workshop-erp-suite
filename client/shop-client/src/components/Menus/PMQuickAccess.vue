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
      <div class="pmqa__tools__datepicker">
        <FaIcon class="pmqa__tools__datepicker__decr"  @click="this.decrSelectedDate" icon="chevron-left"/>
        <div class="pmqa__tools__datepicker__date">
          {{
            this.selectedDate.date == this.date.date &&
            this.selectedDate.month == this.date.month &&
            this.selectedDate.year == this.date.year
              ? 'Today'
              : this.daysShort[this.selectedDate.day] + ', ' + this.monthsShort[this.selectedDate.month] + ' ' + this.selectedDate.date
          }}
        </div>
        <FaIcon class="pmqa__tools__datepicker__incr" @click="this.incrSelectedDate" icon="chevron-right"/>
      </div>
    </div>
    <FaIcon :class="['pmqa__btn', !isCollapsed ? 'extended' : 'collapsed']"
            @click="this.toggleCollapse"
            icon="chevron-circle-right"/>
  </div>
</template>

<script>
import { days, daysShort, months, monthsShort } from './../../misc.js'

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
        day: new Date().getDay(),
        month: new Date().getMonth(),
        year: new Date().getYear(),
        date: new Date().getDate()
      },
      selectedDate: {
        day: new Date().getDay(),
        month: new Date().getMonth(),
        year: new Date().getYear(),
        date: new Date().getDate()
      },
      daysShort: daysShort,
      monthsShort: monthsShort
    }
  },
  methods: {
    toggleCollapse() {
      this.isCollapsed = !this.isCollapsed
    },
    decrSelectedDate() {
      this.selectedDate.date--;
      this.selectedDate.day--;
    },
    incrSelectedDate() {
      this.selectedDate.date++;
      this.selectedDate.day++;
    }
  }
}
</script>

<style scoped>
.pmqa {
  display: flex;
  position: relative;
  font-size: 1.6rem;
  transition: 0.5s;
  background-color: #425B72;
  box-shadow: -2px 0 4px 1px rgba(1,1,1,0.175),
              -1px 0 16px 1px rgba(1,1,1,0.1);
}
.pmqa.extended {
  width: 280px;
}
.pmqa.collapsed {
  width: 60px;
}
.pmqa__btn {
  position: absolute;
  width: 26px;
  height: 26px;
  left: -12px;
  top: calc(50% - 15px);
  transition: transform 0.5s, color 0.2s;
  color: #19344B;
  background-color: #42586E;
  border: 2px solid #42586E;
  border-radius: 15px;
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
  background-color: #8796A4;
  box-sizing: border-box;
  box-shadow: 0px -3px 4px -2px rgba(1,1,1,0.175) inset,
              0px -2px 16px -2px rgba(1,1,1,0.075) inset;
}
.pmqa__notifications__count {
  width: 36px;
  height: 36px;
  margin: 12px;
  text-align: center;
  color: #425B72;
  border-radius: 18px;
  background-color: #EFEFF4;
  box-shadow: 0px 0px 4px rgba(1,1,1,0.125),
              0px 0px 16px rgba(1,1,1,0.075);
}
.pmqa__notifications__count.new-notifications {
  color: #EFEFF4;
  background-color: #b06560;
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
}
.pmqa__notifications__text.collapsed {
  width: 0px;
}
.pmqa__tools {
  margin-top: 60px;
  width: 100%;
}
.pmqa__tools__datepicker {
  display: flex;
  flex-direction: row;
  width: 100%;
  padding: 20px 16px;
  font-size: 2.6rem;
  color: #EFEFF4;
  box-sizing: border-box;
}
.pmqa__tools__datepicker__decr,
.pmqa__tools__datepicker__incr {
  width: 30px;
  height: 30px;
}
.pmqa__tools__datepicker__decr:hover,
.pmqa__tools__datepicker__incr:hover {
  cursor: pointer;
}
.pmqa__tools__datepicker__date {
  width: calc(100% - 60px);
  text-align: center;
}
.pmqa__btn.extended {
  filter: drop-shadow(-4px 0 8px rgba(1,1,1,0.175));
}
.pmqa__btn.collapsed {
  transform: rotate(-180deg);
  filter: drop-shadow(4px 0 8px rgba(1,1,1,0.175));
}
.pmqa__btn:hover {
  color: rgba(25, 52, 75, 0.7);
  cursor: pointer;
}
.pmqa__btn:active {
  color: rgba(25, 52, 75, 0.45);
}
</style>

