<template>
  <UserForm
    v-on:save="save"
    v-bind:action="$resources.es.action.create"
    :createPass="true"
  />
</template>

<script>
import UserForm from "@/components/form/User.vue";
export default {
  name: "NewUser",
  components: {
    UserForm,
  },
  created() {
    this.$store.commit("auth/cleanUser");
  },
  methods: {
    save(data) {
      this.$store.commit("setLoading", true);
      this.$store.dispatch("auth/register", data).then(
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