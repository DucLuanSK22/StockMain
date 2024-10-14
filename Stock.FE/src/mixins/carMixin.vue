<script>
import BookingAPI from "@/apis/BookingAPI";

export default {
    name: "carMixin",

    data() {
        return {
            makeList: [],
            yearList: [],
            modelList: [],
            garageId: "00000000-0000-0000-0000-000000000000",
            objectMaster: {
                make: null,
                year: null,
                model: null
            },
            isFirstLoadPage: false,
        }
    },

    watch: {
        "objectMaster.make"(newValue) {
            if (newValue && !this.isFirstLoadPage) {
                this.fetchModel();
                this.objectMaster.year = null;
                this.objectMaster.model = null;
            }
        },
        "objectMaster.model"(newValue) {
            if (newValue && !this.isFirstLoadPage) {
                this.fetchYears();
                this.objectMaster.year = null;
            }
        },
        "objectMaster.year"(newValue) {
            if (newValue && !this.isFirstLoadPage) {
                this.objectMaster.cars_id = this.modelAndIdCarList.find(
                    (item) => item.year == newValue
                ).cars_id;
            }
        },
    },
    methods: {
        async fetchMakes() {
            try {
                const result = await BookingAPI.getMakeByGarageId(this.garageId);
                this.makeList = result.map((item) => item.make);
            } catch {
                // ignore error
            }
        },
        async fetchYears() {
            try {
                const result = await BookingAPI.getYearsByGarageIdAndMake(
                    this.garageId,
                    this.objectMaster.make,
                    this.objectMaster.model
                );
                this.yearList = result.map((item) => item.year);
                this.modelAndIdCarList = result;
            } catch {
            }
        },
        async fetchModel() {
            try {
                const result = await BookingAPI.getModelsByGarageId(
                    this.garageId,
                    this.objectMaster.make

                );
                this.modelList = result.map((item) => item.model);

            } catch {
            }
        },
    },
}

</script>