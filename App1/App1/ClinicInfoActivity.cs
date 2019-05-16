using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace App1
{
    [Activity(Label = "Clinic Information")]
    public class ClinicInfoActivity : Activity
    {
        private static JToken jToken;
        private static HttpClient client = new HttpClient();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.ClinicInfoLayout);

            /***************************************************************************
          *
          * GET DATA THAT IS SENT FROM THE PREVIOUS SCREEN AND POPULATE TEXTVIEWS     
          *
          **************************************************************************/
            int clinicID = Intent.GetIntExtra("clinicID", 0); //Gets the data from the previous form 

            getClinicInfo(clinicID);

        }

        /***************************************************************************
        *
        * GET CLINIC INFO
        *  - This function is used to retrieve all info relating to a specific clinic
        *       based off of the ID we receive from ClinicActivity
        *  - Populate the view
        *
        ***************************************************************************/
        async void getClinicInfo(int id)
        {
            //TODO API Call to get the info for the clinic
            await callAPIEndpoint(id);

            String name = "Clinic Name TEST";
            String location = "Location";
            String hours = "Hours";
            String capacity = "Capacity";
            String region = "Region";

            //Populate the View
            TextView nameTextView = FindViewById<TextView>(Resource.Id.txtClinicNameHeading);
            nameTextView.Text = name;

            TextView locationTextView = FindViewById<TextView>(Resource.Id.txtClinicLocationDetails);
            locationTextView.Text = location;

            TextView hoursTextView = FindViewById<TextView>(Resource.Id.txtClinicHoursDetails);
            hoursTextView.Text = hours;

            TextView capacityTextView = FindViewById<TextView>(Resource.Id.txtClinicCapacityDetails);
            capacityTextView.Text = capacity;

            TextView regionTextView = FindViewById<TextView>(Resource.Id.txtClinicRegionDetails);
            regionTextView.Text = region;
        }

        /***************************************************************************
        *
        * CALL API FUNCTION 
        *  - This function calls the API endpoint and checks if a positive response 
        *       was received.
        *  - If the response was successful, return the json return 
        *
        ***************************************************************************/
        public async Task callAPIEndpoint(int ID)
        {
            try
            {
                object clinicInfo = new { clinic_id = ID };
                var jsonObj = JsonConvert.SerializeObject(clinicInfo);
                StringContent postData = new StringContent(jsonObj.ToString(), Encoding.UTF8, "application/json");

                HttpRequestMessage request = new HttpRequestMessage();
                request.RequestUri = new Uri("http://choltechcloudservice.cloudapp.net/api/UsersController/getClinic");
                request.Method = HttpMethod.Get;
                request.Headers.Add("Accept", "application/json");
                request.Content = postData;

                HttpResponseMessage response = await client.SendAsync(request);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    HttpContent content = response.Content;

                    //Now assign your content to your data variable, by converting into a string using the await keyword.
                    var data = await content.ReadAsStringAsync();
                    //If the data isn't null return log convert the data using newtonsoft JObject Parse class method on the data.
                    if (content != null)
                    {
                        //Now log your data object in the console
                        Console.WriteLine("data ------------{0}", JObject.Parse(data));
                        jToken = JObject.Parse(data);

                        var success = jToken["success"].ToString();
                        if (success.Equals("true") || success.Equals("True"))
                        {
                            Toast.MakeText(Application.Context, "Clinic Fetched!", ToastLength.Short).Show();
                            Console.WriteLine("DATA ------------{0}", JsonConvert.DeserializeObject(jToken["data"].ToString()));

                            //TODO: return json data 
                        }
                        else
                        {
                            Toast.MakeText(Application.Context, "Error Fetching Clinic!" + jToken["message"].ToString(), ToastLength.Short).Show();
                        }
                    }
                    else
                    {
                        Toast.MakeText(Application.Context, "Error Fetching Clinic!", ToastLength.Short).Show();
                    }
                }
                else
                {
                    Toast.MakeText(Application.Context, "Error Fetching Clinic! Code: " + response.StatusCode, ToastLength.Short).Show();
                }

            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e);
                Toast.MakeText(Application.Context, "Error: " + e.ToString(), ToastLength.Short).Show();
            }
        }
    }
}