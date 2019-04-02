<template>
  <div class="d-flex flex-column" :class="['pmqa__tools', !isCollapsed ? 'extended' : 'collapsed']">
    <!-- <div class="pmqa__datepicker d-flex flex-direction-row align-items-center">
      <FaIcon class="pmqa__datepicker__decr"  @click="this.decrSelectedDate" icon="chevron-left"/>
      <div class="pmqa__datepicker__date">
        {{ this.selectedDateString }}
      </div>
      <FaIcon class="pmqa__datepicker__incr" @click="this.incrSelectedDate" icon="chevron-right"/>
    </div> -->
    <div class="pmqa__times d-flex flex-direction-column align-items-center">
      <vue-cal
        class="pmqa__day-view w-100 mt-2"
        xsmall
        hide-view-selector
        :selected-date="date.current"
        :time-from="7 * 60"
        :time-to="23 * 60"
        default-view="day"
        :disable-views="['years', 'year', 'month', 'week']">
      </vue-cal>
    </div>
  </div>
</template>

<script>
import dayjs from 'dayjs'
import VueCal from 'vue-cal'
import 'vue-cal/dist/vuecal.css'

export default {
  name: 'ProjectManagementDrawer',
  components: {
    VueCal
  },
  props: {
    collapsed: {
      type: Boolean,
      default: true,
      required: true
    }
  },
  data: function () {
    return {
      isCollapsed: this.collapsed,
      date: {
        // current: dayjs(Date()),
        current: new Date(),
        selected: dayjs(Date())
      }
    }
  },
  computed: {
    selectedDateString: function () {
      return (this.date.selected.isSame(this.date.current)
        ? 'Today'
        : this.date.selected.format('ddd MMM D, YYYY'))
    }
  },
  mounted: function () {
    this.date.current = dayjs(new Date())
    this.date.selected = dayjs(this.date.current)
  },
  methods: {
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
@import "../../../styles/variables.scss";

.pmqa__tools {
  width: 100%;
}
.pmqa__datepicker {
  width: 100%;
  padding: 20px 16px;
  font-size: 1.6rem;
  line-height: 30px;
  color: #EFEFF4;
  box-sizing: border-box;
}
.pmqa__datepicker__decr,
.pmqa__datepicker__incr {
  width: 24px;
  height: 24px;
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
  padding: 0 6px;
}
.pmqa__time {
  width: 100%;
  height: $width-collapsed;
  padding: 5px 10px;
  color: $offwhite;
  // background-color: $primary-lighter;
  font-size: 1.2rem;
  border-top: 2px solid $primary-lighter;
  border-left: 2px solid $primary-lighter;
  border-right: 2px solid $primary-lighter;
  &:last-child {
    border-bottom: 2px solid $primary-lighter;
  }
}
.pmqa__times__separator {
  display: block;
  width: 92%;
  height: 1px;
  border: none;
  background-image: linear-gradient(90deg, rgba(0,0,0,0), #EFEFF4, rgba(0,0,0,0));
}
.pmqa__day-view {
  &::-webkit-scrollbar {
    width: 10px;
  }
  &::-webkit-scrollbar-track {
    background: $offwhite;
  }
  &::-webkit-scrollbar-thumb {
    background: $primary-dark;
    &:hover {
      background: $primary-darker;
    }
  }
}
</style>
