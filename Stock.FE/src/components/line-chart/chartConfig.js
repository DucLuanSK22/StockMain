
export const configOptions = {
  series: [{
    data: [],
    name: "",
  }],
  chartOptions: {
    chart: {
      id: 'area-datetime',
      type: 'area',
      height: 350,
      zoom: {
        autoScaleYaxis: true
      },
      toolbar: {
        autoSelected: "pan",
        show: false
      }
    },
    colors: ["#ffad0d"],
    dataLabels: {
      enabled: false
    },
    markers: {
      size: 0,
      style: 'hollow',
    },
    xaxis: {
      type: 'datetime',
      tickAmount: 10,
      labels: {
        show: true,
        formatter: function (value) {
          const date = new Date(value);
          return `${date.getDate()}/${date.getMonth() + 1}`;
        },
        style: {
          colors: '#fff', // Màu của nhãn trục x
          fontSize: '14px', // Kích thước font chữ
          fontWeight: 500 // Độ đậm của chữ
        }
      },
      axisBorder: {
        show: true,
        color: '#cccccc', // Màu đường viền trục x
        height: 1,
        width: '100%',
        offsetX: 0,
        offsetY: 0
      },
      axisTicks: {
        show: true,
        borderType: 'solid',
        color: '#cccccc',
        height: 6,
        offsetX: 0,
        offsetY: 0
      },
    },
    yaxis: {
      labels: {
        formatter: function (value) {
          return `${value.toFixed(0).replace(/\B(?=(\d{3})+(?!\d))/g, ',')}`;
        },
        style: {
          colors: ["#fff"], // Màu sắc của nhãn
          fontSize: "14px", // Kích thước font
          fontFamily: "Arial, sans-serif", // Font chữ
          fontWeight: 600, // Độ đậm
        },
        offsetX: -10, // Điều chỉnh vị trí nhãn theo chiều ngang
        offsetY: 0, // Điều chỉnh vị trí nhãn theo chiều dọc
      },
      axisBorder: {
        show: true, // Hiển thị đường biên của trục
        color: '#FF4500', // Màu của trục Y
        width: 2, // Độ dày đường biên
        offsetX: 0,
        offsetY: 0,
      },
      axisTicks: {
        show: true, // Hiển thị dấu tick
        borderType: 'solid', // Kiểu đường (solid/dashed)
        color: '#FF4500', // Màu sắc dấu tick
        width: 2, // Độ dày dấu tick
        offsetX: 0,
        offsetY: 0,
      },
    },
    tooltip: {
      x: {
        formatter: function (value) {
          const date = new Date(value);
          return `${date.getDate()}/${date.getMonth() + 1}/${date.getFullYear()}`;
        }
      },

    },

    fill: {
      type: 'gradient',
      gradient: {
        shadeIntensity: 1,
        opacityFrom: 0.7,
        opacityTo: 1,
        stops: [0, 100]
      }
    },
  },
}