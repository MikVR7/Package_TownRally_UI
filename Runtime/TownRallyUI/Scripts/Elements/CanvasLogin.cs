using Sirenix.Utilities;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TownRally.UI
{
    internal class CanvasLogin : AbstractCanvas
    {
        internal static EventIn_SetLoginData EventIn_SetLoginData = new EventIn_SetLoginData();
        internal static EventIn_OnBtnGotoRegister EventIn_OnBtnGotoRegister = new EventIn_OnBtnGotoRegister();
        internal static EventIn_OnBtnLogin EventIn_OnBtnLogin = new EventIn_OnBtnLogin();

        private enum InputType
        {
            Email = 0,
            Password = 1,
        }
        
        [SerializeField] private Image imgBackground = null;
        [SerializeField] private TextMeshProUGUI tmpHeader = null;
        [SerializeField] private Dictionary<InputType, InputField> inputFields = new Dictionary<InputType, InputField>();
        [SerializeField] private ButtonField btnLogin = null;
        [SerializeField] private ButtonField btnGotoRegister = null;
        [SerializeField] private TextMeshProUGUI tmpMessage = null;
        private Lang currentMsgLang = Lang.FBAuthLoginSuccess;

        internal override void Init()
        {
            base.Init();
            this.canvasType = CanvasType.Login;

            EventIn_SetLoginData.AddListenerSingle(SetLoginData);

            this.inputFields.ForEach(i => i.Value.Init());
            this.btnGotoRegister.Init(EventIn_OnBtnGotoRegister);
            this.btnLogin.Init(EventIn_OnBtnLogin);
        }

        private void OnEnable()
        {
            EventIn_OnBtnGotoRegister.AddListenerSingle(OnBtnGotoRegister);
            EventIn_OnBtnLogin.AddListenerSingle(OnBtnLogin);
            FirebaseAuthentication.EventOut_DisplayAuthMsg.AddListenerSingle(DisplayAuthMsg);
            this.currentMsgLang = Lang.UILoginMsgStart;
            this.tmpMessage.text = LocalisationManager.VarOut_GetLoc(this.currentMsgLang);
        }

        private void OnDisable()
        {
            EventIn_OnBtnGotoRegister.RemoveListener(OnBtnGotoRegister);
            EventIn_OnBtnLogin.RemoveListener(OnBtnLogin);
            FirebaseAuthentication.EventOut_DisplayAuthMsg.RemoveListener(DisplayAuthMsg);
        }

        protected override void LocalisationUpdated()
        {
            this.tmpHeader.text = LocalisationManager.VarOut_GetLoc(Lang.UILoginHeader);
            this.inputFields[InputType.Email].EventIn_SetPlaceholder.Invoke(LocalisationManager.VarOut_GetLoc(Lang.UILoginInputPlaceholderEmail));
            this.inputFields[InputType.Password].EventIn_SetPlaceholder.Invoke(LocalisationManager.VarOut_GetLoc(Lang.UILoginInputPlaceholderPW));
            this.btnGotoRegister.EventIn_SetText.Invoke(LocalisationManager.VarOut_GetLoc(Lang.UILoginBtnGotoRegister));
            this.btnLogin.EventIn_SetText.Invoke(LocalisationManager.VarOut_GetLoc(Lang.UILoginBtnLogin));
            this.tmpMessage.text = LocalisationManager.VarOut_GetLoc(this.currentMsgLang);
        }

        private void SetLoginData(string email, string pw)
        {
            this.inputFields[InputType.Email].EventIn_SetInputText.Invoke(email);
            this.inputFields[InputType.Password].EventIn_SetInputText.Invoke(pw);
            Debug.Log("LOGIN DATA SET! " + email + " " + pw);
        }

        private void DisplayAuthMsg(MessageType messageType, string message)
        {
            this.tmpMessage.text = message;
        }

        private void OnBtnGotoRegister()
        {
            TownRallyUIMain.EventIn_OpenCanvas.Invoke(CanvasType.Register);
        }
        private void OnBtnLogin()
        {
            List<string> values = new List<string>();
            values.Add(this.inputFields[InputType.Email].VarOut_GetText());
            values.Add(this.inputFields[InputType.Password].VarOut_GetText());
            FirebaseAuthentication.EventIn_FirebaseLogin.Invoke(values);
            this.currentMsgLang = Lang.UILoginMsgInProgress;
            this.tmpMessage.text = LocalisationManager.VarOut_GetLoc(this.currentMsgLang);
        }
    }
}
