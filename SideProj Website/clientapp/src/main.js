import Vue from 'vue'
import App from './App.vue'
import router from './router'
import mgr from './services/security.js'
import api from "./services/api";

Vue.config.productionTip = false;

Vue.mixin({
    data: function () {
        return {
            isAuthenticated: false,
            user: '',
            mgr: mgr
        }
    },
    methods: {
        authenticate: async function (returnPath) {
            const user = await this.$root.getUser(); //see if the user details are in local storage
            if (!!user) {
                this.isAuthenticated = true;
                this.user = user;
            } else {
                await this.$root.signIn(returnPath);
            }
        },
        getUser: async function () {
            try {
                let user = await this.mgr.getUser();
                return user;
            } catch (err) {
                console.log(err);
            }
        },
        signIn: function (returnPath) {
            returnPath ? this.mgr.signinRedirect({ state: returnPath })
                : this.mgr.signinRedirect();
        }
    }
});

let v = new Vue({
    router,
    render: h => h(App)
}).$mount('#app')

Vue.config.productionTip = false;
Vue.prototype.$http = api;
api.defaults.timeout = 10000;
api.interceptors.request.use(
    config => {
        const user = v.$root.user;
        if (user) {
            const authToken = user.access_token;
            if (authToken) {
                config.headers.Authorization = `Bearer ${authToken}`;
            }
        }
        return config;
    },
    error => {
        return Promise.reject(error);
    }
);
api.interceptors.response.use(
    response => {
        if (response.status) {
            if (response.status === 200 || response.status === 201) {
                return Promise.resolve(response);
            }
        }
        return Promise.reject(response);
    },
    error => {
        if (error.response.status) {
            switch (error.response.status) {
            case 400:

                //do something
                break;

            case 401:
                alert("session expired");
                break;
            case 403:
                router.replace({
                    path: "/login",
                    query: { redirect: router.currentRoute.fullPath }
                });
                break;
            case 404:
                alert('page not exist');
                break;
            case 502:
                setTimeout(() => {
                    router.replace({
                        path: "/login",
                        query: {
                            redirect: router.currentRoute.fullPath
                        }
                    });
                }, 1000);
            }
            return Promise.reject(error.response);
        }
    }
);
