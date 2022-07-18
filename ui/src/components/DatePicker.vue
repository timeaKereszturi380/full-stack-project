<template>
    <v-row justify="center">
        <v-date-picker v-model="dateFrom" label="Date From"></v-date-picker>
    </v-row>

    <v-row justify="center">
        <v-date-picker v-model="dateTo" lable="Date To"></v-date-picker>
    </v-row>
</template>

<script>


    export default {
        name: 'DatePicker',
        data: () => ({
            dateFrom: (new Date(Date.now() - (new Date()).getTimezoneOffset() * 60000)).toISOString().substr(0, 10),
            dateTo: (new Date(Date.now() - (new Date()).getTimezoneOffset() * 60000)).toISOString().substr(0, 10)
            
        })

    }
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped lang="stylus">
</style>
