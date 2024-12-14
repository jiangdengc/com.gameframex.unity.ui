using System.Threading.Tasks;
using System;
using GameFrameX.Runtime;

namespace GameFrameX.UI.Runtime
{
    public partial class UIComponent
    {
        /// <summary>
        /// 异步打开全屏UI。
        /// </summary>
        /// <typeparam name="T">UI的具体类型。</typeparam>
        /// <param name="uiFormAssetPath">UI资源路径。</param>
        /// <param name="uiGroupDefine">UI组定义。</param>
        /// <param name="userData">传递给UI的用户数据。</param>
        /// <returns>返回打开的UI实例。</returns>
        public async Task<T> OpenFullScreenAsync<T>(string uiFormAssetPath, UIGroupDefine uiGroupDefine, object userData = null) where T : class, IUIForm
        {
            return await OpenUIFormAsync<T>(uiFormAssetPath, uiGroupDefine.Name, userData, true);
        }

        /// <summary>
        /// 异步打开全屏UI。
        /// </summary>
        /// <typeparam name="T">UI的具体类型。</typeparam>
        /// <param name="uiFormAssetPath">UI资源路径。</param>
        /// <param name="uiFormAssetName">UI资源名称。</param>
        /// <param name="uiGroupDefine">UI组定义。</param>
        /// <param name="userData">传递给UI的用户数据。</param>
        /// <returns>返回打开的UI实例。</returns>
        public async Task<T> OpenFullScreenAsync<T>(string uiFormAssetPath, string uiFormAssetName, UIGroupDefine uiGroupDefine, object userData = null) where T : class, IUIForm
        {
            return await OpenAsync<T>(uiFormAssetPath, uiFormAssetName, uiGroupDefine.Name, true, userData, true);
        }

        /// <summary>
        /// 异步打开全屏UI。
        /// </summary>
        /// <typeparam name="T">UI的具体类型。</typeparam>
        /// <param name="uiFormAssetPath">UI资源路径。</param>
        /// <param name="uiGroupName">UI组名称。</param>
        /// <param name="userData">传递给UI的用户数据。</param>
        /// <returns>返回打开的UI实例。</returns>
        public async Task<T> OpenFullScreenAsync<T>(string uiFormAssetPath, string uiGroupName, object userData = null) where T : class, IUIForm
        {
            return await OpenUIFormAsync<T>(uiFormAssetPath, uiGroupName, userData, true);
        }

        /// <summary>
        /// 异步打开全屏UI。
        /// </summary>
        /// <typeparam name="T">UI的具体类型。</typeparam>
        /// <param name="uiGroupDefine">UI组定义。</param>
        /// <param name="userData">传递给UI的用户数据。</param>
        /// <returns>返回打开的UI实例。</returns>
        public async Task<T> OpenFullScreenAsync<T>(UIGroupDefine uiGroupDefine, object userData = null) where T : class, IUIForm
        {
            return await OpenFullScreenAsync<T>(uiGroupDefine.Name, userData);
        }

        /// <summary>
        /// 异步打开全屏UI。
        /// </summary>
        /// <typeparam name="T">UI的具体类型。</typeparam>
        /// <param name="uiGroupName">UI组名称。</param>
        /// <param name="userData">传递给UI的用户数据。</param>
        /// <returns>返回打开的UI实例。</returns>
        public async Task<T> OpenFullScreenAsync<T>(string uiGroupName, object userData = null) where T : class, IUIForm
        {
            var uiFormAssetName = typeof(T).Name;
            var uiFormAssetPath = Utility.Asset.Path.GetUIPath(uiFormAssetName);

            return await OpenUIFormAsync<T>(uiFormAssetPath, uiGroupName, userData, true);
        }

        /// <summary>
        /// 异步打开UI。
        /// </summary>
        /// <typeparam name="T">UI的具体类型。</typeparam>
        /// <param name="uiFormAssetPath">UI资源路径。</param>
        /// <param name="uiFormAssetName">UI资源名称。</param>
        /// <param name="uiGroupName">UI组名称。</param>
        /// <param name="pauseCoveredUIForm">是否暂停被覆盖的UI。</param>
        /// <param name="userData">传递给UI的用户数据。</param>
        /// <param name="isFullScreen">是否全屏显示。</param>
        /// <returns>返回打开的UI实例。</returns>
        public async Task<T> OpenAsync<T>(string uiFormAssetPath, string uiFormAssetName, string uiGroupName, bool pauseCoveredUIForm, object userData, bool isFullScreen) where T : class, IUIForm
        {
            var ui = await OpenUIFormAsync(uiFormAssetPath, uiFormAssetName, uiGroupName, typeof(T), pauseCoveredUIForm, userData, isFullScreen);
            return ui as T;
        }

