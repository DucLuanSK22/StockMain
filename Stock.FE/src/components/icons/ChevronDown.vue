<template>
  <svg width="16" height="16" viewBox="0 0 16 16" :fill="fill" xmlns="http://www.w3.org/2000/svg">
    <path d="M13 6l-5 5-5-5" stroke="#606F7B" stroke-linecap="round" stroke-linejoin="round"></path>
  </svg>
</template>

<script>
export default {
  name: "ChevronDown",
  props: {
    fill: {
      type: String,
      default: 'none'
    }
  }
};
</script>
