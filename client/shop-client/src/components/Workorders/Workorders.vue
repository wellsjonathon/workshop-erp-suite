<template>
  <b-container>
    <b-row>
      <b-col>
        <b-breadcrumb :items="breadcrumbs" />
      </b-col>
    </b-row>
    <b-row>
      <b-col>
        <b-card no-body header-tag="header">
          <b-row slot="header">
            <b-col class="d-flex align-items-center">
              <h2 class="my-0">Workorders</h2>
            </b-col>
            <b-col class="d-flex justify-content-end">
              <b-button-toolbar>
                <!-- <b-dropdown class="mx-2" right text="Export" size="lg" variant="primary">
                  <b-dropdown-item>PDF</b-dropdown-item>
                </b-dropdown> -->
                <b-button size="lg" variant="primary">
                  <FaIcon icon="plus" />
                  <router-link :to="{ name: 'new_workorder' }">New</router-link>
                </b-button>
              </b-button-toolbar>
            </b-col>
          </b-row>
          <b-card-body>
            <b-row>
              <b-col cols="8">
                <b-button-toolbar class="mb-3">
                  <b-input-group size="lg">
                    <b-form-select v-model="selectedFilters.state" :options="filters.states">
                      <option slot="first" :value="null">State</option>
                    </b-form-select>
                    <b-form-select v-model="selectedFilters.faculty" :options="filters.faculties">
                      <option slot="first" :value="null">Faculty</option>
                    </b-form-select>
                    <b-form-select v-model="selectedFilters.use" :options="filters.uses">
                      <option slot="first" :value="null">Use</option>
                    </b-form-select>
                  </b-input-group>
                  <b-button-group size="lg" class="mx-2">
                    <b-button variant="outline-primary" @click="this.applyFilters">Apply Filters</b-button>
                    <b-button variant="outline-primary" @click="this.clearFilters">Clear Filters</b-button>
                  </b-button-group>
                </b-button-toolbar>
              </b-col>
              <b-col cols="4">
                <b-input-group size="lg">
                  <b-form-input
                    placeholder="Search..." />
                  <b-input-group-append>
                    <b-button variant="primary">
                      <FaIcon icon="search"/>
                    </b-button>
                  </b-input-group-append>
                </b-input-group>
              </b-col>
            </b-row>
            <b-table striped hover outlined :items="workorders" :fields="workorderFields" :primary-key="id">
              <template slot="title" slot-scope="data">
                <router-link :to="{
                  name: 'workorder_by_id',
                  params: { workorderId: data.item.id }}">
                  {{ data.value }}
                </router-link>
              </template>
              <template slot="state" slot-scope="data">
                <div class="state">
                  {{ data.value.name }}
                </div>
              </template>
              <template slot="dateRequiredBy" slot-scope="data">
                {{ displayDate(data.value) }}
              </template>
              <template slot="dateCreated" slot-scope="data">
                {{ displayDate(data.value) }}
              </template>
            </b-table>
            <b-row>
              <b-col>
                Showing 1 of 1000
              </b-col>
              <b-col>
                <b-pagination align="right"
                  v-model="pagination.current"
                  :total-rows="pagination.rows"
                  :per-page="pagination.rowsPerPage"
                  first-text="First"
                  prev-text="Prev"
                  next-text="Next"
                  last-text="Last"
                  variant="primary"
                  size="lg" />
              </b-col>
            </b-row>
          </b-card-body>
        </b-card>
      </b-col>
    </b-row>
  </b-container>
</template>

<script>
import dayjs from 'dayjs'

