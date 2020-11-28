import Vue from 'vue'
import VueRouter from 'vue-router'
import HelloWorld from '../components/HelloWorld.vue'
import LoginPage from '../components/LoginPage.vue'
import Callback from '../components/Callback.vue'


Vue.use(VueRouter)

const routes = [
    {
        path: "/hello",
        name: "hello",
        meta: {
            requiresAuth: true
        },
        component: HelloWorld
    },
    {
        path: "/login",
        name: "login",
        component: LoginPage
    },
    {
        path: '/callback',
        name: 'callback',
        component: Callback
    },
]

const router = new VueRouter({
    mode: 'history',
    routes
});

router.beforeEach(async (to, from, next) => {
    let app = router.app.$data || { isAuthenticated: false };
    if (app.isAuthenticated) {
        //already signed in, we can navigate anywhere
        next();
    } else if (to.matched.some(record => record.meta.requiresAuth)) {
        //authentication is required. Trigger the sign in process, including the return URI
        router.app.authenticate(to.path).then(() => {
            console.log('authenticating a protected url:' + to.path);
            next();
        });
    } else {
        //No auth required. We can navigate
        next();
    }
});

export default router
