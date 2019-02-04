<template>
  <router-link :to="link" :class="['nav__item', !collapsed ? 'extended' : 'collapsed']">
    <FaIcon class="nav__item__icon" :icon="linkIcon"/>
    <div :class="['nav__item__link', !collapsed ? 'extended' : 'collapsed']">
      {{ linkName }}
    </div>
  </router-link>
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
    },
    linkName: {
      type: String,
      required: true
      // other parameters needed?
    },
    linkIcon: {
      type: String,
      required: true
    }
  }
  // computed: {
  //   activeLink: function() {
  //     return this.$router.
  //   }
  // }
}
</script>

<style lang="scss" scoped>
@import "../../styles/variables.scss";

.nav__item {
  display: flex;
  width: 100%;
  margin: 14px 0;
  color: $offwhite;
  transition: 0.2s, border-radius 0.5s;
  &:hover {
    background-color: $primary-lighter;
    cursor: pointer;
  }
  &:active {
    width: calc(100% + 2px);
    transition-duration: 0s;
    background-color: $primary-lightest;
    transform: translate(-2px, 2px);
  }
  &.extended {
    border-radius: 0 4px 4px 0;
  }
}
.nav__item__icon {
  width: 36px;
  height: 36px;
  padding: 12px;
}
.nav__item__link {
  width: $width-extended - $width-collapsed - 20px;
  margin: auto 0;
  overflow: hidden;
  white-space: nowrap;
  transition: width 0.49s;
  &.extended {
    display: block;
  }
  &.collapsed {
    width: 0px;
  }
}
.router-link-active {
  background-color: $offwhite;
  color: $primary;
}
</style>
