<template></template>
<script>
export default {
    name: "enterFormMixin",
    data() {
        return {};
    },
    mounted() {
        window.addEventListener('keydown', this.handleEnterKey);

    },

    beforeUnmount() {
        window.removeEventListener('keydown', this.handleEnterKey)
    },
    methods: {
        handleEnterKey($event) {
            if ($event.keyCode === 13) {
                $event.preventDefault();

                this.handleSubmit();
            }
        }
    },
};
</script>
