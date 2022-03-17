using CoDeEvents;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace TownRally.UI
{
    // TownRallyUIMain.cs
    internal class EventIn_OpenCanvas : EventSystem<CanvasType> { }

    // login/register
    internal class EventIn_OnBtnRegister : EventSystem { }
    internal class EventIn_OnBtnGotoRegister : EventSystem { }
    internal class EventIn_OnBtnLogin : EventSystem { }
    internal class EventIn_OnBtnGotoLogin : EventSystem { }

    // FirebaseAuthentication.cs
    internal class EventOut_DisplayAuthMsg : EventSystem<MessageType, string> { }
    internal class EventIn_FirebaseLogin : EventSystem<List<string>> { }
    internal class EventIn_FirebaseRegister : EventSystem<List<string>> { }
    internal class EventIn_FirebaseLogout : EventSystem { }
    // FirebaseDB.cs
    internal class EventIn_SaveObjectData : EventSystem<string, string> { }
    internal class EventIn_RequestData : EventSystem<string, Action<string>> { }

    // PlayerPrefsManager.cs
    internal class EventIn_SaveUserData : EventSystem<PlayerPrefsID, string> { }
    internal class EventIn_SaveInt : EventSystem<PlayerPrefsID, int> { }

    // UserManager.cs
    internal class EventIn_UserRegistered : EventSystem<string, string, string> { }
    internal class EventIn_UserLoggedIn : EventSystem<string, string, string, string> { }

    // CanvasLogin.cs
    internal class EventIn_SetLoginData : EventSystem<string, string> { }

    // InputField.cs
    internal class EventIn_SetInputText : EventSystem<string> { }
    internal class EventIn_SetPlaceholder : EventSystem<string> { }

    // LocalisationManager.cs
    internal class EventIn_SetLanguage : EventSystem<SystemLanguage> { }
    internal class EventIn_SetLocalisation : EventSystem<Localisation> { }
    internal class EventOut_LocalisationUpdated : EventSystem { }

    // LoadingHandler.cs
    internal class EventIn_AddLoadingJobs : EventSystem<List<LoadingJob>> { }
    internal class EventIn_PerformLoadingJobs : EventSystem { }
    // CanvasLoading.cs
    internal class EventIn_UpdateLoadingProgress : EventSystem<string> { }

    // ButtonField.cs
    internal class EventIn_SetText : EventSystem<string> { }

}
