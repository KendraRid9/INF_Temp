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

       This activity is used to display the login page

   ========================================================================*/
    [Activity(Label = "Login")]
    public class LoginActivity : Activity
    {
        private static JToken jToken;
        private static HttpClient client = new HttpClient();

        /***************************************************************************
      *
      * DEFINE GLOBAL VARIABLES  
      *  - These will be used in other functions  
      *            
      *   authenticated: This indicates if the user was authenticated          
      *
      **************************************************************************/
        bool authenticated = false;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.LoginPage);

            /***************************************************************************
          *
          * GET DATA THAT IS SENT FROM THE PREVIOUS SCREEN AND PRE-POPULATE FORM      
          *
          **************************************************************************/
            String email = Intent.GetStringExtra("email"); //Gets the data from the previous form 

            if (email != null)
            {
                EditText emailField = FindViewById<EditText>(Resource.Id.txtInputLoginEmail);
                emailField.Text = email; //Sets the value in the form to the one we received from the checkExistingUserActivity
            }


            /***************************************************************************
            *
            * GET BUTTONS FROM LAYOUT RESORUCE
            *  - This is so we can sign up for the button's click events        
            *
            **************************************************************************/
            Button loginButton = FindViewById<Button>(Resource.Id.btnLoginSubmit);

            /***************************************************************************
            *
            * SETUP EVENT HANDLERS FOR THE BUTTONS 
            *  - This will allow certain functions to be bound to certain evetns        
            *
            **************************************************************************/
            loginButton.Click += new EventHandler(onLoginSubmitClicked);
        }

        /***************************************************************************
        *
        * LOGIN BUTTON EVENT HANDLER 
        *  - When the login button is clicked this function is called 
        *  - This function directs to the HomeActivity if successful
        *
        ***************************************************************************/
        async void onLoginSubmitClicked(object sender, EventArgs e)
        {
            /***************************************************************************
            *
            * GET INPUT FIELDS FROM LAYOUT RESORUCE
            *  - This is so we can extract the input details from the form       
            *
            **************************************************************************/
            EditText email = FindViewById<EditText>(Resource.Id.txtInputLoginEmail);
            EditText password = FindViewById<EditText>(Resource.Id.txtInputLoginPassword);

            bool validForm = validateForm((String)email, (String)password);

            if (validForm)
            {
                
                await authenticateUser((String)email, (String)password);
                
                //remove:
                Intent intent = new Intent(this, typeof(HomeActivity));
                StartActivity(intent);

                if (authenticated)
                {
                    //If login is successful we redirect to the home activity
                    //Intent intent = new Intent(this, typeof(HomeActivity));
                    //StartActivity(intent);
                }
                else
                {
                    //Display Error
                    AlertDialog alertDialog2 = new AlertDialog.Builder(this).Create();
                    alertDialog2.SetTitle("Login");
                    alertDialog2.SetMessage("Login was unsuccessful! Retry.");
                    alertDialog2.Show();
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
        bool validateForm(String email, String password)
        {
            //Check if fields don't contain values
            if (email == null && password != null)
            {
                //Display Email Error
                AlertDialog alertDialog = new AlertDialog.Builder(this).Create();
                alertDialog.SetTitle("Form Validation");
                alertDialog.SetMessage("You did not enter an email. Please try again.");
                alertDialog.Show();

                return false;
            }
            else if (password == null && email != null)
            {
                //Display Password Error
                AlertDialog alertDialog = new AlertDialog.Builder(this).Create();
                alertDialog.SetTitle("Form Validation");
                alertDialog.SetMessage("You did not enter a password. Please try again.");
                alertDialog.Show();

                return false;
            }
            else if (password == null && email == null)
            {
                //Display Password Error
                AlertDialog alertDialog = new AlertDialog.Builder(this).Create();
                alertDialog.SetTitle("Form Validation");
                alertDialog.SetMessage("You did not enter an email or password. Please try again.");
                alertDialog.Show();

                return false;
            }

            return true;


            //TODO Check email regex 

            //TODO Check password length - Has to be 8 characters
        }

        /***************************************************************************
      *
      * AUTHENTICATE USER FORM 
      *  - This function calls the CHOLTECH API to check if the user can be authenticated
      *  - If the response from the API is a success return true else return false
      *
      ***************************************************************************/
        
       // return bool
        async Task authenticateUser(String email, String password)
        {
            //TODO Call API and extract and check response - api/UserController/Authenticate

            /*try
            {
                object userInfo = new { email = email, password = password };
                var jsonObj = JsonConvert.SerializeObject(userInfo);
                StringContent postData = new StringContent(jsonObj.ToString(), Encoding.UTF8, "application/json");

                HttpRequestMessage request = new HttpRequestMessage();
                request.RequestUri = new Uri("http://choltechcloudservice.cloudapp.net/api/UsersController/Authenticate");
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
                            Toast.MakeText(Application.Context, "Authenticated!", ToastLength.Short).Show();
                            Console.WriteLine("DATA ------------{0}", JsonConvert.DeserializeObject(jToken["data"].ToString()));

                            //TODO: set if authenticated
                            authenticated = true;
                        }
                        else
                        {
                            Toast.MakeText(Application.Context, "Not Authenticated!" + jToken["message"].ToString(), ToastLength.Short).Show();
                            authenticated = false;
                        }
                    }
                    else
                    {
                        Toast.MakeText(Application.Context, "Error Authenticating User!", ToastLength.Short).Show();
                        authenticated = false;
                    }
                }
                else
                {
                    Toast.MakeText(Application.Context, "Error Authenticating User! Code: " + response.StatusCode, ToastLength.Short).Show();
                    authenticated = false;
                }

            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e);
                Toast.MakeText(Application.Context, "Error: " + e.ToString(), ToastLength.Short).Show();
                authenticated = false;
            }*/

            authenticated = false;

            /*String response = "Success";

            //TODO Error Handling - Check with Kendra with how are doing this
            if ((response != null) && (response == "Success"))
            {
                //TODO Make these more subtle
                //Display Successful Login 
                AlertDialog alertDialog = new AlertDialog.Builder(this).Create();
                alertDialog.SetTitle("Login");
                alertDialog.SetMessage("Login Successful");
                alertDialog.Show();
                return true;
            }

            if ((response != null) && (response == "Faliure"))
            {
                //Display Authentication Error 
                AlertDialog alertDialog = new AlertDialog.Builder(this).Create();
                alertDialog.SetTitle("Authenticatin");
                alertDialog.SetMessage("You did not enter an email or password. Please try again.");
                alertDialog.Show();
            }

            if (response == null)
            {
                //Display No Response Error 
                AlertDialog alertDialog = new AlertDialog.Builder(this).Create();
                alertDialog.SetTitle("Authenticatin");
                alertDialog.SetMessage("No response recevied from the server");
                alertDialog.Show();
            }

            return false;*/
        }

    }
}