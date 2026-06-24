import { createRouter, createWebHistory } from 'vue-router'

import LoginPage from '@/pages/LoginPage.vue'
import RegisterPage from '@/pages/RegisterPage.vue'
import ChatPage from '@/pages/ChatPage.vue'

import { useAuth } from '@/ApiServices/useAuth'

const router = createRouter({
  history: createWebHistory(),
  routes: [
    {
      path: '/',
      redirect: '/login',
    },
    {
      path: '/login',
      component: LoginPage,
    },
    {
      path: '/register',
      component: RegisterPage,
    },
    {
      path: '/chat',
      component: ChatPage,
    },
  ],
})

router.beforeEach((to) => {
  const { isAuthenticated } = useAuth()

  if (to.path === '/chat' && !isAuthenticated.value) {
    return '/login'
  }

  if (
    isAuthenticated.value &&
    (to.path === '/login' || to.path === '/register')
  ) {
    return '/chat'
  }
})

export default router