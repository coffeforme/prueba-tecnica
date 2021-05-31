<template>
  <div class="pa-4">
    <v-row>
      <v-col cols="6" class="pa-2">
        <v-card>
          <v-card-title
            >Bienvenido {{ $store.state.profile.fullname }}</v-card-title
          >
          <v-card-subtitle>{{ $store.state.profile.role }}</v-card-subtitle>
          <v-card-text>
            <p>
              <strong>{{ $resources.es.common.cel }}</strong>
              {{ $store.state.profile.cel }}
            </p>
            <p>
              <strong>{{ $resources.es.common.email }}</strong>
              {{ $store.state.profile.email }}
            </p>
            <p>
              <strong>{{ $resources.es.common.address }}</strong>
              {{ $store.state.profile.address }}
            </p>
          </v-card-text>
        </v-card>
      </v-col>
      <v-col cols="6" class="pa-2" id="scrolling">
        <v-row dense>
          <v-col v-for="card in cards" :key="card.title" :cols="card.flex">
            <v-card>
              <v-card-subtitle>{{ card.date }}</v-card-subtitle>
              <v-card-text>
                <v-row>
                  <v-col cols md="6">Subtotal:</v-col>
                  <v-col cols md="6">{{ card.subtotal }} </v-col>
                  <v-col cols md="6">Impuestos:</v-col>
                  <v-col cols md="6">{{ card.tax }} </v-col>
                  <v-col cols md="6">Total:</v-col>
                  <v-col cols md="6">{{ card.total }} </v-col>
                </v-row>
              </v-card-text>
              <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn> Ver detalles <v-icon small>fas fa-list</v-icon> </v-btn>
              </v-card-actions>
            </v-card>
          </v-col>
        </v-row>
      </v-col>
    </v-row>
  </div>
</template>

<script>
export default {
  name: "Home",
  data: function () {
    return {
      VUE_APP_LOGO: process.env.VUE_APP_LOGO,
      cards: [
        {
          title: "Factura 1",
          flex: 12,
          date: new Date("2021-01-03"),
          subtotal: 1000,
          taxpercent: 16,
          tax: 160,
          total: 1160,
        },
        {
          title: "Factura 2",
          flex: 12,
          date: new Date("2021-02-03"),
          subtotal: 1000,
          taxpercent: 16,
          tax: 160,
          total: 1160,
        },
        {
          title: "Factura 3",
          flex: 12,
          date: new Date("2021-03-03"),
          subtotal: 1000,
          taxpercent: 16,
          tax: 160,
          total: 1160,
        },
      ],
    };
  },
  created() {
    this.$store.dispatch("pagerole/getAll").then(
      () => {},
      () => {}
    );
  },
};
</script>
