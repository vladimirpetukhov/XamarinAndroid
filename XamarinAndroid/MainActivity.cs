namespace XamarinAndroid
{
    #region usings
    using Android.App;
    using Android.OS;
    using Android.Support.V7.App;
    using Android.Runtime;
    using Android.Widget;
    using Android.Support.V7.Widget;
    using System;
    using XamarinAndroid.Adapter;
    using XamarinAndroid.Models;
    using System.Collections.Generic;
    using System.Linq;
    using XamarinAndroid.Fragments;
    using XamarinAndroid.EventListeners;
    using Android.Text;
    #endregion

    [Activity(Label = "", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        #region props
        ImageView _searchBtn;
        ImageView _addBtn;
        EditText _editTxt;
        EditText _searchTxt;
        RecyclerView _recyclerView;
        List<Alumini> AluminiList;


        AddAluminiFragment _addAluminiFragment;
        AluminiListeners _aluminiListeners;
        AluminiAdapter _aluminiAdapter;

        #endregion

        #region methods
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            
            SetContentView(Resource.Layout.activity_main);

            Initialize();

            RetrieveData();

        }

        private void SetupRecyclerView()
        {
            //Add items to linear format
            _recyclerView.SetLayoutManager(new Android.Support.V7.Widget.LinearLayoutManager(_recyclerView.Context));
            this._aluminiAdapter = new AluminiAdapter(AluminiList);
            this._aluminiAdapter.DeleteItemClick += Adapter_DeleteItemClick;
            _recyclerView.SetAdapter(this._aluminiAdapter);
        }

        private void Adapter_DeleteItemClick(object sender, AluminiAdapterClickEventArgs e)
        {
            string key = AluminiList[e.Position].ID;

            Android.Support.V7.App.AlertDialog.Builder deleteAlumini = new Android.Support.V7.App.AlertDialog.Builder(this);
            deleteAlumini.SetTitle("DELETE ALUMINI");
            deleteAlumini.SetMessage("Are you sure?");
            deleteAlumini.SetPositiveButton("Delete", (delete, args) =>
            {
                _aluminiListeners.Delete(key);
            });
            deleteAlumini.SetNegativeButton("Cancel", (delete, args) =>
            {
                _aluminiListeners.Dispose();
            });

            deleteAlumini.Show();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public void RetrieveData()
        {
            _aluminiListeners = new AluminiListeners();
            _aluminiListeners.Create();
            _aluminiListeners.AluminiRetrieved += AluminiListener_AluminiRetrieved;
        }

        private void AluminiListener_AluminiRetrieved(object sender, AluminiListeners.AluminiDataEventArgs e)
        {
            AluminiList = e.Alumini;
            SetupRecyclerView();
        }

        private void Initialize()
        {
            this._recyclerView = (RecyclerView)FindViewById(Resource.Id.recyclerView1);
            this._searchBtn = (ImageView)FindViewById(Resource.Id.searchBtn);
            this._addBtn = (ImageView)FindViewById(Resource.Id.addBtn);
            this._editTxt = (EditText)FindViewById(Resource.Id.editTxt);

            _editTxt.TextChanged += Search_Text;
            this._searchBtn.Click += _searchBtn_Click;
            this._addBtn.Click += _addBtn_Click;
            
        }

        private void Search_Text(object sender, TextChangedEventArgs e)
        {// ssss ljkjhjlghhjg
            List<Alumini> searchResult = (from alumini in AluminiList
                                          where
                                          alumini.FullName.ToLower().Contains(_editTxt.Text.ToLower()) ||
                                          alumini.Set.ToLower().Contains(_editTxt.Text.ToLower()) ||
                                          alumini.Department.ToLower().Contains(_editTxt.Text.ToLower()) ||
                                          alumini.Status.ToLower().Contains(_editTxt.Text.ToLower())
                                          select alumini).ToList();

            this._aluminiAdapter = new AluminiAdapter(searchResult);
            this._recyclerView.SetAdapter(this._aluminiAdapter);                                        
        }

        private void _addBtn_Click(object sender, System.EventArgs e)
        {
            _addAluminiFragment = new AddAluminiFragment();
            var trans = SupportFragmentManager.BeginTransaction();

            _addAluminiFragment.Show(trans, "add alumini");
        }

        private void _searchBtn_Click(object sender, System.EventArgs e)
        {
            if (this._editTxt.Visibility == Android.Views.ViewStates.Gone)
            {
                this._editTxt.Visibility = Android.Views.ViewStates.Visible;
            }
            else
            {
                this._editTxt.ClearFocus();
                this._editTxt.Visibility = Android.Views.ViewStates.Gone;
            }
        }
        #endregion
    }
}