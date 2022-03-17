using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using EnhancedUI.EnhancedScroller;

namespace TownRally.UI
{
    internal class ScrollerControllerTours : MonoBehaviour, IEnhancedScrollerDelegate
    {
        private List<ScrollerDataTours> data;
        [SerializeField] private EnhancedScroller scroller = null;
        [SerializeField] private CellTour prefabCellTour = null;

        private void Start()
        {
            data = new List<ScrollerDataTours>();
            data.Add(new ScrollerDataTours() { tourTitle = "Mur-Rally" });
            data.Add(new ScrollerDataTours() { tourTitle = "Geschichte-Tour" });
            data.Add(new ScrollerDataTours() { tourTitle = "Schloﬂberg-Tour" });
            data.Add(new ScrollerDataTours() { tourTitle = "Innenstadt-Tour" });
            scroller.Delegate = this;
            scroller.ReloadData();
        }
        public int GetNumberOfCells(EnhancedScroller scroller)
        {
            return data.Count;
        }
        public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
        {
            return 100f;
        }
        public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int
        dataIndex, int cellIndex)
        {
            CellTour cellView = scroller.GetCellView(prefabCellTour) as CellTour;
            cellView.SetData(data[dataIndex]);
            return cellView;
        }
    }
}
