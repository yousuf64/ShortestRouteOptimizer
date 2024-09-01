<script setup lang="ts">
import CalculatorCard from '@/components/CalculatorCard.vue'
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

      <CalculatorCard />
    </div>
  </div>
</template>

<style scoped>
</style>