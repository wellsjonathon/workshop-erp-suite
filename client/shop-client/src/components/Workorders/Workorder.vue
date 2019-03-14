<template>
  <b-container>
    <b-row>
      <div class="content-header border-bottom w-100">
        <b-breadcrumb :items="breadcrumbs" />
        <h2>{{ workorder.title }}</h2>
      </div>
    </b-row>
    <b-row>
      <b-button-toolbar>
        <b-button>
          <FaIcon icon="edit"/>
          Edit
        </b-button>
        <b-button-group>
          <b-button>Add Comment</b-button>
          <b-button>Add Note</b-button>
        </b-button-group>
        <b-dropdown right variant="primary" text="Other State Actions">
          <b-dropdown-item v-for="transition in transitions" :key="transition.id">
            {{ transition.nextState.name }}
          </b-dropdown-item>
        </b-dropdown>
      </b-button-toolbar>
    </b-row>
    <b-row>
      <b-col cols="8">
        <b-row>
          <h3>Description</h3>
          <b-form-textarea></b-form-textarea>
        </b-row>
        <b-row>
          <b-tabs class="w-100">
            <b-tab title="Materials" active>
              <b-table hover outlined :items="workorder.materials">

              </b-table>
            </b-tab>
            <b-tab title="Time Entries">
              <b-table hover outlined :items="workorder.timeEntries">

              </b-table>
            </b-tab>
          </b-tabs>
        </b-row>
        <b-row>
          <b-tabs class="w-100">
            <b-tab title="Comments" active>

            </b-tab>
            <b-tab title="Notes">

            </b-tab>
          </b-tabs>
        </b-row>
      </b-col>
      <b-col cols="4">

      </b-col>
    </b-row>
  </b-container>
  <!--
  <div class="container">
    <div class="container__row">
      <div class="breadcrumbs">
        <ul>
          <li><router-link to="/home">Home</router-link></li>
          <li><router-link to="/workorders">Workorders</router-link></li>
          <li>
            <router-link
              :to="{
                name: 'workorder_by_id',
                params: { workorderId: this.workorderId }}">
              2019-S1-00{{ this.workorderId }}
            </router-link>
          </li>
        </ul>
      </div>
    </div>
    <div class="container__row">
      <div class="card col1">
        <div class="card__row card__header">
          <div style="display: flex; flex-direction: row; align-items: center;">
            <h1>2019-S1-00{{ this.workorderId }}</h1>
            <div class="status-row" style="margin-left: 15px;">
              <span class="status__indicator"></span>
              <span class="status__name">{{this.workorder.status.name}}</span>
            </div>
          </div>
          <div class="btn-group--unattached">
            <button @click="this.changeStatus">Change Status</button>
            <button>Edit</button>
          </div>
        </div>
        <div class="card__row">
            <div>
              <p><strong>Client Name:</strong> {{this.workorder.clientName}}</p>
              <p><strong>Client Phone Number:</strong> {{this.workorder.clientPhoneNumber}}</p>
              <p><strong>Client Email:</strong> {{this.workorder.clientEmail}}</p>
            </div>
        </div>
      </div>
    </div>
    <div class="container__row">
      <div class="card col2">
        <div class="card__row card__header">
          <h1>Materials</h1>
        </div>
        <div class="card__row data-table">
            <table>
              <thead>
                <th scope="col">Material</th>
                <th scope="col">Quantity</th>
                <th scope="col">Units</th>
                <th scope="col">Cost (per unit)</th>
                <th scope="col">Total Cost</th>
              </thead>
              <tbody>
                <tr v-for="material in materials" :key="material.id">
                  <td>{{material.name}}</td>
                  <td>{{material.quantityUsed}}</td>
                  <td>{{material.units}}</td>
                  <td>${{material.costPerUnit}}</td>
                  <td>${{material.quantityUsed * material.costPerUnit}}</td>
                </tr>
              </tbody>
              <tfoot>
                <td></td>
                <td></td>
                <td></td>
                <td>Total Materials Cost:</td>
                <td>${{calculateTotalMaterialCost()}}</td>
              </tfoot>
            </table>
          </div>
      </div>
      <div class="card col2">
        <div class="card__row card__header">
          <h1>Labour &amp; Time Entries</h1>
        </div>
      </div>
    </div>
  </div>-->
</template>

<script>
export default {
  name: "Workorder",
  props: {
    workorderId: {
      type: Number,
      required: true
    }
  },
  data: function () {
    return {
      workorder: null,
      transitions: null,
      materials: [ // Hardcoded placeholder, will be in workorder from server
        {
          id: 1,
          name: "Steel Tube",
          quantityUsed: 18,
          units: "inches",
          costPerUnit: 1.99
        },
        {
          id: 2,
          name: "4041 Aluminum",
          quantityUsed: 2,
          units: "square feet",
          costPerUnit: 36
        },
        {
          id: 3,
          name: "UHMW",
          quantityUsed: 3,
          units: "square feet",
          costPerUnit: 18
        }
      ],
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
          text: this.workorderId,
          to: { name: 'workorder_by_id', params: { workorderId: this.workorderId } }
        }
      ]
    }
  },
  mounted: function () {
    this.$http
      .all([
        this.$http.get('https://localhost:5001/api/workorders/' + this.workorderId),
        this.$http.get('https://localhost:5001/api/workorders/' + this.workorderId + '/state/transitions')
      ])
      .then(this.$http.spread((workorder, transitions) => {
        this.workorder = workorder.data
        this.transitions = transitions.data
      }))
  },
  methods: {
    calculateTotalMaterialCost() {
      var totalCost = 0
      this.materials.forEach(material => {
        totalCost += material.quantityUsed * material.costPerUnit
      })
      return totalCost
    },
    changeStatus() {
      console.log("Update status from: " + this.workorder.status.id)
      this.$http
        .put('https://localhost:5001/api/Workorders/' + this.workorderId + '/status',
          { ID: this.workorder.status.id + 1 })
        .then(response => {
          this.workorder = response.data
          console.log("New status: " + this.workorder.status.id)
        })
    }
  }
}
</script>

<style lang="scss" scoped>
@import "../../styles/variables.scss";
.card__row {
  &.card__details {
    display: flex;
    flex-direction: column;
    & span {
      display: block;
    }
  }
}
</style>
