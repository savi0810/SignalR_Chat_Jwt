<script setup lang="ts">
import { useAuth } from '@/ApiServices/useAuth'
import { ref } from 'vue'
import { Button } from '@/components/ui/button'
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card'
import { Input } from '@/components/ui/input'
import { Label } from '@/components/ui/label'
import { useRouter } from 'vue-router'

const router = useRouter()

const { register: registerUser } = useAuth()

const username = ref('')
const email = ref('')
const password = ref('')
async function handleRegister() {
  try {
    if (!username.value || !email.value || !password.value) {
      throw new Error('Пожалуйста, заполните все поля.')
    }
    await registerUser(username.value, email.value, password.value)

    // Симуляция успешной регистрации
    console.log('Регистрация прошла успешно! Теперь вы можете войти в систему.')
    router.push('/login')
  } catch (err) {
    console.error(err instanceof Error ? err.message : 'Неизвестная ошибка')
  }
}
</script>

<template>
  <Card class="w-full max-w-sm">
    <CardHeader>
      <CardTitle class="text-xl">Регистрация</CardTitle>
    </CardHeader>

    <CardContent>
      <form class="space-y-4" @submit.prevent="handleRegister">
        <div class="space-y-1.5">
          <Label for="reg-username">Имя пользователя</Label>
          <Input id="reg-username" v-model="username" type="text" placeholder="username" autocomplete="username" />
        </div>

        <div class="space-y-1.5">
          <Label for="reg-email">Электронная почта</Label>
          <Input id="reg-email" v-model="email" type="email" placeholder="mail@example.com" autocomplete="email" />
        </div>

        <div class="space-y-1.5">
          <Label for="reg-password">Пароль</Label>
          <Input id="reg-password" v-model="password" type="password" placeholder="••••••••" autocomplete="new-password" />
        </div>

        <Button class="w-full" type="submit">Создать аккаунт</Button>
      </form>

      <p class="mt-4 text-center text-sm text-muted-foreground">
        Уже есть аккаунт?
        <button class="font-medium text-primary underline-offset-4 hover:underline" @click="router.push('/login')">
          Войти
        </button>
      </p>
    </CardContent>
  </Card>
</template>
