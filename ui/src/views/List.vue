<template>
  <div class="sms-list">
    <h1>This is the sms list page</h1>
  </div>
</template>


<script>
    var axios = require('axios');
    export default {
       
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
                    })
                    .catch(function (error) {
                        console.log(error);
                    });

            }

         
        }

    }


</script>
