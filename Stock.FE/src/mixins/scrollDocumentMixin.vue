<template></template>
<script>
export default {
    name: "scrollDocumentMixin",
    data() {
        return {};
    },
    mounted() {
        const ele = document.querySelector('.container');
        ele.addEventListener("scroll", this.handleScroll);
    },
    beforeUnmount() {
        const ele = document.querySelector('.container');
        ele.removeEventListener("scroll", this.handleScroll);
    },
    methods: {
        handleScroll() {

            const ele = document.querySelector('.container');
            if (ele.scrollTop > 300) {
                this.$refs['backToTop'].classList.add("active");
            } else {
                this.$refs['backToTop'].classList.remove("active");
            }
        }
    },
};
</script>
