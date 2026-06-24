<script setup lang="ts">
import { ref, onMounted, onBeforeUnmount } from 'vue'
import { Button } from '@/components/ui/button'
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card'
import { Input } from '@/components/ui/input'
import * as signalR from '@microsoft/signalr'
import { useRouter } from 'vue-router'
import { useAuth } from '@/ApiServices/useAuth'

const router = useRouter()
const { logout } = useAuth()

const AUTH_API = 'http://localhost:5212'
const TOKEN_KEY = 'auth_token'
const USERNAME_KEY = 'auth_username'

type ChatMessage = {
  id: number
  author: string
  text: string
}

const username = ref(localStorage.getItem(USERNAME_KEY))
const draft = ref('')

const messages = ref<ChatMessage[]>([
  {
    id: Date.now(),
    author: 'Система',
    text: 'Добро пожаловать в чат',
  },
])

const token = localStorage.getItem(TOKEN_KEY)

const client = new signalR.HubConnectionBuilder()
  .withUrl(`${AUTH_API}/chathub`, {
    accessTokenFactory: () => token || '',
  })
  .withAutomaticReconnect()
  .build()

onMounted(async () => {
  try {
    await client.start()
    console.log('SignalR connected')
  } catch (err) {
    console.error(err)
  }
})

onBeforeUnmount(async () => {
  await client.stop()
})

client.on('ReceiveMessage', (user, message) => {
  messages.value.push({
    id: Date.now(),
    author: user,
    text: message,
  })
})

async function sendMessage() {
  if (!draft.value.trim()) return

  try {
    await client.invoke('Send', username.value, draft.value)
    draft.value = ''
  } catch (err) {
    console.error(err)
  }
}

function handleLogout() {
  logout()
  router.push('/login')
}
</script>

<template>
    <Card class="h-full w-[90vh]">
      <CardHeader class="border-b">
        <div class="flex items-center justify-between">
          <CardTitle>Общий чат</CardTitle>

          <Button
            variant="outline"
            @click="handleLogout"
          >
            Выйти
          </Button>
        </div>
      </CardHeader>

      <CardContent class="flex h-[70vh] flex-col ">
        <div class="flex-1 overflow-y-auto ">
          <div
            v-for="message in messages"
            :key="message.id"
            class="border-b py-3"
          >
            <div class="text-muted-foreground">
              {{ message.author }}
            </div>

            <div class="text-sm">
              {{ message.text }}
            </div>
          </div>
        </div>

          <form
            class="flex gap-3 mt-6"
            @submit.prevent="sendMessage"
          >
            <Input
              v-model="draft"
              class="flex-1"
              placeholder="Введите сообщение..."
            />

            <Button type="submit">
              Отправить
            </Button>
          </form>
      </CardContent>
    </Card>
</template>