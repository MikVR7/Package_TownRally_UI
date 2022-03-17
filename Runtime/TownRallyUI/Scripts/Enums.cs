namespace TownRally.UI
{
    internal enum CanvasType
    {
        Splashscreen = 0,
        Loading = 1,
        Register = 2,
        Login = 3,
        SelectRally = 4,
    }

    internal enum ManagerType
    {
        FirebaseInitiator = 0,
        PlayerPrefs = 1,
        Users = 2,
        Localisation = 3,
        Loading = 4,
    }

    internal enum PlayerPrefsID
    {
        User_Email = 0,
        User_PW = 1,
        User_Nickname = 2,
        User_Language = 3,
        Localisation = 4,
    }

    internal enum MessageType
    {
        Info = 0,
        Warning = 1,
        Error = 2,
    }

    internal enum LoadingJob
    {
        Language = 0,
    }
}
