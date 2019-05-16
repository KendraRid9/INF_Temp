﻿using System;
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

       This activity is used to display the latest news relating to Cholera.

   ========================================================================*/
    [Activity(Label = "News Feed")]
    public class NewsFeedActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.NewsFeedLayout);
        }

        //TODO: Subscribe to RSS feed possibly 

        //TODO: Display latest news feed
    }
}