using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace App1
{
    /*=======================================================================

       This activity is used to display all the users details as well as 
       provide them with the ability to edit their details
   ========================================================================*/
    [Activity(Label = "Profile")]
    public class ProfileActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.ProfileLayout);

            //TODO: Populate text boxes with appropraite user profile details using populateProfile function

            //TODO: Allow onclick edit functionality on each text box - maybe add icons
        }
    }
}