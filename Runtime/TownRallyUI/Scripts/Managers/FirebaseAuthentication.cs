using Firebase;
using Firebase.Auth;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace TownRally.UI
{
    internal class FirebaseAuthentication
    {
        internal static EventOut_DisplayAuthMsg EventOut_DisplayAuthMsg = new EventOut_DisplayAuthMsg();
        internal static EventIn_FirebaseLogin EventIn_FirebaseLogin = new EventIn_FirebaseLogin();
        internal static EventIn_FirebaseRegister EventIn_FirebaseRegister = new EventIn_FirebaseRegister();
        internal static EventIn_FirebaseLogout EventIn_FirebaseLogout = new EventIn_FirebaseLogout();

        //private DependencyStatus dependencyStatus;
        private FirebaseAuth auth;
        private FirebaseUser user;
        //private FirebaseApp app;
        private MonoBehaviour mbInitiator = null;

        internal void Init(MonoBehaviour mbInitiator)
        {
            this.mbInitiator = mbInitiator;
            EventIn_FirebaseLogin.AddListener(FirebaseLogin);
            EventIn_FirebaseRegister.AddListener(FirebaseRegister);
            EventIn_FirebaseLogout.AddListener(FirebaseLogout);

            auth = FirebaseAuth.DefaultInstance;
            Debug.Log("FIREBASE AUTHENTICATION DONE!");
        }

        private void FirebaseLogin(List<string> data)
        {
            this.mbInitiator.StartCoroutine(Login(data[0], data[1]));
        }

        private void FirebaseRegister(List<string> data)
        {
            this.mbInitiator.StartCoroutine(Register(data[0], data[1], data[2], data[3]));
        }

        private IEnumerator Login(string email, string password)
        {
            // Call the Firebase auth signin function passing the email and password
            Task<FirebaseUser> LoginTask = auth.SignInWithEmailAndPasswordAsync(email, password);
            // Wait until the task completes
            yield return new WaitUntil(predicate: () => LoginTask.IsCompleted);

            if (LoginTask.Exception != null)
            {
                // if there are errors handle them
                Debug.LogWarning(message: $"Failed to register task with {LoginTask.Exception}");
                FirebaseException firebaseEx = LoginTask.Exception.GetBaseException() as FirebaseException;
                AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

                string message = "Login Failed!";
                switch (errorCode)
                {
                    case AuthError.MissingEmail:
                        message = "Missing Email";
                        break;
                    case AuthError.MissingPassword:
                        message = "Missing Password";
                        break;
                    case AuthError.WrongPassword:
                        message = "Wrong Password";
                        break;
                    case AuthError.InvalidEmail:
                        message = "Invalid Email";
                        break;
                    case AuthError.UserNotFound:
                        message = "Account does not exist";
                        break;
                }
                EventOut_DisplayAuthMsg.Invoke(MessageType.Error, message);
            }
            else
            {
                // user is now logged in
                // now get the result
                user = LoginTask.Result;
                Debug.LogFormat("User signed in successfully: {0} ({1})", user.DisplayName, user.Email);
                EventOut_DisplayAuthMsg.Invoke(MessageType.Info, "Logged In");
                UserManager.EventIn_UserLoggedIn.Invoke(email, password, user.DisplayName, user.UserId);

                // TODO: check if that is really necessary
                // and what happens if there are two users with same name?
                this.mbInitiator.StartCoroutine(UpdateUsernameAuth(user.DisplayName));

                yield return new WaitForSecondsRealtime(1f);
                TownRallyUIMain.EventIn_OpenCanvas.Invoke(CanvasType.SelectRally);
            }
        }

        private IEnumerator Register(string email, string password1, string password2, string username)
        {
            if (username == string.Empty)
            {
                // if the username field is blank show a warning
                EventOut_DisplayAuthMsg.Invoke(MessageType.Warning, "Missing Username");
            }
            else if (!password1.Equals(password2))
            {
                // if the password does not match show a warning
                EventOut_DisplayAuthMsg.Invoke(MessageType.Warning, "Password does not match!");
            }
            else
            {
                // call the firebase auth signin function passing the email and password
                Task<FirebaseUser> registerTask = auth.CreateUserWithEmailAndPasswordAsync(email, password1);
                // wait until the task completes
                yield return new WaitUntil(predicate: () => registerTask.IsCompleted);

                if (registerTask.Exception != null)
                {
                    // if there are errors handle them
                    Debug.LogWarning(message: $"Failed to register task with {registerTask.Exception}");
                    FirebaseException firebaseEx = registerTask.Exception.GetBaseException() as FirebaseException;
                    AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

                    string message = "Register Failed!";
                    switch (errorCode)
                    {
                        case AuthError.MissingEmail:
                            message = "Missing Email";
                            break;
                        case AuthError.MissingPassword:
                            message = "Missing Password";
                            break;
                        case AuthError.WeakPassword:
                            message = "Weak Password";
                            break;
                        case AuthError.EmailAlreadyInUse:
                            message = "Email already in use";
                            break;
                    }
                    EventOut_DisplayAuthMsg.Invoke(MessageType.Error, message);
                }
                else
                {
                    // user has now been created
                    // now get the result
                    user = registerTask.Result;

                    if (user != null)
                    {
                        // create a user profile and set the username
                        UserProfile profile = new UserProfile { DisplayName = username };

                        // call the firebase auth update user profile function passing the profile with the username
                        Task profileTask = user.UpdateUserProfileAsync(profile);
                        // wait until the task completes
                        yield return new WaitUntil(predicate: () => profileTask.IsCompleted);

                        if (profileTask.Exception != null)
                        {
                            // if there are errors handle them
                            Debug.LogWarning(message: $"Failed to register task with {profileTask.Exception}");
                            FirebaseException firebaseEx = profileTask.Exception.GetBaseException() as FirebaseException;
                            AuthError errorCode = (AuthError)(firebaseEx.ErrorCode);
                            EventOut_DisplayAuthMsg.Invoke(MessageType.Error, "Username set failed!");
                        }
                        else
                        {
                            // username is now set
                            // now return to login screen
                            UserManager.EventIn_UserRegistered.Invoke(email, password1, username);
                            yield return new WaitForSecondsRealtime(1f);
                            CanvasLogin.EventIn_SetLoginData.Invoke(email, password1);
                            TownRallyUIMain.EventIn_OpenCanvas.Invoke(CanvasType.Login);
                        }
                    }
                }
            }
        }

        private void FirebaseLogout()
        {
            this.auth.SignOut();
        }

        private IEnumerator UpdateUsernameAuth(string username)
        {
            Debug.Log("UPDATE USERNAME AUTH!");
            // Create a user profile and set the username
            UserProfile profile = new UserProfile { DisplayName = username };

            // Call the Firebase auth update user profile function passing the profile with the username
            Task profileTask = user.UpdateUserProfileAsync(profile);
            // Wait until the task compiles
            yield return new WaitUntil(predicate: () => profileTask.IsCompleted);

            if (profileTask.Exception != null)
            {
                Debug.LogWarning("Failed to register task with " + profileTask.Exception);
            }
            else
            {
                // Auth username is now updated
                Debug.Log("Auth username (" + username + ") is now updated!");
            }
        }
    }
}
