<template>
    <div>
        <apexchart width="500" type="donut" :options="chartOptions" :series="series"></apexchart>
    </div>
</template>

<script>
export default {
    props: {
        assetProps: {
            type: Array,
            default: []
        }
    },
    /**
     * Dữ liệu cho biểu đồ doughnut.
     *
     * Gồm 2 phần: series và chartOptions.
     * - Series: Là dữ liệu cho các phần của biểu đồ.
     *   - data: Là mảng số nguyên, mỗi phần tử là phần trăm của phần tương ứng.
     * - ChartOptions: Là các tùy chọn cho biểu đồ.
     *   - chart: Loại biểu đồ.
     *   - labels: Nhãn cho các phần của biểu đồ.
     *   - responsive: Tùy chọn cho màn hình khác nhau.
     *       - breakpoint: Kích thước màn hình.
     *       - options: Các tùy chọn cho màn hình.
     *           - chart: Tùy chọn cho biểu đồ.
     *           - legend: Tùy chọn cho legend.
     */
    data() {
        return {
            series: this.assetProps,
            chartOptions: {
                chart: {
                    type: 'donut', // Đặt loại biểu đồ là 'donut'
                },
                labels: ['Cổ phiếu', 'Tiền'], // Nhãn cho các phần của biểu đồ
                colors: ['#f68fff', '#46d96f'],
                legend: {
                    formatter: function (val, opts) {
                        return `<span class="legend-text-wrapper"><span class="legend-text">${val}</span><span>` + " " + (opts.w.globals.series[opts.seriesIndex] / 1000000).toFixed(2) + ' triệu';
                    },
                    fontSize: '16px', // Tùy chỉnh kích thước font chữ
                    fontWeight: 600, // Độ dày font chữ                    
                    labels: {
                        colors: ['#fff', '#fff'], // Đặt màu sắc cho từng mục trong legend
                    },
                    marginLeft: '10px',
                },
                tooltip: {
                    enabled: true,
                    y: {
                        // Custom tooltip value when hovering
                        formatter: function (val, opts) {
                            return val % 1000000 == 0 ? val / 1000000 + "M" : (val / 1000000).toFixed(2) + "M";
                        },

                    }
                },
                responsive: [
                    {
                        breakpoint: 480,
                        options: {
                            chart: {
                                width: 200, // Điều chỉnh chiều rộng cho màn hình nhỏ hơn 480px
                            },
                            legend: {
                                position: 'bottom', // Vị trí legend cho màn hình nhỏ
                            },
                        },
                    },
                ],
                dataLabels: {
                    enabled: true,
                    formatter: function (val) {
                        return val.toFixed(2) + "%"
                    },

                }
            }
        };
    },
    watch: {
        assetProps: {
            handler(newValue) {
                this.series = [...newValue]
                ///.map(_ => this.$ms.common.formatAmount(_));
            },
            deep: true
        }
    },
    mounted() {
        console.log(this.assetProps);
    },
    methods: {

    }
};
</script>