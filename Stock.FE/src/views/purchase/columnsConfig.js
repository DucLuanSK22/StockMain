import Enums from "@/resources/enumeration";

export const normalCommandColumns = [
    {
        field: "command_type",
        headerName: "Lệnh",
        type: Enums.EnumColumnType.Text,
        textAlign: 'left',
        width: 50,
        minWidth: 100,
        isColor: true,
    },
    {
        field: "stock_code",
        headerName: "Mã",
        type: Enums.EnumColumnType.Text,
        textAlign: 'right',
        width: 60,
        minWidth: 60,
        isColor: true,
    },
    {
        field: "created_at",
        headerName: "Thời gian",
        type: Enums.EnumColumnType.Time,
        textAlign: 'right',
        width: 120,
        minWidth: 60
    },
    {
        field: "order_price",
        headerName: "Giá đặt",
        type: Enums.EnumColumnType.Text,
        textAlign: 'right',
        width: 80,
        minWidth: 80,
    },
    {
        field: "matched_price",
        headerName: "Giá khớp",
        type: Enums.EnumColumnType.Text,
        textAlign: 'right',
        width: 80,
        minWidth: 80,
    },
    {
        field: "volume",
        headerName: "Khối lượng",
        type: Enums.EnumColumnType.Number,
        textAlign: 'right',
        width: 80,
        minWidth: 80,
    },
    {
        field: "transaction_type",
        headerName: "Trạng thái",
        type: Enums.EnumColumnType.Text,
        textAlign: 'right',
        width: 80,
        minWidth: 80,
    },
];


export const dealColumns = [   
    {
        field: "stock_code",
        headerName: "Mã",
        type: Enums.EnumColumnType.Text,
        textAlign: 'right',
        width: 60,
        minWidth: 60,
        isColor: true,
    },
    {
        field: "total_volume",
        headerName: "Khối lượng mở",
        type: Enums.EnumColumnType.Text,
        textAlign: 'right',
        width: 120,
        minWidth: 60,
        isColor: true,
    },
    {
        field: "total_tradeable_volume",
        headerName: "Được giao dịch",
        type: Enums.EnumColumnType.Text,
        textAlign: 'right',
        width: 80,
        minWidth: 80,
    },
    {
        field: "cost_value",
        headerName: "Giá hòa vốn",
        type: Enums.EnumColumnType.Text,
        textAlign: 'right',
        width: 80,
        minWidth: 80,
    },
    {
        field: "current_price",
        headerName: "Giá hiện tại",
        type: Enums.EnumColumnType.Text,
        textAlign: 'right',
        width: 80,
        minWidth: 80,
        isColor: true,
    },
    {
        field: "market_value",
        headerName: "Giá trị hiện tại",
        type: Enums.EnumColumnType.Number,
        textAlign: 'right',
        width: 80,
        minWidth: 80,
    },
     
      {
        field: "profit_loss",
        headerName: "Lãi chưa chốt",
        type: Enums.EnumColumnType.Number,
        textAlign: 'right',
        width: 80,
          minWidth: 80,
          isColor: true,
    },
      {
        field: "profit_loss_by_percent",
        headerName: "% lãi",
        type: Enums.EnumColumnType.Percent,
        textAlign: 'right',
        width: 80,
          minWidth: 80,
          isColor: true,
    },
];


export const dealDatax = [   
    {
        stock_code: "BWE",
        total_volume: 900,
        total_tradeable_volume: 900,
        cost_value: 28.043,
        current_price: 20.90,
        market_value: 531000,
        profit_loss: 198128,
        profit_loss_by_percent: 27.22,
        different: 0
    },
];


export const normalCommandDatax = [
    {
        command_type: "Mua",
        stock_code: "BWE",
        created_at: new Date(),
        order_price: 1.25,
        matched_price: null,
        volume: 300,
        transaction_type: 'Chờ gửi',
        different: 0
    },   
];