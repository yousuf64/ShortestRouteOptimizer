<script setup lang="ts" generic="T">
import { useSlots, inject, onMounted } from 'vue'

const props = defineProps<{
  value: T
}>()

const slots = useSlots()
const callback = inject<(value: T, text: string) => void>('selected')
const defaultValue = inject('default-value')

const selected = (): void => {
  const text = typeof slots.default !== 'undefined' ? (slots.default()[0].children as string).trim() : ''
  callback?.(props.value, text)
}

onMounted(() => {
  if (defaultValue === props.value) {
    const text = typeof slots.default !== 'undefined' ? (slots.default()[0].children as string).trim() : ''
    callback?.(props.value, text)
  }
})

</script>

<template>
  <button
    type="button"
    class="block w-full text-left px-4 py-2 hover:bg-gray-100"
    @click="selected"
  >
    <slot />
  </button>
</template>