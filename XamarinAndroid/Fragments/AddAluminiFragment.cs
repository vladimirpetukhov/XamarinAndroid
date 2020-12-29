using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SupportV7= Android.Support.V7.App;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using FR.Ganfra.Materialspinner;
using Java.Util;
using Firebase.Database;
using XamarinAndroid.Helpers;

namespace XamarinAndroid.Fragments
{   

    public class AddAluminiFragment : Android.Support.V4.App.DialogFragment
    {

        #region fields
        TextInputLayout fullNameText;
        TextInputLayout departmentText;
        TextInputLayout setText;
        MaterialSpinner statusSpinner;
        Button submitButton;

        string status;
        List<string> statusList;
        ArrayAdapter<string> adapter;
        #endregion

        #region ctor
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);           
            
        }
        

        #endregion

        #region methods
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.addalumini, container, false);

            fullNameText = (TextInputLayout)view.FindViewById(Resource.Id.fullNameText);
            departmentText = (TextInputLayout)view.FindViewById(Resource.Id.departmentText);
            setText = (TextInputLayout)view.FindViewById(Resource.Id.setText);
            statusSpinner = (MaterialSpinner)view.FindViewById(Resource.Id.statusSpinner);
            submitButton = (Button)view.FindViewById(Resource.Id.submitButton);

            submitButton.Click += SubmitButton_Click;
            SetUpStatusSpinner();

            return view;
        }

        private void SubmitButton_Click(object sender, EventArgs e)
        {
            string fullName = fullNameText.EditText.Text;
            string department = departmentText.EditText.Text;
            string set = setText.EditText.Text;

            HashMap aluminiInfo = new HashMap();
            aluminiInfo.Put("fullName", fullName);
            aluminiInfo.Put("department", department);
            aluminiInfo.Put("set", set);
            aluminiInfo.Put("status", status);

            SupportV7.AlertDialog.Builder saveDataAlert = new SupportV7.AlertDialog.Builder(Activity);
            saveDataAlert.SetTitle("SAVE ALUMINI INFO");
            saveDataAlert.SetMessage("Are you sure?");
            saveDataAlert.SetPositiveButton("Continue", (senderAlert,args) =>
            {
                DatabaseReference aluminiRef= AppDataHelper.GetDatabase().GetReference("alumini").Push();
                aluminiRef.SetValue(aluminiInfo);
                this.Dismiss();
            });
            saveDataAlert.SetNegativeButton("Cancel", (senderAlert, args) =>
            {
                saveDataAlert.Dispose();
            });

            saveDataAlert.Show();
        }

        private void SetUpStatusSpinner()
        {
            statusList = new List<string>();

            statusList.Add("Graduated");
            statusList.Add("Undergraduate");
            statusList.Add("Dropped Out");
            statusList.Add("Failed");

            adapter = new ArrayAdapter<string>(Activity, Android.Resource.Layout.SimpleSpinnerDropDownItem, statusList);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);

            statusSpinner.Adapter = adapter;
            statusSpinner.ItemSelected += StatusSpinner_ItemSelection;
        }

        private void StatusSpinner_ItemSelection(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if(e.Position!=-1)
            status = statusList[e.Position];
        }
        #endregion
    }
}