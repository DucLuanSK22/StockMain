
export const data = {
  'VNINDEX': {
    '1W': {
      labels: ['31/08', '01/09', '02/09', '03/09', '04/09', '05/09', '06/09'],
      datasets: [
        {
          label: '',
          borderColor: '#34c85e',
          backgroundColor: 'rgba(52, 200, 94, 0.2)',
          fill: true,
          data: [1230, 1245, 1225, 1210, 1205, 1230, 1240] // Dữ liệu nhiều hơn theo ngày trong tuần
        }
      ]
    },
    '1M': {
      labels: [
        '01/08', '05/08', '10/08', '15/08', '20/08', '25/08', '30/08',
        '01/09', '05/09', '10/09', '15/09', '20/09', '25/09', '30/09'
      ],
      datasets: [
        {
          label: '',
          borderColor: '#34c85e',
          backgroundColor: 'rgba(52, 200, 94, 0.2)',
          fill: true,
          data: [1210, 1220, 1235, 1240, 1230, 1255, 1265, 1270, 1280, 1260, 1275, 1290, 1300, 1310] // Dữ liệu nhiều hơn theo tháng
        }
      ]
    },
    '6M': {
      labels: [
        '01/04', '15/04', '01/05', '15/05', '01/06', '15/06', '01/07', '15/07', '01/08', '15/08', '01/09', '15/09'
      ],
      datasets: [
        {
          label: '',
          borderColor: '#34c85e',
          backgroundColor: 'rgba(52, 200, 94, 0.2)',
          fill: true,
          data: [1150, 1185, 1200, 1190, 1215, 1220, 1240, 1230, 1255, 1265, 1270, 1280] // Dữ liệu cho 6 tháng
        }
      ]
    },
    '1Y': {
      labels: [
        '09/2022', '10/2022', '11/2022', '12/2022', '01/2023', '02/2023', '03/2023', 
        '04/2023', '05/2023', '06/2023', '07/2023', '08/2023', '09/2023'
      ],
      datasets: [
        {
          label: '',
          borderColor: '#34c85e',
          backgroundColor: 'rgba(52, 200, 94, 0.2)',
          fill: true,
          data: [1150, 1160, 1175, 1200, 1225, 1250, 1230, 1260, 1240, 1270, 1290, 1305, 1320] // Dữ liệu nhiều hơn cho 1 năm
        }
      ]
    }
  },    
  'VN30': {
    '1W': {
      labels: ['31/08', '01/09', '02/09', '03/09', '04/09', '05/09', '06/09'],
      datasets: [
        {
          label: '',
          borderColor: '#ff6600',
          backgroundColor: 'rgba(255, 102, , 0.2)',
          fill: true,
          data: [1250, 1260, 1245, 1220, 1210, 1255, 1265] // Nhiều hơn theo tuần
        }
      ]
    },
    '1M': {
      labels: [
        '01/08', '05/08', '10/08', '15/08', '20/08', '25/08', '30/08',
        '01/09', '05/09', '10/09', '15/09', '20/09', '25/09', '30/09'
      ],
      datasets: [
        {
          label: '',
          borderColor: '#ff6600',
          backgroundColor: 'rgba(255, 102, , 0.2)',
          fill: true,
          data: [1230, 1245, 1260, 1250, 1280, 1290, 1305, 1320, 1330, 1315, 1340, 1355, 1370, 1380] // Thêm dữ liệu
        }
      ]
    },
    '6M': {
      labels: [
        '01/04', '15/04', '01/05', '15/05', '01/06', '15/06', '01/07', '15/07', '01/08', '15/08', '01/09', '15/09'
      ],
      datasets: [
        {
          label: '',
          borderColor: '#ff6600',
          backgroundColor: 'rgba(255, 102, , 0.2)',
          fill: true,
          data: [1180, 1190, 1205, 1230, 1245, 1255, 1260, 1270, 1285, 1300, 1310, 1325] // Dữ liệu cho 6 tháng
        }
      ]
    },
    '1Y': {
      labels: [
        '09/2022', '10/2022', '11/2022', '12/2022', '01/2023', '02/2023', '03/2023', 
        '04/2023', '05/2023', '06/2023', '07/2023', '08/2023', '09/2023'
      ],
      datasets: [
        {
          label: '',
          borderColor: '#ff6600',
          backgroundColor: 'rgba(255, 102, , 0.2)',
          fill: true,
          data: [1180, 1200, 1225, 1240, 1255, 1265, 1280, 1290, 1310, 1320, 1335, 1345, 1360] // Nhiều hơn theo năm
        }
      ]
    }
    },
  'VN30F1M': {
    '1W': {
      labels: ['31/08', '01/09', '02/09', '03/09', '04/09', '05/09', '06/09'],
      datasets: [
        {
          label: '',    
          borderColor: '#3499c8',
          backgroundColor: 'rgba(52, 153, 200, 0.9)',
          fill: true,
          data: [1250, 1260, 1245, 1220, 1210, 1255, 2265] // Nhiều hơn theo tuần
        }
      ]
    },
    '1M': {
      labels: [
        '01/08', '05/08', '10/08', '15/08', '20/08', '25/08', '30/08',
        '01/09', '05/09', '10/09', '15/09', '20/09', '25/09', '30/09'
      ],
      datasets: [
        {
          label: '',
          borderColor: '#3499c8',
          backgroundColor: 'rgba(52, 153, 200, 0.9)',
          fill: true,
          data: [1230, 1245, 1260, 1250, 1280, 1290, 1305, 1320, 1330, 1315, 1340, 1355, 1370, 1380] // Thêm dữ liệu
        }
      ]
    },
    '6M': {
      labels: [
        '01/04', '15/04', '01/05', '15/05', '01/06', '15/06', '01/07', '15/07', '01/08', '15/08', '01/09', '15/09'
      ],
      datasets: [
        {
          label: '',
          borderColor: '#3499c8',
          backgroundColor: 'rgba(52, 153, 200, 0.9)',
          fill: true,
          data: [1180, 1190, 1205, 1230, 1245, 1255, 1260, 1270, 1285, 1300, 1310, 1325] // Dữ liệu cho 6 tháng
        }
      ]
    },
    '1Y': {
      labels: [
        '09/2022', '10/2022', '11/2022', '12/2022', '01/2023', '02/2023', '03/2023', 
        '04/2023', '05/2023', '06/2023', '07/2023', '08/2023', '09/2023'
      ],
      datasets: [
        {
          label: '',
          borderColor: '#3499c8',
          backgroundColor: 'rgba(52, 153, 200, 0.9)',
          fill: true,
          data: [1180, 1200, 1225, 1240, 1255, 1265, 1280, 1290, 1310, 1320, 1335, 1345, 1360] // Nhiều hơn theo năm
        }
      ]
    }
  }
}



export const options = {
  responsive: true,
    maintainAspectRatio: false,
   plugins: {
      legend: { display: false }
    },
    scales: {
    x: {
      grid: {
        display: false // Tắt lưới ngang trục X nếu bạn không muốn hiển thị
            },
        ticks: {
            display: false // Tắt luôn label trên trục X
        }
    },
    y: {
    display: false,
      beginAtZero: true
    }
  }
}
