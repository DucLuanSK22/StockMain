<template>
    <Doughnut :data="data" :options="options" />
</template>

<script lang="ts">
import { Chart as ChartJS, ArcElement, Tooltip, Legend } from 'chart.js'
import { Doughnut } from 'vue-chartjs'
import * as chartConfig from './chartConfig.ts'

ChartJS.register(ArcElement, Tooltip, Legend)

export default {
    name: 'Doughnut',
    components: {
        Doughnut
    },
    data() {
        return chartConfig
    }
}
</script>
