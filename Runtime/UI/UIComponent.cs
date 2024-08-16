/*using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GameFrameX.Asset.Runtime;
using UnityEngine;
using GameFrameX.Runtime;

namespace GameFrameX.UI.Runtime
{
    /// <summary>
    /// UI组件
    /// </summary>
    [DisallowMultipleComponent]
    [AddComponentMenu("Game Framework/UI")]
    public abstract class UIComponent : GameFrameworkComponent
    {
        private IUIManager m_UIManager;
        protected IAssetManager AssetManager;
        protected UI Root;
        protected UI HiddenRoot;
        protected UI FloorRoot;
        protected UI NormalRoot;
        protected UI FixedRoot;
        protected UI WindowRoot;
        protected UI TipRoot;
        protected UI BlackBoardRoot;
        protected UI DialogueRoot;
        protected UI GuideRoot;
        protected UI LoadingRoot;
        protected UI NotifyRoot;
        protected UI SystemRoot;
        private readonly Dictionary<UILayer, Dictionary<string, UI>> m_UILayerDictionary = new Dictionary<UILayer, Dictionary<string, UI>>(16);

        private void Start()
        {
            AssetManager = GameFrameworkEntry.GetModule<IAssetManager>();
            if (AssetManager == null)
            {
                Log.Fatal("Asset manager is invalid.");
                return;
            }
        }

        protected override void Awake()
        {
            ImplementationComponentType = Type.GetType(componentType);
            InterfaceComponentType = typeof(IUIManager);
            base.Awake();
            m_UIManager = GameFrameworkEntry.GetModule<IUIManager>();
            if (m_UIManager == null)
            {
                Log.Fatal("UI manager is invalid.");
                return;
            }


            Root = CreateRootNode();
            GameFrameworkGuard.NotNull(Root, nameof(Root));
            Root.Show();
            HiddenRoot = CreateNode(Root, UILayer.Hidden);
            FloorRoot = CreateNode(Root, UILayer.Floor);
            NormalRoot = CreateNode(Root, UILayer.Normal);
            FixedRoot = CreateNode(Root, UILayer.Fixed);
            WindowRoot = CreateNode(Root, UILayer.Window);
            TipRoot = CreateNode(Root, UILayer.Tip);
            BlackBoardRoot = CreateNode(Root, UILayer.BlackBoard);
            DialogueRoot = CreateNode(Root, UILayer.Dialogue);
            GuideRoot = CreateNode(Root, UILayer.Guide);
            LoadingRoot = CreateNode(Root, UILayer.Loading);
            NotifyRoot = CreateNode(Root, UILayer.Notify);
            SystemRoot = CreateNode(Root, UILayer.System);


            m_UILayerDictionary[UILayer.Hidden] = new Dictionary<string, UI>(64);
            m_UILayerDictionary[UILayer.Floor] = new Dictionary<string, UI>(64);
            m_UILayerDictionary[UILayer.Normal] = new Dictionary<string, UI>(64);
            m_UILayerDictionary[UILayer.Fixed] = new Dictionary<string, UI>(64);
            m_UILayerDictionary[UILayer.Window] = new Dictionary<string, UI>(64);
            m_UILayerDictionary[UILayer.Tip] = new Dictionary<string, UI>(64);
            m_UILayerDictionary[UILayer.BlackBoard] = new Dictionary<string, UI>(64);
            m_UILayerDictionary[UILayer.Dialogue] = new Dictionary<string, UI>(64);
            m_UILayerDictionary[UILayer.Guide] = new Dictionary<string, UI>(64);
            m_UILayerDictionary[UILayer.Loading] = new Dictionary<string, UI>(64);
            m_UILayerDictionary[UILayer.Notify] = new Dictionary<string, UI>(64);
            m_UILayerDictionary[UILayer.System] = new Dictionary<string, UI>(64);
        }

        /// <summary>
        /// 创建根节点
        /// </summary>
        /// <returns></returns>
        protected abstract UI CreateRootNode();

        /// <summary>
        /// 创建UI节点
        /// </summary>
        /// <param name="root">根节点</param>
        /// <param name="layer">UI层级</param>
        /// <returns></returns>
        protected abstract UI CreateNode(object root, UILayer layer);

        /// <summary>
        /// 添加全屏UI对象
        /// </summary>
        /// <param name="creator">UI创建器</param>
        /// <param name="descFilePath">UI目录</param>
        /// <param name="layer">目标层级</param>
        /// <param name="userData">用户自定义数据</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>返回创建后的UI对象</returns>
        public T AddToFullScreen<T>(System.Func<object, T> creator, string descFilePath, UILayer layer, object userData = null) where T : UI
        {
            return Add<T>(creator, descFilePath, layer, true, userData);
        }

        /// <summary>
        /// 异步添加UI 对象
        /// </summary>
        /// <param name="creator">UI创建器</param>
        /// <param name="descFilePath">UI目录</param>
        /// <param name="layer">目标层级</param>
        /// <param name="isFullScreen">是否全屏</param>
        /// <param name="userData">用户自定义数据</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>返回创建后的UI对象</returns>
        public abstract UniTask<T> AddAsync<T>(System.Func<object, T> creator, string descFilePath, UILayer layer, bool isFullScreen = false, object userData = null) where T : UI;


        /// <summary>
        /// 添加UI对象
        /// </summary>
        /// <param name="creator">UI创建器</param>
        /// <param name="descFilePath">UI目录</param>
        /// <param name="layer">目标层级</param>
        /// <param name="isFullScreen">是否全屏</param>
        /// <param name="userData">用户自定义数据</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>返回创建后的UI对象</returns>
        /// <exception cref="ArgumentNullException">创建器不存在,引发参数异常</exception>
        public virtual T Add<T>(System.Func<object, T> creator, string descFilePath, UILayer layer, bool isFullScreen = false, object userData = null) where T : UI
        {
            GameFrameworkGuard.NotNull(creator, nameof(creator));
            GameFrameworkGuard.NotNull(descFilePath, nameof(descFilePath));
            var ui = creator(userData);
            Add(ui, layer);
            if (isFullScreen)
            {
                ui.MakeFullScreen();
            }

            return ui;
        }


        /// <summary>
        /// 从UI管理列表中删除所有的UI
        /// </summary>
        public void RemoveAll()
        {
            var tempKv = new Dictionary<string, UI>(32);
            foreach (var kv in m_UILayerDictionary)
            {
                tempKv.Clear();
                foreach (var fui in kv.Value)
                {
                    tempKv[fui.Key] = fui.Value;
                }

                foreach (var fui in tempKv)
                {
                    Remove(fui.Key);
                    fui.Value.Dispose();
                }

                kv.Value.Clear();
            }

            tempKv.Clear();
            m_UILayerDictionary.Clear();
        }

        protected UI Add(UI ui, UILayer layer)
        {
            GameFrameworkGuard.NotNull(ui, nameof(ui));
            if (m_UILayerDictionary[layer].ContainsKey(ui.Name))
            {
                return m_UILayerDictionary[layer][ui.Name];
            }

            m_UILayerDictionary[layer][ui.Name] = ui;
            switch (layer)
            {
                case UILayer.Hidden:
                    HiddenRoot.Add(ui);
                    break;
                case UILayer.Floor:
                    FloorRoot.Add(ui);
                    break;
                case UILayer.Normal:
                    NormalRoot.Add(ui);
                    break;
                case UILayer.Fixed:
                    FixedRoot.Add(ui);
                    break;
                case UILayer.Window:
                    WindowRoot.Add(ui);
                    break;
                case UILayer.Tip:
                    TipRoot.Add(ui);
                    break;
                case UILayer.BlackBoard:
                    BlackBoardRoot.Add(ui);
                    break;
                case UILayer.Dialogue:
                    DialogueRoot.Add(ui);
                    break;
                case UILayer.Guide:
                    GuideRoot.Add(ui);
                    break;
                case UILayer.Loading:
                    LoadingRoot.Add(ui);
                    break;
                case UILayer.Notify:
                    NotifyRoot.Add(ui);
                    break;
                case UILayer.System:
                    SystemRoot.Add(ui);
                    break;
            }

            return ui;
        }

        /// <summary>
        /// 根据UI名称从UI管理列表中移除
        /// </summary>
        /// <param name="uiName"></param>
        /// <returns></returns>
        public bool Remove(string uiName)
        {
            GameFrameworkGuard.NotNullOrEmpty(uiName, nameof(uiName));
            if (SystemRoot.Remove(uiName))
            {
                m_UILayerDictionary[UILayer.System].Remove(uiName);

                return true;
            }

            if (NotifyRoot.Remove(uiName))
            {
                m_UILayerDictionary[UILayer.Notify].Remove(uiName);
                return true;
            }

            if (HiddenRoot.Remove(uiName))
            {
                m_UILayerDictionary[UILayer.Hidden].Remove(uiName);
                return true;
            }

            if (FloorRoot.Remove(uiName))
            {
                m_UILayerDictionary[UILayer.Floor].Remove(uiName);
                return true;
            }

            if (NormalRoot.Remove(uiName))
            {
                m_UILayerDictionary[UILayer.Normal].Remove(uiName);
                return true;
            }

            if (FixedRoot.Remove(uiName))
            {
                m_UILayerDictionary[UILayer.Fixed].Remove(uiName);
                return true;
            }

            if (WindowRoot.Remove(uiName))
            {
                m_UILayerDictionary[UILayer.Window].Remove(uiName);
                return true;
            }

            if (TipRoot.Remove(uiName))
            {
                m_UILayerDictionary[UILayer.Tip].Remove(uiName);
                return true;
            }

            if (BlackBoardRoot.Remove(uiName))
            {
                m_UILayerDictionary[UILayer.BlackBoard].Remove(uiName);
                return true;
            }

            if (DialogueRoot.Remove(uiName))
            {
                m_UILayerDictionary[UILayer.Dialogue].Remove(uiName);
                return true;
            }

            if (GuideRoot.Remove(uiName))
            {
                m_UILayerDictionary[UILayer.Guide].Remove(uiName);
                return true;
            }

            if (LoadingRoot.Remove(uiName))
            {
                m_UILayerDictionary[UILayer.Loading].Remove(uiName);
                return true;
            }

            return false;
        }

        /// <summary>
        /// 根据UI名称和层级从UI管理列表中移除
        /// </summary>
        /// <param name="uiName">UI名称</param>
        /// <param name="layer">层级</param>
        /// <returns></returns>
        public void Remove(string uiName, UILayer layer)
        {
            GameFrameworkGuard.NotNullOrEmpty(uiName, nameof(uiName));
            switch (layer)
            {
                case UILayer.Hidden:
                    HiddenRoot.Remove(uiName);
                    m_UILayerDictionary[UILayer.Hidden].Remove(uiName);
                    break;
                case UILayer.Floor:
                    FloorRoot.Remove(uiName);
                    m_UILayerDictionary[UILayer.Floor].Remove(uiName);
                    break;
                case UILayer.Normal:
                    NormalRoot.Remove(uiName);
                    m_UILayerDictionary[UILayer.Normal].Remove(uiName);
                    break;
                case UILayer.Fixed:
                    FixedRoot.Remove(uiName);
                    m_UILayerDictionary[UILayer.Fixed].Remove(uiName);
                    break;
                case UILayer.Window:
                    WindowRoot.Remove(uiName);
                    m_UILayerDictionary[UILayer.Window].Remove(uiName);
                    break;
                case UILayer.Tip:
                    TipRoot.Remove(uiName);
                    m_UILayerDictionary[UILayer.Tip].Remove(uiName);
                    break;
                case UILayer.BlackBoard:
                    BlackBoardRoot.Remove(uiName);
                    m_UILayerDictionary[UILayer.BlackBoard].Remove(uiName);
                    break;
                case UILayer.Dialogue:
                    DialogueRoot.Remove(uiName);
                    m_UILayerDictionary[UILayer.Dialogue].Remove(uiName);
                    break;
                case UILayer.Guide:
                    GuideRoot.Remove(uiName);
                    m_UILayerDictionary[UILayer.Guide].Remove(uiName);
                    break;
                case UILayer.Loading:
                    LoadingRoot.Remove(uiName);
                    m_UILayerDictionary[UILayer.Loading].Remove(uiName);
                    break;
                case UILayer.Notify:
                    NotifyRoot.Remove(uiName);
                    m_UILayerDictionary[UILayer.Notify].Remove(uiName);
                    break;
                case UILayer.System:
                    SystemRoot.Remove(uiName);
                    m_UILayerDictionary[UILayer.System].Remove(uiName);
                    break;
            }
        }

        /// <summary>
        /// 判断UI名称是否在UI管理列表
        /// </summary>
        /// <param name="uiName">UI名称</param>
        /// <returns></returns>
        public bool Has(string uiName)
        {
            GameFrameworkGuard.NotNullOrEmpty(uiName, nameof(uiName));
            return Get(uiName) != null;
        }

        /// <summary>
        /// 判断UI是否在UI管理列表，如果存在则返回对象，不存在返回空值
        /// </summary>
        /// <param name="uiName">UI名称</param>
        /// <param name="fui"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool Has<T>(string uiName, out T fui) where T : UI
        {
            GameFrameworkGuard.NotNullOrEmpty(uiName, nameof(uiName));
            var ui = Get(uiName);
            fui = ui as T;
            return fui != null;
        }

        /// <summary>
        /// 根据UI名称获取UI对象
        /// </summary>
        /// <param name="uiName">UI名称</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Get<T>(string uiName) where T : UI
        {
            T fui = default;
            GameFrameworkGuard.NotNullOrEmpty(uiName, nameof(uiName));
            foreach (var kv in m_UILayerDictionary)
            {
                if (kv.Value.TryGetValue(uiName, out var ui))
                {
                    fui = ui as T;
                    break;
                }
            }

            return fui;
        }

        /// <summary>
        /// 根据UI名称获取UI对象
        /// </summary>
        /// <param name="uiName"></param>
        /// <returns></returns>
        public UI Get(string uiName)
        {
            GameFrameworkGuard.NotNullOrEmpty(uiName, nameof(uiName));
            foreach (var kv in m_UILayerDictionary)
            {
                if (kv.Value.TryGetValue(uiName, out var ui))
                {
                    return ui;
                }
            }

            return null;
        }

        protected virtual void OnDestroy()
        {
            RemoveAll();
            Root?.Dispose();
            Root = null;
        }
    }
}*/