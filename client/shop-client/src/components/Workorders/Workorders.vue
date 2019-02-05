<template>
  <!-- <div class="page">
    <div class="page_header">
      <h1 class="page_header__title">
        Workorders
      </h1>
      <div class="page_header__search">
        <input class="page_header__search__input" type="text" placeholder="Search...">
        <button class="page_header__search__btn">
          <FaIcon icon="search"/>
        </button>
        <button class="page_header__search__advanced-btn" @click="this.toggleAdvSearch">
          <FaIcon class="btn-icon" icon="chevron-down"/>
        </button>
      </div>
    </div>
    <div :class="['adv-search', advSearchCollapsed ? 'collapsed' : 'extended']">
    </div> -->
    <div class="container">
      <div class="container__row">
        <div class="breadcrumbs">
          <ul>
            <li><router-link to="/home">Home</router-link></li>
            <li><router-link to="/workorders">Workorders</router-link></li>
          </ul>
        </div>
      </div>
      <div class="container__row">
        <div class="card">
          <div class="card__row">
            <h1>Workorders</h1>
          </div>
          <div class="card__row">
            <div class="filters floating-tab-group">
              <div class="filters__tab">All</div>
              <div class="filters__tab">New</div>
              <div class="filters__tab">In Progress</div>
              <div class="filters__tab">More</div>
            </div>
            <div class="pagination">
              <span class="pagination__showing">1 - 20 of 1000</span>
              <div class="pagination__btns btn-group">
                <button id="pagination--back"><FaIcon icon="chevron-left"/></button>
                <button id="pagination--forward"><FaIcon icon="chevron-right"/></button>
              </div>
            </div>
          </div>
          <div class="card__row data-table">
            <table>
              <thead>
                <th scope="col">ID</th>
                <th scope="col">Faculty</th>
                <th scope="col">Client</th>
                <th scope="col">Description</th>
                <th scope="col">Date Created</th>
                <th scope="col">Date Requested By</th>
                <th scope="col">Status</th>
                <th scope="col"></th>
              </thead>
              <tbody>
                <tr v-for="workorder in workorders" :key="workorder.id">
                  <td>{{workorder.id}}</td>
                  <td>SSE</td> <!-- placeholder -->
                  <td>{{workorder.clientName}}</td>
                  <td>{{workorder.description}}</td>
                  <td>{{displayDate(workorder.dateCreated)}}</td>
                  <td>{{displayDate(workorder.dateRequiredBy)}}</td>
                  <td class="status-row">
                    <span class="status__indicator"></span>
                    <span class="status__name">{{workorder.status.name}}</span>
                  </td>
                  <td>
                    <router-link
                      :to="{ name: 'workorder_by_id',
                      params: { workorderId: workorder.id }}">
                      View
                    </router-link> |
                    <a href="">Edit</a> |
                    <a href="">Delete</a>
                  </td>
                </tr>
              </tbody>
            </table>
          </div>
        </div>
      </div>
    </div>
  <!-- </div> -->
</template>

<script>
import dayjs from 'dayjs'

export default {
  name: "Workorders",
  data: function () {
    return {
      workorders: null,
      advSearchCollapsed: true
    }
  },
  methods: {
    toggleAdvSearch() {
      this.advSearchCollapsed = !this.advSearchCollapsed
    },
    displayDate(date) {
      return dayjs(date).format('MMMM DD, YYYY');
    }
  },
  mounted: function () {
    this.$http
      .get('https://localhost:5001/api/Workorders')
      .then(response => {
        this.workorders = response.data;
      })
  }
}
</script>

<style lang="scss" scoped>
@import "../../styles/variables.scss";

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
.card__row {
  display: flex;
  margin: 15px;
  flex-direction: row;
  justify-content: space-between;
}
.filters {
  display: flex;
  flex-direction: row;
  // border: 1px solid $grey;
  // background-color: $white;
}
.filters__tab {
  // padding: 7px 14px;
  background-color: $offwhite-med;
  // border: 1px solid $grey;
  box-sizing: border-box;

  // &:first-child {
  //   border-radius: $radius 0 0 $radius;
  // }
  // &:last-child {
  //   border-radius: 0 $radius $radius 0;
  // }
  &:hover {
    cursor: pointer;
    background-color: darken($offwhite-med, 5%);
    // border: 1px solid $grey;
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
    &:hover {
      background-color: darken($offwhite, 5%);
    }
  }
}
.data-table {
  margin: 15px;
  & table {
    width: 100%;
    text-align: left;
    border: 1px solid $grey;
    border-spacing: 0;
    border-radius: $radius;
    // border-collapse: collapse;
  }
  & thead {
    background-color: $offwhite;
    & th {
      border-bottom: 1px solid $grey;
    }
    & th:first-child {
      border-radius: $radius 0 0 0;
    }
    & th:last-child {
      border-radius: 0 $radius 0 0;
    }
  }
  & tr:last-child td {
    &:first-child {
      border-radius: 0 0 0 $radius;
    }
    &:last-child {
      border-radius: 0 0 $radius 0;
    }
  }
  & tr:not(:last-child) {
    border-bottom: 1px solid $grey;
  }
  & td, th {
    padding: 15px 10px;
  }
  & tr:hover {
    // cursor: pointer;
    background-color: darken($offwhite-med, 5%);
  }
  & .status-row {
    display: inline-flex;
    flex-direction: row;
    align-items: baseline;
    & .status__indicator {
      margin: 0 5px 0 0;
      width: 1rem;
      height: 1rem;
      border-radius: 0.5rem;
      background-color: $green;
    }
  }
}
</style>
