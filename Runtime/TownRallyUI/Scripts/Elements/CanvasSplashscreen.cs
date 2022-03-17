using System.Collections;
using UnityEngine;


namespace TownRally.UI
{
    internal class CanvasSplashscreen : AbstractCanvas
    {
        private Animator animator = null;
        internal override void Init()
        {
            base.Init();
            this.canvasType = CanvasType.Splashscreen;
            this.gameObject.SetActive(true);
            this.animator = GetComponent<Animator>();
            StartCoroutine(HideSplashscreen());
        }

        private IEnumerator HideSplashscreen()
        {
            yield return new WaitForSecondsRealtime(1f);
            animator.SetTrigger("fadeout");

            // after splashscreen: perform loading jobs
            LoadingManager.EventIn_AddLoadingJobs.Invoke(new System.Collections.Generic.List<LoadingJob>() { LoadingJob.Language });
            TownRallyUIMain.EventIn_OpenCanvas.Invoke(CanvasType.Loading);
        }

        private void AnimEvent_TurnOffSplashScreen()
        {
            this.gameObject.SetActive(false);
        }

        protected override void LocalisationUpdated() { }
    }
}
