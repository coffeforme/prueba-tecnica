<template>
  <v-container>
    <form @submit.prevent="handleSubmit" :lazy-validation="true">
      <v-row>
        <v-col cols="6">
          <v-text-field
            v-model="user.firstname"
            :label="$resources.es.common.name"
            dense
            :error-messages="errorfirstname"
            :disabled="$store.state.loading"
            @input="$v.user.firstname.$touch()"
            @blur="$v.user.firstname.$touch()"
          ></v-text-field>
        </v-col>
        <v-col cols="6">
          <v-text-field
            v-model="user.lastname"
            :label="$resources.es.common.lastname"
            dense
            :error-messages="errorlastname"
            :disabled="$store.state.loading"
            @input="$v.user.lastname.$touch()"
            @blur="$v.user.lastname.$touch()"
          ></v-text-field>
        </v-col>

        <v-col cols="6">
          <v-menu
            v-model="menu2"
            :close-on-content-click="false"
            :nudge-right="40"
            transition="scale-transition"
            offset-y
            min-width="290px"
          >
            <template v-slot:activator="{ on }">
              <v-text-field
                v-model="user.date"
                :label="$resources.es.common.birth"
                :error-messages="errorbirth"
                :disabled="$store.state.loading"
                @input="$v.user.date.$touch()"
                @blur="$v.user.date.$touch()"
                readonly
                v-on="on"
              ></v-text-field>
            </template>
            <v-date-picker
              v-model="user.date"
              @input="menu2 = false"
              show-current
              flat
            ></v-date-picker>
          </v-menu>
        </v-col>
        <v-col cols="6">
          <v-text-field
            v-model="user.cel"
            :label="$resources.es.common.cel"
            dense
            :error-messages="errorcel"
            :disabled="$store.state.loading"
            @input="$v.user.cel.$touch()"
            @blur="$v.user.cel.$touch()"
          ></v-text-field>
        </v-col>
        <v-col cols="6">
          <v-text-field
            v-model.lazy="user.address"
            :label="$resources.es.common.address"
            dense
            :error-messages="erroraddress"
            :disabled="$store.state.loading"
            @input="$v.user.address.$touch()"
            @blur="$v.user.address.$touch()"
          ></v-text-field>
        </v-col>
        <v-col cols="6">
          <v-text-field
            v-model.lazy="user.email"
            :label="$resources.es.common.email"
            dense
            :error-messages="erroremail"
            :disabled="$store.state.loading"
            @input="$v.user.email.$touch()"
            @blur="$v.user.email.$touch()"
          ></v-text-field>
        </v-col>
        <v-col cols="6">
          <v-select
            :items="$store.state.role.rolelist"
            item-text="description"
            item-value="uid"
            :label="$resources.es.common.rol"
            v-model="user.id_rol"
            :error-messages="errorrol"
            :disabled="$store.loading"
            @input="$v.user.id_rol.$touch()"
            @blur="$v.user.id_rol.$touch()"
            dense
          ></v-select>
        </v-col>
        <v-col cols="6">
          <v-text-field
            v-model.lazy="user.username"
            :label="$resources.es.common.username"
            dense
            :error-messages="errorusername"
            :disabled="$store.state.loading"
            @input="$v.user.username.$touch()"
            @blur="$v.user.username.$touch()"
          ></v-text-field>
        </v-col>
        <v-col cols="6" v-if="createPass">
          <v-text-field
            v-model.lazy="user.pass"
            :label="$resources.es.common.password"
            dense
            type="password"
            :error-messages="errorpass"
            :disabled="$store.state.loading"
            @input="$v.user.pass.$touch()"
            @blur="$v.user.pass.$touch()"
          ></v-text-field>
        </v-col>
        <v-col cols="6" v-if="createPass">
          <v-text-field
            v-model.lazy="repeatPass"
            :label="$resources.es.common.repeatPassword"
            dense
            type="password"
            :error-messages="errorrepeatpass"
            :disabled="$store.state.loading"
            @input="$v.repeatPass.$touch()"
            @blur="$v.repeatPass.$touch()"
          ></v-text-field>
        </v-col>
        <v-col cols="12">
          <v-btn
            class="btn"
            block
            dense
            type="submit"
            color="success"
            :elevation="!$v.$invalid ? 10 : 0"
            :disabled="$store.state.loading || $v.$invalid"
            >{{ action }}</v-btn
          >
        </v-col>
        <v-col cols="12">
          <v-btn color="primary" elevation="10" block @click="$router.go(-1)"
            >Volver</v-btn
          >
        </v-col>
      </v-row>
    </form>
  </v-container>
