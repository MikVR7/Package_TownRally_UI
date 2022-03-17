using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TownRally.UI
{
    public enum Lang
    {
        // canvas loading
        UILoadingHeader = 1000,
        UILoadingLanguage = 1001,
        UILoadingLanguageDone = 1002,
        UILoadingStart = 1003,
        UILoadingDone = 1099,
        // canvas register
        UIRegisterHeader = 2000,
        UIRegisterInputPlaceholderUsername = 2001,
        UIRegisterInputPlaceholderEmail = 2002,
        UIRegisterInputPlaceholderPW1 = 2003,
        UIRegisterInputPlaceholderPW2 = 2004,
        UIRegisterBtnRegister = 2005,
        UIRegisterBtnGotoLogin = 2006,
        UIRegisterMsgStart = 2007,
        UIRegisterMsgInProgress = 2008,
        // canvas login
        UILoginHeader = 3000,
        UILoginInputPlaceholderEmail = 3001,
        UILoginInputPlaceholderPW = 3002,
        UILoginBtnLogin = 3003,
        UILoginBtnGotoRegister = 3004,
        UILoginMsgStart = 3005,
        UILoginMsgInProgress = 3006,
        // select rally
        UISelectrallyHeader = 4000,
        // FirebaseAuthentication
        FBAuthErrorMissingUsername = 4001,
        FBAuthErrorMissingEmail = 4002,
        FBAuthErrorMissingPassword = 4003,
        FBAuthErrorWrongPassword = 4004,
        FBAuthErrorInvalidEmail = 4005,
        FBAuthErrorUserNotFound = 4006,
        FBAuthErrorPasswordsDoNotMatch = 4007,
        FBAuthErrorWeakPassword = 4008,
        FBAuthErrorEmailAlreadyInUse = 4009,
        FBAuthErrorCouldNotCreateUser = 4010,
        FBAuthLoginSuccess = 4011,
        // rallies
        RallyNameMurrally = 10000,
        RallyDescMurrally = 10001,
        RallyNameSchlossberg = 10002,
        RallyDescSchlossberg = 10003,
        // stations murrally
        RallyMurrallyS1Name = 20001,
        RallyMurrallyS1Desc = 20002,
        RallyMurrallyS2Name = 20003,
        RallyMurrallyS2Desc = 20004,
        RallyMurrallyS3Name = 20005,
        RallyMurrallyS3Desc = 20006,
        RallyMurrallyS4Name = 20007,
        RallyMurrallyS4Desc = 20008,
        RallyMurrallyS5Name = 20009,
        RallyMurrallyS5Desc = 20010,
        RallyMurrallyS6Name = 20011,
        RallyMurrallyS6Desc = 20012,
        RallyMurrallyS7Name = 20013,
        RallyMurrallyS7Desc = 20014,
        RallyMurrallyS8Name = 20015,
        RallyMurrallyS8Desc = 20016,
        // stations schlossberg
        RallySchlossbergS1Name = 21000,
        RallySchlossbergS1Desc = 21001,
        RallySchlossbergS2Name = 21002,
        RallySchlossbergS2Desc = 21003,
        RallySchlossbergS3Name = 21004,
        RallySchlossbergS3Desc = 21005,
    }

    public class Localisation
    {
        internal SystemLanguage CurrentLanguage { get; set; } = SystemLanguage.Unknown;
        public string LocalisationVersion { get; set; } = "1.0";
        public Dictionary<Lang, string> KeyValues { get; set; } = new Dictionary<Lang, string>();
    }
}
