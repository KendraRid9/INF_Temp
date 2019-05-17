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

       This activity is used as the point of navigation to the other options
       within the settings. 

   ========================================================================*/
    [Activity(Label = "Settings")]
    public class SettingsActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.SettingsLayout);

            /***************************************************************************
           *
           * GET BUTTONS FROM LAYOUT RESORUCE
           *  - This is so we can sign up for the button's click events        
           *
           **************************************************************************/
            Button FAQButton = FindViewById<Button>(Resource.Id.btnSettingsFAQ);
            Button TermsButton = FindViewById<Button>(Resource.Id.btnSettingsTermsAndConditions);
            Button AboutButton = FindViewById<Button>(Resource.Id.btnSettingsAboutUs);
            Button ContactButton = FindViewById<Button>(Resource.Id.btnSettingsContactUs);

            /***************************************************************************
            *
            * SETUP EVENT HANDLERS FOR THE BUTTONS 
            *  - This will allow certain functions to be bound to certain evetns        
            *
            **************************************************************************/
            FAQButton.Click += new EventHandler(onFAQClicked);
            TermsButton.Click += new EventHandler(onTermsClicked);
            AboutButton.Click += new EventHandler(onAboutClicked);
            ContactButton.Click += new EventHandler(onContactClicked);
        }

        /***************************************************************************
       *
       * FAQ BUTTON EVENT HANDLER 
       *  - When the FAQ button is clicked this function is called 
       *  - This function directs to the FAQActivity
       *
       ***************************************************************************/
        void onFAQClicked(object sender, EventArgs e)
        {
            //If the FAQ button is clicked navigate to the FAQActivity
            Intent intent = new Intent(this, typeof(FAQActivity));
            StartActivity(intent);
        }

        /***************************************************************************
      *
      * TERMS AND CONDITIONS BUTTON EVENT HANDLER 
      *  - When the Terms and Conditions button is clicked this function is called 
      *  - This function directs to the TermsActivity
      *
      ***************************************************************************/
        void onTermsClicked(object sender, EventArgs e)
        {
            //If the Terms and Conditions button is clicked navigate to the TermsActivity
            Intent intent = new Intent(this, typeof(TermsActivity));
            StartActivity(intent);
        }

        /***************************************************************************
      *
      * ABOUT US BUTTON EVENT HANDLER 
      *  - When the about us button is clicked this function is called 
      *  - This function directs to the AboutActivity
      *
      ***************************************************************************/
        void onAboutClicked(object sender, EventArgs e)
        {
            //If the about us button is clicked navigate to the AboutActivity
            Intent intent = new Intent(this, typeof(AboutActivity));
            StartActivity(intent);
        }

        /***************************************************************************
     *
     * CONTACT US BUTTON EVENT HANDLER 
     *  - When the contact us button is clicked this function is called 
     *  - This function directs to the ContactActivity
     *
     ***************************************************************************/
        void onContactClicked(object sender, EventArgs e)
        {
            //If the contact us button is clicked navigate to the ContactActivity
            Intent intent = new Intent(this, typeof(ContactActivity));
            StartActivity(intent);
        }
    }
}