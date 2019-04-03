<template>
  <router-link :to="link" :class="['nav__item', !collapsed ? 'extended' : 'collapsed']">
    <div class="nav-icon-container">
      <FaIcon class="icon" :icon="linkIcon" v-if="!isAccount"/>
      <div class="icon account-icon" v-if="isAccount">{{ getInitials(linkName) }}</div>
      <!-- <img src="https://placehold.it/32/efeff4" alt="" class="icon account-icon" v-if="isAccount"> -->
    </div>
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
    },
    isAccount: {
      type: Boolean,
      required: false,
      default: false
    }
  },
  methods: {
    getInitials(name) {
      var names = name.split(' ')
      var initials = names[0].substring(0, 1).toUpperCase()

      if (names.length > 1) {
        initials += names[names.length - 1].substring(0, 1).toUpperCase()
      }
      return initials
    }
  }
}
</script>

<style lang="scss" scoped>
@import "../../styles/variables.scss";

.nav__item {
  display: flex;
  align-items: center;
  width: 100%;
  height: $width-collapsed - 12px;
  margin: 7px 0;
  color: $offwhite;
  transition: 0.2s, border-radius 0.5s;
  &:hover {
    background-color: $primary-lighter;
    cursor: pointer;
  }
  &:active {
    transition-duration: 0s;
    background-color: $primary-lightest;
    transform: translate(0, 2px);
  }
}
.nav-icon-container {
  width: $width-collapsed;
  text-align: center;
  & .account-icon {
    width: $account-icon-size;
    height: $account-icon-size;
    line-height: $account-icon-size;
    padding: 0;
    margin: 0 auto;
    font-size: 1.75rem;
    // font-weight: 700;
    text-align: center;
    border-radius: $account-icon-size / 2;
    background-color: $offwhite;
    color: $primary;
  }
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
  & .account-icon {
    color: $offwhite;
    background-color: $primary;
  }
}
</style>
