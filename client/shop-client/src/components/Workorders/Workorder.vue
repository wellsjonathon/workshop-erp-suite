<template>
  <b-container fluid>
    <b-row>
      <b-col>
        <b-breadcrumb :items="breadcrumbs" />
      </b-col>
    </b-row>
    <b-row align-h="center" class="mt-3">
      <b-col sm="12" lg="10">
        <b-card no-body header-tag="header">
          <b-row slot="header">
            <b-col class="d-flex align-items-center">
              <h2 class="my-0">{{ workorder.readableId }}: {{ workorder.title }}</h2>
            </b-col>
            <b-col class="d-flex justify-content-end">
              <b-button-toolbar>
                <b-button-group size="lg" class="mr-2">
                  <b-button v-b-modal.add-comment variant="outline-primary">Add Comment</b-button>
                  <b-button variant="outline-primary">Add Note</b-button>
                </b-button-group>
                <b-button v-b-modal.change-status variant="primary" size="lg" class="mr-2">Update Status</b-button>
                <b-button size="lg" variant="primary">
                  <FaIcon icon="edit"/>
                  Edit
                </b-button>
              </b-button-toolbar>
            </b-col>
          </b-row>
          <b-card-body>
            <b-row>
              <b-col sm="7" lg="8">
                <div class="d-flex flex-column">
                  <div class="mb-5">
                    <div class="d-flex flex-row w-100 align-items-baseline mb-2">
                      <h3 class="mb-2">Description</h3>
                      <span class="underline-separator flex-grow-1"></span>
                    </div>
                    <p class="my-0">{{ workorder.description }}</p>
                  </div>
                  <div>
                    <div class="d-flex flex-row w-100 align-items-baseline mb-2">
                      <h3 class="mb-2">Drawings &amp; Attachments</h3>
                      <span class="underline-separator flex-grow-1"></span>
                    </div>
                    <b-card class="attachments-display">
                      <p class="text-danger my-0">No drawings or other attachments added.</p>
                    </b-card>
                  </div>
                </div>
              </b-col>
              <b-col sm="5" lg="4">
                <div class="d-flex flex-column">
                  <div class="mb-5">
                    <div class="d-flex flex-row w-100 align-items-baseline">
                      <h3 class="mb-2">Details</h3>
                      <span class="underline-separator flex-grow-1"></span>
                    </div>
                    <b-form-group class="details-row"
                      label="Status:"
                      label-cols-sm="3"
                      label-cols-xl="4"
                      label-size="lg"
                      label-align="left">
                      <span class="details-span">{{ workorder.state.name }}</span>
                    </b-form-group>
                    <b-form-group class="details-row"
                      label="Faculty:"
                      label-cols-sm="3"
                      label-cols-xl="4"
                      label-size="lg"
                      label-align="left">
                      <span class="details-span">{{ workorder.faculty.name }}</span>
                    </b-form-group>
                    <b-form-group class="details-row"
                      label="Purpose:"
                      label-cols-sm="3"
                      label-cols-xl="4"
                      label-size="lg"
                      label-align="left">
                      <span class="details-span">{{ workorder.purpose.name }}</span>
                    </b-form-group>
                    <b-form-group class="details-row"
                      label="Date Required:"
                      label-cols-sm="3"
                      label-cols-xl="4"
                      label-size="lg"
                      label-align="left">
                      <span class="details-span">{{ displayDate(workorder.dateRequiredBy) }}</span>
                    </b-form-group>
                    <b-form-group class="details-row"
                      label="Date Created:"
                      label-cols-sm="3"
                      label-cols-xl="4"
                      label-size="lg"
                      label-align="left">
                      <span class="details-span">{{ displayDate(workorder.dateCreated) }}</span>
                    </b-form-group>
                  </div>
                  <div>
                    <div class="d-flex flex-row w-100 align-items-baseline">
                      <h3 class="mb-2">Client Info</h3>
                      <span class="underline-separator flex-grow-1"></span>
                    </div>
                    <!-- <b-card> -->
                      <b-form-group class="details-row"
                        label="Name:"
                        label-cols-sm="3"
                        label-cols-xl="4"
                        label-size="lg"
                        label-align="left">
                        <span class="details-span">{{ workorder.clientName }}</span>
                      </b-form-group>
                      <b-form-group class="details-row"
                        label="Phone Number:"
                        label-cols-sm="3"
                        label-cols-xl="4"
                        label-size="lg"
                        label-align="left">
                        <span class="details-span">{{ workorder.clientPhoneNumber }}</span>
                      </b-form-group>
                      <b-form-group class="details-row"
                        label="Email:"
                        label-cols-sm="3"
                        label-cols-xl="4"
                        label-size="lg"
                        label-align="left">
                        <span class="details-span">{{ workorder.clientEmail }}</span>
                      </b-form-group>
                    <!-- </b-card> -->
                  </div>
                </div>
              </b-col>
            </b-row>
          </b-card-body>
        </b-card>
      </b-col>
    </b-row>
    <b-row class="justify-content-center">
      <b-col sm="12" lg="10">
        <b-row class="my-3">
          <b-col>
            <b-card no-body>
              <b-tabs card>

                <b-tab title="Materials" active>
                  <!-- <div class="d-flex flex-row justify-content-end w-100">
                  </div> -->
                  <b-table hover outlined :items="materials" :fields="materialsFields" :busy="isMaterialsBusy">
                    <template slot="thead-top" slot-scope="data">
                      <tr>
                        <th colspan="6">
                          <div class="d-flex flex-row justify-content-between align-items-end">
                            <h4>Total Material Cost: <span class="text-primary">${{ calculateTotalMaterialCost() }}</span></h4>
                            <b-button v-b-modal.add-material size="lg" variant="primary" class="float-right">
                              <FaIcon icon="plus" class="mr-3"/>
                              Add material
                            </b-button>
                          </div>
                        </th>
                      </tr>
                    </template>
                    <div slot="table-busy" class="text-center text-danger my-2">
                      <b-spinner class="align-middle"></b-spinner>
                    </div>
                    <template slot="vendorMaterial" slot-scope="data">
                      {{ (data.value.vendor != null) ? data.value.vendor.name : 'No vendor specified' }}
                    </template>
                    <template slot="costPerUnit" slot-scope="data">
                      ${{ data.value }}
                    </template>
                    <template slot="total" slot-scope="data">
                      ${{ (data.item.quantityUsed * data.item.costPerUnit).toFixed(2) }}
                    </template>
                  </b-table>
                </b-tab>

                <b-tab title="Time Entries & Labour">
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
                  <div class="d-flex flex-row justify-content-end w-100">
                    <b-button v-b-modal.add-comment variant="primary" size="lg" class="mb-3">
                      <FaIcon icon="plus" class="mr-3"/>
                      Add Comment
                    </b-button>
                  </div>
                  <b-list-group>
                    <b-list-group-item v-for="comment in commentsAscendingAge" :key="comment.timestamp">
                      <Comment :comment="comment" />
                    </b-list-group-item>
                  </b-list-group>
                </b-tab>

                <b-tab title="Working Notes">

                </b-tab>

              </b-tabs>
            </b-card>
          </b-col>
        </b-row>
      </b-col>
      <!-- <b-col sm="5" lg="4">
        <b-row class="my-3">
        </b-row>
      </b-col> -->
    </b-row>

    <b-modal id="add-comment" title="Add Comment" v-model="showAddComment">
      <b-form>
        <b-form-group
          id="comment-username-group"
          class="details-row"
          label="Username:"
          label-cols-sm="3"
          label-cols-xl="2"
          label-size="lg"
          label-align="right">
          <span class="details-span">{{ addCommentModal.username }}</span>
        </b-form-group>
        <b-form-group
          id="comment-group"
          class="details-row"
          label="Comment:"
          label-cols-sm="3"
          label-cols-xl="2"
          label-size="lg"
          label-align="right"
          label-for="comment-input">
          <b-form-textarea
            id="comment-input"
            type="text"
            size="lg"
            v-model="addCommentModal.comment"
            required
            placeholder="Add a comment..."/>
        </b-form-group>
      </b-form>
      <div slot="modal-footer" class="w-100 d-flex flex-row justify-content-end">
        <b-button variant="outline-danger" size="lg" class="mr-2" @click="cancelAddComment()">Cancel</b-button>
        <b-button variant="primary" size="lg" @click="addComment()">Add Comment</b-button>
      </div>
    </b-modal>

    <b-modal id="change-status" title="Change Status" v-model="showChangeStatus">
      <b-form>
        <b-form-group
          id="state-group"
          label="Next State:"
          label-cols-sm="3"
          label-cols-xl="2"
          label-size="lg"
          label-align="right"
          label-for="state-select">
          <b-form-select
            id="state-select"
            size="lg"
            required
            v-model="stateChange.selectedTransition"
              :options="formattedTransitions">
              <option slot="first" :value="null">Select one...</option>
          </b-form-select>
        </b-form-group>
        <b-form-group
          id="state-comment-group"
          label="Comment:"
          label-cols-sm="3"
          label-cols-xl="2"
          label-size="lg"
          label-align="right"
          label-for="state-comment-input">
          <b-form-textarea
            id="state-comment-input"
            type="text"
            size="lg"
            v-model="stateChange.comment"
            required
            placeholder="Add a comment on the status update..."/>
        </b-form-group>
      </b-form>
      <div slot="modal-footer" class="w-100 d-flex flex-row justify-content-end">
        <b-button variant="outline-danger" size="lg" class="mr-2" @click="cancelChangeStatus()">Cancel</b-button>
        <b-button variant="primary" size="lg" @click="changeStatus()">Add Comment</b-button>
      </div>
    </b-modal>

    <b-modal id="add-material" title="Add Material" v-model="showAddMaterial" size="md">
      <b-form>
        <b-form-group
          id="material-group"
          label="Material:"
          label-cols-sm="4"
          label-cols-xl="3"
          label-size="lg"
          label-align="right"
          label-for="material-select">
          <b-form-select
            id="material-select"
            size="lg"
            required
            text-field="name"
            value-field="id"
            v-model="addMaterialModal.material"
            :options="allMaterials">
              <option slot="first" :value="null">Select one...</option>
          </b-form-select>
        </b-form-group>
        <b-form-group
          id="quantity-group"
          label="Quantity:"
          label-cols-sm="4"
          label-cols-xl="3"
          label-size="lg"
          label-align="right"
          label-for="quantity-input">
          <b-input-group size="lg">
            <b-form-input
              id="quantity-input"
              type="text"
              size="lg"
              v-model="addMaterialModal.quantity"
              required/>
            <b-input-group-append class="cost-append d-flex justify-content-center align-items-center">
              <span>
                {{ (selectedMaterial != null) ? selectedMaterial.unitOfMeasure.name : "-"}}
              </span>
            </b-input-group-append>
          </b-input-group>
        </b-form-group>
        <b-form-group
          id="cost-group"
          label="Cost Per Unit:"
          label-cols-sm="4"
          label-cols-xl="3"
          label-size="lg"
          label-align="right"
          label-for="cost-input">
          <b-input-group size="lg" prepend="$">
            <b-form-input
              id="cost-input"
              type="text"
              size="lg"
              v-model="addMaterialModal.costPerUnit"
              required/>
            <b-input-group-append class="cost-append d-flex justify-content-center align-items-center">
              <span>
                Per {{ (selectedMaterial != null) ? selectedMaterial.unitOfMeasure.name : "-"}}
              </span>
            </b-input-group-append>
          </b-input-group>
        </b-form-group>
      </b-form>
      <div slot="modal-footer" class="w-100 d-flex flex-row justify-content-end">
        <b-button variant="outline-danger" size="lg" class="mr-2" @click="this.cancelAddMaterial">Cancel</b-button>
        <b-button variant="primary" size="lg" @click="addMaterial()">Add Material</b-button>
      </div>
    </b-modal>

  </b-container>
