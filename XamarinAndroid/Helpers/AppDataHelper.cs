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
using Firebase;
using Firebase.Database;



namespace XamarinAndroid.Helpers
{
    public static class AppDataHelper
    {
        public static FirebaseDatabase GetDatabase()
        {
            var app = FirebaseApp.InitializeApp(Application.Context);
            FirebaseDatabase database;

            if (app == null)
            {
                var option = new FirebaseOptions.Builder()
                    .SetApplicationId("xamarinfirebase-27ed9")
                    .SetApiKey("AIzaSyCUzu0M3ZRvhVxPRyexjefaWDHsvxBqkb8")
                    .SetDatabaseUrl("https://xamarin-android-62f49-default-rtdb.europe-west1.firebasedatabase.app")
                    .SetStorageBucket("")
                    .Build();
                
                app = FirebaseApp.InitializeApp(Application.Context, option);
                database = FirebaseDatabase.GetInstance(app);
            }
            else
            {
                database = FirebaseDatabase.GetInstance(app);
            }

            return database;
        }
    }
}