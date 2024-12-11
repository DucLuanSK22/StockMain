import Enums from "@/resources/enumeration";
export const columns = [
    {
        field: "stock_code",
        headerName: "Mã",
        groupRow: 'trading_floor',
        type: Enums.EnumColumnType.Text,
        textAlign: 'left',
        width: 100,
        minWidth: 100,
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
        field: "matched_price",
        headerName: "Khớp",
        type: Enums.EnumColumnType.Decimal,
        textAlign: 'right',
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
        field: "change_price",
        headerName: "%",
        type: Enums.EnumColumnType.Percent,
        textAlign: 'right',
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
        headerName: "Tổng KL",
        type: Enums.EnumColumnType.Number,
        textAlign: 'right',
        width: 80,
        minWidth: 80,
    },
];
