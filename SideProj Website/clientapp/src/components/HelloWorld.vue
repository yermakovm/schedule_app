<template>
    <div class="hello">
        <h1>Kek</h1>
        <button @click="callSecureApi">Call Secure API</button>
        <div v-for="(item,index) in services" :key="index">
            <p>
                {{ item }}
            </p>
        </div>
    </div>
</template>

<script>
    import api from "../services/api";

    export default {
        name: 'HelloWorld',
        props: {
            msg: String
        },
        data() {
            return {
                values: ["no data yet"],
                services: []
            }
        },
        methods: {
            callSecureApi: async function () {
                try {
                    const response = await api.get("weatherforecast");
                    this.services = response.data;
                } catch (err) {
                    console.log(err);
                }
            }
        }
    }
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
    h3 {
        margin: 40px 0 0;
    }

    ul {
        list-style-type: none;
        padding: 0;
    }

    li {
        display: inline-block;
        margin: 0 10px;
    }

    a {
        color: #42b983;
    }
</style>
