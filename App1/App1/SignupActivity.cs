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

       This activity is used to display the signup page

   ========================================================================*/
    [Activity(Label = "Register")]
    public class SignupActivity : Activity
    {
        private static JToken jToken;
        private static HttpClient client = new HttpClient();

        /***************************************************************************
      *
      * DEFINE GLOBAL VARIABLES  
      *  - These will be used in other functions  
      *            
      *   successfulRegistration: This indicates if the user was succesfully registered          
      *
      **************************************************************************/
        bool successfulRegistration = false;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.SignupLayout);

            /***************************************************************************
             *
             * GET DATA THAT IS SENT FROM THE PREVIOUS SCREEN AND PRE-POPULATE FORM      
             *
             **************************************************************************/
            String email = Intent.GetStringExtra("email"); //Gets the data from the previous form 

            if (email != null)
            {
                EditText emailField = FindViewById<EditText>(Resource.Id.txtInputSignupEmail);
                emailField.Text = email; //Sets the value in the form to the one we received from the checkExistingUserActivity
            }


            /***************************************************************************
            *
            * GET BUTTONS FROM LAYOUT RESORUCE
            *  - This is so we can sign up for the button's click events        
            *
            **************************************************************************/
            Button signupButton = FindViewById<Button>(Resource.Id.btnRegister);

            /***************************************************************************
            *
            * SETUP EVENT HANDLERS FOR THE BUTTONS 
            *  - This will allow certain functions to be bound to certain evetns        
            *
            **************************************************************************/
            signupButton.Click += new EventHandler(onRegisterClicked);
        }

        /***************************************************************************
        *
        * SIGNUP BUTTON EVENT HANDLER 
        *  - When the register button is clicked this function is called 
        *  - This function directs to the HomeActivity
        *
        ***************************************************************************/
        async void onRegisterClicked(object sender, EventArgs e)
        {
            String email = FindViewById<EditText>(Resource.Id.txtInputSignupEmail).Text;
            String name = FindViewById<EditText>(Resource.Id.txtInputName).Text;
            String surname = FindViewById<EditText>(Resource.Id.txtInputSurname).Text;
            String location = "SA";
            //String location = FindViewById<EditText>(Resource.Id.dropInputLocation).Text;
            String password = FindViewById<EditText>(Resource.Id.txtInputSignupPassword).Text;
            String confirmPassword = FindViewById<EditText>(Resource.Id.txtInputConfirmSignupPassword).Text;

            bool validForm = validateForm(email, name, surname, location, password, confirmPassword);

            if (validForm)
            {
                //Retrieve User Details so that they can be added to the DB 
                await registerUser(email, name, surname, location, password);

                if (successfulRegistration)
                {
                    //If validation and creation is successful we redirect to the home activity
                    Intent intent = new Intent(this, typeof(HomeActivity));
                    StartActivity(intent);
                }
                else
                {
                    //Display Registration Error
                    AlertDialog alertDialog = new AlertDialog.Builder(this).Create();
                    alertDialog.SetTitle("Registration Error");
                    alertDialog.SetMessage("We could not create your profile at this time. Please try again later.");
                    alertDialog.Show();
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
        bool validateForm(String email, String name, String surname, String location, String password, String confirmPassword)
        {
            //Check if fields don't contain values
            if (email == null || name == null || surname == null || location == null || password == null || confirmPassword == null)
            {
                //Display Missing Info Error
                AlertDialog alertDialog = new AlertDialog.Builder(this).Create();
                alertDialog.SetTitle("Form Validation");
                alertDialog.SetMessage("You did not enter all the required information. Please try again.");
                alertDialog.Show();

                return false;
            }

            //Check that the passwords match 
            if (password != confirmPassword)
            {
                //Display Incorrect Password Match
                AlertDialog alertDialog = new AlertDialog.Builder(this).Create();
                alertDialog.SetTitle("Form Validation");
                alertDialog.SetMessage("Your passwords did not match. Please try again.");
                alertDialog.Show();

                return false;
            }

            return true;

            //TODO Check email regex 
        }

        /***************************************************************************
         *
         * REGISTER USER  
         *  - This function registers the user on the system
         *
         ***************************************************************************/
        async Task registerUser(String email, String name, String surname, String location, String password)
        {
            //TODO Call API 

            /*String response = "Success";
            if (response == "Success")
            {
                return true;
            }

            return false;*/
            try
            {
                object userInfo = new { fname = name, lname = surname, email = email, phone = 0721765005, birthday = "", region_id = 1, type_id = 2, password = password };
                var jsonObj = JsonConvert.SerializeObject(userInfo);
                StringContent postData = new StringContent(jsonObj.ToString(), Encoding.UTF8, "application/json");

                HttpRequestMessage request = new HttpRequestMessage();
                request.RequestUri = new Uri("http://choltechcloudservice.cloudapp.net/api/UsersController/addUser");
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
                            Toast.MakeText(Application.Context, "Successfully Registered!", ToastLength.Short).Show();
                            Console.WriteLine("DATA ------------{0}", JsonConvert.DeserializeObject(jToken["data"].ToString()));

                            //TODO: set if registered
                            successfulRegistration = true;
                        }
                        else
                        {
                            Toast.MakeText(Application.Context, "Error Registering User!" + jToken["message"].ToString(), ToastLength.Short).Show();
                            successfulRegistration = false;
                        }
                    }
                    else
                    {
                        Toast.MakeText(Application.Context, "Error Registering User!", ToastLength.Short).Show();
                        successfulRegistration = false;
                    }
                }
                else
                {
                    Toast.MakeText(Application.Context, "Error Registering User! Code: " + response.StatusCode, ToastLength.Short).Show();
                    successfulRegistration = false;
                }

            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e);
                Toast.MakeText(Application.Context, "Error: " + e.ToString(), ToastLength.Short).Show();
                successfulRegistration = false;
            }
        }
    }
}