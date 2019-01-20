<template>
  <div :class="['pmqa', !isCollapsed ? 'extended' : 'collapsed']">
    <FaIcon :class="['pmqa__btn', !isCollapsed ? 'extended' : 'collapsed']"
            @click="this.toggleCollapse"
            icon="chevron-circle-right"/>
  </div>
</template>

<script>
export default {
  name: 'PMQuickAccess',
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
  transition-duration: 0.5s;
  background-color: #425B72;
  box-shadow: -2px 0 4px 1px rgba(1,1,1,0.175),
              -1px 0 16px 1px rgba(1,1,1,0.1);
}
.pmqa.extended {
  width: 300px;
}
.pmqa.collapsed {
  width: 60px;
}
.pmqa__btn {
  position: absolute;
  width: 26px;
  height: 26px;
  left: -12px;
  top: calc(50% - 15px);
  transition: transform 0.5s, color 0.2s;
  color: #19344B;
  background-color: #42586E;
  border: 2px solid #42586E;
  border-radius: 15px;
}
.pmqa__btn.extended {
  filter: drop-shadow(-4px 0 8px rgba(1,1,1,0.175));
}
.pmqa__btn.collapsed {
  transform: rotate(-180deg);
  filter: drop-shadow(4px 0 8px rgba(1,1,1,0.175));
}
.pmqa__btn:hover {
  color: rgba(25, 52, 75, 0.7);
  cursor: pointer;
}
.pmqa__btn:active {
  color: rgba(25, 52, 75, 0.45);
}
</style>

