<template>
  <div class="page">
    <div class="page__heading">
      <div class="page__heading__title">
        Workorders
      </div>
      <div class="page__heading__search">
        <input class="page__heading__search__input" type="text" placeholder="Search...">
        <button class="page__heading__search__btn">
          <FaIcon icon="search"/>
        </button>
        <button class="page__heading__search__advanced-btn" @click="this.toggleAdvSearch">
          <FaIcon class="btn-icon" icon="chevron-down"/>
        </button>
      </div>
    </div>
    <div :class="['adv-search', advSearchCollapsed ? 'collapsed' : 'extended']">
    </div>
    <div class="page__tabs">
      <div>All</div>
    </div>
    <div>
      <ul>
        <li v-for="workorder in workorders" :key="workorder.id">
          ID: {{workorder.id}} <br/>
          Client: {{workorder.clientName}} <br/>
          Client Email: {{workorder.clientEmail}} <br/>
        </li>
      </ul>
    </div>
  </div>
</template>

<script>
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

<style scoped>
.page__heading {
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  padding: 21px 28px;
  border-bottom: 1px solid #DDDDDD; /* Maybe reconsider shade later*/
  background-color: #FFFFFF;
  /* font-weight: 700; */
  /* box-shadow: 0px 2px 4px -1px rgba(1,1,1,0.125),
              0px 1px 16px -1px rgba(1,1,1,0.075); */
}
.page__heading__title {
  /* padding: 21px 28px; */
  font-size: 3.5rem;
}
.page__heading__search {
  display: flex;
  width: 35%;
  /* height: 35px; */
}
.page__heading__search__input {
  width: 100%;
  /* height: 35px; */
  padding: 7px;
  border: 1px solid #CCCCCC;
  border-right: none;
  border-radius: 7px 0 0 7px;
  box-sizing: border-box;

}
.page__heading__search__btn {
  border-radius: 0 7px 7px 0;
  background-color: #498754;
}
.page__heading__search__advanced-btn {
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
.page__tabs {
  height: 35px;
  background-color: #FFFFFF;
  border-bottom: 1px solid #DDDDDD;
}
</style>
