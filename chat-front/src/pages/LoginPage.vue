<script setup lang="ts">
import { ref } from 'vue'
import { useAuth } from '@/ApiServices/useAuth'
import { Button } from '@/components/ui/button'
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card'
import { Input } from '@/components/ui/input'
import { Label } from '@/components/ui/label'
import { useRouter } from 'vue-router'

const router = useRouter()

const { login } = useAuth()

const username = ref('')
const password = ref('')


async function handleLogin() {
  try {
    if (!username.value || !password.value) {
      throw new Error('Пожалуйста, заполните все поля.')
    }
    await login(username.value, password.value)

    console.log('Вход выполнен успешно!')
    router.push('/chat')
  }
   catch (err) {
    console.error(err instanceof Error ? err.message : 'Неизвестная ошибка')
  }
}
</script>

<template>
  <Card class="w-full max-w-sm">
    <CardHeader>
      <CardTitle class="text-xl">Вход</CardTitle>
    </CardHeader>

    <CardContent>
      <form class="space-y-4" @submit.prevent="handleLogin">
        <div class="space-y-1.5">
          <Label for="login-username">Имя пользователя</Label>
          <Input id="login-username" v-model="username" type="text" placeholder="username" />
        </div>

        <div class="space-y-1.5">
          <Label for="login-password">Пароль</Label>
          <Input id="login-password" v-model="password" type="password" placeholder="••••••••" />
        </div>

        <Button class="w-full" type="submit">Войти</Button>
      </form>

      <p class="mt-4 text-center text-sm text-muted-foreground">
        Нет аккаунта?
        <button class="font-medium text-primary underline-offset-4 hover:underline" @click="router.push('/register')">
          Зарегистрироваться
        </button>
      </p>
    </CardContent>
  </Card>
</template>
