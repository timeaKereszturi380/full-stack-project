

<template>

   
    <v-simple-table dark>
        <template v-slot:default>
            <thead>
                <tr>
                    <th class="text-left">
                        Date
                    </th>
                    <th class="text-left">
                        Content
                    </th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="sms in smsItems"
                    :key="sms.id">
                    <td>{{ sms.date }}</td>
                    <td>{{ sms.content }}</td>
                </tr>
            </tbody>
        </template>
    </v-simple-table>
</template>


<script>
    var axios = require('axios');
    const self = this;
    export default {

        data: () => ({

            picker: (new Date(Date.now() - (new Date()).getTimezoneOffset() * 60000)).toISOString().substr(0, 10),
            smsItems: [],
            headers: [
                {
                    text: 'Date',
                    align: 'start',
                    sortable: false,
                    value: 'date',
                },
                { text: 'Content', sortable: false, value: 'content' }
            ],
        }),

 
       
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
                    .then(response => {
                        var json = JSON.stringify(response.data);
                        var items = []

                        let jsonObj = JSON.parse(json);

                        //console.log(jsonObj);

                        for (var i = 0; i < jsonObj.items.length; i++) {
                            let sms = jsonObj.items[i];
                            let derived = { "id": sms.id,"date": sms.sentDateUtc, "content": sms.content }
                            items.push(derived);
                        }

                        this.smsItems = items;

                        console.log(this.smsItems);
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
