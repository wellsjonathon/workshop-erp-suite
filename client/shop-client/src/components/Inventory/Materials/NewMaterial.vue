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
  name: 'NewMaterial',
  data: function () {
    return {
      breadcrumbs: [
        {
          text: 'Home',
          to: { name: 'home' }
        },
        {
          text: 'Materials',
          to: { name: 'materials' }
        },
        {
          text: 'New',
          to: { name: 'new_material' }
        }
      ]
    }
  }
}
</script>

<style lang="scss" scoped>
@import "../../../styles/variables.scss";

</style>

