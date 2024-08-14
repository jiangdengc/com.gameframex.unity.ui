using UnityEngine;
using UnityEngine.Scripting;

namespace GameFrameX.UI.Runtime
{
    [Preserve]
    public class GameFrameXUICroppingHelper : MonoBehaviour
    {
        [Preserve]
        private void Start()
        {
            _ = typeof(IUIManager);
            _ = typeof(UIManager);
            _ = typeof(UI);
            _ = typeof(UIComponent);
            _ = typeof(UILayer);
        }
    }
}