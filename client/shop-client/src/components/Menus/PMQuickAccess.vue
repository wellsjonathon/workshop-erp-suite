<template>
  <div :class="['pmqa', !isCollapsed ? 'extended' : 'collapsed']">
    <FaIcon :class="['pmqa__btn', !isCollapsed ? 'extended' : 'collapsed']"
            @click="this.toggleCollapse"
            icon="chevron-circle-right"/>
  </div>
</template>

<script>
export default {
  name: 'ProjectManagementQuickAccess',
  props: {
    collapsed: {
      type: Boolean,
      default: false
    }
  },
  data: function () {
    return {
      isCollapsed: this.collapsed
    }
  },
  methods: {
    toggleCollapse() {
      this.isCollapsed = !this.isCollapsed
    }
  }
}
</script>

<style scoped>
.pmqa {
  /* width: 300px; */
  transition-duration: 0.5s;
  background-color: #425B72;
  box-shadow: -2px -2px 4px rgba(1,1,1,0.1),
              -1px -1px 16px rgba(1,1,1,0.075);
}
.pmqa.extended {
  width: 300px;
}
.pmqa.collapsed {
  width: 50px;
}
.pmqa__btn {
  position: relative;
  width: 26px;
  height: 26px;
  left: -12px;
  /* left: calc(0% - 12px); */
  top: calc(50% - 13px);
  transition-duration: 0.5s;
  color: #19344B;
  background-color: #42586E;
  border: 2px solid #42586E;
  border-radius: 14px;
}
.pmqa__btn.collapsed {
  transform: rotate(-180deg);
}
.pmqa__btn:hover {
  color: rgba(25, 52, 75, 0.7);
  cursor: pointer;
}
.pmqa__btn:active {
  color: rgba(25, 52, 75, 0.45);
}
</style>