export default {
  name: "Workorders",
  data: function () {
    return {
      workorders: null,
      workorderFields: [
        {
          key: 'id',
          sortable: true
        },
        {
          key: 'title',
          sortable: true
        },
        {
          key: 'clientName',
          sortable: true
        },
        {
          key: 'faculty.name',
          label: "Faculty",
          sortable: true
        },
        {
          key: 'dateRequiredBy',
          sortable: true
        },
        {
          key: 'dateCreated',
          sortable: true
        },
        {
          key: 'state',
          label: 'State',
          sortable: true
          // Thoughts: Rather pass full state object and use a template to style that column
          // And sort by state.id, not state.name
        }
      ],
      pagination: {
        current: 1,
        rows: 100,
        rowsPerPage: 10
      },
      filters: {
        states: null,
        faculties: null,
        uses: null
      },
      selectedFilters: {
        state: null,
        faculty: null,
        use: null
      },
      advSearchCollapsed: true,
      breadcrumbs: [
        {
          text: 'Home',
          to: { name: 'home' }
        },
        {
          text: 'Workorders',
          to: { name: 'workorders' }
        },
      ]
    }
  },
  methods: {
    toggleAdvSearch() {
      this.advSearchCollapsed = !this.advSearchCollapsed
    },
    displayDate(date) {
      return dayjs(date).format('MMMM DD, YYYY');
    },
    applyFilters() {
      this.$http
        .get('https://localhost:5001/api/workorders/filter', {
          params: {
            state: this.selectedFilters.state,
            faculty: this.selectedFilters.faculty,
            use: this.selectedFilters.use
          }
        })
        .then(response => {
          this.workorders = response.data
        })
    },
    clearFilters() {
      this.$http
        .get('https://localhost:5001/api/workorders')
        .then(response => {
          this.workorders = response.data
        })
      this.selectedFilters = {
        state: null,
        faculty: null,
        use: null
      }
    },
    formatFilters(data) {
      return data.map(item => {
        return {
          value: item.id,
          text: item.name
        }
      })
    }
  },
  mounted: function () {
    this.$http
      .all([
        this.$http.get('https://localhost:5001/api/workorders'),
        this.$http.get('https://localhost:5001/api/workorders/faculties'),
        this.$http.get('https://localhost:5001/api/workorders/uses'),
        this.$http.get('https://localhost:5001/api/workflow/states')
      ])
      .then(this.$http.spread((workorders, faculties, uses, states) => {
        this.workorders = workorders.data;
        this.filters.faculties = this.formatFilters(faculties.data);
        this.filters.uses = this.formatFilters(uses.data);
        this.filters.states = this.formatFilters(states.data);
      }))
    //this.$http
    //  .get('')
  }
}
</script>

<style lang="scss" scoped>
@import "../../styles/variables.scss";

.state {
  text-transform: uppercase;
  text-align: center;
  font-weight: 700;
  border: 2px solid $green;
  border-radius: $radius;
  color: $green;
}

.state-row {
  display: inline-flex;
  flex-direction: row;
  font-size: 1.2rem;
  align-items: baseline;
  & .indicator {
    margin: 0 5px 0 0;
    width: 1rem;
    height: 1rem;
    border-radius: 0.5rem;
    background-color: $green;
  }
}

.page_header {
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  padding: 21px 28px;
  border-bottom: 1px solid $offwhite; /* Maybe reconsider shade later*/
  background-color: $white;
  /* font-weight: 700; */
  /* box-shadow: 0px 2px 4px -1px rgba(1,1,1,0.125),
              0px 1px 16px -1px rgba(1,1,1,0.075); */
}
.page_header__search {
  display: flex;
  width: 35%;
  /* height: 35px; */
}
.page_header__search__input {
  width: 100%;
  /* height: 35px; */
  padding: 7px;
  border: 1px solid $grey;
  border-right: none;
  border-radius: $radius 0 0 $radius;
  box-sizing: border-box;

}
.page_header__search__btn {
  border-radius: 0 $radius $radius 0;
  background-color: $green;
}
.page_header__search__advanced-btn {
  /* width: 100%; */
  margin-left: 7px;

}
.adv-search {
  height: 35px;
  background-color: #FFFFFF;
  border-bottom: 1px solid #DDDDDD;
  transition: height 0.5s;
}
.adv-search.extended {
}
.adv-search.collapsed {
  height: 0;
}
.filters {
  display: flex;
  flex-direction: row;
  // border: 1px solid $grey;
  // background-color: $white;
}
.filters__tab {
  background-color: $offwhite;
  box-sizing: border-box;
  color: black;
  font-weight: bold;
  &:hover {
    cursor: pointer;
    background-color: darken($offwhite, 5%);
  }
}
.pagination {
  display: flex;
  flex-direction: row;
  align-items: baseline;
  & .pagination__showing {
    margin: 0 25px;
  }
  & .pagination__btns button {
    background-color: $offwhite;
    color: $dark-grey;
    &:hover {
      background-color: darken($offwhite, 5%);
    }
  }
}
</style>
