// Vue imports
import Vue from 'vue'
import Router from 'vue-router'

// 3rd party imports
import Auth from '@okta/okta-vue'

// our own imports
import Hello from '@/components/Hello'
import WeightRecords from '@/components/WeightRecords'

Vue.use(Auth, {
  issuer: 'https://{oktaDomain}/oauth2/default',
  client_id: '{oktaClientId}',
  redirect_uri: 'http://localhost:8080/implicit/callback',
  scope: 'openid profile email'
})

Vue.use(Router)

let router = new Router({
  mode: 'history',
  routes: [
    {
      path: '/',
      name: 'Hello',
      component: Hello
    },
    {
      path: '/implicit/callback',
      component: Auth.handleCallback()
    },
    {
      path: '/weight-trackings',
      name: 'WeightRecords',
      component: WeightRecords,
      meta: {
        requiresAuth: true
      }
    },

  ]
})

router.beforeEach(Vue.prototype.$auth.authRedirectGuard())

export default router