</template>
<script>
import { validationMixin } from "vuelidate";
import {
  required,
  maxLength,
  minLength,
  email,
  sameAs,
  requiredIf,
} from "vuelidate/lib/validators";

import User from "@/models/auth/user";
export default {
  mixins: [validationMixin],
  name: "FormUser",
  props: ["data", "action", "createPass"],
  data() {
    return {
      user: this.$store.state.auth.user ?? new User(),
      menu2: false,
      repeatPass: null,
    };
  },
  created() {
    this.$store.dispatch("role/getAll");
    this.user.date = this.user.date ?? this.$resources.es.currentDate;
  },
  computed: {
    errorfirstname() {
      const errors = [];
      if (!this.$v.user.firstname.$dirty) return errors;
      !this.$v.user.firstname.required &&
        errors.push(this.$resources.es.common.required);
      return errors;
    },
    errorlastname() {
      const errors = [];
      if (!this.$v.user.lastname.$dirty) return errors;
      !this.$v.user.lastname.required &&
        errors.push(this.$resources.es.common.required);
      return errors;
    },
    errorbirth() {
      const errors = [];
      if (!this.$v.user.date.$dirty) return errors;
      !this.$v.user.date.required &&
        errors.push(this.$resources.es.common.required);
      return errors;
    },
    errorusername() {
      const errors = [];
      if (!this.$v.user.username.$dirty) return errors;
      !this.$v.user.username.required &&
        errors.push(this.$resources.es.common.required);
      return errors;
    },
    errorrol() {
      const errors = [];
      if (!this.$v.user.id_rol.$dirty) return errors;
      !this.$v.user.id_rol.required &&
        errors.push(this.$resources.es.common.required);
      return errors;
    },
    errorcel() {
      const errors = [];
      if (!this.$v.user.cel.$dirty) return errors;
      !this.$v.user.cel.required &&
        errors.push(this.$resources.es.common.required);
      return errors;
    },
    erroremail() {
      const errors = [];
      if (!this.$v.user.email.$dirty) return errors;
      !this.$v.user.email.email && errors.push("Email incorrecto");
      !this.$v.user.email.required &&
        errors.push(this.$resources.es.common.required);
      return errors;
    },
    erroraddress() {
      const errors = [];
      if (!this.$v.user.address.$dirty) return errors;
      !this.$v.user.address.required &&
        errors.push(this.$resources.es.common.required);
      return errors;
    },
    errorpass() {
      const errors = [];
      if (!this.$v.user.pass.$dirty) return errors;
      !this.$v.user.pass.required &&
        errors.push(this.$resources.es.common.required);
      !this.$v.user.pass.minLength &&
        errors.push("La contraseña debe tener mínimo 6 caracteres");
      return errors;
    },
    errorrepeatpass() {
      const errors = [];
      if (!this.$v.repeatPass.$dirty) return errors;
      !this.$v.repeatPass.required &&
        errors.push(this.$resources.es.common.required);
      !this.$v.repeatPass.sameAsPass &&
        errors.push("Las contraseñas deben ser iguales");
      return errors;
    },
  },
  methods: {
    handleSubmit() {
      this.$emit("save", this.user);
    },
  },
  validations: {
    user: {
      firstname: { required },
      lastname: { required },
      date: { required },
      cel: { required },
      username: { required },
      email: { required, email },
      address: { required },
      id_rol: { required },
      pass: {
        required: requiredIf(function () {
          return this.createPass;
        }),
        minLength: minLength(6),
      },
    },
    repeatPass: {
      required: requiredIf(function () {
        return this.createPass;
      }),
      sameAsPass: sameAs(function () {
        return this.user.pass;
      }),
    },
  },
};
</script>