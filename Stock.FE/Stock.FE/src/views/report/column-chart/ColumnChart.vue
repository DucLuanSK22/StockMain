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
        },
        type: {
            type: String,
            default: 'price'
        },
        mainColor: {
            type: String,
            required: true
        }
    },

    data() {
        return {
            series: [{ name: 'Chênh lệch', data: [] }],
            chartOptions: {
                chart: {
                    type: 'bar',
                    height: 450
                },
                colors: [this.mainColor],
                dataLabels: {
                    enabled: true,
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
                        // formatter: function (value) {
                        //     return value / 1000000 + " M"; // Thêm text sau giá trị trục Y
                        // },
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
                    me.series[0].data = newValue.data;



                    this.chartOptions = {
                        ...this.chartOptions,
                        xaxis: {
                            ...this.chartOptions.xaxis,
                            categories: newValue.labels
                        }
                    };
                    if (me.type === 'volume') {
                        this.chartOptions = {
                            ...this.chartOptions,
                            dataLabels: {
                                ...this.chartOptions.dataLabels,
                                formatter: function (val) {
                                    return val % 1000000000 == 0 ? val / 1000000000 + "Tỷ" : (val / 1000000000).toFixed(2) + "Tỷ";
                                },
                            },
                            yaxis: {
                                ...this.chartOptions.yaxis,
                                labels: {
                                    ...this.chartOptions.yaxis.labels,
                                    formatter: function (val) {
                                        return val % 1000000000 == 0 ? val / 1000000000 + "Tỷ" : (val / 1000000000).toFixed(2) + "Tỷ";
                                    },
                                }

                            }

                        };
                    }
                }
            },
            deep: true
        }
    }

};
</script>

<style scoped lang="scss"></style>
