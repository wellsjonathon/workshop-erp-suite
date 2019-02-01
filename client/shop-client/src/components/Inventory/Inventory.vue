<template>
  <div class="page">
    <div class="page__heading">
      <div class="page__heading__title">
        Inventory
      </div>
    </div>
    <div class="content-area">
      <!-- 
      <ul>
        <li v-for="material in materials" :key="material.id">
          ID: {{material.id}} <br/>
          Name: {{material.name}} <br/>
          Category: {{material.MaterialCategory}} <br/>
          Type: {{material.MaterialType}} <br/>
          Quantity in Stock: {{material.quantityInStock}} <br/>
          Details: {{material.details}}
        </li>
      </ul>
      -->
    <!-- Code to be removed later --> 
      <table>
        <tr>
          <th>ID</th>
          <th>Name</th>
          <th>Category </th> 
          <th>Type</th>
          <th>Quantity In Stock </th>
          <th>Details</th>
        </tr>
        <tbody>
          </tbody>
        <tr v-for="material in materials" :key="material.id">
          <!-- <td>{{material.id}}</td> -->
          <td>{{material.id}}</td>
          <td>{{material.name}}</td>
          <td>{{material.MaterialCategory}}</td>
          <td>{{material.MaterialType}}</td>
          <td>{{material.quantityInStock}}</td>
          <td>{{material.details}}</td>
        </tr>
      </table>
    </div>
  </div>

</template>

<script>
export default {
  name: "Inventory",
  data: function() {
    return {
      materials: null
    }
  },
  mounted: function () {
    this.$http
      .get('https://localhost:5001/api/Materials')
      .then(response => {
        console.log(response.data)
        this.materials = response.data;
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
/* Code to be removed later */
.content-area {
  padding: 14px;
}

table {
  width: 100%;
  margin: 25px auto;
  border-collapse: collapse;
  border: 1px solid #eee;
  border-bottom: 2px solid #425B72;
}
tr, td {
  font-size: 1.6rem;
  color: #560;
}
tr:hover {
    background: #f4f4f4;
}
th, td {
  color: #999;
  border: 1px solid #eee;
  padding: 12px 35px;
  border-collapse: collapse;
}
th {
  background: #425B71;
  color: #fff;
  text-transform: uppercase;
  font-size: 12px;
}

</style>
