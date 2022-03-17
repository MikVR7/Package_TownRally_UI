using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace TownRally.UI
{
    internal class LocalisationCreator : MonoBehaviour
    {
        private static readonly string PATH_LANG = "localisation/lang_&1";

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                CreateLanguageData(SystemLanguage.English);
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                CreateLanguageData(SystemLanguage.German);
            }
        }

        private void CreateLanguageData(SystemLanguage language)
        {
            Localisation loc = new Localisation();
            loc.CurrentLanguage = language;
            loc.LocalisationVersion = "1.0|" + System.DateTime.Now.ToString("yyyy_MM_dd|hh_mm_ss");
            if (language == SystemLanguage.English)
            {
                loc.KeyValues = this.CreateLanguageDataEnglish();
            }
            else if(language == SystemLanguage.German)
            {
                loc.KeyValues = this.CreateLanguageDataGerman();
            }
            string data = JsonConvert.SerializeObject(loc);
            FirebaseDB.EventIn_SaveObjectData.Invoke(PATH_LANG.Replace("&1", loc.CurrentLanguage.ToString()), data);
            Debug.Log("Updated language " + language);
        }

        private Dictionary<Lang, string> CreateLanguageDataEnglish()
        {
            Dictionary<Lang, string> keyValuePairs = new Dictionary<Lang, string>();

            // canvas loading
            keyValuePairs.Add(Lang.UILoadingHeader, "Loading data");
            keyValuePairs.Add(Lang.UILoadingLanguage, "Loading language data (&1)");
            keyValuePairs.Add(Lang.UILoadingLanguageDone, "Language loading finished (&1)");
            keyValuePairs.Add(Lang.UILoadingDone, "Loading done!");
            keyValuePairs.Add(Lang.UILoadingStart, "Loading started...");

            // canvas register
            keyValuePairs.Add(Lang.UIRegisterHeader, "Welcome");
            keyValuePairs.Add(Lang.UIRegisterInputPlaceholderUsername, "Username");
            keyValuePairs.Add(Lang.UIRegisterInputPlaceholderEmail, "Email");
            keyValuePairs.Add(Lang.UIRegisterInputPlaceholderPW1, "Password");
            keyValuePairs.Add(Lang.UIRegisterInputPlaceholderPW2, "Repeat password");
            keyValuePairs.Add(Lang.UIRegisterBtnRegister, "Register");
            keyValuePairs.Add(Lang.UIRegisterBtnGotoLogin, "Back to Login");
            keyValuePairs.Add(Lang.UIRegisterMsgStart, "Enter data");
            keyValuePairs.Add(Lang.UIRegisterMsgInProgress, "Registering ...");

            // canvas login
            keyValuePairs.Add(Lang.UILoginHeader, "Login");
            keyValuePairs.Add(Lang.UILoginInputPlaceholderEmail, "Email");
            keyValuePairs.Add(Lang.UILoginInputPlaceholderPW, "Password");
            keyValuePairs.Add(Lang.UILoginBtnLogin, "Login");
            keyValuePairs.Add(Lang.UILoginBtnGotoRegister, "Register");
            keyValuePairs.Add(Lang.UILoginMsgStart, "Enter data");
            keyValuePairs.Add(Lang.UILoginMsgInProgress, "Logging in ...");

            // canvas select rally
            keyValuePairs.Add(Lang.UISelectrallyHeader, "Select rally");

            // FirebaseAuthentication
            keyValuePairs.Add(Lang.FBAuthErrorMissingUsername, "Missing Username");
            keyValuePairs.Add(Lang.FBAuthErrorMissingEmail, "Missing Email");
            keyValuePairs.Add(Lang.FBAuthErrorMissingPassword, "Missing Password");
            keyValuePairs.Add(Lang.FBAuthErrorWrongPassword, "Wrong Password");
            keyValuePairs.Add(Lang.FBAuthErrorInvalidEmail, "Invalid Email");
            keyValuePairs.Add(Lang.FBAuthErrorUserNotFound, "Account does not exist");
            keyValuePairs.Add(Lang.FBAuthErrorPasswordsDoNotMatch, "Password does not match");
            keyValuePairs.Add(Lang.FBAuthErrorWeakPassword, "Weak Password");
            keyValuePairs.Add(Lang.FBAuthErrorEmailAlreadyInUse, "Email already in use");
            keyValuePairs.Add(Lang.FBAuthErrorCouldNotCreateUser, "Could not create user");
            keyValuePairs.Add(Lang.FBAuthLoginSuccess, "Logged In!");

            // Rallies
            keyValuePairs.Add(Lang.RallyNameMurrally, "Mur rally");
            keyValuePairs.Add(Lang.RallyDescMurrally, "Rally about the mur...");
            keyValuePairs.Add(Lang.RallyNameSchlossberg, "Schlossberg");
            keyValuePairs.Add(Lang.RallyDescSchlossberg, "Rally about the schlossberg");
            // stations murrally
            keyValuePairs.Add(Lang.RallyMurrallyS1Name, "Arrival");
            keyValuePairs.Add(Lang.RallyMurrallyS1Desc, "Arrival");
            keyValuePairs.Add(Lang.RallyMurrallyS2Name, "Mur isle");
            keyValuePairs.Add(Lang.RallyMurrallyS2Desc, "");
            keyValuePairs.Add(Lang.RallyMurrallyS3Name, "Murnockerl");
            keyValuePairs.Add(Lang.RallyMurrallyS3Desc, "");
            keyValuePairs.Add(Lang.RallyMurrallyS4Name, "Settling of the Mur");
            keyValuePairs.Add(Lang.RallyMurrallyS4Desc, "");
            keyValuePairs.Add(Lang.RallyMurrallyS5Name, "Nature");
            keyValuePairs.Add(Lang.RallyMurrallyS5Desc, "");
            keyValuePairs.Add(Lang.RallyMurrallyS6Name, "Fishing");
            keyValuePairs.Add(Lang.RallyMurrallyS6Desc, "");
            keyValuePairs.Add(Lang.RallyMurrallyS7Name, "Mur power plant");
            keyValuePairs.Add(Lang.RallyMurrallyS7Desc, "");
            keyValuePairs.Add(Lang.RallyMurrallyS8Name, "Graz and the mur");
            keyValuePairs.Add(Lang.RallyMurrallyS8Desc, "");
            // stations schlossberg
            keyValuePairs.Add(Lang.RallySchlossbergS1Name, "History");
            keyValuePairs.Add(Lang.RallySchlossbergS1Desc, "");
            keyValuePairs.Add(Lang.RallySchlossbergS2Name, "2nd world war");
            keyValuePairs.Add(Lang.RallySchlossbergS2Desc, "");
            keyValuePairs.Add(Lang.RallySchlossbergS3Name, "Kriegssteig");
            keyValuePairs.Add(Lang.RallySchlossbergS3Desc, "");

            return keyValuePairs;
        }

        private Dictionary<Lang, string> CreateLanguageDataGerman()
        {
            Dictionary<Lang, string> keyValuePairs = new Dictionary<Lang, string>();

            // canvas loading
            keyValuePairs.Add(Lang.UILoadingHeader, "Lade daten");
            keyValuePairs.Add(Lang.UILoadingLanguage, "Lade Sprache (&1)");
            keyValuePairs.Add(Lang.UILoadingLanguageDone, "Sprache geladen (&1)");
            keyValuePairs.Add(Lang.UILoadingDone, "Fertig!");
            keyValuePairs.Add(Lang.UILoadingStart, "Lade Daten ...");

            // canvas register
            keyValuePairs.Add(Lang.UIRegisterHeader, "Willkommen");
            keyValuePairs.Add(Lang.UIRegisterInputPlaceholderUsername, "Benutzername");
            keyValuePairs.Add(Lang.UIRegisterInputPlaceholderEmail, "Email");
            keyValuePairs.Add(Lang.UIRegisterInputPlaceholderPW1, "Passwort");
            keyValuePairs.Add(Lang.UIRegisterInputPlaceholderPW2, "Passwort wiederholen");
            keyValuePairs.Add(Lang.UIRegisterBtnRegister, "Registrieren");
            keyValuePairs.Add(Lang.UIRegisterBtnGotoLogin, "Zum Login");

            // canvas login
            keyValuePairs.Add(Lang.UILoginHeader, "Login");
            keyValuePairs.Add(Lang.UILoginInputPlaceholderEmail, "Email");
            keyValuePairs.Add(Lang.UILoginInputPlaceholderPW, "Passwort");
            keyValuePairs.Add(Lang.UILoginMsgStart, "Dateneingabe");
            keyValuePairs.Add(Lang.UILoginBtnLogin, "Login");
            keyValuePairs.Add(Lang.UILoginBtnGotoRegister, "Zum Registrieren");
            keyValuePairs.Add(Lang.UILoginMsgInProgress, "Logge ein...");

            // canvas select rally
            keyValuePairs.Add(Lang.UISelectrallyHeader, "Rally Auswahl");

            // FirebaseAuthentication
            keyValuePairs.Add(Lang.FBAuthErrorMissingUsername, "Benutzername fehlt");
            keyValuePairs.Add(Lang.FBAuthErrorMissingEmail, "Email fehlt");
            keyValuePairs.Add(Lang.FBAuthErrorMissingPassword, "Passwort fehlt");
            keyValuePairs.Add(Lang.FBAuthErrorWrongPassword, "Falsches Passwort");
            keyValuePairs.Add(Lang.FBAuthErrorInvalidEmail, "Ungültige Email");
            keyValuePairs.Add(Lang.FBAuthErrorUserNotFound, "Benutzerkonto existiert nicht");
            keyValuePairs.Add(Lang.FBAuthErrorPasswordsDoNotMatch, "Passworteingaben sind nicht gleich");
            keyValuePairs.Add(Lang.FBAuthErrorWeakPassword, "Schwaches Passwort");
            keyValuePairs.Add(Lang.FBAuthErrorEmailAlreadyInUse, "Diese Emailadresse wird bereits genutzt");
            keyValuePairs.Add(Lang.FBAuthErrorCouldNotCreateUser, "Konto konnte nicht angelegt werden");
            keyValuePairs.Add(Lang.FBAuthLoginSuccess, "Login erfolgreich!");

            // Rallies
            keyValuePairs.Add(Lang.RallyNameMurrally, "Mur rally");
            keyValuePairs.Add(Lang.RallyDescMurrally, "Rally über die Mur...");
            keyValuePairs.Add(Lang.RallyNameSchlossberg, "Schlossberg");
            keyValuePairs.Add(Lang.RallyDescSchlossberg, "Rally über den Schloßberg");
            // stations murrally
            keyValuePairs.Add(Lang.RallyMurrallyS1Name, "Arrival");
            keyValuePairs.Add(Lang.RallyMurrallyS1Desc, "Arrival");
            keyValuePairs.Add(Lang.RallyMurrallyS2Name, "Murinsel");
            keyValuePairs.Add(Lang.RallyMurrallyS2Desc, "");
            keyValuePairs.Add(Lang.RallyMurrallyS3Name, "Murnockerl");
            keyValuePairs.Add(Lang.RallyMurrallyS3Desc, "");
            keyValuePairs.Add(Lang.RallyMurrallyS4Name, "Besiedlung der Mur");
            keyValuePairs.Add(Lang.RallyMurrallyS4Desc, "");
            keyValuePairs.Add(Lang.RallyMurrallyS5Name, "Natur");
            keyValuePairs.Add(Lang.RallyMurrallyS5Desc, "");
            keyValuePairs.Add(Lang.RallyMurrallyS6Name, "Fischfang");
            keyValuePairs.Add(Lang.RallyMurrallyS6Desc, "");
            keyValuePairs.Add(Lang.RallyMurrallyS7Name, "Murkraftwerk");
            keyValuePairs.Add(Lang.RallyMurrallyS7Desc, "");
            keyValuePairs.Add(Lang.RallyMurrallyS8Name, "Graz und die Mur");
            keyValuePairs.Add(Lang.RallyMurrallyS8Desc, "");
            // stations schlossberg
            keyValuePairs.Add(Lang.RallySchlossbergS1Name, "Geschichte");
            keyValuePairs.Add(Lang.RallySchlossbergS1Desc, "");
            keyValuePairs.Add(Lang.RallySchlossbergS2Name, "2ter Weltkrieg");
            keyValuePairs.Add(Lang.RallySchlossbergS2Desc, "");
            keyValuePairs.Add(Lang.RallySchlossbergS3Name, "Kriegssteig");
            keyValuePairs.Add(Lang.RallySchlossbergS3Desc, "");

            return keyValuePairs;
        }
    }
}
