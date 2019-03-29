<template>
  <div class="notifications d-flex flex-column align-items-center">
    <div class="notification">WO-2019-1-006<br/>New workorder received</div>
    <div class="notification">WO-2019-1-002<br/>New comment from Sandra</div>
  </div>
</template>

<script>
export default {
  name: 'NotificationDrawer'
}
</script>

<style lang="scss" scoped>
@import "../../../styles/variables.scss";

.notifications {
  width: 100%;
  padding:12px 6px;
  font-size: 1.4rem;
  & .notification {
    width: 100%;
    height: $width-collapsed;
    padding: 6px;
    margin: 3px;
    color: $primary;
    background-color: $offwhite;
    border: 1px solid rgba(0,0,0,0.125);
    border-radius: 2.5px;
  }
}
</style>
