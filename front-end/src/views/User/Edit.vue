<template>
  <UserForm
    v-on:save="save"
    v-bind:action="$resources.es.action.update"
    :createPass="false"
  />
</template>

<script>
import UserForm from "@/components/form/User.vue";
export default {
  name: "EditUser",
  components: {
    UserForm,
  },
  methods: {
    save(data) {
      this.$store.commit("setLoading", true);
      this.$store.dispatch("auth/update", data).then(
        () => {
          this.$store.commit("setLoading", false);
          this.$router.push({ name: "list" });
        },
        () => {
          this.$store.commit("setLoading", false);
        }
      );
    },
  },
};
</script>