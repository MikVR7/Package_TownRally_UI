using CodeEvents;
using EnhancedUI.EnhancedScroller;
using TMPro;
using UnityEngine;

namespace TownRally.UI
{
    internal class EventIn_OnRallySelected : EventSystem { }
    internal class CellTour : EnhancedScrollerCellView
    {
        private EventIn_OnRallySelected EventIn_OnRallySelected = new EventIn_OnRallySelected();

        [SerializeField] private TextMeshProUGUI tmpTourTitle = null;
        [SerializeField] private ButtonField btnSelector = null;
        private string tourTitle = string.Empty;

        public void SetData(ScrollerDataTours data)
        {
            this.tourTitle = data.tourTitle;
            tmpTourTitle.text = data.tourTitle;
            EventIn_OnRallySelected.AddListener(OnRallySelected);
            btnSelector.Init(EventIn_OnRallySelected);
        }

        private void OnRallySelected()
        {
            Debug.Log("Rally selected: " + this.tourTitle);
        }
    }
}
