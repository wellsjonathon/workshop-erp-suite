<template>
  <b-container fluid>
    <b-row>
      <b-col>
        <b-breadcrumb :items="breadcrumbs" />
        <h2>{{ workorder.title }}</h2>
      </b-col>
    </b-row>
    <b-row class="mt-3">
      <b-col>
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
      </b-col>
    </b-row>
    <b-row>
      <b-col cols="8">
        <b-row class="my-3">
          <b-col>
            <b-card>
              <h3>Description</h3>
              <p>{{ workorder.description }}</p>
            </b-card>
          </b-col>
        </b-row>
        <b-row class="my-3">
          <b-col>
            <b-card no-body>
              <b-tabs card>

                <b-tab title="Materials" active>
                  <b-table hover outlined :items="materials" :fields="materialsFields">
                    <template slot="vendorMaterial" slot-scope="data">
                      {{ (data.value.vendor != null) ? data.value.vendor.name : 'No vendor specified' }}
                    </template>
                    <template slot="costPerUnit" slot-scope="data">
                      ${{ data.value }}
                    </template>
                    <template slot="total" slot-scope="data">
                      ${{ data.item.quantityUsed * data.item.costPerUnit }}
                    </template>
                  </b-table>
                </b-tab>

                <b-tab title="Time Entries">
                  <b-table hover outlined :items="workorder.timeEntries">

                  </b-table>
                </b-tab>

              </b-tabs>
            </b-card>
          </b-col>
        </b-row>
        <b-row class="my-3">
          <b-col>
            <b-card no-body>
              <b-tabs card>

                <b-tab title="Comments" active>

                </b-tab>

                <b-tab title="Notes">

                </b-tab>

              </b-tabs>
            </b-card>
          </b-col>
        </b-row>
      </b-col>
      <b-col cols="4">
        <b-row class="my-3">
          <b-col>
            <b-card>
              <h3>Details</h3>
              Purpose: Research
            </b-card>
          </b-col>
        </b-row>
        <b-row class="my-3">
          <b-col>
            <b-card>
              <h3>Total</h3>
            </b-card>
          </b-col>
        </b-row>
        <b-row class="my-3">
          <b-col>
            <b-card>
              <h3>Estimate</h3>
            </b-card>
          </b-col>
        </b-row>
      </b-col>
    </b-row>
  </b-container>
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
      materials: null,
      materialsFields: [
        {
          key: 'material.name',
          label: 'Material',
          sortable: true
        },
        {
          key: 'vendorMaterial',
          label: 'Vendor',
          sortable: true
        },
        {
          key: 'quantityUsed',
          label: 'Quantity',
          sortable: false
        },
        {
          key: 'unitOfMeasure.name',
          label: 'Unit',
          sortable: false
        },
        {
          key: 'costPerUnit',
          sortable: true
        },
        {
          key: 'total',
          label: 'Total',
          sortable: true
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
        this.$http.get('https://localhost:5001/api/workorders/' + this.workorderId + '/state/transitions'),
        this.$http.get('https://localhost:5001/api/workorders/' + this.workorderId + '/materials')
      ])
      .then(this.$http.spread((workorder, transitions, materials) => {
        this.workorder = workorder.data
        this.transitions = transitions.data,
        this.materials = materials.data
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

<style lang="scss">
@import "../../styles/variables.scss";
</style>
