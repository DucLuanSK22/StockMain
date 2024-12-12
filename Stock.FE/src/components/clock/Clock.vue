<template>
    <div class="clock">
        <div id="date-time"></div>
    </div>
</template>

<script>
export default {
    mounted() {
        window.addEventListener("load", this.counter());
    },
    beforeUnmount() {
        window.removeEventListener("load", this.counter());
    },
    methods: {
        counter() {
            clock();
            function clock() {
                const today = new Date();
                const hours = today.getHours();
                const minutes = today.getMinutes();
                const seconds = today.getSeconds();

                const hour = hours < 10 ? "0" + hours : hours;
                const minute = minutes < 10 ? "0" + minutes : minutes;
                const second = seconds < 10 ? "0" + seconds : seconds;

                //make clock a 12-hour time clock
                const hourTime = hour > 12 ? hour - 12 : hour;
                const ampm = hour < 12 ? "AM" : "PM";

                // get date components
                const month = today.getMonth() + 1;
                const year = today.getFullYear();
                const day = today.getDate();



                //get current date and time
                const date = `${day < 10 ? "0" + day : day}/${month < 10 ? "0" + month : month}/${year}`;
                const time = hourTime + ":" + minute + ":" + second + ampm;

                //combine current date and time
                const dateTime = date + " - " + time;

                document.getElementById("date-time").innerHTML = dateTime;
                setTimeout(clock, 1000);
            }
        }
    }

}
</script>
<style lang="scss" scoped>
#clock {
    max-width: 600px;
}

#date-time {
    margin-right: 12px;
    color: white;
    font-weight: 500;
}
</style>