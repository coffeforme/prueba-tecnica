<template>
  <div>
    <v-btn color="primary" tile block :to="{ name: 'new' }" v-if="create">{{
      $resources.es.user.new
    }}</v-btn>
    <v-text-field
      class="mx-2"
      v-model="searchText"
      label="Buscar"
    ></v-text-field>
    <v-data-table
      :headers="headers"
      :items="$store.state.auth.userlist"
      class="elevation-1"
      :search="searchText"
      :loading="$store.state.auth.loading"
      item-key="id"
    >
      <template v-slot:item.actions="{ item }">
        <v-tooltip bottom v-if="edit">
          <template #activator="{ on }">
            <v-icon small class="mr-2" @click="editItem(item)" v-on="on"
              >fas fa-pencil-alt</v-icon
            >
          </template>
          <span>Editar</span>
        </v-tooltip>
        <v-tooltip bottom v-if="del">
          <template #activator="{ on }">
            <v-icon small @click="deleteItem(item)" v-on="on"
              >fas fa-user-minus</v-icon
            >
          </template>
          <span>Eliminar</span>
        </v-tooltip>
      </template>
    </v-data-table>
  </div>
</template>
<script>
import Pusher from "pusher-js";
import { ENVIROMENT } from "@/config";
export default {
  name: "List",
  data: function () {
    return {
      headers: [
        {
          text: this.$resources.es.common.user,
          align: "start",
          value: "fullname",
        },
        { text: this.$resources.es.common.cel, value: "cel" },
        { text: this.$resources.es.common.address, value: "address" },
        { text: this.$resources.es.common.email, value: "email" },
        { text: this.$resources.es.common.birth, value: "date" },
        { text: this.$resources.es.common.rol, value: "role" },
        {
          text: this.$resources.es.common.actions,
          value: "actions",
          sortable: false,
        },
      ],
      expanded: [],
      searchText: "",
    };
  },

  created() {
    this.fill();
    if (this.$store.state.actions.length == 0)
      this.$router.push({ name: "home" });
  },
  computed: {
    create() {
      return (
        this.$store.state.actions.filter((f) => f.id_action == 1).length > 0
      );
    },
    edit() {
      return (
        this.$store.state.actions.filter((f) => f.id_action == 2).length > 0
      );
    },
    del() {
      return (
        this.$store.state.actions.filter((f) => f.id_action == 3).length > 0
      );
    },
  },
  methods: {
    fill() {
      this.$store.commit("setLoading", true);
      this.$store.dispatch("auth/getAll").then(
        () => {
          this.$store.commit("setLoading", false);
        },
        () => {
          this.$store.commit("setLoading", false);
        }
      );
    },
    editItem(item) {
      item.date = item.date.substring(0, 10);
      this.$store.commit("auth/setUser", item);
      this.$router.push({ name: "edit" });
    },
    addClient(item) {
      this.$router.push({ name: "assignment" });
    },
  },
};
</script>