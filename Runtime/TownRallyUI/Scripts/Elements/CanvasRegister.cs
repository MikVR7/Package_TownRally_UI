using Sirenix.Utilities;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TownRally.UI
{
    internal class CanvasRegister : AbstractCanvas
    {
        private enum InputType
        {
            Username = 0,
            Password1 = 1,
            Password2 = 2,
            Email = 3,
        }

        internal static EventIn_OnBtnRegister EventIn_OnBtnRegister = new EventIn_OnBtnRegister();
        internal static EventIn_OnBtnGotoLogin EventIn_OnBtnGotoLogin = new EventIn_OnBtnGotoLogin();

        [SerializeField] private Image imgBackground = null;
        [SerializeField] private TextMeshProUGUI tmpHeader = null;
        [SerializeField] private Dictionary<InputType, InputField> inputFields = new Dictionary<InputType, InputField>();
        [SerializeField] private ButtonField btnRegister = null;
        [SerializeField] private ButtonField btnGotoLogin = null;
        [SerializeField] private TextMeshProUGUI tmpMessage = null;
        private Lang currentMsgLang = Lang.FBAuthLoginSuccess;

        internal override void Init()
        {
            base.Init();
            this.canvasType = CanvasType.Register;
            this.inputFields.ForEach(i => i.Value.Init());
            this.btnRegister.Init(EventIn_OnBtnRegister);
            this.btnGotoLogin.Init(EventIn_OnBtnGotoLogin);
        }

        private void OnEnable()
        {
            EventIn_OnBtnRegister.AddListenerSingle(OnBtnRegister);
            EventIn_OnBtnGotoLogin.AddListenerSingle(OnBtnGotoLogin);
            FirebaseAuthentication.EventOut_DisplayAuthMsg.AddListenerSingle(DisplayAuthMsg);
            this.currentMsgLang = Lang.UIRegisterMsgStart;
            this.tmpMessage.text = LocalisationManager.VarOut_GetLoc(this.currentMsgLang);
        }

        private void OnDisable()
        {
            EventIn_OnBtnRegister.RemoveListener(OnBtnRegister);
            EventIn_OnBtnGotoLogin.RemoveListener(OnBtnGotoLogin);
            FirebaseAuthentication.EventOut_DisplayAuthMsg.RemoveListener(DisplayAuthMsg);
        }

        protected override void LocalisationUpdated()
        {
            this.tmpHeader.text = LocalisationManager.VarOut_GetLoc(Lang.UIRegisterHeader);
            this.inputFields[InputType.Email].EventIn_SetPlaceholder.Invoke(LocalisationManager.VarOut_GetLoc(Lang.UIRegisterInputPlaceholderEmail));
            this.inputFields[InputType.Password1].EventIn_SetPlaceholder.Invoke(LocalisationManager.VarOut_GetLoc(Lang.UIRegisterInputPlaceholderPW1));
            this.inputFields[InputType.Password2].EventIn_SetPlaceholder.Invoke(LocalisationManager.VarOut_GetLoc(Lang.UIRegisterInputPlaceholderPW2));
            this.inputFields[InputType.Username].EventIn_SetPlaceholder.Invoke(LocalisationManager.VarOut_GetLoc(Lang.UIRegisterInputPlaceholderUsername));
            this.btnGotoLogin.EventIn_SetText.Invoke(LocalisationManager.VarOut_GetLoc(Lang.UIRegisterBtnGotoLogin));
            this.btnRegister.EventIn_SetText.Invoke(LocalisationManager.VarOut_GetLoc(Lang.UIRegisterBtnRegister));
            this.tmpMessage.text = LocalisationManager.VarOut_GetLoc(this.currentMsgLang);
        }

        private void DisplayAuthMsg(MessageType messageType, string message)
        {
            this.tmpMessage.text = message;
        }

        private void OnBtnRegister()
        {
            List<string> values = new List<string>();
            values.Add(this.inputFields[InputType.Email].VarOut_GetText());
            values.Add(this.inputFields[InputType.Password1].VarOut_GetText());
            values.Add(this.inputFields[InputType.Password2].VarOut_GetText());
            values.Add(this.inputFields[InputType.Username].VarOut_GetText());
            FirebaseAuthentication.EventIn_FirebaseRegister.Invoke(values);
            this.currentMsgLang = Lang.UIRegisterMsgStart;
            this.tmpMessage.text = LocalisationManager.VarOut_GetLoc(this.currentMsgLang);
        }

        private void OnBtnGotoLogin()
        {
            TownRallyUIMain.EventIn_OpenCanvas.Invoke(CanvasType.Login);
        }
    }
}
