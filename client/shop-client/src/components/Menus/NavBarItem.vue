<template>
  <div class="nav__item">
    <FaIcon class="nav__item__icon" :icon="linkIcon"/>
    <div :class="['nav__item__link', !collapsed ? 'extended' : 'collapsed']">
      {{ link }}
    </div>
  </div>
</template>

<script>
export default {
  name: 'NavBarItem',
  props: {
    collapsed: {
      type: Boolean,
      required: true
      // default? Should be value passed from NavBar, may not need default
    },
    link: {
      type: String,
      required: true
      // other parameters needed?
    },
    linkIcon: {
      type: String,
      required: true
    }
  }
}
</script>

<style scoped>
.nav__item {
  display: flex;
  width: 100%;
  margin: 14px 0;
  color: #EFEFF4;
  transition-duration: 0.2s;
}
.nav__item:hover {
  background-color: #607589;
  cursor: pointer;
}
.nav__item:active {
  width: calc(100% + 2px);
  transition-duration: 0s;
  background-color: #8796A4;
  transform: translate(-2px, 2px);
}
.nav__item__icon {
  width: 36px;
  height: 36px;
  padding: 12px;
}
.nav__item__link {
  width: 220px; /* Upgrade to Sass, use extended - collapsed */
  /* height: 60px; */
  margin: auto 0;
  overflow: hidden;
  white-space: nowrap;
  transition-duration: 0.55s;
}
.nav__item__link.extended {
  display: block;
}
.nav__item__link.collapsed {
  width: 0px;
}
</style>
