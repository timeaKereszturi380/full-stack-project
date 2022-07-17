<!--<template>
  <div class="sms-list">
    <h1>This is the sms list page</h1>
  </div>
</template>-->

<template>

    <table>
        <thead>
            <tr>
                <th>Date Sent</th>
            </tr>

            <tr>
                <th>Content</th>
            </tr>

        </thead>
        <tbody>
            <tr v-for="sms in items">
                <th>{{ sms.date }}</th>
                <!--<td>{{ sms.content }}</td>-->
            </tr>
        </tbody>

    </table>
    

</template>

<script>
    var axios = require('axios');
    export default {

        props: {
            items: []
        },

        methods: {

            
            fetch() {

                var config = {
                    method: 'get',
                    url: 'https://f806-90-200-19-209.ngrok.io/history?from=17-07-2022&to=17-07-2022',
                    headers: {
                        'sms-api-key': 'ff44db7e-5088-4a1f-8c2c-1b75edea62e6'
                    }
                };

                axios(config)
                    .then(function (response) {
                        console.log(JSON.stringify(response.data));
                        var items = []
                        
                        const json = JSON.stringify(response);
                        //Object.entries(json).forEach(([key, value]) => {
                        //    console.log(`${key}: ${value}`);
                        //});
                        this.items = items

                        console.log(JSON.stringify(data.items));
                    })
                    .catch(function (error) {
                        console.log(error);
                    });

            }

         
        },

        beforeMount() {
            this.fetch();
        },

    }


</script>
