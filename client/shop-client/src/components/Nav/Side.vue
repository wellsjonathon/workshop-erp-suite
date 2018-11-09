<template>
  <div class="nav__side">
    <div id="nav__side__toggle">
      +
    </div>
  </div>
</template>

<script>
export default {
  name: 'NavSide'
}
</script>

<style scoped>
.nav__side {
  width: 100%;
  background-color: #F6F6F6;
  box-shadow: 2px 2px 4px rgba(1,1,1,0.1),
              1px 1px 16px rgba(1,1,1,0.075);
}
#nav__side__toggle {
  width: 24px;
  height: 24px;
  position: absolute;
  right: 0;
  top: 45%;
  border-radius: 12px;
  box-shadow: 0px 0px 4px rgba(1, 1, 1, 0.2);
}
</style>

