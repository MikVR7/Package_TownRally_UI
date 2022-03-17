using UnityEngine;

namespace TownRally.UI
{
    internal class AbstractManager : MonoBehaviour
    {
        [SerializeField] private ManagerType managerType = ManagerType.FirebaseInitiator;

        internal virtual void Init() { }

        internal ManagerType VarOut_GetManagerType() { return managerType; }
    }
}