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

        This activity is used to provide a point of navigation for all the 
        topics relating to the cholera disease.        
            
    ========================================================================*/
    [Activity(Label = "Cholera Information")]
    public class CholeraInfoActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.CholeraInfoLayout);

            /***************************************************************************
           *
           * GET BUTTONS FROM LAYOUT RESORUCE
           *  - This is so we can sign up for the button's click events        
           *
           **************************************************************************/
            Button symptomsButton = FindViewById<Button>(Resource.Id.btnSymptoms);
            Button causesButton = FindViewById<Button>(Resource.Id.btnCauses);
            Button preventionsButton = FindViewById<Button>(Resource.Id.btnPreventionMethods);
            Button purificationButton = FindViewById<Button>(Resource.Id.btnPurification);
            Button treatmentsButton = FindViewById<Button>(Resource.Id.btnTreatments);
            Button riskFactorButton = FindViewById<Button>(Resource.Id.btnRiskFactors);

            /***************************************************************************
            *
            * SETUP EVENT HANDLERS FOR THE BUTTONS 
            *  - This will allow certain functions to be bound to certain evetns        
            *
            **************************************************************************/
            symptomsButton.Click += new EventHandler(onSymptomsClicked);
            causesButton.Click += new EventHandler(onCausesClicked);
            preventionsButton.Click += new EventHandler(onPreventionClicked);
            purificationButton.Click += new EventHandler(onPurificationClicked);
            treatmentsButton.Click += new EventHandler(onTreatmentClicked);
            riskFactorButton.Click += new EventHandler(onRiskFactorClicked);
        }

        /***************************************************************************
       *
       * SYMPTOMS BUTTON EVENT HANDLER 
       *  - When the symptoms button is clicked this function is called 
       *  - This function directs to the SymptomActivity
       *
       ***************************************************************************/
        void onSymptomsClicked(object sender, EventArgs e)
        {
            //If the symptoms button is clicked navigate to the SymptomActivity
            Intent intent = new Intent(this, typeof(SymptomActivity));
            StartActivity(intent);
        }

        /***************************************************************************
        *
        * CAUSES BUTTON EVENT HANDLER 
        *  - When the causes button is clicked this function is called 
        *  - This function directs to the CausesActivity
        *
        ***************************************************************************/
        void onCausesClicked(object sender, EventArgs e)
        {
            //If the causes button is clicked navigate to the CausesActivity
            Intent intent = new Intent(this, typeof(CausesActivity));
            StartActivity(intent);
        }

        /***************************************************************************
        *
        * PREVENTION METHODS BUTTON EVENT HANDLER 
        *  - When the prevention methods button is clicked this function is called 
        *  - This function directs to the PreventionMethodsActivity
        *
        ***************************************************************************/
        void onPreventionClicked(object sender, EventArgs e)
        {
            //If the prevention methods button is clicked navigate to the PreventionMethodsActivity
            Intent intent = new Intent(this, typeof(PreventionMethodsActivity));
            StartActivity(intent);
        }

        /***************************************************************************
        *
        * PURIFICATION METHODS BUTTON EVENT HANDLER 
        *  - When the purification methods button is clicked this function is called 
        *  - This function directs to the PurificationMethodsActivity
        *
        ***************************************************************************/
        void onPurificationClicked(object sender, EventArgs e)
        {
            //If the purification methods button is clicked navigate to the PurificationMethodsActivity
            Intent intent = new Intent(this, typeof(PurificationMethodsActivity));
            StartActivity(intent);
        }

        /***************************************************************************
        *
        * TREATMENT BUTTON EVENT HANDLER 
        *  - When the treatment button is clicked this function is called 
        *  - This function directs to the TreatmentsActivity
        *
        ***************************************************************************/
        void onTreatmentClicked(object sender, EventArgs e)
        {
            //If the treatments button is clicked navigate to the TreatmentsActivity
            Intent intent = new Intent(this, typeof(TreatmentsActivity));
            StartActivity(intent);
        }

        /***************************************************************************
        *
        * RISK FACTOR BUTTON EVENT HANDLER 
        *  - When the risk factor button is clicked this function is called 
        *  - This function directs to the RiskFactorActivity
        *
        ***************************************************************************/
        void onRiskFactorClicked(object sender, EventArgs e)
        {
            //If the risk factor button is clicked navigate to the RiskFactorActivity
            Intent intent = new Intent(this, typeof(RiskFactorsActivity));
            StartActivity(intent);
        }
    }
}