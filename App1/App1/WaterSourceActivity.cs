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
    /*=======================================================================

        This activity is used to display the nearest water sources. The user 
        should be able to click on the location point or info box and be 
        redirected to a page which is dynamically populated with the 
        appropriate info.

    ========================================================================*/
    [Activity(Label = "Water Sources")]
    public class WaterSourceActivity : Activity
    {
        private static JToken jToken;
        private static HttpClient client = new HttpClient();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.WaterSourceLayout);

            //TODO: Add Map functionality

            //TODO: Call API Endpoint and get all water sources

            //Dynamically Added Buttons
            int numWaterSources = 5; //Get this value from the number of water sources that are returned
            addButtons(numWaterSources);
        }

        /***************************************************************************
        *
        * ADD BUTTONS FUNCTION 
        *  - This function handles dynamically adding the button for the water sources navigation     
        *
        ***************************************************************************/
        void addButtons(int num)
        {
            //Retrieve the layout so that we can append UI elements to it
            LinearLayout linearLayout = FindViewById<LinearLayout>(Resource.Id.waterSourcesMapLinearLayout);

            //Loop through each item in the JSON object and create a UI element for each 
            for (int i = 0; i < num; i++)
            {
                //Create Button foreach hospital
                Button button = new Button(this);
                button.Text = "Water Source " + (i + 1);

                //Connect the button to an onclick event handler which is used to send details to the WaterSourcesInfoActivity
                button.Click += new EventHandler(button_click);

                //TODO Uniquely identify these buttons given water sources id so that we can use this to send to the view

                //Define the button layout
                LinearLayout.LayoutParams layoutParams = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent);

                //Set margin between sections
                layoutParams.BottomMargin = 10;

                //Assign the layoutParams
                button.LayoutParameters = layoutParams;

                //Add the button as a child of your ViewGroup
                linearLayout.AddView(button);
            }
        }

        /***************************************************************************
        *
        * BUTTON ONCLICK EVENT HANDLER 
        *  - Used to redirect to the WaterSourcesInfoActivity 
        *  - Sends the water sources ID with it so that the correct water sources Info is populated
        *
        ***************************************************************************/
        protected void button_click(object sender, EventArgs e)
        {
            int id = 0; //TODO Figure out how to send id from addButton function to this event
            Intent intent = new Intent(this, typeof(WaterSourceInfoActivity));
            intent.PutExtra("waterSourceID", id); //Add data which will be sent to the WaterSourcesInfoActivity
            StartActivity(intent);
        }

        /***************************************************************************
        *
        * CALL API FUNCTION 
        *  - This function calls the API endpoint and checks if a positive response 
        *       was received.
        *  - If the response was successful, return the json return 
        *  ?? Return number of water sources? Not water sources info?
        *
        ***************************************************************************/
        public async Task callAPIEndpoint()
        {
            try
            {
                HttpRequestMessage request = new HttpRequestMessage();
                request.RequestUri = new Uri("http://choltechcloudservice.cloudapp.net/api/UsersController/getAllWaterSources");
                request.Method = HttpMethod.Get;
                request.Headers.Add("Accept", "application/json");

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
                            Toast.MakeText(Application.Context, "Water Sources Fetched!", ToastLength.Short).Show();
                            Console.WriteLine("DATA ------------{0}", JsonConvert.DeserializeObject(jToken["data"].ToString()));

                            //TODO: return json data OR num of water sources
                        }
                        else
                        {
                            Toast.MakeText(Application.Context, "Error Fetching Water Sources!" + jToken["message"].ToString(), ToastLength.Short).Show();
                        }
                    }
                    else
                    {
                        Toast.MakeText(Application.Context, "Error Fetching Water Sources!", ToastLength.Short).Show();
                    }
                }
                else
                {
                    Toast.MakeText(Application.Context, "Error Fetching Water Sources! Code: " + response.StatusCode, ToastLength.Short).Show();
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