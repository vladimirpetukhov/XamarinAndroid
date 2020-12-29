namespace XamarinAndroid.EventListeners
{
    #region usings
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Firebase.Database;
    using XamarinAndroid.Helpers;
    using XamarinAndroid.Models;
    #endregion

    public class AluminiListeners : Java.Lang.Object, IValueEventListener
    {
        #region fields
        List<Alumini> aluminis = new List<Alumini>();

        public event EventHandler<AluminiDataEventArgs> AluminiRetrieved;

        public class AluminiDataEventArgs : EventArgs
        {
            public List<Alumini> Alumini { get; set; }
        }
        #endregion

        #region methods
        public void OnCancelled(DatabaseError error)
        {
            throw new NotImplementedException();
        }

        public void OnDataChange(DataSnapshot snapshot)
        {
            if (snapshot.Value != null)
            {
                var child = snapshot.Children.ToEnumerable<DataSnapshot>();
                aluminis.Clear();
                foreach (var data in child)
                {
                    Alumini alumini = new Alumini();
                    alumini.ID = data.Key;
                    alumini.FullName = data.Child("fullName").Value.ToString();
                    alumini.Department = data.Child("department").Value.ToString();
                    alumini.Set = data.Child("set").Value.ToString();
                    alumini.Status = data.Child("status").Value.ToString();

                    aluminis.Add(alumini);
                }

                AluminiRetrieved.Invoke(this, new AluminiDataEventArgs { Alumini = aluminis });
            }
        }

        public void Create()
        {
            DatabaseReference aluminiRef = AppDataHelper.GetDatabase().GetReference("alumini");
            aluminiRef.AddValueEventListener(this);
        }

        public void Delete(string key)
        {
            DatabaseReference reference = AppDataHelper.GetDatabase().GetReference("alumini/" + key);
            reference.RemoveValue();
        }
        #endregion
    }
}