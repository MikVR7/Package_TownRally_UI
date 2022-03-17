using Sirenix.OdinInspector;
using UnityEngine;

namespace TownRally.UI
{
    internal abstract class AbstractCanvas : SerializedMonoBehaviour
    {
        protected CanvasType canvasType = CanvasType.Splashscreen;
        protected bool initDone = false;

        internal virtual void Init()
        {
            this.gameObject.SetActive(false); 
            LocalisationManager.EventOut_LocalisationUpdated.AddListenerSingle(LocalisationUpdated);
            initDone = true;
        }

        internal CanvasType VarOut_GetCanvasType() { return canvasType; }
        internal static CanvasType VarOut_GetCurrentOpenCanvas { get; private set; }

        protected virtual void OnEnable()
        {
            VarOut_GetCurrentOpenCanvas = VarOut_GetCanvasType();
            if (!initDone) { return; }
        }

        protected abstract void LocalisationUpdated();
    }
}
