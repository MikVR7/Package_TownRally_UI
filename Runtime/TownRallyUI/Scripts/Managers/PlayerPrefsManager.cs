using System.Collections.Generic;
using UnityEngine;

namespace TownRally.UI
{
    internal class PlayerPrefsManager : AbstractManager
    {
        internal static readonly string PLAYPREF_EMAIL = "TownRally.User.Email";
        internal static readonly string PLAYPREF_NICKNAME = "TownRally.User.Nickname";
        internal static readonly string PLAYPREF_PW = "TownRally.User.PW";
        internal static readonly string PLAYPREF_LANGUAGE = "TownRally.User.Language";

        internal static EventIn_SaveUserData EventIn_SaveUserData = new EventIn_SaveUserData();
        internal static EventIn_SaveInt EventIn_SaveInt = new EventIn_SaveInt();

        private static Dictionary<PlayerPrefsID, string> ppIdNames = new Dictionary<PlayerPrefsID, string>();

        internal override void Init()
        {
            base.Init();

            ppIdNames.Clear();
            ppIdNames.Add(PlayerPrefsID.User_Email, PLAYPREF_EMAIL);
            ppIdNames.Add(PlayerPrefsID.User_PW, PLAYPREF_PW);
            ppIdNames.Add(PlayerPrefsID.User_Nickname, PLAYPREF_NICKNAME);
            ppIdNames.Add(PlayerPrefsID.User_Language, PLAYPREF_LANGUAGE);

            EventIn_SaveUserData.AddListener(SaveUserData);
            EventIn_SaveInt.AddListener(SaveInt);
        }

        internal static bool VarOut_HasKeys(List<PlayerPrefsID> ids)
        {
            for(int i = 0; i < ids.Count; i++)
            {
                if(!PlayerPrefs.HasKey(ppIdNames[ids[i]])) { return false; }
            }
            return true;
        }
        internal static bool VarOut_HasKey(PlayerPrefsID id)
        {
            return PlayerPrefs.HasKey(ppIdNames[id]);
        }

        internal static string VarOut_GetPlayerPrefsData(PlayerPrefsID id)
        {
            return PlayerPrefs.GetString(ppIdNames[id]);
        }

        private void SaveUserData(PlayerPrefsID id, string data)
        {
            PlayerPrefs.SetString(ppIdNames[id], data);
        }

        internal static int VarOut_GetInt(PlayerPrefsID id)
        {
            return PlayerPrefs.GetInt(ppIdNames[id]);
        }

        private void SaveInt(PlayerPrefsID id, int data)
        {
            PlayerPrefs.SetInt(ppIdNames[id], data);
        }

    }
}
