<template>
  <v-container class="sms-composer">
    <v-layout row wrap>
      <v-flex xs12>
          <v-form v-model="valid" @submit.prevent="submit">
              <v-text-field label="Mobile Number"></v-text-field>
              <v-textarea label="Sms Content"></v-textarea>

              <v-btn class="mr-4"
                  
                     @click="submit"
                     :disabled="invalid">
                  Send SMS
              </v-btn>

          </v-form>
      </v-flex>
    </v-layout>
  </v-container>
</template>


<script>

    import axios from 'axios'
    import cors from 'cors'

    export default {
        name: 'SmsComposer',
        data: () => ({
            valid: true
        }),

        methods: {

            submit() {

                let data = { SMSRecipient: "string", SMSFrom: "string", Content: "some text message" };
                let token = "ff44db7e-5088-4a1f-8c2c-1b75edea62e6";
                var config = {
                    method: 'post',
                    url: 'https://f806-90-200-19-209.ngrok.io/send',
                    headers: {
                        'sms-api-key': token,
                        'Content-Type': 'application/json'
                    },
                    data: data
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

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped lang="stylus">
</style>
