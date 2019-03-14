<template>
  <b-container>
    <b-row>
      <b-col>
        <b-breadcrumb :items="breadcrumbs" />
      </b-col>
    </b-row>
  </b-container>
</template>

<script>
export default {
  name: "NewWorkorder",
  data: function () {
    return {
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
          text: 'New Workorder',
          to: { name: 'new_workorder' }
        }
      ]
    }
  }
}
</script>

<style lang="sass" scoped>
@import "../../styles/variables.scss";

</style>
