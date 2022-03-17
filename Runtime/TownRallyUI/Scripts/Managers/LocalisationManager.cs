using UnityEngine;
using Newtonsoft.Json;

namespace TownRally.UI
{
    internal class LocalisationManager : AbstractManager
    {
        private static LocalisationManager Instance = null;
        internal static EventIn_SetLanguage EventIn_SetLanguage = new EventIn_SetLanguage();
        internal static EventIn_SetLocalisation EventIn_SetLocalisation = new EventIn_SetLocalisation();
        internal static EventOut_LocalisationUpdated EventOut_LocalisationUpdated = new EventOut_LocalisationUpdated();

        private static readonly string ERROR_MSG_LANG = "LOC_MISSING";

        private Localisation localisation = new Localisation();

        internal static string VarOut_GetLoc(Lang key)
        {
            if((Instance != null) && Instance.localisation.KeyValues.ContainsKey(key)) { return Instance.localisation.KeyValues[key]; }
            else { return ERROR_MSG_LANG; }
        }

        internal static SystemLanguage VarOut_GetCurrentLanguage()
        {
            return Instance.localisation.CurrentLanguage;
        }

        internal override void Init()
        {
            base.Init();
            Instance = this;
            EventIn_SetLanguage.RemoveListener(SetLanguage);
            EventIn_SetLocalisation.RemoveListener(SetLocalisation);
            EventIn_SetLanguage.AddListener(SetLanguage);
            EventIn_SetLocalisation.AddListener(SetLocalisation);
        }

        private void SetLanguage(SystemLanguage language)
        {
            this.localisation.CurrentLanguage = language;
            PlayerPrefsManager.EventIn_SaveInt.Invoke(PlayerPrefsID.User_Language, (int)this.localisation.CurrentLanguage);
            this.GetLocalisation(this.localisation.CurrentLanguage);
        }

        private void GetLocalisation(SystemLanguage language)
        {
            // TODO: Check if there is a newer localisation on server!
            // currently localisation is always loaded from server!
            //// get it from player prefs manager
            //if (PlayerPrefsManager.VarOut_HasKey(PlayerPrefsID.Localisation))
            //{
            //    string locData = PlayerPrefsManager.VarOut_GetPlayerPrefsData(PlayerPrefsID.Localisation);
            //    Localisation localisation = JsonConvert.DeserializeObject<Localisation>(locData);
            //    // Player prefs has a different Localisation than currently set language
            //    if (localisation.CurrentLanguage == language)
            //    {
            //        this.SetLocalisation(localisation);
            //    }
            //    else
            //    {
            //        FirebaseDB.EventIn_RequestData.Invoke("localisation/lang_" + language, SetLocalisationRaw);
            //    }
            //}
            //else
            //{
                FirebaseDB.EventIn_RequestData.Invoke("localisation/lang_" + language, SetLocalisationRaw);
            //}
        }

        private void SetLocalisationRaw(string locData)
        {
            PlayerPrefsManager.EventIn_SaveUserData.Invoke(PlayerPrefsID.Localisation, locData);
            Localisation localisation = JsonConvert.DeserializeObject<Localisation>(locData);
            this.SetLocalisation(localisation);
        }

        private void SetLocalisation(Localisation localisation)
        {
            this.localisation = localisation;
            EventOut_LocalisationUpdated.Invoke();
        }
    }
}
