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

       This activity is used as the central point of navigation for the app

   ========================================================================*/
    [Activity(Label = "Home")]
    public class HomeActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.HomeLayout);

            //TODO: Implement burger menu side nav

            /***************************************************************************
           *
           * GET BUTTONS FROM LAYOUT RESORUCE
           *  - This is so we can sign up for the button's click events        
           *
           **************************************************************************/
            Button clinicButton = FindViewById<Button>(Resource.Id.btnClinicHome);
            Button hospitalButton = FindViewById<Button>(Resource.Id.btnHospitalHome);
            Button waterSourceButton = FindViewById<Button>(Resource.Id.btnWaterSourcesHome);
            Button choleraInfoButton = FindViewById<Button>(Resource.Id.btnCholeraInfoHome);
            Button forumButton = FindViewById<Button>(Resource.Id.btnForumHome);
            Button newsfeedButton = FindViewById<Button>(Resource.Id.btnNewsFeedHome);
            Button findHelpButton = FindViewById<Button>(Resource.Id.btnFindHelpHome);
            Button profileButton = FindViewById<Button>(Resource.Id.btnProfileHome);
            Button settingsButton = FindViewById<Button>(Resource.Id.btnSettingsHome);
            Button logoutButton = FindViewById<Button>(Resource.Id.btnLogoutHome);


            /***************************************************************************
            *
            * SETUP EVENT HANDLERS FOR THE BUTTONS 
            *  - This will allow certain functions to be bound to certain evetns        
            *
            **************************************************************************/
            clinicButton.Click += new EventHandler(onClinicClicked);
            hospitalButton.Click += new EventHandler(onHospitalClicked);
            waterSourceButton.Click += new EventHandler(onWaterSourceClicked);
            choleraInfoButton.Click += new EventHandler(onInfoClicked);
            forumButton.Click += new EventHandler(onForumClicked);
            newsfeedButton.Click += new EventHandler(onNewsFeedClicked);
            findHelpButton.Click += new EventHandler(onFindHelpClicked);
            profileButton.Click += new EventHandler(onProfileClicked);
            settingsButton.Click += new EventHandler(onSettingsClicked);
            logoutButton.Click += new EventHandler(onLogoutClicked);
        }

        /***************************************************************************
       *
       * CLINIC BUTTON EVENT HANDLER 
       *  - When the clinic button is clicked this function is called 
       *  - This function directs to the ClinicActivity
       *
       ***************************************************************************/
        void onClinicClicked(object sender, EventArgs e)
        {
            //If the clinic button is clicked navigate to the ClinicActivity
            Intent intent = new Intent(this, typeof(ClinicActivity));
            StartActivity(intent);
        }

        /***************************************************************************
        *
        * HOSPITAL BUTTON EVENT HANDLER 
        *  - When the hospital button is clicked this function is called 
        *  - This function directs to the HospitalActivity
        *
        ***************************************************************************/
        void onHospitalClicked(object sender, EventArgs e)
        {
            //If the hospital button is clicked navigate to the HospitalActivity
            Intent intent = new Intent(this, typeof(HospitalActivity));
            StartActivity(intent);
        }

        /***************************************************************************
        *
        * WATER SOURCE BUTTON EVENT HANDLER 
        *  - When the water source button is clicked this function is called 
        *  - This function directs to the WaterSourceActivity
        *
        ***************************************************************************/
        void onWaterSourceClicked(object sender, EventArgs e)
        {
            //If the water source button is clicked navigate to the WaterSourceActivity
            Intent intent = new Intent(this, typeof(WaterSourceActivity));
            StartActivity(intent);
        }

        /***************************************************************************
        *
        * CHOLERA INFO BUTTON EVENT HANDLER 
        *  - When the cholera info button is clicked this function is called 
        *  - This function directs to the CholeraInfoActivity
        *
        ***************************************************************************/
        void onInfoClicked(object sender, EventArgs e)
        {
            //If the cholera info button is clicked navigate to the CholeraInfoActivity
            Intent intent = new Intent(this, typeof(CholeraInfoActivity));
            StartActivity(intent);
        }

        /***************************************************************************
        *
        * FORUM BUTTON EVENT HANDLER 
        *  - When the forum button is clicked this function is called 
        *  - This function directs to the ForumActivity
        *
        ***************************************************************************/
        void onForumClicked(object sender, EventArgs e)
        {
            //If the forum button is clicked navigate to the ForumActivity
            Intent intent = new Intent(this, typeof(ForumActivity));
            StartActivity(intent);
        }

        /***************************************************************************
        *
        * NEWS FEED BUTTON EVENT HANDLER 
        *  - When the news feed button is clicked this function is called 
        *  - This function directs to the NewsFeedActivity
        *
        ***************************************************************************/
        void onNewsFeedClicked(object sender, EventArgs e)
        {
            //If the news feed button is clicked navigate to the NewsFeedActivity
            Intent intent = new Intent(this, typeof(NewsFeedActivity));
            StartActivity(intent);
        }

        /***************************************************************************
        *
        * FIND HELP BUTTON EVENT HANDLER 
        *  - When the find help button is clicked this function is called 
        *  - This function directs to the FindHelpActivity
        *
        ***************************************************************************/
        void onFindHelpClicked(object sender, EventArgs e)
        {
            //If the find help button is clicked navigate to the FindHelpActivity
            Intent intent = new Intent(this, typeof(FindHelpActivity));
            StartActivity(intent);
        }

        /***************************************************************************
       *
       * PROFILE BUTTON EVENT HANDLER 
       *  - When the profile button is clicked this function is called 
       *  - This function directs to the ProfileActivity
       *
       ***************************************************************************/
        void onProfileClicked(object sender, EventArgs e)
        {
            //If the profile button is clicked navigate to the ProfileActivity
            Intent intent = new Intent(this, typeof(ProfileActivity));
            StartActivity(intent);
        }

        /***************************************************************************
      *
      * SETTINGS BUTTON EVENT HANDLER 
      *  - When the settings button is clicked this function is called 
      *  - This function directs to the SettingsActivity
      *
      ***************************************************************************/
        void onSettingsClicked(object sender, EventArgs e)
        {
            //If the settings button is clicked navigate to the SettingsActivity
            Intent intent = new Intent(this, typeof(SettingsActivity));
            StartActivity(intent);
        }

        /***************************************************************************
     *
     * LOGOUT BUTTON EVENT HANDLER 
     *  - When the logout button is clicked this function is called 
     *  - This function directs to the MainActivity
     *
     ***************************************************************************/
        void onLogoutClicked(object sender, EventArgs e)
        {
            //TODO: Create a function that logouts the user out thereby destroy sessions, etc
            bool sessionDestroyed = true; //This will changed based on the logoutUser function

            if (sessionDestroyed)
            {
                //If the logout button is clicked navigate to the MainActivity
                Intent intent = new Intent(this, typeof(MainActivity));
                StartActivity(intent);
            }
            else
            {
                //TODO Contain this within the logoutUser function instead with other checks for session destruction
                //Display Error
                AlertDialog alertDialog = new AlertDialog.Builder(this).Create();
                alertDialog.SetTitle("Logout");
                alertDialog.SetMessage("We could not log you out. Please retry.");
                alertDialog.Show();
            }
        }
    }
}