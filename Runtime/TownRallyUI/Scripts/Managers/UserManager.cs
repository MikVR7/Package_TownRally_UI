
using System.Collections.Generic;

namespace TownRally.UI
{
    internal class UserManager : AbstractManager
    {
        internal static EventIn_UserRegistered EventIn_UserRegistered = new EventIn_UserRegistered();
        internal static EventIn_UserLoggedIn EventIn_UserLoggedIn = new EventIn_UserLoggedIn();

        internal static string VarOut_Email { get; private set; } = string.Empty;
        internal static string VarOut_Username { get; private set; } = string.Empty;
        internal static string VarOut_PW { get; private set; } = string.Empty;
        internal static string VarOut_UserID { get; private set; } = string.Empty; 

        internal override void Init()
        {
            base.Init();
            EventIn_UserRegistered.AddListener(UserRegistered);
            EventIn_UserLoggedIn.AddListener(UserLoggedIn);
        }

        internal static bool VarOut_UserDataAvailable()
        {
            List<PlayerPrefsID> list = new List<PlayerPrefsID>();
            list.Add(PlayerPrefsID.User_PW);
            list.Add(PlayerPrefsID.User_Email);
            list.Add(PlayerPrefsID.User_Nickname);

            bool dataAvailable = PlayerPrefsManager.VarOut_HasKeys(list);
            if(dataAvailable)
            {
                GetDataFromPlayerPrefs();
            }
            return PlayerPrefsManager.VarOut_HasKeys(list);
        }

        private static void GetDataFromPlayerPrefs()
        {
            VarOut_Email = PlayerPrefsManager.VarOut_GetPlayerPrefsData(PlayerPrefsID.User_Email);
            VarOut_PW = PlayerPrefsManager.VarOut_GetPlayerPrefsData(PlayerPrefsID.User_PW);
            VarOut_Username = PlayerPrefsManager.VarOut_GetPlayerPrefsData(PlayerPrefsID.User_Nickname);
        }

        private void UserRegistered(string email, string pw, string nickname)
        {
            VarOut_Email = email;
            VarOut_Username = nickname;
            VarOut_PW = pw;
            PlayerPrefsManager.EventIn_SaveUserData.Invoke(PlayerPrefsID.User_Email, email);
            PlayerPrefsManager.EventIn_SaveUserData.Invoke(PlayerPrefsID.User_Nickname, nickname);
            PlayerPrefsManager.EventIn_SaveUserData.Invoke(PlayerPrefsID.User_PW, pw);
        }

        private void UserLoggedIn(string email, string pw, string displayName, string userID)
        {
            VarOut_Email = email;
            VarOut_PW = pw;
            VarOut_Username = displayName;
            VarOut_UserID = userID;
            PlayerPrefsManager.EventIn_SaveUserData.Invoke(PlayerPrefsID.User_Email, email);
            PlayerPrefsManager.EventIn_SaveUserData.Invoke(PlayerPrefsID.User_PW, pw);
        }
    }
}
