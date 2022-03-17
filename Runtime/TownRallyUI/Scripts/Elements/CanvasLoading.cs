using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace TownRally.UI
{
    internal class CanvasLoading : AbstractCanvas
    {
        internal EventIn_UpdateLoadingProgress EventIn_UpdateLoadingProgress = new EventIn_UpdateLoadingProgress();

        [SerializeField] private TextMeshProUGUI tmpHeader = null;
        [SerializeField] private TextMeshProUGUI tmpText = null;
        
        internal override void Init()
        {
            base.Init();
            EventIn_UpdateLoadingProgress.AddListener(UpdateLoadingProgress);
            this.canvasType = CanvasType.Loading;
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            if (!initDone) { return; }
            // fire up loading manager and load all jobs that are in List
            LoadingManager.EventIn_PerformLoadingJobs.Invoke();
        }

        private void UpdateLoadingProgress(string progress)
        {
            this.tmpText.text = progress;
        }

        protected override void LocalisationUpdated()
        {
            this.tmpHeader.text = LocalisationManager.VarOut_GetLoc(Lang.UILoadingHeader);
            this.tmpText.text = LocalisationManager.VarOut_GetLoc(Lang.UILoadingStart);
        }
    }
}
