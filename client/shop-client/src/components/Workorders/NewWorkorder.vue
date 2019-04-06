<template>
  <b-container fluid>
    <b-row align-h="start">
      <b-col>
        <b-breadcrumb :items="breadcrumbs" />
      </b-col>
    </b-row>
    <b-row align-h="center">
      <b-col sm="10" lg="8">
        <b-form id="workorder-form" @submit="submitWorkorder()">
          <b-card no-body header-tag="header">
            <b-row slot="header">
              <b-col class="d-flex align-items-center">
                <h2 class="my-0">New Workorder</h2>
              </b-col>
              <b-col class="d-flex justify-content-end">
                <b-button-toolbar>
                  <b-button @click="cancelWorkorder()" size="lg" class="mx-2" variant="outline-danger">
                    Cancel
                  </b-button>
                  <b-button type="submit" size="lg" variant="primary">
                    <!-- <FaIcon icon="plus" class="mr-2"/> -->
                    Create workorder
                    <!-- <router-link :to="{ name: 'new_workorder' }">Create</router-link> -->
                  </b-button>
                </b-button-toolbar>
              </b-col>
            </b-row>
            <b-card-body>
                <b-form-group
                  id="workorder-details-group"
                  class="mb-2"
                  label="Workorder Details"
                  label-size="lg"
                  label-cols-sm="4"
                  label-cols-lg="3"
                  label-class="font-weight-bold pt-0">

                  <b-form-group
                    id="title-group"
                    label="Title:"
                    label-cols-sm="3"
                    label-cols-xl="2"
                    label-size="lg"
                    label-align="right"
                    label-for="title-input">
                    <b-form-input
                      id="title-input"
                      type="text"
                      size="lg"
                      v-model="workorder.title"
                      required
                      placeholder="Project title..."/>
                  </b-form-group>

                  <b-form-group
                    id="purpose-group"
                    label="Purpose:"
                    label-cols-sm="3"
                    label-cols-xl="2"
                    label-size="lg"
                    label-align="right"
                    label-for="purpose-input">
                    <b-form-select
                      id="purpose-input"
                      size="lg"
                      required
                      v-model="workorder.purposeId"
                      :options="options.purposes">
                      <option slot="first" :value="null">Select one...</option>
                    </b-form-select>
                  </b-form-group>

                  <b-form-group
                    id="faculty-group"
                    label="Faculty:"
                    label-cols-sm="3"
                    label-cols-xl="2"
                    label-size="lg"
                    label-align="right"
                    label-for="faculty-input">
                    <b-form-select
                      id="faculty-input"
                      size="lg"
                      required
                      v-model="workorder.facultyId"
                        :options="options.faculties">
                        <option slot="first" :value="null">Select one...</option>
                    </b-form-select>
                  </b-form-group>

                  <b-form-group
                    id="date-required-group"
                    label="Date Required:"
                    label-cols-sm="3"
                    label-cols-xl="2"
                    label-size="lg"
                    label-align="right"
                    label-for="date-required-input">
                    <div class="d-flex flex-column w-100">
                      <vue-cal
                        xsmall
                        class="datepicker vuecal--rounded-theme vuecal-workorder vuecal--green-theme"
                        :selected-date="workorder.dateRequiredBy"
                        @day-focus="workorder.dateRequiredBy = $event"
                        hide-view-selector
                        :time="false"
                        default-view="month"
                        :disable-views="['week', 'day']">
                      </vue-cal>
                    </div>
                  </b-form-group>

                  <b-form-group
                    id="description-group"
                    label="Description:"
                    label-cols-sm="3"
                    label-cols-xl="2"
                    label-size="lg"
                    label-align="right"
                    label-for="description-input">
                    <b-form-textarea
                      id="description-input"
                      type="text"
                      size="lg"
                      v-model="workorder.description"
                      required
                      placeholder="Describe the project requirements..."/>
                  </b-form-group>

                  <b-form-group
                    id="quote-requested-group"
                    label="Quote Requested:"
                    label-cols-sm="3"
                    label-cols-xl="2"
                    label-size="lg"
                    label-align="right"
                    label-for="quote-requested-input">
                    <b-form-checkbox
                      id="quote-requested-input"
                      size="lg"
                      v-model="workorder.quoteRequested"/>
                  </b-form-group>

                  <b-form-group
                    id="attachments-group"
                    label="Attachments:"
                    label-cols-sm="3"
                    label-cols-xl="2"
                    label-size="lg"
                    label-align="right"
                    label-for="attachments-input">
                    <b-form-file
                      id="attachments-input"
                      multiple
                      v-model="attachments"
                      placeholder="Choose or drop drawings and attachments here..."
                      drop-placeholder="Drop drawings and attachments here..."/>
                  </b-form-group>

                </b-form-group>
                <b-form-group
                  id="client-info-group"
                  class="border-top pt-4"
                  label="Client Contact Information"
                  label-size="lg"
                  label-cols-sm="4"
                  label-cols-lg="3"
                  label-class="font-weight-bold pt-0">

                  <b-form-group
                    id="client-name-group"
                    label="Name:"
                    label-cols-sm="3"
                    label-cols-xl="2"
                    label-size="lg"
                    label-align="right"
                    label-for="client-name-input">
                    <b-form-input
                      id="client-name-input"
                      type="text"
                      size="lg"
                      v-model="workorder.clientName"
                      required/>
                  </b-form-group>

                  <b-form-group
                    id="client-phone-group"
                    label="Phone Number:"
                    label-cols-sm="3"
                    label-cols-xl="2"
                    label-size="lg"
                    label-align="right"
                    label-for="client-phone-input">
                    <b-form-input
                      id="client-phone-input"
                      type="text"
                      size="lg"
                      v-model="workorder.clientPhoneNumber"
                      required/>
                  </b-form-group>

                  <b-form-group
                    id="client-email-group"
                    label="Email:"
                    label-cols-sm="3"
                    label-cols-xl="2"
                    label-size="lg"
                    label-align="right"
                    label-for="client-email-input">
                    <b-form-input
                      id="client-email-input"
                      type="text"
                      size="lg"
                      v-model="workorder.clientEmail"
                      required/>
                  </b-form-group>

                </b-form-group>
            </b-card-body>
          </b-card>
        </b-form>
      </b-col>
    </b-row>
  </b-container>
