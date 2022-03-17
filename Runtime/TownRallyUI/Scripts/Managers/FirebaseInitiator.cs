using Firebase;
using System;
using UnityEngine;

namespace TownRally.UI
{
    internal class FirebaseInitiator : AbstractManager
    {
        private DependencyStatus dependencyStatus;
        private FirebaseAuthentication auth;
        private FirebaseDB database;

        internal override void Init()
        {
            auth = new FirebaseAuthentication();
            database = new FirebaseDB();

            // Check that all of the necessary dependencies for Firebase are present on the system
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
            {
                dependencyStatus = task.Result;
                if (dependencyStatus == DependencyStatus.Available)
                {
                    // If they are available initialize Firebase
                    Debug.Log("Setting up Firebase Auth");
                    // Set the authentication instance object
                    try
                    {
                        this.auth.Init(this);
                        this.database.Init(this);
                    }
                    catch(Exception ex)
                    {
                        Debug.Log("ERROR: " + ex.Message);
                    }
                    //auth = FirebaseAuth.DefaultInstance;
                }
                else
                {
                    Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
                }
            });
        }
    }
}
