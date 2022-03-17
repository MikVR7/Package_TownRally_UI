using TMPro;
using UnityEngine;

namespace TownRally.UI
{
    internal class InputField : MonoBehaviour
    {
        internal EventIn_SetInputText EventIn_SetInputText = new EventIn_SetInputText();
        internal EventIn_SetPlaceholder EventIn_SetPlaceholder = new EventIn_SetPlaceholder();

        [SerializeField] private string placeholder = string.Empty;
        private TMP_InputField tmpInputField = null;
        private TMP_Text tmpPlaceholder = null;
        
        internal void Init()
        {
            EventIn_SetInputText.AddListener(SetInputText);
            EventIn_SetPlaceholder.AddListener(SetPlaceholder);
            this.tmpInputField = this.GetComponent<TMP_InputField>();
            this.tmpPlaceholder = this.tmpInputField.placeholder.GetComponent<TMP_Text>();
            if(!string.IsNullOrEmpty(placeholder)) { this.SetPlaceholder(placeholder); }
        } 

        internal string VarOut_GetText()
        {
            return this.tmpInputField.text;
        }

        private void SetInputText(string value)
        {
            this.tmpInputField.text = value;
        }

        private void SetPlaceholder(string value)
        {
            this.tmpPlaceholder.text = value;
        }
    }
}
