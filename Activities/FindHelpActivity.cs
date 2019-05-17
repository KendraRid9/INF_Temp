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

       This activity is used to display the nearest doctors/groups/etc
       as well as their contact details and locations.      

   ========================================================================*/
    [Activity(Label = "Find Help")]
    public class FindHelpActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.FindHelpLayout);
        }

        //TODO: Possibly implement this functionality if we have time
    }

}