using CodeEvents;
using Firebase.Database;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using Newtonsoft.Json;
using System;

namespace TownRally.UI
{
    internal class FirebaseDB
    {
        internal static EventIn_SaveObjectData EventIn_SaveObjectData = new EventIn_SaveObjectData();
        internal static EventIn_RequestData EventIn_RequestData = new EventIn_RequestData();

        private DatabaseReference dbReference;
        private MonoBehaviour mbInitiator;

        internal void Init(MonoBehaviour mbInitiator)
        {
            this.mbInitiator = mbInitiator;
            this.dbReference = FirebaseDatabase.DefaultInstance.RootReference;

            EventIn_SaveObjectData.AddListener(SaveObjectData);
            EventIn_RequestData.AddListener(RequestData);
        }

        //private void SaveDataButton(string username)
        //{
        //    //StartCoroutine(UpdateUsernameAuth(username));
        //    this.mbInitiator.StartCoroutine(UpdateUsernameDatabase(username));
        //}

        private void SaveObjectData(string path, string jsonString)
        {
            Debug.Log("PATH: " + path + " " + jsonString);
            this.mbInitiator.StartCoroutine(SaveObjectDataAsync(path, jsonString));
        }
        private IEnumerator SaveObjectDataAsync(string path, string jsonString)
        {
            Task dbTask = this.dbReference.Child(path).SetRawJsonValueAsync(jsonString);
            yield return new WaitUntil(predicate: () => dbTask.IsCompleted);

            if (dbTask.Exception != null)
            {
                Debug.LogWarning("Failed to register task with " + dbTask.Exception);
            }
            else
            {
                // database username is now updated
            }
        }

        private void RequestData(string path, Action<string> responseEvent)
        {
            this.mbInitiator.StartCoroutine(RequestDataAsync(path, responseEvent));
        }
        private IEnumerator RequestDataAsync(string path, Action<string> responseEvent)
        {
            Task<DataSnapshot> dbTask = this.dbReference.Child(path).GetValueAsync();
            yield return new WaitUntil(predicate: () => dbTask.IsCompleted);

            if (dbTask.Exception != null)
            {
                Debug.LogWarning("Failed to register task with " + dbTask.Exception);
            }
            else
            {
                responseEvent.Invoke(JsonConvert.SerializeObject(dbTask.Result.Value));
            }
        }

        //private IEnumerator UpdateUsernameDatabase(string username)
        //{

        //    Task dbTask = this.dbReference.Child("users").Child(UserManager.VarOut_UserID).Child("username").SetValueAsync(username);

        //    yield return new WaitUntil(predicate: () => dbTask.IsCompleted);

        //    if (dbTask.Exception != null)
        //    {
        //        Debug.LogWarning("Failed to register task with " + dbTask.Exception);
        //    }
        //    else
        //    {
        //        // database username is now updated
        //    }
        //}

        //private IEnumerator UpdateKills(int kills)
        //{
        //    Task dbTask = this.dbReference.Child("users").Child(UserManager.VarOut_UserID).Child("kills").SetValueAsync(kills);

        //    yield return new WaitUntil(predicate: () => dbTask.IsCompleted);

        //    if (dbTask.Exception != null)
        //    {
        //        Debug.LogWarning("Failed database " + dbTask.Exception);
        //    }
        //    else
        //    {
        //        // database updated
        //    }
        //}

        //private IEnumerator LoadUserData()
        //{
        //    var dbTask = this.dbReference.Child("users").Child(UserManager.VarOut_UserID).GetValueAsync();

        //    yield return new WaitUntil(predicate: () => dbTask.IsCompleted);

        //    if (dbTask.Exception != null)
        //    {
        //        Debug.LogWarning("Failed to register task with " + dbTask.Exception);
        //    }
        //    else if (dbTask.Result.Value == null)
        //    {

        //    }
        //    else
        //    {
        //        DataSnapshot snapshot = dbTask.Result;
        //        string data = snapshot.Child("kills").Value.ToString();
        //    }
        //}

        //private IEnumerator LoadScoreboardData()
        //{
        //    Task<DataSnapshot> dbTask = this.dbReference.Child("users").OrderByChild("kills").GetValueAsync();

        //    yield return new WaitUntil(predicate: () => dbTask.IsCompleted);

        //    if (dbTask == null)
        //    {
        //        Debug.LogWarning("Failed to register task with " + dbTask.Exception);
        //    }
        //    else
        //    {
        //        // data has been retrieved
        //        DataSnapshot snapshot = dbTask.Result;

        //        // loop results
        //        foreach (DataSnapshot childSnapshot in snapshot.Children.Reverse<DataSnapshot>())
        //        {

        //        }
        //    }
        //}
    }
}
