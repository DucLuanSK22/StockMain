import Enums from "@/resources/enumeration";

export const normalCommandColumns = [
    {
        field: "transaction_type",
        headerName: "Lệnh",
        type: Enums.EnumColumnType.Enum,
        enumName: 'TransactionType',
        textAlign: 'left',
        width: 50,
        minWidth: 100,
        funcCusColor: (value, keyColor = null) => {
            let key = (keyColor || keyColor === 0) ? keyColor : value;
            let result = 'white';
            switch (key) {
                case 0:
                    result = 'green';
                    break;
                case 1:
                    result = 'red';
                    break;
                default:
                    result = 'white';
                    break;
            }
            return result;
        },
    },
    {
        field: "stock_code",
        headerName: "Mã",
        type: Enums.EnumColumnType.Text,
        textAlign: 'right',
        width: 60,
        minWidth: 60,
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
        type: Enums.EnumColumnType.Decimal,
        textAlign: 'right',
        width: 80,
        minWidth: 80,
    },
    {
        field: "matched_price",
        headerName: "Giá khớp",
        type: Enums.EnumColumnType.Decimal,
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
        field: "status",
        headerName: "Trạng thái",
        type: Enums.EnumColumnType.Enum,
        enumName: 'TransactionStatus',
        textAlign: 'right',
        width: 80,
        minWidth: 80,
        functionCustomColorTD: (status) => {
            switch (status) {
                case 0:
                    return 'yellow';
                case 1:
                    return 'green';

            }
        }
    },
];


export const dealColumns = [
    {
        field: "stock_code",
        headerName: "Mã",
        type: Enums.EnumColumnType.Text,
        textAlign: 'left',
        width: 60,
        minWidth: 60,
        keyColor: 'difference',
        funcCusColor: (value, keyColor = null) => {
            let key = (keyColor || keyColor === 0) ? keyColor : value;
            let result = 'white';
            switch (key) {
                case 0:
                    result = 'green';
                    break;
                case 1:
                    result = 'red';
                    break;
                case 2:
                    result = 'yellow';
                    break;
                default:
                    result = 'white';
                    break;
            }
            return result;
        },
    },
    {
        field: "total_volume",
        headerName: "KL mở",
        type: Enums.EnumColumnType.Number,
        textAlign: 'right',
        width: 80,
        minWidth: 60,
    },
    {
        field: "total_tradeable_volume",
        headerName: "Được GD",
        type: Enums.EnumColumnType.Number,
        textAlign: 'right',
        width: 80,
        minWidth: 80,
    },

    {
        field: "matched_price",
        headerName: "Giá GD",
        type: Enums.EnumColumnType.Decimal,
        textAlign: 'right',
        width: 80,
        minWidth: 80,
    },

    {
        field: "current_price",
        headerName: "Giá hiện tại",
        type: Enums.EnumColumnType.Decimal,
        textAlign: 'right',
        width: 80,
        minWidth: 80,
        keyColor: 'difference',
        funcCusColor: (value, keyColor = null) => {
            let key = (keyColor || keyColor === 0) ? keyColor : value;
            let result = 'white';
            switch (key) {
                case 0:
                    result = 'green';
                    break;
                case 1:
                    result = 'red';
                    break;
                case 2:
                    result = 'yellow';
                    break;
                default:
                    result = 'white';
                    break;
            }
            return result;
        },
    },
    {
        field: "cost_value",
        headerName: "GT hiện tại",
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
        keyColor: 'is_profit',
        funcCusColor: (value, keyColor = null) => {
            let key = (keyColor || keyColor === 0) ? keyColor : value;
            let result = 'white';
            switch (key) {
                case 0:
                    result = 'green';
                    break;
                case 1:
                    result = 'red';
                    break;
                default:
                    result = 'white';
                    break;
            }
            return result;
        },
    },
    {
        field: "profit_loss_by_percent",
        headerName: "% lãi",
        type: Enums.EnumColumnType.Percent,
        textAlign: 'right',
        width: 80,
        minWidth: 80,
        keyColor: 'is_profit',
        funcCusColor: (value, keyColor = null) => {
            let key = (keyColor || keyColor === 0) ? keyColor : value;
            let result = 'white';
            switch (key) {
                case 0:
                    result = 'green';
                    break;
                case 1:
                    result = 'red';
                    break;
                default:
                    result = 'white';
                    break;
            }
            return result;
        },
    },
];
