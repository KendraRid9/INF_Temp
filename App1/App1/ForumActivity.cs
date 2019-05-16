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

       This activity is used to display the available forums and provide the 
       user with the option to join/view those forums

   ========================================================================*/
    [Activity(Label = "Forum")]
    public class ForumActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.ForumLayout);

            /***************************************************************************
           *
           * GET BUTTONS FROM LAYOUT RESORUCE
           *  - This is so we can sign up for the button's click events        
           *
           **************************************************************************/
            Button choleraAwareJoinButton = FindViewById<Button>(Resource.Id.btnCholeraAwareJoin);
            Button waterRestrictionsJoinButton = FindViewById<Button>(Resource.Id.btnWaterRestrictionsJoin);

            /***************************************************************************
            *
            * SETUP EVENT HANDLERS FOR THE BUTTONS 
            *  - This will allow certain functions to be bound to certain evetns        
            *
            **************************************************************************/
            choleraAwareJoinButton.Click += new EventHandler(onCholeraAwareJoinClicked);
            waterRestrictionsJoinButton.Click += new EventHandler(onWaterRestrictionsJoinClicked);
        }

        /***************************************************************************
       *
       * CHOLERA AWARENESS JOIN BUTTON EVENT HANDLER 
       *  - When the cholera awareness join button is clicked this function is called 
       *  - This function directs to the CholeraAwarenessForumActivity
       *
       ***************************************************************************/
        void onCholeraAwareJoinClicked(object sender, EventArgs e)
        {
            //If the cholera awareness join button is clicked navigate to the CholeraAwarenessForumActivity
            Intent intent = new Intent(this, typeof(CholeraAwarenessForumActivity));
            StartActivity(intent);
        }

        /***************************************************************************
        *
        * WATER RESTRICTIONS JOIN BUTTON EVENT HANDLER 
        *  - When the water restrictions join button is clicked this function is called 
        *  - This function directs to the WaterRestrictionsForumActivity
        *
        ***************************************************************************/
        void onWaterRestrictionsJoinClicked(object sender, EventArgs e)
        {
            //If the water restrictions join  button is clicked navigate to the WaterRestrictionsForumActivity
            Intent intent = new Intent(this, typeof(WaterRestrictionsForumActivity));
            StartActivity(intent);
        }
    }
}