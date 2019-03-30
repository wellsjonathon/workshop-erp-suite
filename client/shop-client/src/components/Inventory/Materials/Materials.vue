<template>
  <b-container fluid>
    <b-row>
      <b-col>
        <b-breadcrumb :items="breadcrumbs" />
      </b-col>
    </b-row>
    <b-row align-h="center">
      <b-col sm="12" lg="10">
        <b-card no-body header-tag="header">
          <b-row slot="header">
            <b-col class="d-flex align-items-center">
              <h2 class="my-0">Materials</h2>
            </b-col>
            <b-col class="d-flex justify-content-end">
              <b-button-toolbar>
                <b-button size="lg" variant="primary">
                  <FaIcon icon="plus" class="mr-3"/>
                  <router-link :to="{ name: 'new_material' }">New material</router-link>
                </b-button>
              </b-button-toolbar>
            </b-col>
          </b-row>
          <b-card-body>
            <b-row class="mb-3">
              <b-col cols="8">
                <b-button-toolbar>
                  <b-input-group size="lg">
                    <b-form-select
                      v-model="selectedFilters.type"
                      :options="filters.types">
                      <option slot="first" :value="null">Type</option>
                    </b-form-select>
                    <b-form-select
                      v-model="selectedFilters.category"
                      :options="filters.categories">
                      <option slot="first" :value="null">Category</option>
                    </b-form-select>
                  </b-input-group>
                  <b-button-group size="lg" class="mx-2">
                    <b-button variant="outline-primary">Apply Filters</b-button>
                    <b-button variant="outline-primary">Clear Filters</b-button>
                  </b-button-group>
                </b-button-toolbar>
              </b-col>
              <b-col cols="4">
                <b-button-toolbar class="d-flex justify-content-end">
                  <b-input-group size="lg" class="mr-2 flex-grow-1">
                    <b-form-input
                      placeholder="Search..."
                      v-model="searchQuery" />
                    <b-input-group-append>
                      <b-button variant="primary" @click="search()">
                        <FaIcon icon="search"/>
                      </b-button>
                    </b-input-group-append>
                  </b-input-group>
                  <b-button variant="outline-primary" size="lg" @click="clearSearch()">
                    Clear
                  </b-button>
                </b-button-toolbar>
              </b-col>
            </b-row>
            <b-table striped hover outlined :items="materials" :fields="materialsFields" :primary-key="id">
              <template slot="name" slot-scope="data">
                <router-link :to="{
                  name: 'material_by_id',
                  params: { materialId: data.item.id }}">
                  {{ data.value }}
                </router-link>
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
export default {
  name: 'Materials',
  data: function () {
    return {
      materials: null,
      materialsFields: [
        {
          key: 'name',
          sortable: true
        },
        {
          key: 'details',
          sortable: false
        },
        {
          key: 'type.name',
          label: 'Type',
          sortable: true
        },
        {
          key: 'category.name',
          label: 'Category',
          sortable: true
        },
        {
          key: 'unitOfMeasure.name',
          label: 'Default Unit',
          sortable: false
        },
        {
          key: 'quantityInStock',
          sortable: true
        }
      ],
      filters: {
        types: null,
        categories: null
      },
      selectedFilters: {
        type: null,
        category: null
      },
      limitOptions: [10, 25, 50],
      limit: 10,
      searchQuery: null,
      pagination: {
        current: 1,
        rows: 100,
        rowsPerPage: 10
      },
      breadcrumbs: [
        {
          text: 'Home',
          to: { name: 'home' }
        },
        {
          text: 'Materials',
          to: { name: 'materials' }
        },
      ]
    }
  },
  methods: {
    changeRowCount(count) {
      this.$http
        .get('https://localhost:5001/api/inventory/materials', {
          params: {
            limit: this.limit
          }
        })
        .then(response => {
          this.materials = response.data
        })
    },
    search() {
      this.$http
        .get('https://localhost:5001/api/inventory/materials/search', {
          params: {
            q: this.searchQuery,
            limit: this.limit
          }
        })
        .then(response => {
          this.materials = response.data
        })
    },
    clearSearch() {
      this.searchQuery = null
      this.$http
        .get('https://localhost:5001/api/inventory/materials/filter', {
          params: {
            type: this.selectedFilters.type,
            category: this.selectedFilters.category,
            limit: this.limit
          }
        })
        .then(response => {
          this.materials = response.data
        })
    },
    applyFilters() {
      this.$http
        .get('https://localhost:5001/api/inventory/materials/filter', {
          params: {
            type: this.selectedFilters.type,
            category: this.selectedFilters.category,
            limit: this.limit
          }
        })
        .then(response => {
          this.materials = response.data
        })
    },
    clearFilters() {
      this.$http
        .get('https://localhost:5001/api/inventory/materials', {
          params: {
            limit: this.limit
          }
        })
        .then(response => {
          this.materials = response.data
        })
      this.selectedFilters = {
        type: null,
        category: null
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
        this.$http.get('https://localhost:5001/api/inventory/materials'),
        this.$http.get('https://localhost:5001/api/inventory/materials/types'),
        this.$http.get('https://localhost:5001/api/inventory/materials/categories')
      ])
      .then(this.$http.spread((materials, types, categories) => {
        this.materials = materials.data,
        this.filters.types = this.formatFilters(types.data),
        this.filters.categories = this.formatFilters(categories.data)
      }))
  }
}
</script>

<style lang="scss">
@import "../../../styles/variables.scss";

</style>
