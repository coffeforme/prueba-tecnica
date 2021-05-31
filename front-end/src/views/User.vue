<template>
  <v-card :loading="$store.state.loading">
    <v-card-text class="ma-0 pa-0">
      <router-view />
    </v-card-text>
  </v-card>
</template>

<script>
export default {
  name: "User",
};
</script>