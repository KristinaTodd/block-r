<template>
  <nav class="navbar navbar-expand-lg navbar-light bg-light">
    <router-link class="navbar-brand logo" :to="{ name: 'home' }"><i class="fas fa-cubes"></i>Blockr</router-link>
    <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarText"
      aria-controls="navbarText" aria-expanded="false" aria-label="Toggle navigation">
      <span class="navbar-toggler-icon"></span>
    </button>
    <div class="collapse navbar-collapse" id="navbarText">
      <ul class="navbar-nav mr-auto secondFont">
        <li class="nav-item" :class="{ active: $route.name == 'home' }">
          <router-link :to="{ name: 'home' }" class="nav-link">Home</router-link>
        </li>
        <li class="nav-item" v-if="$auth.isAuthenticated" :class="{ active: $route.name == 'dashboard' }">
          <router-link class="nav-link" :to="{ name: 'dashboard' }">Dashboard</router-link>
        </li>
      </ul>
      <span class="navbar-text">
        <button class="btn btn-success secondFont" @click="login" v-if="!$auth.isAuthenticated">
          Login
        </button>
        <button class="btn btn-danger secondFont" @click="logout" v-else>logout</button>
      </span>
    </div>
  </nav>
</template>

<script>
  import axios from "axios";

  let _api = axios.create({
    baseURL: "https://localhost:5001",
    withCredentials: true
  });
  export default {
    name: "Navbar",
    methods: {
      async login() {
        await this.$auth.loginWithPopup();
        await this.$auth.getUserData();
        this.$store.dispatch("setBearer", this.$auth.bearer);
        console.log("this.$auth.user: ");
        console.log(this.$auth.user);
      },
      async logout() {
        this.$store.dispatch("resetBearer");
        await this.$auth.logout({ returnTo: window.location.origin });
      }
    }
  };
</script>

<style>
  .logo {
    font-family: 'Bungee', cursive;
  }

  .fas {
    color: teal;
  }

  .secondFont {
    font-family: 'Antic', sans-serif;

  }
</style>