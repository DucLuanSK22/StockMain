<template>
    <div>
        <apexchart type="bar" height="450" :options="chartOptions" :series="series"></apexchart>
    </div>
</template>

<script>
export default {
    props: {
        dataProps: {
            type: Array,
            default: []
        }
    },

    data() {
        return {
            series: [
                {
                    name: 'Tiền',
                    data: [44, 55, 41, 67, 22, 43, 21, 49]
                },
                {
                    name: 'Cổ phiếu ',
                    data: [13, 23, 20, 8, 13, 27, 33, 12]
                },
            ],
            chartOptions: {
                chart: {
                    type: 'bar',
                    height: 450,
                    stacked: true,
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
                    categories: [
                        '2011',
                        '2011',
                        '2011',
                        '2011',
                        '2012',
                        '2012',
                        '2012',
                        '2012'
                    ],
                    style: {
                        colors: ['#85868a'], // Đổi màu chữ nhãn trục Y
                        fontSize: '14px', // Thay đổi kích thước font chữ
                        fontWeight: 600 // Độ dày của font chữ
                    }
                },
                yaxis: {
                    labels: {
                        formatter: function (value) {
                            return value + " M"; // Thêm text sau giá trị trục Y
                        },
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
            /**
             * Hàm cập nhật giá trị của series.
             * 
             * @function
             * @param {Array} dataProps - Mảng chứa các giá trị cho series.
             *             
             */
            handler() {
                this.series = this.dataProps;
            },
            deep: true
        }
    }

};
</script>

<style scoped lang="scss"></style>