</template>

<script>
import VueCal from 'vue-cal'
import 'vue-cal/dist/vuecal.css'

export default {
  name: "NewWorkorder",
  components: {
    VueCal
  },
  data: function () {
    return {
      workorder: {
        title: '',
        description: '',
        // attachments: [],
        dateRequiredBy: null,
        purposeId: null,
        facultyId: null,
        quoteRequested: false,
        clientName: '',
        clientPhoneNumber: '',
        clientEmail: ''
      },
      attachments: [], // TODO: Get rid of this, just for testing
      options: {
        faculties: null,
        purposes: null,
      },
      breadcrumbs: [
        {
          text: 'Home',
          to: { name: 'home' }
        },
        {
          text: 'Workorders',
          to: { name: 'workorders' }
        },
        {
          text: 'New',
          to: { name: 'new_workorder' }
        }
      ]
    }
  },
  methods: {
    cancelWorkorder() {
      this.$router.push({ name: 'workorders' })
    },
    submitWorkorder() {
      console.log(JSON.stringify(this.workorder))
      this.$http
        .post('https://localhost:5001/api/workorders', this.workorder)
        .then(response => {
          console.log(response.data)
          this.$router.push({ name: 'workorder_by_id', params: { workorderId: response.data.id } })
        })
    },
    formatOptions(data) {
      return data.map(item => {
        return {
          value: item.id,
          text: item.name
        }
      })
    }
  },
  created: function () {
    this.$http
      .all([
        this.$http.get('https://localhost:5001/api/workorders/purposes'),
        this.$http.get('https://localhost:5001/api/workorders/faculties')
      ])
      .then(this.$http.spread((purposes, faculties) => {
        this.options.purposes = this.formatOptions(purposes.data),
        this.options.faculties = this.formatOptions(faculties.data)
      }))
  }
}
</script>

<style lang="scss">
@import "../../styles/variables.scss";

label {
  font-size: 1.2rem;
  // width: 80px;
}
.form-row {
  // margin: 0.75rem 0 0;
}
.form-group {
  margin-bottom: 1.5rem !important;
}
.datepicker {
  width: 100%;
  // width: 200px;
  height: 350px;
}
.date-required-display {
  marign-top: 5px;
  margin-bottom: 5px;
  height: 2rem;
  line-height: 2rem;
  font-size: 1.25rem;
}
.vuecal-workorder{
  .vuecal {
    font-size: 1rem;
  }
  .vuecal__cell {
    font-size: 1.25rem;
    &.today .vuecal__cell-content {
      background-color: $primary !important;
    }
    &.selected .vuecal__cell-content {
      border: 1.5px solid $primary-dark !important;
    }
  }
  .vuecal__cell-content {
    background-color: fade-out($primary-lighter, 0.8) !important;
  }

  #client-info-group {
    border-style: dashed !important;
  }
  #description, #description-input {
    height: 100px;
  }
  #attachments-input {
    height: $input-height-lg;
    line-height: $input-line-height-lg;
  }
  /*
  .vuecal__menu, .vuecal__cell-events-count {background-color: #42b983;}
  .vuecal__menu li {border-bottom-color: #fff;color: #fff;}
  .vuecal__menu li.active {background-color: rgba(255, 255, 255, 0.15);}
  .vuecal__title {background-color: $primary-lighter;}
  .vuecal__cell.today, .vuecal__cell.current {background-color: rgba(240, 240, 255, 0.4);}
  .vuecal:not(.vuecal--day-view) .vuecal__cell.selected {background-color: rgba(235, 255, 245, 0.4);}
  .vuecal__cell.selected:before {border-color: $success; background-color: fade-out($success, 0.7)}
  */
}
</style>
