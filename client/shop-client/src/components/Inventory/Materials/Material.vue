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
  name: 'Material',
  props: {
    materialId: {
      type: Number,
      required: true
    }
  },
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
          text: this.materialId,
          to: { name: 'material_by_id', params: { materialId: this.maetrialId } }
        }
      ]
    }
  }
}
</script>

<style lang="scss" scoped>
@import "../../../styles/variables.scss";

</style>

