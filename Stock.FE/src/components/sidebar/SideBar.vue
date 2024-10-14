<template>
  <div class="sidebar">

    <div class="sidebar__top">
      <div class="fake_loading_top " v-if="loading">
        <skeleton />
      </div>
      <div v-else>
        <div class="sidebar__top-title">Gara gần tôi</div>
        <div class="filter__wrapper" v-if="!noneFilter">
          <div class="filter__item" v-for="(item, index) in listFilter" :key="index" @click="showFilter($event, index)"
            :id="filtering.includes(item.id) && 'active'">
            <span>
              {{ item.text }}
            </span>
            <MFilter :filterId="item.id" :data="item.data" :chooseType="item.choose" :index="index"
              :modelValue="item.modelValue" v-if="currentIndexFilter == index" @saveOptions="handleSaveOptions"
              @clearChecked="handleClearChecked" v-click-outside="$event => handleClickOutside($event)" />
          </div>
        </div>
      </div>
    </div>
    <div class="sidebar-item__wrapper">
      <div v-if="loading">
        <skeleton type="sidebar-item" />
        <div class="horizontal-separator"></div>
        <skeleton type="sidebar-item" />
      </div>
      <SideBarItem v-else v-for="(location, index) in locations" :key="location?.garage_id" :location="location"
        :garaIndex="index"
        image="https://storage.googleapis.com/rp-production-public-content/3z624a2usqnwc07nb8ni93deixc7"
        @mouseenter="handleMouseMove(index, location)" @click="handleClickItem(location)" />
    </div>
  </div>
</template>

