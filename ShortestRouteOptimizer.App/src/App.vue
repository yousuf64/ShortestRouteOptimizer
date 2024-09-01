<script setup lang="ts">

import DropDown from '@/components/DropDown.vue'
import DropDownItem from '@/components/DropDownItem.vue'
import Art from '@/assets/art.svg?component'
import Calculator from '@/assets/calculator.svg?component'
import Loader from '@/assets/loader.svg?component'
import { onMounted, ref } from 'vue'
import { calculateShortestPath, fetchNodes } from '@/services/api-service'

const nodes = ref<string[]>([])
const fromNode = ref<string | null>(null)
const toNode = ref<string | null>(null)
const result = ref<{ fromNode: string, toNode: string, distance: number, path: string[] } | null>(null)
const calculating = ref(false)

const onCalculate = async (): Promise<void> => {
  if (fromNode.value === null || toNode.value === null) {
    return
  }

  calculating.value = true
  const data = await calculateShortestPath(fromNode.value, toNode.value)

  result.value = {
    fromNode: fromNode.value,
    toNode: toNode.value,
    distance: data.distance,
    path: data.nodeNames
  }
  calculating.value = false;
}

const onReset = (): void => {
  fromNode.value = null
  toNode.value = null
  result.value = null
}

onMounted(async () => {
  nodes.value = await fetchNodes()
})

</script>

<template>
  <div
    class="bg-gradient-to-b from-[#1154A3] from-40% via-[#E7F3FF] via-0% to-[#E7F3FF] to-0% flex justify-center min-h-screen"
  >
    <div class="2xl:w-2/5 lg:w-3/5 mt-8 2xl:pt-8 2xl:pb-14 lg:pt-6 lg:pb-12">
      <div class="text-center 2xl:mb-12 lg:mb-4">
        <h1 class="2xl:text-3xl lg:text-2xl font-semibold text-white 2xl:mb-4 lg:mb-2">
          Dijkstra's Algorithm Calculator
        </h1>
        <p class="text-white 2xl:text-base lg:text-sm">
          Discovering Optimal Routes Through Nodes Using Dijkstra's Method
        </p>
      </div>

      <br>

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
    </div>
  </div>
</template>

<style scoped>
</style>