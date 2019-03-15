<template>
  <b-container>
    <b-row>
      <b-col>
        <b-breadcrumb :items="breadcrumbs" />
      </b-col>
    </b-row>
    <b-row class="ml-3">
      <h2>New Workorder</h2>
    </b-row>
    <b-card>
      <b-form>
        <b-col cols="7">
          <b-form-group
            id="titleGroup"
            label="Title:"
            label-for="titleInput">
            <b-form-input
              id="titleInput"
              type="text"
              v-model="workorder.title"
              required
              placeholder="Short workorder title"/>
          </b-form-group>
          <b-form-group
            id="descriptionGroup"
            label="Description:"
            label-for="descriptionInput">
            <b-form-textarea
              id="descriptionInput"
              type="text"
              v-model="workorder.description"
              required
              placeholder="Describe the project requirements"/>
          </b-form-group>
        </b-col>
        <b-col cols="5">

        </b-col>
      </b-form>
    </b-card>
  </b-container>
</template>

<script>
export default {
  name: "NewWorkorder",
  data: function () {
    return {
      workorder: {
        title: '',
        description: ''
      },
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
          text: 'New',
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
