using CoDeEvents;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using System.Linq;

namespace TownRally.UI
{   
    internal class TownRallyUIMain : SerializedMonoBehaviour
    {
        private static TownRallyUIMain Instance;
        internal static EventIn_OpenCanvas EventIn_OpenCanvas = new EventIn_OpenCanvas();

        [SerializeField] private Transform holderManagers = null;
        [SerializeField] private Transform holderCanvases = null;
        private Dictionary<CanvasType, AbstractCanvas> canvases = new Dictionary<CanvasType, AbstractCanvas>();
        private Dictionary<ManagerType, AbstractManager> managers = new Dictionary<ManagerType, AbstractManager>();
        private CanvasType currentOpenCanvasType = CanvasType.Splashscreen;

        internal static AbstractCanvas VarOut_GetCanvas(CanvasType type) { return Instance.canvases[type]; }
        internal static CanvasType VarOut_GetCurrentOpenCanvasType() { return Instance.currentOpenCanvasType; }

        private void Awake()
        {
            Instance = this;
            EventIn_OpenCanvas.AddListener(OpenCanvas);

            // get all managers and initialize
            List<AbstractManager> managerList = this.holderManagers.GetComponents<AbstractManager>().ToList();
            this.managers.Clear();
            managerList.ForEach(i => { this.managers.Add(i.VarOut_GetManagerType(), i); i.Init(); });
            Debug.Log("Managers count: " + managers.Count);

            // get all canvases and initialize
            List<AbstractCanvas> canvasList = this.holderCanvases.GetComponentsInChildren<AbstractCanvas>(true).ToList();
            this.canvases.Clear();
            canvasList.ForEach(i => { i.Init(); this.canvases.Add(i.VarOut_GetCanvasType(), i); });
            Debug.Log("Canvases count: " + canvases.Count);

            // start app with splashscreen
            this.OpenCanvas(CanvasType.Splashscreen);
        }

        private void OpenCanvas(CanvasType type)
        {
            Debug.Log("Open canvas: " + type);
            this.currentOpenCanvasType = type;
            this.canvases.ForEach(i => i.Value.gameObject.SetActive(i.Value.VarOut_GetCanvasType().Equals(type)));
        }
    }
}