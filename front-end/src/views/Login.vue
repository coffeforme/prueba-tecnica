<template>
  <div>
    <v-card :loading="loading">
      <v-img
        class="white--text align-end"
        height="200px"
        max-height="200px"
        :src="VUE_APP_LOGO"
      >
        <div class="fill-height repeating-gradient"></div>
        <v-card-title>
          <div class="text-center">
            <h1
              class="font-weight-black text--primary black--text"
              style="font-color: #000"
            >
              Iniciar sesión
            </h1>
          </div>
        </v-card-title>
      </v-img>

      <v-card-text>
        <form @submit.prevent="handleLogin" lazy-validation autocomplete="off">
          <v-row>
            <v-col cols="12">
              <v-text-field
                v-model="user.username"
                :label="$resources.es.common.username"
                prepend-icon="fas fa-at"
                :error-messages="errorsusername"
                :disabled="loading"
                @input="$v.user.username.$touch()"
                @blur="$v.user.username.$touch()"
                dense
              ></v-text-field>
              <v-col cols="12"></v-col>
              <v-text-field
                v-model="user.pass"
                label="Contraseña"
                prepend-icon="fas fa-key"
                required
                type="password"
                :error-messages="errorpass"
                :disabled="loading"
                dense
              ></v-text-field>
            </v-col>
            <v-col cols="12" dense>
              <v-btn
                class="btn"
                block
                dense
                type="submit"
                color="success"
                elevation="15"
                :disabled="loading || $v.$invalid"
                >Ingresar</v-btn
              >
            </v-col>
          </v-row>
        </form>
      </v-card-text>
    </v-card>
  </div>
</template>
<script>
import { mapActions } from "vuex";
import { validationMixin } from "vuelidate";
import { required, maxLength } from "vuelidate/lib/validators";
import { ENVIROMENT } from "@/config";
import Login from "@/models/login";
export default {
  mixins: [validationMixin],
  name: "Login",
  data() {
    return {
      user: new Login("", ""),
      loading: false,
      VUE_APP_LOGO: process.env.VUE_APP_LOGO,
      ENV: process.env.NODE_ENV,
    };
  },
  methods: {
    ...mapActions(["login"]),
  },
  computed: {
    loggedIn() {
      return this.$store.state.auth.status.loggedIn;
    },
    errorsusername() {
      const errors = [];
      if (!this.$v.user.username.$dirty) return errors;
      !this.$v.user.username.required && errors.push("Campo requerido");
    },
    errorpass() {
      const errors = [];
      if (!this.$v.user.pass.$dirty) return errors;
      !this.$v.user.pass.required && errors.push("Campo requerido");
      return "";
    },
    messages() {
      return this.$store.state.messages;
    },
  },
  created() {
    if (ENVIROMENT.dev) console.log(`Is logged in : ${this.loggedIn}`);
    if (this.loggedIn) {
      this.$router.push({ name: "home" });
    }
  },
  methods: {
    handleLogin() {
      this.loading = true;
      if (this.user.username && this.user.pass) {
        this.$store.dispatch("auth/login", this.user).then(
          () => {
            if (ENVIROMENT.dev) console.log("Redirecting to home");
            this.$router.push({ name: "home" });
          },
          () => {
            this.loading = false;
          }
        );
      }
    },
  },
  validations: {
    user: { username: { required }, pass: { required } },
  },
};
</script>