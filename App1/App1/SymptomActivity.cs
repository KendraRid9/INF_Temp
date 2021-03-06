﻿using System;
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

       This activity is used to display all the information regarding 
       the symptoms of Cholera. This information is pulled from the DB 

   ========================================================================*/
    [Activity(Label = "Symptoms")]
    public class SymptomActivity : Activity
    {
        private static JToken jToken;
        private static HttpClient client = new HttpClient();

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.SymptomLayout);

            //Called to display the info on the screen
            await populateInfoAsync();
        }

        /***************************************************************************
        *
        * POPULATE FUNCTION 
        *  - This function handles the populating of the screen with the info 
        *      received from the API       
        *
        ***************************************************************************/
        async Task populateInfoAsync()
        {
            // DEFINE OBJECT FOR JSON RETURNED
            await callAPIEndpoint();

            /*String[] str = new String[3];

            str[0] = "Working";
            str[1] = "Testing";
            str[2] = "WorkingTestingWorking";*/

            //Retrieve the layout so that we can append UI elements to it
            LinearLayout linearLayout = FindViewById<LinearLayout>(Resource.Id.symptomLinearLayout);

            //Loop through each item in the JSON object and create a UI element for each 
            foreach (var i in jToken["data"])
            {
                Console.WriteLine("--------- HERE ---------");
                //Create TextView for the Title Attribute
                TextView title = new TextView(this);
                title.Text = i["Title"].ToString(); //TODO Change this to array values
                title.TextSize = 20;

                //Create TextView for the Description Attribute
                TextView description = new TextView(this);
                description.Text = i["Description"].ToString(); //TODO Change this to array values
                description.TextSize = 16;

                //Define the TextView layout
                LinearLayout.LayoutParams layoutParams = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent);

                //Set margin between sections
                layoutParams.BottomMargin = 10;

                //Assign the layoutParams
                description.LayoutParameters = layoutParams;

                //Add the TextView as a child of your ViewGroup
                linearLayout.AddView(title);
                linearLayout.AddView(description);

            }
        }

        /***************************************************************************
        *
        * CALL API FUNCTION 
        *  - This function calls the API endpoint and checks if a positive response 
        *       was received.
        *  - If the response was successful, return the json return 
        *
        ***************************************************************************/
        public async Task callAPIEndpoint()
        {
            try
            {
                HttpRequestMessage request = new HttpRequestMessage();
                request.RequestUri = new Uri("http://choltechcloudservice.cloudapp.net/api/CholeraController/getAllSymptoms");
                request.Method = HttpMethod.Get;
                request.Headers.Add("Accept", "application/json");

                HttpResponseMessage response = await client.SendAsync(request);
                if(response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    HttpContent content = response.Content;
                    
                    //Now assign your content to your data variable, by converting into a string using the await keyword.
                    var data = await content.ReadAsStringAsync();
                    //If the data isn't null return log convert the data using newtonsoft JObject Parse class method on the data.
                    if (content != null)
                    {
                        //Now log your data object in the console
                        Console.WriteLine("data ------------{0}", data);
                        jToken = JObject.Parse(data);

                        var success = jToken["success"].ToString();
                        if (success.Equals("true") || success.Equals("True"))
                        {
                            Toast.MakeText(Application.Context, "Symptoms Fetched!", ToastLength.Short).Show();
                            Console.WriteLine("DATA ------------{0}", jToken["data"]);

                            foreach (var i in jToken["data"])
                            {
                                Console.WriteLine("SYMPTOM ------------{0}", i);
                            }

                            //TODO: return json data
                        }
                        else
                        {
                            Toast.MakeText(Application.Context, "Error Fetching Symptoms!" + jToken["message"].ToString(), ToastLength.Short).Show();
                        }
                    }
                    else
                    {
                        Toast.MakeText(Application.Context, "Error Fetching Symptoms!", ToastLength.Short).Show();
                    }
                }
                else
                {
                    Toast.MakeText(Application.Context, "Error Fetching Symptoms! Code: " + response.StatusCode, ToastLength.Short).Show();
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