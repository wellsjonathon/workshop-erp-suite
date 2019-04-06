<template>
  <b-container>
    <b-row>
      <b-col>
        <b-breadcrumb :items="breadcrumbs" />
      </b-col>
    </b-row>
    <b-row align-h="center" class="title">
      <b-col sm="12" lg="12">
          <b-row slot="header">
            <b-col class="d-flex align-items-center">
              <h2 class="my-0">Time Entries</h2>
            </b-col>
            <b-col class="d-flex justify-content-end">
              <b-button v-b-modal.add-time-entry variant="primary" size="lg" class="mr-2">
                Add Time Entry
              </b-button>
            </b-col>
          </b-row>
      </b-col>
    </b-row>
      <b-col>
        <vue-cal defaultView='week'
                :disable-views="['years', 'year']"
                :events="events"
                :time-from="8 * 60"
                :time-to="18 * 60"
                :time-step="30"
                events-on-month-view="short"
                class="calendar">
        </vue-cal>
      </b-col>
      <b-col>
       <!-- {{timeEntries}} -->
      </b-col>
    <b-modal id="add-time-entry" title="Add Time Entry">
      <b-form>
        <b-form-group
          id="timeTypeId-group"
          label="Type of Entry:"
          label-cols-sm="3"
          label-cols-xl="2"
          label-size="lg"
          label-align="right"
          label-for="time-type-select">
          <b-form-select
            id="time-type-select"
            size="lg"
            required
            v-model="newTimeEntry.selectedTimeType">
            <option slot="first" :value="null">Select one...</option>
            </b-form-select>
          </b-form-group>
          <b-form-group
            id="billable-group"
            label="Billable:"
            label-cols-sm="3"
            label-cols-xl="2"
            label-size="lg"
            label-align="right"
            label-for="billable-select">
            <b-form-select
              id="billable-select"
              size="lg"
              required
              v-model="newTimeEntry.selectedBillableTime">
              <option slot="first" :value="null">Select one..</option>
            </b-form-select>
          </b-form-group>
          <b-form-group
            id="duration-group"
            label="Duration:"
            label-cols-sm="3"
            label-cols-xl="2"
            label-size="lg"
            label-align="right"
            label-for="duration-input">
            <b-form-input
              id="duration-input"
              type="text"
              size="lg"
              v-model="newTimeEntry.duration"
              required/>
          </b-form-group>
      </b-form>
    </b-modal>
  </b-container>
</template>

<script>
import dayjs from 'dayjs'
import VueCal from 'vue-cal'
import 'vue-cal/dist/vuecal.css'

export default {
  name: "ProjectManagement",
  components: { VueCal },
  data: function () {
    return {
      events: [],
      timeEntries: [],
      newTimeEntry: {
        selectedTimeType: null,
        selectedBillableTime: null,
        duration:''
      },
      breadcrumbs: [
        {
          text: 'Home',
          to: { name: 'home' }
        },
        {
          text: 'Project Management',
          to: { name: 'project_management' }
        },
      ],
      timeEntryFields: [
        {
          key: 'id',
          label: "Id",
          sortable: true
        },
        {
          key: 'notes',
          label: "Notes",
          sortable: false
        },
        {
          key:'timeType.name',
          label: "Location",
          sortable: true
        },
        {
          key:'startTime',
          label: 'Start',
          sortable: true
        },
        {
          key:'endTime',
          label: 'End',
          sortable: true
        }
      ],
    }
  },
  methods: {
    displayDate(date){
      return dayjs(date).format('MMMM DD, YYYY');
    },
    formatCalendarDate(date){
      return dayjs(date).format('YYYY-MM-DD HH:mm');
    },
    toTimeEntry(timeEntries){
      return timeEntries.map(e => {
        return{
            title: e.notes,
            start: this.formatCalendarDate(e.startTime),
            end: this.formatCalendarDate(e.endTime)
            //content: e.workorder.readableId
          }
        }
      )
    }
  },
  mounted: function () {
    this.$http
      .all([
        this.$http.get('https://localhost:5001/api/calendar/time-entries'),
        this.$http.get('https://localhost:5001/api/calendar/events')
      ])
      .then(this.$http.spread((timeEntries, events) => {
        this.timeEntries = timeEntries.data
        console.log(this.timeEntries)
        this.events = this.toTimeEntry(timeEntries.data)
      }))
  },

}
</script>

<style lang="scss">
@import "../../styles/variables.scss";
.button{
  padding: 21px;
}

.title{
  padding-bottom: 1em;
}

.calendar{
  height: 100vh,
}

.vuecal {
    font-size: 1.15rem;
  }

.vuecal__flex.vuecal__menu{
  background-color: $primary;
  color: white;
}
.vuecal__flex{
  background-color: white;
  border: white;
}

.vuecal__event{
  background-color: fade-out($primary, 0.4);
  color: white;
}

.vuecal__body{
  overflow-y: auto;
  // &::-webkit-scrollbar {
  //   width: 0.75em;
  // }
  // &::-webkit-scrollbar-thumb {
  //   border-radius: 0.325rem;
  //   background-color: $primary-lightest;
  // }
  // &::-webkit-scrollbar-track {
  //   background: $offwhite;
  // }
}

.selected::before{
  border-color: rgb(236, 236, 236);
}



.vuecal__event-title{
  font-size: 1.2em;
  font-weight: bold;
  margin: 4px 0 8px;
}

.vuecal__time-column > .vuecal__time-cell {
  color: $primary;
}

</style>