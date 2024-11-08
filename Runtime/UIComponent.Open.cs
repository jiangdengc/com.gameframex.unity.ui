using System.Threading.Tasks;
using System;
using GameFrameX.Runtime;

namespace GameFrameX.UI.Runtime
{
    public partial class UIComponent
    {
        /// <summary>
        /// 打开界面。
        /// </summary>
        /// <param name="uiFormAssetPath">界面所在路径</param>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <param name="uiGroupName">界面组名称。</param>
        /// <param name="isFullScreen"></param>
        /// <returns>界面的序列编号。</returns>
        public Task<IUIForm> OpenUIFormAsync<T>(string uiFormAssetPath, string uiFormAssetName, string uiGroupName, bool isFullScreen = false) where T : class, IUIForm
        {
            return OpenUIFormAsync(uiFormAssetPath, uiFormAssetName, uiGroupName, typeof(T), false, null, isFullScreen);
        }

        /// <summary>
        /// 打开全屏界面。
        /// </summary>
        /// <param name="uiFormAssetPath">界面所在路径</param>
        /// <param name="uiGroupName"></param>
        /// <param name="userData"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<T> OpenFullScreenUIFormAsync<T>(string uiFormAssetPath, string uiGroupName, object userData = null) where T : class, IUIForm
        {
            return await OpenUIFormAsync<T>(uiFormAssetPath, uiGroupName, userData, true);
        }

        /// <summary>
        /// 打开界面。仅仅适用于类型和名称完全相同的情况
        /// </summary>
        /// <param name="uiGroupName">UI组名</param>
        /// <param name="userData"></param>
        /// <param name="isFullScreen"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<T> OpenUIFormAsync<T>(string uiGroupName, object userData = null, bool isFullScreen = false) where T : class, IUIForm
        {
            var uiFormAssetName = typeof(T).Name;
            var uiFormAssetPath = Utility.Asset.Path.GetUIPath(uiFormAssetName);
            return await OpenUIFormAsync<T>(uiFormAssetPath, uiGroupName, userData, isFullScreen);
        }

        /// <summary>
        /// 打开界面。
        /// </summary>
        /// <param name="uiFormAssetPath">界面所在路径</param>
        /// <param name="uiGroupName"></param>
        /// <param name="userData"></param>
        /// <param name="isFullScreen"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<T> OpenUIFormAsync<T>(string uiFormAssetPath, string uiGroupName, object userData = null, bool isFullScreen = false) where T : class, IUIForm
        {
            string uiFormAssetName = typeof(T).Name;
            var ui = await OpenUIFormAsync(uiFormAssetPath, uiFormAssetName, uiGroupName, typeof(T), false, userData, isFullScreen);
            return ui as T;
        }

        /// <summary>
        /// 打开界面。
        /// </summary>
        /// <param name="uiFormAssetPath">界面所在路径</param>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <param name="uiGroupName">界面组名称。</param>
        /// <param name="pauseCoveredUIForm">是否暂停被覆盖的界面。</param>
        /// <param name="isFullScreen">是否全屏</param>
        /// <returns>界面的序列编号。</returns>
        public Task<IUIForm> OpenUIFormAsync<T>(string uiFormAssetPath, string uiFormAssetName, string uiGroupName, bool pauseCoveredUIForm, bool isFullScreen = false) where T : class, IUIForm
        {
            return OpenUIFormAsync(uiFormAssetPath, uiFormAssetName, uiGroupName, typeof(T), pauseCoveredUIForm, null, isFullScreen);
        }

        /// <summary>
        /// 打开界面。
        /// </summary>
        /// <param name="uiFormAssetPath">界面所在路径</param>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <param name="uiGroupName">界面组名称。</param>
        /// <param name="userData">用户自定义数据。</param>
        /// <param name="isFullScreen">是否全屏</param>
        /// <returns>界面的序列编号。</returns>
        public Task<IUIForm> OpenUIFormAsync<T>(string uiFormAssetPath, string uiFormAssetName, string uiGroupName, object userData, bool isFullScreen = false) where T : IUIForm
        {
            return OpenUIFormAsync(uiFormAssetPath, uiFormAssetName, uiGroupName, typeof(T), false, userData, isFullScreen);
        }


        /// <summary>
        /// 打开界面。
        /// </summary>
        /// <param name="uiFormAssetPath">界面所在路径</param>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <param name="uiGroupName">界面组名称。</param>
        /// <param name="uiFormType">界面逻辑类型。</param>
        /// <param name="pauseCoveredUIForm">是否暂停被覆盖的界面。</param>
        /// <param name="isFullScreen">是否全屏</param>
        /// <param name="userData">用户自定义数据。</param>
        /// <returns>界面的序列编号。</returns>
        public async Task<IUIForm> OpenUIFormAsync(string uiFormAssetPath, string uiFormAssetName, string uiGroupName, Type uiFormType, bool pauseCoveredUIForm, object userData, bool isFullScreen = false)
        {
            return await m_UIManager.OpenUIFormAsync(uiFormAssetPath, uiFormAssetName, uiGroupName, uiFormType, pauseCoveredUIForm, userData, isFullScreen);
        }
    }
}