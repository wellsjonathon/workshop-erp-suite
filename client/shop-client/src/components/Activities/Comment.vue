<template>
   <div class="py-2 d-flex flex-row">
    <div class="comment-icon mr-4 flex-shrink-0">
      {{ getInitials(comment.username) }}
    </div>
    <div class="comment-content d-flex flex-column flex-grow-1">
      <div class="comment-header d-flex flex-row">
        <h4 class="mr-2">{{ comment.username }}</h4><h4 class="comment-timestamp">@ {{ formatTimestamp(comment.timestamp) }}</h4>
      </div>
      <div class="comment-body">
        <span>{{ comment.comment }}</span>
      </div>
    </div>
  </div>
</template>

<script>
import dayjs from 'dayjs'

export default {
  name: "Comment",
  props: {
    comment: {
      type: Object,
      required: true
    }
  },
  data: function () {
    return {
      // comment: null
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
    },
    formatTimestamp(timestamp) {
      return dayjs(timestamp).format('h:mm a, dddd MMM DD, YYYY');
    }
  }
}
</script>

<style lang="scss">
@import "../../styles/variables.scss";

.comment-icon {
  width: $width-collapsed;
  height: $width-collapsed;
  line-height: $width-collapsed;
  font-size: 2.5rem;
  text-align: center;
  border-radius: $width-collapsed / 2;
  background-color: $primary-lighter;
  color: $offwhite;
  box-shadow: 0px 0px 4px 1px rgba(0,0,0,0.15) inset;
}
.comment-header {
  vertical-align: bottom;
}
.comment-timestamp {
  font-weight: 400;
  color: $gray-600;
}
.comment-body {
  font-size: 1.25rem;
}
</style>
