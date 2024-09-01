<script setup lang="ts" generic="T">
import { onMounted, provide, ref, toRefs, type UnwrapRef, watch } from 'vue'
import type { DropdownInterface } from 'flowbite'
import { Dropdown, initFlowbite } from 'flowbite'
import DropdownArrow from '@/assets/dropdown-arrow.svg?component'
import { getId } from '@/shared/utils'

const props = defineProps<{
  placeholder: string | null,
  modelValue: T | null
}>()

const emit = defineEmits(['update:modelValue', 'selected'])

const { modelValue } = toRefs(props)
const selectedValue = ref<T | null>(modelValue.value)
const selectedText = ref<string | null>(null)

const buttonId = getId()
const dropdownId = getId()
const placeholder = props.placeholder || 'Select item'

const closeDropdown = (): void => {
  const $targetEl = document.getElementById(dropdownId)
  const $triggerEl = document.getElementById(buttonId)

  const dropdown: DropdownInterface = new Dropdown(
    $targetEl,
    $triggerEl
  )

  dropdown.hide()
}

watch(modelValue, (value) => {
  if (value === null) {
    selectedText.value = null
  }
})

provide('selected', (value: UnwrapRef<T>, text: string) => {
  selectedValue.value = value
  selectedText.value = text
  emit('update:modelValue', value)
  closeDropdown()
})

provide('default-value', selectedValue.value)

onMounted(() => {
  initFlowbite()
})
</script>

<template>
  <div class="relative">
    <button
      :id="buttonId"
      :data-dropdown-toggle="dropdownId"
      class="bg-white border border-gray-300 hover:bg-gray-100 w-full focus:ring-4 focus:outline-none focus:ring-blue-300 font-normal rounded-lg text-sm px-3 py-2.5 text-center inline-flex items-center justify-between"
      type="button"
    >
      <span :class="selectedText === null ? 'text-gray-400' : 'text-gray-900'">
        {{ selectedText ?? placeholder }}
      </span>
      <DropdownArrow />
    </button>

    <div
      :id="dropdownId"
      class="z-10 hidden border border-gray-300 w-full bg-white divide-y divide-gray-100 rounded-lg shadow"
    >
      <ul
        class="py-2 text-sm text-gray-700 dark:text-gray-200"
        :aria-labelledby="buttonId"
      >
        <slot />
      </ul>
    </div>
  </div>
</template>