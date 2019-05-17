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

       This activity is used as the "splash" page where users can either login
       or signup

   ========================================================================*/
    [Activity(MainLauncher = true, Icon = "@mipmap/icon", Theme = "@android:style/Theme.NoTitleBar")]
    public class MainActivity : Activity
    {
        private static JToken jToken;
        private static HttpClient client = new HttpClient();

        /***************************************************************************
        *
        * DEFINE GLOBAL VARIABLES  
        *  - These will be used in other functions  
        *            
        *   loggedIn: This indicates if the user has already logged in           
        *
        **************************************************************************/
        bool loggedIn = false;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            /***************************************************************************
            *
            * GET BUTTONS FROM LAYOUT RESORUCE
            *  - This is so we can sign up for the button's click events        
            *
            **************************************************************************/
            Button loginButton = FindViewById<Button>(Resource.Id.btnLogin);
            Button signupButton = FindViewById<Button>(Resource.Id.btnSignup);

            /***************************************************************************
            *
            * SETUP EVENT HANDLERS FOR THE BUTTONS 
            *  - This will allow certain functions to be bound to certain evetns        
            *
            **************************************************************************/
            loginButton.Click += new EventHandler(onLoginClicked);
            signupButton.Click += new EventHandler(onSignupClicked);
        }

        /***************************************************************************
         *
         * LOGIN BUTTON EVENT HANDLER 
         *  - When the login button is clicked this function is called 
         *  - This function directs to the LoginActivity or HomeActivity
         *
         ***************************************************************************/
        async void onLoginClicked(object sender, EventArgs e)
        {
            String email = "";
            await checkIfCurrentlyLoggedIn(email);

            if (loggedIn)
            {
                //If already logged in
                Intent intent = new Intent(this, typeof(HomeActivity));
                StartActivity(intent);
            }
            else
            {
                //If not already logged in
                Intent intent = new Intent(this, typeof(LoginActivity));
                StartActivity(intent);
            }
        }

        /***************************************************************************
         *
         * SIGNUP BUTTON EVENT HANDLER 
         *  - When the signup button is clicked this function is called  
         *  - This function directs to the CheckExistingUserActivity
         *
         ***************************************************************************/
        void onSignupClicked(object sender, EventArgs e)
        {
            //Redirect to check if the user already exists
            Intent intent = new Intent(this, typeof(CheckExistingUserActivity));
            StartActivity(intent);
        }

        /***************************************************************************
         *
         * CHECK IF CURRENTLY LOGGED IN FUNCTION
         *  - This function is to check if the user is currently logged in
         *  - Check if a session has been established
         *      - If it has been return true else return false        
         *
         ***************************************************************************/
        async Task checkIfCurrentlyLoggedIn(String email)
        {
            //TODO Check if email is associated with a session ID
            /* bool session = false;

             if (session)
             {
                 return true;
             }

             return false;*/

            // remove
            loggedIn = false;


            /*try
            {
                object userInfo = new { email = email };
                var jsonObj = JsonConvert.SerializeObject(userInfo);
                StringContent postData = new StringContent(jsonObj.ToString(), Encoding.UTF8, "application/json");

                HttpRequestMessage request = new HttpRequestMessage();
                request.RequestUri = new Uri("http://choltechcloudservice.cloudapp.net/api/UsersController/ValidateIDs");
                request.Method = HttpMethod.Post;  //**************** POST/GET ??? *******************
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
                            Toast.MakeText(Application.Context, "User already Logged into a Session!", ToastLength.Short).Show();
                            Console.WriteLine("DATA ------------{0}", JsonConvert.DeserializeObject(jToken["data"].ToString()));

                            //TODO: set if user has a session
                            loggedIn = true;
                        }
                        else
                        {
                            Toast.MakeText(Application.Context, "User not Logged into a Session!" + jToken["message"].ToString(), ToastLength.Short).Show();
                            loggedIn = false;
                        }
                    }
                    else
                    {
                        Toast.MakeText(Application.Context, "Error Checking Session!", ToastLength.Short).Show();
                        loggedIn = false;
                    }
                }
                else
                {
                    Toast.MakeText(Application.Context, "Error Checking Session! Code: " + response.StatusCode, ToastLength.Short).Show();
                    loggedIn = false;
                }

            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e);
                Toast.MakeText(Application.Context, "Error: " + e.ToString(), ToastLength.Short).Show();
                loggedIn = false;
            }*/
        }

    }
}