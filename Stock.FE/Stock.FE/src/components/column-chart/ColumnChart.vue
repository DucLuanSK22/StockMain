<template>
    <div>
        <apexchart type="bar" height="450" :options="chartOptions" :series="series"></apexchart>
    </div>
</template>

<script>
export default {
    props: {
        dataProps: {
            type: Object,
            default: []
        }
    },

    data() {
        return {
            series: [],
            chartOptions: {
                chart: {
                    type: 'bar',
                    height: 450,
                    stacked: true,
                },
                colors: ['#46d96f', '#f68fff'],
                dataLabels: {
                    enabled: true,
                    formatter: function (val) {
                        return val % 1000000 == 0 ? val / 1000000 + "M" : (val / 1000000).toFixed(2) + "M";
                    },
                    style: {
                        fontSize: '12px',
                        colors: ["white"]
                    }
                },
                responsive: [
                    {
                        breakpoint: 480,
                        options: {
                            legend: {
                                position: 'bottom',
                                offsetX: -10,
                                offsetY: 0
                            }
                        }
                    }
                ],
                xaxis: {
                    categories: [],
                    labels: {
                        style: {
                            colors: ['#fff'],
                            fontSize: '14px',
                            fontWeight: 600
                        }
                    }
                },
                yaxis: {
                    labels: {
                        formatter: (value) => `${value / 1_000_000} M`,
                        style: {
                            colors: ['#85868a'], // Đổi màu chữ nhãn trục Y
                            fontSize: '14px', // Thay đổi kích thước font chữ
                            fontWeight: 600 // Độ dày của font chữ
                        }
                    }
                },
                fill: {
                    opacity: 1
                },
                legend: {
                    position: 'right',
                    offsetX: 0,
                    offsetY: 50
                }
            }
        };
    },
    watch: {
        dataProps: {

            handler(newValue) {
                const me = this;
                if (newValue) {
                    me.series = newValue.data;
                    let xaxisLabels = newValue.originData.map(_ => _.created_at.split("T")[0]);


                    this.chartOptions = {
                        ...this.chartOptions,
                        xaxis: {
                            ...this.chartOptions.xaxis,
                            categories: xaxisLabels
                        }
                    };
                }
            },
            deep: true
        }
    }

};
</script>

<style scoped lang="scss"></style>
