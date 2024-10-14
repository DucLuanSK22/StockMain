<template>
    <div class="notifications">
        <div class="notifications__header">

            <span class="title ng-font-neutral-80">Thông báo</span>

            <div class="notifications_filter flex-start">
                <div class="notifications_filter__item  flex-center" v-for="(item) in filterType" :key="item.id" :class="{
                    'active': item.id == currentFilterType
                }" @click="handleChooseCurrentFilter($event, item)">
                    <span>{{ item.text }}</span>

                </div>


            </div>
            <div class="horizontal-separator"></div>
        </div>
        <div class="notifications__body">
            <div class="notifications_item flex-between" :class="{
                    'unread': item.unread
                }" v-for="(item, index) in computedNotifications" :key="index" @click="handleViewNotification(item)"
                v-if="computedNotifications.length > 0">
                <div class="notifications_item__right flex-center ">

                    <img v-if="item.type == 0" src="@/assets/img/download.png" alt="icon" />

                    <img v-else src="@/assets/img/promo.png" alt="icon" />

                </div>
                <div class="notifications_item__left">
                    <div class="description">{{ item.description }}</div>
                    <span class="time">{{ $ms.common.formatTime(item.created_time) }}</span>
                </div>

                <div v-if="item.unread" class="notification__unread"></div>
            </div>
            <div v-else class="no_notification">
                <img src="@/assets/img/notification.png" alt="icon" />
                <div class="no_notification_title">Bạn chưa có thông báo nào</div>
            </div>


        </div>
    </div>

</template>

<script>
import BookingAPI from '@/apis/BookingAPI.js';
import NotificationAPI from '@/apis/NotificationAPI.js';
export default {
    name: "PopupNotifications",
    props: {
        notificationsProps: {
            type: Array,
            default: []
        }
    },
    data() {
        return {
            // 0 - lọc tất cả
            // 1 - lọc những messege chưa đọc
            filterType: [
                {
                    id: 0,
                    text: 'Tất cả'
                },
                {
                    id: 1,
                    text: 'Chưa đọc'
                },
            ],
            /**
             * mặc định cho lọc tất cả
             */
            currentFilterType: 0,
            datas: this.notificationsProps
        }
    },
    computed: {
        /**
         * A computed property that filters notifications based on the currentFilterType.
         *
         * @return {Array} The filtered array of notifications.
        * @author nttue 17.05.2024
         */
        computedNotifications() {

            if (this.currentFilterType == 0) {
                return this.datas
            }
            else if (this.currentFilterType == 1) {
                return this.datas.filter(item => item.unread)
            }

        },

    },
    watch: {
        notificationsProps: {
            /**
             * A function to handle new notifications.
             *
             * @param {Object} newNotifications - The new notifications to be handled
             * @return {void} No return value
             * @author nttue 17.05.2024
             */
            handler(newNotifications) {
                this.datas = newNotifications;
            },
            deep: true
        }
    },

    methods: {
        handleChooseCurrentFilter($event, item) {
            $event.stopPropagation();
            this.currentFilterType = item.id;
        },
        /**
         * A function to handle viewing notifications.
         *
         * @param {Object} item - The notification item to be viewed
         * @return {Promise} - A promise representing the result of the function execution
         * @author nttue 17.05.2024
         */
        async handleViewNotification(item) {
            try {

                if (item.refid && item.type === 0) {

                    var booking = await BookingAPI.getById(item.refid);
                    const objectBooking = {
                        currentStep: 4,
                        BookingInfo: booking,
                        modeView: true
                    }
                    this.$ms.cache.setCache("booking", objectBooking);
                    this.$router.push({ path: `/booking/${booking.garage_id}` });
                }
                await NotificationAPI.updateUnRead(item.user_notifications_id);
                this.datas.forEach(notify => {
                    notify.unread = false;
                });
            } catch (error) {
                console.log(error);
            }
        }
    },
};
</script>

<style lang="scss" scoped>
@import "./popup-notifications.scss";
</style>
