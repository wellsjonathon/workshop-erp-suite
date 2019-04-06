<template>
  <b-container>
    <b-row>
      <b-col>
        <b-breadcrumb :items="breadcrumbs" />
      </b-col>
    </b-row>
    <b-row>
      <b-table striped hover outlined :items="timeEntries" :fields="timeEntryfields" :primary-key="id">

      </b-table>
    </b-row>
  </b-container>
</template>

<script>
import dayjs from 'dayjs'
export default {
  name: "NewTimeEntry",
  data: function () {
    return {
      breadcrumbs: [
        {
          text: 'Home',
          to: { name: 'home' }
        },
        {
          text: 'Project Management',
          to: { name: 'project_management'}
        },
        {
          text: 'New Time Entry',
          to: { name: 'new_time_entry' }
        },
      ]
    }
  }
}
</script>
