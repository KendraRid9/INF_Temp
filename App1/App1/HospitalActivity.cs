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

      This activity is used to display the nearest hospitals. The user should 
      be able to click on the location point or info box and be redirected 
      to a page which is dynamically populated with the appropriate info.

  ========================================================================*/
    [Activity(Label = "Hospitals")]
    public class HospitalActivity : Activity
    {
        private static JToken jToken;
        private static HttpClient client = new HttpClient();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.HospitalLayout);

            //TODO: Add Map functionality

            //TODO: Call API Endpoint and get all hospitals

            //Dynamically Added Buttons
            int numHospitals = 5; //Get this value from the number of hospitals that are returned
            addButtons(numHospitals);
        }

        /***************************************************************************
        *
        * ADD BUTTONS FUNCTION 
        *  - This function handles dynamically adding the button for the hospital navigation     
        *
        ***************************************************************************/
        void addButtons(int num)
        {
            //Retrieve the layout so that we can append UI elements to it
            LinearLayout linearLayout = FindViewById<LinearLayout>(Resource.Id.hospitalMapLinearLayout);

            //Loop through each item in the JSON object and create a UI element for each 
            for (int i = 0; i < num; i++)
            {
                //Create Button foreach hospital
                Button button = new Button(this);
                button.Text = "Hospital " + (i + 1);

                //Connect the button to an onclick event handler which is used to send details to the HospitalInfoActivity
                button.Click += new EventHandler(button_click);

                //TODO Uniquely identify these buttons given hospital id so that we can use this to send to the view

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
        *  - Used to redirect to the HospitalInfoActivity 
        *  - Sends the hospital ID with it so that the correct hospital Info is populated
        *
        ***************************************************************************/
        protected void button_click(object sender, EventArgs e)
        {
            int id = 0; //TODO Figure out how to send id from addButton function to this event
            Intent intent = new Intent(this, typeof(HospitalInfoActivity));
            intent.PutExtra("hospitalID", id); //Add data which will be sent to the HospitalInfoActivity
            StartActivity(intent);
        }

        /***************************************************************************
        *
        * CALL API FUNCTION 
        *  - This function calls the API endpoint and checks if a positive response 
        *       was received.
        *  - If the response was successful, return the json return 
        *  ?? Return number of hospitals? Not hospital info?
        *
        ***************************************************************************/
        public async Task callAPIEndpoint()
        {
            try
            {
                HttpRequestMessage request = new HttpRequestMessage();
                request.RequestUri = new Uri("http://choltechcloudservice.cloudapp.net/api/UsersController/getAllHospitals");
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
                            Toast.MakeText(Application.Context, "Hospitals Fetched!", ToastLength.Short).Show();
                            Console.WriteLine("DATA ------------{0}", JsonConvert.DeserializeObject(jToken["data"].ToString()));

                            //TODO: return json data OR num of hospitals
                        }
                        else
                        {
                            Toast.MakeText(Application.Context, "Error Fetching Hospitals!" + jToken["message"].ToString(), ToastLength.Short).Show();
                        }
                    }
                    else
                    {
                        Toast.MakeText(Application.Context, "Error Fetching Hospitals!", ToastLength.Short).Show();
                    }
                }
                else
                {
                    Toast.MakeText(Application.Context, "Error Fetching Hospitals! Code: " + response.StatusCode, ToastLength.Short).Show();
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