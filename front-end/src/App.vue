<template>
  <v-app :style="{background: $vuetify.theme.themes[theme].background}" scroll-target="#scrolling">
    <v-app-bar app absolute clipped-left :color="$vuetify.theme.themes[theme].background" dense>
      <v-toolbar-title>{{APP_NAME}} - {{$route.meta.display}}</v-toolbar-title>
      <v-spacer></v-spacer>
      <v-btn
        v-if="loggedIn"
        v-for="page in $store.state.menu"
        :key="page.id"
        link
        :to="{name:page.route}"
        @click="handlePageActions(page)"
        icon
      >
        <v-icon>{{page.icon}}</v-icon>
      </v-btn>
      <v-menu v-if="loggedIn" bottom>
        <template v-slot:activator="{ on, attrs }">
          <v-btn icon v-bind="attrs" v-on="on">
            <v-icon>fas fa-ellipsis-v</v-icon>
          </v-btn>
        </template>

        <v-list>
          <v-list-item
            v-for="page in $store.state.menu"
            :key="page.id"
            link
            :to="{name:page.route}"
            @click="handlePageActions(page)"
          >
            <v-list-item-icon>
              <v-icon>{{ page.icon }}</v-icon>
            </v-list-item-icon>
            <v-list-item-title>{{ page.title }}</v-list-item-title>
          </v-list-item>
          <v-divider></v-divider>
          <v-list-item link @click="$store.dispatch('auth/logout')">
            <v-list-item-icon>
              <v-icon>fas fa-door-open</v-icon>
            </v-list-item-icon>
            <v-list-item-content>
              <v-list-item-title>Cerrar sesión</v-list-item-title>
            </v-list-item-content>
          </v-list-item>
        </v-list>
      </v-menu>
    </v-app-bar>

    <v-main>
      <v-container fill-height v-if="!loggedIn">
        <v-row align="start" justify="center">
          <v-col cols="12" md="12" lg="12" xl="12">
            <ErrorMessage />
            <router-view />
          </v-col>
        </v-row>
      </v-container>

      <v-main v-if="loggedIn" fluid class="pa-0 ma-0">
        <v-sheet class="overflow-y-auto" :height="$vuetify.breakpoint.height-80">
          <ErrorMessage />
          <router-view />
        </v-sheet>
      </v-main>
    </v-main>
  </v-app>
</template>

<script>
import ErrorMessage from "@/components/ErrorMessage.vue";
import { mapState } from "vuex";
import { ENVIROMENT } from "@/config";
export default {
  name: "App",
  data: () => ({
    APP_NAME: ENVIROMENT.APP_NAME,
  }),
  methods: {
    handlePageActions(event) {
      this.$store.commit("setActions", JSON.parse(event.actions));
    },
  },
  computed: {
    theme() {
      return this.$vuetify.theme.dark ? "dark" : "light";
    },
    loggedIn() {
      return this.$store.state.auth.status.loggedIn;
    },
  },
  components: {
    ErrorMessage,
  },
};
</script>
<style>
.no-margin {
  margin: 0;
  padding: 0;
}
</style>