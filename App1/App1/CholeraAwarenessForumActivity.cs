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

        This activity is used to display all the posts relating to a 
        particular forum. This info will need to be pulled from the DB. The 
        user can read the posts and add new posts as well.
            
    ========================================================================*/
    [Activity(Label = "Cholera Awareness Forum")]
    public class CholeraAwarenessForumActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.CholeraAwarenessForumLayout);

            /***************************************************************************
           *
           * GET BUTTONS FROM LAYOUT RESORUCE
           *  - This is so we can sign up for the button's click events        
           *
           **************************************************************************/
            Button addPostButton = FindViewById<Button>(Resource.Id.btnCholeraAwareAddPost);

            /***************************************************************************
            *
            * SETUP EVENT HANDLERS FOR THE BUTTONS 
            *  - This will allow certain functions to be bound to certain evetns        
            *
            **************************************************************************/
            addPostButton.Click += new EventHandler(onAddPostClicked);
        }

        //TODO: Dynamically display all previous posts relating to the forum

        /***************************************************************************
       *
       * ADD POST BUTTON EVENT HANDLER 
       *  - When the add post button is clicked this function is called 
       *  - This function opens a popup form which allows the user to type a message and post it 
       *
       ***************************************************************************/
        void onAddPostClicked(object sender, EventArgs e)
        {
            //If the add post button is clicked a popup form opens which allows the user to type a message and post it 
            //TODO Implement Form popup for adding a forum post
        }

    }
}