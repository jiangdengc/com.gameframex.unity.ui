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
            _ = typeof(UIStringKey);
            _ = typeof(UIComponent);
            _ = typeof(UIIntKey);
            _ = typeof(UIGroupHelperBase);
            _ = typeof(UIFormLogic);
            _ = typeof(UIFormHelperBase);
            _ = typeof(UIForm);
            _ = typeof(DefaultUIFormHelper);
            _ = typeof(CloseUIFormCompleteEventArgs);
            // _ = typeof(OpenUIFormDependencyAssetEventArgs);
            _ = typeof(OpenUIFormFailureEventArgs);
            _ = typeof(OpenUIFormSuccessEventArgs);
            // _ = typeof(OpenUIFormUpdateEventArgs);
        }
    }
}