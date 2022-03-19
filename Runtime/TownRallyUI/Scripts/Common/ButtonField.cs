using CodeEvents;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TownRally.UI
{
    internal class ButtonField : MonoBehaviour
    {
        internal EventIn_SetText EventIn_SetText = new EventIn_SetText();

        private Button button = null;
        private EventSystem eventOnClick = null;
        private TextMeshProUGUI tmpText = null;

        internal void Init(EventSystem eventOnClick)
        {
            this.eventOnClick = eventOnClick;
            this.button = this.GetComponent<Button>();
            this.button.onClick.AddListener(OnClick);
            EventIn_SetText.AddListenerSingle(SetText);
            this.tmpText = this.GetComponentInChildren<TextMeshProUGUI>();
        }

        private void OnClick()
        {
            Debug.Log("ON CLICK: " + this.eventOnClick != null);
            this.eventOnClick.InvokeSafe();
        }

        private void SetText(string text)
        {
            this.tmpText.text = text;
        }
    }
}