</template>

<script>
import dayjs from 'dayjs'
import Comment from './../Activities/Comment.vue'

export default {
  name: "Workorder",
  props: {
    workorderId: {
      type: Number,
      required: true
    }
  },
  components: {
    Comment
  },
  data: function () {
    return {
      workorder: null,
      materials: null,
      transitions: null,
      stateChange: {
        selectedTransition: null,
        username: "Jonathon Wells",
        comment: ''
      },
      addMaterialModal: {
        material: null,
        quantity: 0,
        costPerUnit: 0
      },
      addCommentModal: {
        username: "Jonathon Wells",
        comment: ''
      },
      allMaterials: null,
      showChangeStatus: false,
      showAddMaterial: false,
      showAddComment: false,
      isMaterialsBusy: false,
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
          text: this.workorderReadableId,
          to: { name: 'workorder_by_id', params: { workorderId: this.workorderId } }
        }
      ]
    }
  },
  computed: {
    commentsAscendingAge: function () {
      return this.workorder.comments.reverse()
    },
    formattedTransitions: function () {
      return this.formatTransitions(this.transitions)
    },
    selectedMaterial: function () {
      return this.allMaterials.find(mat => { return mat.id === this.addMaterialModal.material })
    }
  },
  created: function () {
    this.isMaterialsBusy = true
    this.$http
      .all([
        this.$http.get('https://localhost:5001/api/workorders/' + this.workorderId),
        this.$http.get('https://localhost:5001/api/workorders/' + this.workorderId + '/state/transitions'),
        this.$http.get('https://localhost:5001/api/workorders/' + this.workorderId + '/materials'),
        this.$http.get('https://localhost:5001/api/inventory/materials')
      ])
      .then(this.$http.spread((workorder, transitions, materials, allMaterials) => {
        this.workorder = workorder.data
        this.transitions = transitions.data,
        this.materials = materials.data,
        this.breadcrumbs[this.breadcrumbs.length - 1] = workorder.data.readableId,
        this.allMaterials = allMaterials.data
        this.isMaterialsBusy = false
      }))
  },
  methods: {
    displayDate(date) {
      return dayjs(date).format('MMMM DD, YYYY');
    },
    calculateTotalMaterialCost() {
      var totalCost = 0
      this.materials.forEach(material => {
        totalCost += material.quantityUsed * material.costPerUnit
      })
      return totalCost.toFixed(2)
    },
    formatTransitions(transitions) {
      return transitions.map(t => {
        return {
          value: t.nextState.id,
          text: t.nextState.name
        }
      })
    },
    changeStatus() {
      this.$http
        .post('https://localhost:5001/api/workorders/' + this.workorderId + '/state',
          {
            NewStateId: this.stateChange.selectedTransition,
            username: this.stateChange.username,
            comment: this.stateChange.comment
          })
        .then(response => {
          this.$http
            .get('https://localhost:5001/api/workorders/' + this.workorderId)
            .then(workorder => {
              this.workorder = workorder.data
            })
        })
      this.showChangeStatus = false
    },
    cancelChangeStatus() {
      this.stateChange = {
        selectedTransition: null,
        username: "Jonathon Wells",
        comment: ''
      }
      this.showChangeStatus = false
    },
    addMaterial() {
      this.$http
        .post('https://localhost:5001/api/workorders/' + this.workorderId + "/materials",
          {
            MaterialId: this.selectedMaterial.id,
            UnitOfMeasureId: this.selectedMaterial.unitOfMeasureId,
            CostPerUnit: Number(this.addMaterialModal.costPerUnit),
            QuantityUsed: Number(this.addMaterialModal.quantity)
          })
        .then(response => {
          this.materials.push(response.data)
        })
      this.showAddMaterial = false
    },
    cancelAddMaterial() {
      this.addMaterialModal = {
        material: null,
        quantity: 0,
        costPerUnit: 0
      }
      this.showAddMaterial = false
    },
    addComment() {
      this.$http
        .post('https://localhost:5001/api/workorders/' + this.workorderId + "/comments",
          this.addCommentModal)
        .then(response => {
          this.workorder.comments.push(response.data)
        })
      this.showAddComment = false
    },
    cancelAddComment() {
      this.addCommentModal = {
        username: "Jonathon Wells",
        comment: ''
      }
      this.showAddComment = false
    }
  }
}
</script>

<style lang="scss">
@import "../../styles/variables.scss";


.form-group.details-row {
  font-size: 1.25rem;
  margin-bottom: 0 !important;
  & .form-row {
    display: flex;
    flex-direction: row;
    align-items: center;
    & .col-form-label {
      font-weight: 600;
    }
  }
}
.details-span {
  font-size: inherit;
}
.cost-append {
  // width: 3rem;
  padding: 0 4px;
  height: inherit;
  font-size: 1.3rem;
  border: 1px solid $gray-400;
  background-color: $gray-300;
}
.attachments-display {
  //  height: 100px;
}
.underline-separator {
  border-bottom: 2px solid $gray-700;
  margin-left: 4px;
}
#comment-input {
  height: 100px;
}
</style>
