using System.Collections.Generic;
using UnityEngine;

namespace TownRally.UI
{
    internal class LoadingManager : AbstractManager
    {
        internal static EventIn_AddLoadingJobs EventIn_AddLoadingJobs = new EventIn_AddLoadingJobs();
        internal static EventIn_PerformLoadingJobs EventIn_PerformLoadingJobs = new EventIn_PerformLoadingJobs();

        internal List<LoadingJob> VarOut_LoadingJobs = new List<LoadingJob>();
        
        private CanvasLoading canvasLoading = null;
        
        internal override void Init()
        {
            base.Init();
            EventIn_AddLoadingJobs.AddListener(AddLoadingJobs);
            EventIn_PerformLoadingJobs.AddListener(PerformLoadingJobs);
            VarOut_LoadingJobs.Add(LoadingJob.Language);
        }

        private void AddLoadingJobs(List<LoadingJob> loadingJobs)
        {
            loadingJobs.ForEach(i => this.VarOut_LoadingJobs.Add(i));
        }

        private void PerformLoadingJobs()
        {
            if (this.canvasLoading == null)
            {
                this.canvasLoading = TownRallyUIMain.VarOut_GetCanvas(CanvasType.Loading) as CanvasLoading;
            }
            PerformNextJob();
        }

        private void PerformNextJob()
        {
            if(TownRallyUIMain.VarOut_GetCurrentOpenCanvasType() != CanvasType.Loading)
            {
                TownRallyUIMain.EventIn_OpenCanvas.Invoke(CanvasType.Loading);
            }

            if(this.VarOut_LoadingJobs.Count > 0)
            {
                if (this.VarOut_LoadingJobs[0].Equals(LoadingJob.Language))
                {
                    this.LoadLanguage();
                }
            }
            else
            {
                Debug.Log("Loading finished.");
                this.canvasLoading.EventIn_UpdateLoadingProgress.Invoke(LocalisationManager.VarOut_GetLoc(Lang.UILoadingDone));
            }
        }

        // JOB: Language loading
        private void LoadLanguage()
        {
            // TODO: load that localisation from PlayerPrefs (basic Languages)
            this.canvasLoading.EventIn_UpdateLoadingProgress.Invoke("Loading Languages...");

            SystemLanguage lang = Application.systemLanguage;
            if (PlayerPrefsManager.VarOut_HasKey(PlayerPrefsID.User_Language))
            {
                lang = (SystemLanguage)PlayerPrefsManager.VarOut_GetInt(PlayerPrefsID.User_Language);
            }
            LocalisationManager.EventOut_LocalisationUpdated.RemoveListener(DoneLoadLanguage);
            LocalisationManager.EventOut_LocalisationUpdated.AddListener(DoneLoadLanguage);
            LocalisationManager.EventIn_SetLanguage.Invoke(lang);
        }

        private void DoneLoadLanguage()
        {
            Debug.Log("DONE LOAD LANGUAGE!");
            this.canvasLoading.EventIn_UpdateLoadingProgress.Invoke(LocalisationManager.VarOut_GetLoc(Lang.UILoadingLanguageDone));
            this.PerformNextJob();
        }
    }
}
