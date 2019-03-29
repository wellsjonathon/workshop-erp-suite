<template>
  <b-container>
    <b-row>
      <b-col>
        <b-breadcrumb :items="breadcrumbs" />
      </b-col>
    </b-row>
    <b-row>
      <h2>Time Entries</h2>
    </b-row>
    <b-col>
      <vue-cal defaultView='week'
              :disable-views="['years', 'year']"
              :events="events"
              :time-from="8 * 60"
              events-on-month-view="short"
              class="calendar">
      </vue-cal>
    </b-col>
    <b-col>
    </b-col>
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
          }
        }
      )
    }
      //let mapped = []
      // for(let entry in timeEntries){
      //   mapped.push({
      //       title: timeEntries[entry].notes,
      //       start: timeEntries[entry].startTime,
      //       end: timeEntries[entry].endTime
      //     })
      // }
      // console.log(mapped)
      // this.events = mapped
  },
  mounted: function () {
    this.$http
      .all([
        this.$http.get('https://localhost:5001/api/calendar/time-entries'),
        this.$http.get('https://localhost:5001/api/calendar/events')
      ])
      .then(this.$http.spread((timeEntries, events) => {
        this.timeEntries = timeEntries.data
        this.events = this.toTimeEntry(timeEntries.data)
        console.log(this.events)
      }))
  },

}
</script>

<style lang="scss" scoped>
@import "../../styles/variables.scss";
.button{
  padding: 21px;
}

.button_text{
   color: white;
}
.calendar{
  height: 500px,
}


</style>