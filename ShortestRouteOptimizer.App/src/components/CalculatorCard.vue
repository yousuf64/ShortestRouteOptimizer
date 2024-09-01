<script setup lang="ts">
import DropDown from '@/shared/DropDown/DropDown.vue'
import DropDownItem from '@/shared/DropDown/DropDownItem.vue'
import Art from '@/assets/art.svg?component'
import Calculator from '@/assets/calculator.svg?component'
import Loader from '@/assets/loader.svg?component'
import { onMounted, ref } from 'vue'
import { calculateShortestPath, fetchNodes } from '@/services/api-service'
import type { CalculatorResultModel } from '@/types'

const nodes = ref<string[]>([])
const fromNode = ref<string | null>(null)
const toNode = ref<string | null>(null)
const result = ref<CalculatorResultModel | null>(null)
const calculating = ref(false)

const onCalculate = async (): Promise<void> => {
  if (fromNode.value === null || toNode.value === null) {
    return
  }

  try {
    calculating.value = true
    const data = await calculateShortestPath(fromNode.value, toNode.value)

    result.value = {
      fromNode: fromNode.value,
      toNode: toNode.value,
      distance: data.distance,
      path: data.nodeNames
    }
  } catch {
    alert('Oops... Something went wrong!')
  } finally {
    calculating.value = false
  }
}

const onReset = (): void => {
  fromNode.value = null
  toNode.value = null
  result.value = null
}

onMounted(async () => {
  try {
    nodes.value = await fetchNodes()
  } catch {
    alert('Oops... Something went wrong!')
  }
})
</script>

<template>
  <div class="w-full max-w-full mx-auto bg-white rounded-lg shadow-lg grid grid-cols-2">
    <div class="p-8">
      <form
        class="space-y-6"
        @submit.prevent="onCalculate"
        @reset="onReset"
      >
        <h2 class="text-xl font-semibold text-[#1154A3]">
          Select Path
        </h2>

        <div>
          <label class="block text-gray-700 text-sm font-normal mb-2">From Node</label>
          <DropDown
            v-model="fromNode"
            class="mb-5"
            placeholder="Select node"
          >
            <DropDownItem
              v-for="node in nodes"
              :key="node"
              :value="node"
            >
              {{ node }}
            </DropDownItem>
          </DropDown>
        </div>

        <div>
          <label class="block text-gray-700 text-sm font-normal mb-2">To Node</label>
          <DropDown
            v-model="toNode"
            placeholder="Select node"
          >
            <DropDownItem
              v-for="node in nodes"
              :key="node"
              :value="node"
            >
              {{ node }}
            </DropDownItem>
          </DropDown>
        </div>

        <div class="flex gap-2.5 items-center">
          <button
            type="reset"
            class="px-4 py-3 bg-white border border-[#DA753C] text-[#DA753C] rounded-lg hover:bg-orange-100 leading-5 transition"
          >
            Clear
          </button>
          <button
            type="submit"
            class="inline-flex items-center gap-4 px-4 py-3 border border-[#DA753C] bg-[#DA753C] leading-5 text-white rounded-lg hover:bg-[#f18343] transition disabled:bg-[#c9672e] disabled:border-[#c9672e]"
            :disabled="fromNode === null || toNode === null || calculating"
          >
            Calculate
            <Loader v-if="calculating" />
            <Calculator v-if="!calculating" />
          </button>
        </div>
      </form>
    </div>

    <div
      v-if="result === null"
      class="flex justify-center items-center"
    >
      <Art />
    </div>

    <div
      v-else
      class="bg-gray-100 p-8 flex flex-col rounded-tr-lg rounded-br-lg"
    >
      <h2 class="font-semibold text-[#1154A3] mb-3">
        Result
      </h2>
      <div class="bg-white w-full h-full rounded-lg p-6 text-sm leading-tight">
        <div class="mb-5">
          From Node Name = "{{ result.fromNode
          }}", To Node Name = "{{ result.toNode }}": {{ result.path.join(', ') }}
        </div>
        <div>
          Total Distance: {{ result.distance }}
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped>

</style>