<script>
import SideBarItem from "@/components/sidebar-item/SideBarItem.vue";
import MFilter from "@/components/filter/MFilter.vue";
import GarageAPI from "@/apis/GarageAPI";
export default {
  name: "Sidebar",
  components: {
    SideBarItem,
    MFilter,
  },
  props: {
    /**
     * Danh sách các garage
     */
    locationProps: {
      type: Array,
      default: [],
    },
    /**
     * Cờ kiểm tra có để chế độ filter hay không
     */
    noneFilter: {
      type: Boolean,
      default: false,
    },
  },
  data() {
    return {
      filtering: [],
      listFilter: [
        /**
         * Lọc theo loại xe
         */
        {
          text: "Loại xe",
          id: "typeCars",
          data: [],
          choose: "radio",
          modelValue: "",
        },
        /**
        * Lọc theo khoảng cách hoặc đánh giá
        */
        {
          text: "Sắp xếp theo",
          id: "sortBy",
          modelValue: "",
          data: [
            {
              text: "Khoảng cách gần nhất",
              value: "distance",
              id: 1,
            },
            {
              text: "Đánh giá cao nhất",
              value: "avg_rating",
              id: 2,
            },
          ],
          choose: "radio",
        },
        /**
        * Lọc theo thời gian mở cửa
        */
        {
          id: "openTime",
          text: "Giờ mở cửa",
          modelValue: "",
          data: [
            {
              text: "Ngay bây giờ",
              value: 0,
              id: 1,
            },
            {
              text: "Sau giờ làm việc",
              value: 1,
              id: 2,
            },
            {
              text: "Trước giờ làm việc",
              value: 2,
              id: 3,
            },
            {
              text: "Tất cả thời gian",
              value: 3,
              id: 4,
            },
          ],
          choose: "radio",
        },
        /**
        * Lọc theo dịch vụ sửa xe
        */
        {
          id: "services",
          text: "Dịch vụ",
          data: [],
          choose: "checkbox",
        },
      ],
      /**
        * Danh sách các garage
        */
      locations: [],
      currentIndexFilter: -1, // Cờ điểm kiểm tra filter đang được bật để người dùng chọn.
    };
  },
  emits: ["sortBy", "typeCars", "openTime", "showPosition"],
  mounted() {
    this.initialData();
  },
  computed: {
    loading() {
      return this.locationProps.length == 0;
    }
  },
  watch: {
    /**
     * Sets the value of the locations property to the provided newLocations.
     *
     * @param {Array} newLocations - An array of new locations.
     */
    locationProps(newLocations) {
      this.locations = newLocations;
    },
  },
  methods: {
    /**
     * Handle click outside event.
     *
     * @param {$event} $event - The event object
     * @return {void} 
     */
    handleClickOutside() {
      this.$ms.common.handleClickOutSide(event.target, "filter__item", () => {
        this.currentIndexFilter = -1;
      });
    },


    /**
     * Clears the checked status of all items in the listFilter[3].data array and emits an event to clear the services.
     * Also toggles the filter at the specified index and updates the filtering state for the 'services' filter.
     *
     * @param {number} index - The index of the filter to toggle.
     * @return {void}
     */
    handleClearChecked(index) {
      const me = this;
      me.listFilter[3].data = me.listFilter[3].data.map((item) => {
        return {
          ...item,
          value: false,
        };
      });
      me.$emit("services", []);
      me.toggleFilter(index);

      me.handleFiltering('services', false);
    },
    /**
     * Handle filtering based on the given filterId and value.
     *
     * @param {String} filterId - The ID of the filter to be applied
     * @param {String} value - The value used for filtering
     */
    handleFiltering(filterId, value) {
      const me = this;
      if (value) {

        if (!me.filtering.includes(filterId)) {
          me.filtering = [...me.filtering, filterId]
        }
      } else {
        me.filtering = me.filtering.filter(item => item !== filterId)
      }
    },

    /**
     * Handles saving options based on the filterId, value, and index.
     *
     * @param {string} filterId - The ID of the filter.
     * @param {any} value - The value to be saved.
     * @param {number} index - The index of the filter.
     * @return {void}
     */
    handleSaveOptions(filterId, value, index) {
      const me = this;
      switch (filterId) {
        case "typeCars":
          me.listFilter[0].modelValue = value;
          me.$emit("typeCars", value);

          me.handleFiltering(filterId, value);
          break;
        case "sortBy":
          me.listFilter[1].modelValue = value;
          if (value == "distance") {
            me.$emit("sortBy", "distance asc");
          } else if (value == "avg_rating") {
            me.$emit("sortBy", "avg_rating desc, distance asc");
          }
          me.handleFiltering(filterId, value);
          break;
        case "openTime":
          me.listFilter[2].modelValue = value;
          me.$emit("openTime", value ? value : 0);
          me.handleFiltering(filterId, value);
          break;
        case "services":
          me.$emit(
            "services",
            me.listFilter[3].data
              .filter((item) => item.value)
              .map((item) => item.text)
          );

          me.handleFiltering(filterId, me.listFilter[3].data
            .filter((item) => item.value).length > 0);
          break;
      }

      me.toggleFilter(index);
    },
    /**
     * Toggles the filter based on the provided index.
     *
     * @param {number} index - The index of the filter to toggle
     * @return {void}
     */
    toggleFilter(index) {
      if (this.currentIndexFilter != index) {
        this.currentIndexFilter = index;
      } else {
        this.currentIndexFilter = -1;
      }
    },
    /**
     * Fetches initial data for cars and services, populates the listFilter accordingly.
     *
     * @return {void} 
     */
    async initialData() {
      try {

        let carsList = this.$ms.cache.getCache("cars");
        let servicesList = this.$ms.cache.getCache("services");
        if (carsList && carsList.length >= 0) {
          this.listFilter[0].data = [...carsList];
        } else {
          let cars = await GarageAPI.getCars();
          let dataConvert = cars.map((item, index) => {
            return {
              text: item.make,
              value: item.make,
              id: index,
            };
          });
          this.listFilter[0].data = [...dataConvert];
          this.$ms.cache.setCache("cars", dataConvert);
        }

        if (servicesList && servicesList.length >= 0) {
          this.listFilter[3].data = [...servicesList];
        } else {
          let services = await GarageAPI.getGarageServices();
          let dataConvert = services.map((item, index) => {
            return {
              text: item.service_name,
              value: false,
              id: index,
            };
          });
          this.listFilter[3].data = [...dataConvert];
          this.$ms.cache.setCache("services", dataConvert);

        }
      }
      catch (e) {
        console.log(e);
      }
    },
    /**
     * Toggles the filter based on the provided index if the event target is not a child of "filter-gara".
     *
     * @param {Event} event - The event object
     * @param {number} index - The index of the filter to toggle
     * @return {void}
     */
    showFilter(event, index) {
      // Nếu sự kiện người dùng click chứa filter-gara thì không làm gì cả.
      if (this.$ms.common.findParent(event.target, "filter-gara", 'filter__item')) {
        return;
      }
      // Ngược lại gọi vào hàm toggleFilter để bật tắt chế độ filter.
      else {
        this.toggleFilter(index);
      }
    },
    /**
     * Emits a "showPosition" event with the provided index and location.
     *
     * @param {number} index - The index associated with the location.
     * @param {any} location - The location data to show.
     */
    handleMouseMove(index, location) {
      this.$emit("showPostion", index, location);
    },

    /**
     * Navigates to the garage detail page for the given location.
     *
     * @param {Object} location - The location object containing the garage ID.
     * @return {void}
     */
    handleClickItem(location) {
      this.$router.push({ path: `/garage-detail/${location.garage_id}` });
    }
  },
};
</script>

<style lang="scss">
@import "./sidebar.scss";
</style>
