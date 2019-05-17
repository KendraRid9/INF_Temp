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

       This activity is used to check if the user already exists in the system 
       via a check done on their email

   ========================================================================*/
    [Activity(Label = "Enter Email")]
    public class CheckExistingUserActivity : Activity
    {
        private static JToken jToken;
        private static HttpClient client = new HttpClient();

        /***************************************************************************
       *
       * DEFINE GLOBAL VARIABLES  
       *  - These will be used in other functions  
       *            
       *   existingUser: This indicates if the user already exists within the system           
       *
       **************************************************************************/
        bool existingUser = false;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.CheckExistingUserLayout);

            /***************************************************************************
           *
           * GET BUTTONS FROM LAYOUT RESORUCE
           *  - This is so we can sign up for the button's click events        
           *
           **************************************************************************/
            Button continueButton = FindViewById<Button>(Resource.Id.btnCheckUserContinue);

            /***************************************************************************
            *
            * SETUP EVENT HANDLERS FOR THE BUTTONS 
            *  - This will allow certain functions to be bound to certain evetns        
            *
            **************************************************************************/
            continueButton.Click += new EventHandler(onContinueClicked);
        }

        /***************************************************************************
        *
        * CONTINUE BUTTON EVENT HANDLER 
        *  - When the continue button is clicked this function is called 
        *  - This function directs to the SignupActivity or LoginActivity
        *
        ***************************************************************************/
        async void onContinueClicked(object sender, EventArgs e)
        {
            /***************************************************************************
            *
            * GET INPUT FIELDS FROM LAYOUT RESORUCE
            *  - This is so we can extract the input details from the form       
            *
            **************************************************************************/
            String email = FindViewById<EditText>(Resource.Id.txtInputCheckUserEmail).Text;

            bool validForm = validateForm(email);

            if (validForm)
            {
                await checkExisitingUser(email);

                if (existingUser == false)
                {
                    //If the user does not exist then we redirect to the signup activity
                    Intent intent = new Intent(this, typeof(SignupActivity));
                    intent.PutExtra("email", email); //Add data which will be sent to the SignupActivity
                    StartActivity(intent);
                }
                else
                {
                    //Display Error
                    AlertDialog alertDialog = new AlertDialog.Builder(this).Create();
                    alertDialog.SetTitle("Signup");
                    alertDialog.SetMessage("You are already a user. Rather login.");
                    alertDialog.Show();

                    //Redirect to the LoginActivity so that the existing user can now login intead
                    Intent intent = new Intent(this, typeof(LoginActivity));
                    intent.PutExtra("email", email); //Add data which will be sent to the LoginActivity
                    StartActivity(intent);
                }
            }


        }

        /***************************************************************************
      *
      * VALIDATE FORM 
      *  - This function checks that the form is correctly filled out before details
      *    are sent to the DB       
      *
      ***************************************************************************/
        bool validateForm(String email)
        {
            //Check if fields don't contain values
            if (email == null)
            {
                //Display Email Error
                AlertDialog alertDialog = new AlertDialog.Builder(this).Create();
                alertDialog.SetTitle("Form Validation");
                alertDialog.SetMessage("You did not enter an email. Please try again.");
                alertDialog.Show();

                return false;
            }

            return true;
        }

        /***************************************************************************
        *
        * CHECK IF THE USER ALREADY EXISTS
        *  - This function is to check if the user already exisits in the DB
        *  - If the user does then return true else return false
        *
        ***************************************************************************/
        async Task checkExisitingUser(String email)
        {
            try
            {
                object userInfo = new { email = email };
                var jsonObj = JsonConvert.SerializeObject(userInfo);
                StringContent postData = new StringContent(jsonObj.ToString(), Encoding.UTF8, "application/json");

                HttpRequestMessage request = new HttpRequestMessage();
                request.RequestUri = new Uri("http://choltechcloudservice.cloudapp.net/api/UsersController/AuthenticateEmail");
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
                            Toast.MakeText(Application.Context, "User Exists!", ToastLength.Short).Show();
                            Console.WriteLine("DATA ------------{0}", JsonConvert.DeserializeObject(jToken["data"].ToString()));

                            //TODO: set if user exists
                            existingUser = true;
                        }
                        else
                        {
                            Toast.MakeText(Application.Context, "User Does Not Exist!" + jToken["message"].ToString(), ToastLength.Short).Show();
                            existingUser = false;
                        }
                    }
                    else
                    {
                        Toast.MakeText(Application.Context, "Error Checking Email!", ToastLength.Short).Show();
                        existingUser = false;
                    }
                }
                else
                {
                    Toast.MakeText(Application.Context, "Error Checking Email! Code: " + response.StatusCode, ToastLength.Short).Show();
                    existingUser = false;
                }

            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e);
                Toast.MakeText(Application.Context, "Error: " + e.ToString(), ToastLength.Short).Show();
                existingUser = false;
            }
            //TODO Check if email exists - API call
            /*bool exists = false;

            if (exists)
            {
                return true;
            }

            return false;*/
        }
    }
}