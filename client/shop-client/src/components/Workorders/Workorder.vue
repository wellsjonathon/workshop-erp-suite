<template>
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
              {{ this.workorderId }}
            </router-link>
          </li>
        </ul>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "Workorder",
  props: {
    workorderId: {
      type: Number,
      required: true
    }
  }
}
</script>

<style lang="scss" scoped>

</style>
