<template>
  <div>
    <v-alert
      v-for="(msg,ix) in messages"
      :key="ix"
      v-model="msg.show"
      border="left"
      close-text="Close Alert"
      :color="msg.status"
      dark
      dismissible
      elevation="15"
      transition="slide-y-reverse-transition"
      tile
      outlined
      text
      dense
    >{{msg.msg}}</v-alert>
  </div>
</template>

<script>
export default {
  name: "ErrorMessage",
  computed: {
    messages() {
      return this.$store.state.messages;
    }
  }
};
</script>