        /// <summary>
        /// 异步打开UI。
        /// </summary>
        /// <typeparam name="T">UI的具体类型。</typeparam>
        /// <param name="uiFormAssetPath">UI资源路径。</param>
        /// <param name="uiFormAssetName">UI资源名称。</param>
        /// <param name="uiGroupName">UI组名称。</param>
        /// <param name="pauseCoveredUIForm">是否暂停被覆盖的UI。</param>
        /// <param name="userData">传递给UI的用户数据。</param>
        /// <param name="isFullScreen">是否全屏显示。</param>
        /// <returns>返回打开的UI实例。</returns>
        public async Task<T> OpenAsync<T>(string uiFormAssetPath, string uiFormAssetName, UIGroupDefine uiGroupName, bool pauseCoveredUIForm, object userData, bool isFullScreen) where T : class, IUIForm
        {
            return await OpenAsync<T>(uiFormAssetPath, uiFormAssetName, uiGroupName.Name, pauseCoveredUIForm, userData, isFullScreen);
        }

        /// <summary>
        /// 异步打开UI。
        /// </summary>
        /// <typeparam name="T">UI的具体类型。</typeparam>
        /// <param name="uiFormAssetPath">UI资源路径。</param>
        /// <param name="uiFormAssetName">UI资源名称。</param>
        /// <param name="uiGroupDefine">UI组定义。</param>
        /// <returns>返回打开的UI实例。</returns>
        public Task<IUIForm> OpenAsync<T>(string uiFormAssetPath, string uiFormAssetName, UIGroupDefine uiGroupDefine) where T : class, IUIForm
        {
            return OpenUIFormAsync(uiFormAssetPath, uiFormAssetName, uiGroupDefine.Name, typeof(T), false, null);
        }

        /// <summary>
        /// 异步打开UI。
        /// </summary>
        /// <typeparam name="T">UI的具体类型。</typeparam>
        /// <param name="uiFormAssetPath">UI资源路径。</param>
        /// <param name="uiFormAssetName">UI资源名称。</param>
        /// <param name="uiGroupName">UI组名称。</param>
        /// <param name="isFullScreen">是否全屏显示。</param>
        /// <returns>返回打开的UI实例。</returns>
        public Task<IUIForm> OpenAsync<T>(string uiFormAssetPath, string uiFormAssetName, string uiGroupName, bool isFullScreen = false) where T : class, IUIForm
        {
            return OpenUIFormAsync(uiFormAssetPath, uiFormAssetName, uiGroupName, typeof(T), false, null, isFullScreen);
        }

        /// <summary>
        /// 异步打开UI。
        /// </summary>
        /// <typeparam name="T">UI的具体类型。</typeparam>
        /// <param name="uiGroupName">UI组名称。</param>
        /// <param name="userData">传递给UI的用户数据。</param>
        /// <returns>返回打开的UI实例。</returns>
        public async Task<T> OpenAsync<T>(string uiGroupName, object userData = null) where T : class, IUIForm
        {
            var uiFormAssetName = typeof(T).Name;
            var uiFormAssetPath = Utility.Asset.Path.GetUIPath(uiFormAssetName);
            return await OpenUIFormAsync<T>(uiFormAssetPath, uiGroupName, userData);
        }

        /// <summary>
        /// 异步打开UI。
        /// </summary>
        /// <typeparam name="T">UI的具体类型。</typeparam>
        /// <param name="uiGroupDefine">UI组定义。</param>
        /// <param name="userData">传递给UI的用户数据。</param>
        /// <returns>返回打开的UI实例。</returns>
        public async Task<T> OpenAsync<T>(UIGroupDefine uiGroupDefine, object userData = null) where T : class, IUIForm
        {
            return await OpenAsync<T>(uiGroupDefine.Name, userData);
        }

        /// <summary>
        /// 异步打开UI。
        /// </summary>
        /// <param name="uiFormAssetPath">UI资源路径。</param>
        /// <param name="uiGroupName">UI组名称。</param>
        /// <param name="userData">传递给UI的用户数据。</param>
        /// <param name="isFullScreen">是否全屏显示。</param>
        /// <typeparam name="T">UI的具体类型。</typeparam>
        /// <returns>返回打开的UI实例。</returns>
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
        /// <param name="userData">用户自定义数据。</param>
        /// <param name="isFullScreen">是否全屏</param>
        /// <returns>界面的序列编号。</returns>
        public Task<IUIForm> OpenUIFormAsync<T>(string uiFormAssetPath, string uiFormAssetName, string uiGroupName, object userData, bool isFullScreen = false) where T : class, IUIForm
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
        private async Task<IUIForm> OpenUIFormAsync(string uiFormAssetPath, string uiFormAssetName, string uiGroupName, Type uiFormType, bool pauseCoveredUIForm, object userData, bool isFullScreen = false)
        {
            return await m_UIManager.OpenUIFormAsync(uiFormAssetPath, uiFormAssetName, uiGroupName, uiFormType, pauseCoveredUIForm, userData, isFullScreen);
        }
    }
}