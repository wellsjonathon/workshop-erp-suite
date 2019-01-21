<template>
  <div class="page">
    <div class="page__heading">
      <div class="page__heading__title">
        Workorders
      </div>
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
  data: function() {
    return {
      workorders: null
    }
  },
  mounted: function () {
    this.$http
      .get('https://localhost:5001/api/Workorders')
      .then(response => {
        console.log(response.data)
        this.workorders = response.data;
      })
  }
}
</script>

<style scoped>
.page__heading {
  padding: 21px 28px;
  font-size: 4rem;
  /* font-weight: 700; */
  /* box-shadow: 0px 2px 4px -1px rgba(1,1,1,0.125),
              0px 1px 16px -1px rgba(1,1,1,0.075); */
}
</style>
