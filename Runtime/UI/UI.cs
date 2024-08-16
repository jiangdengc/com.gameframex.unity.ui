/*using System;
using System.Collections.Generic;
using System.Linq;
using GameFrameX.ObjectPool;
using UnityEngine;

namespace GameFrameX.UI.Runtime
{
    public abstract class UI : ObjectBase, IDisposable
    {
        /// <summary>
        /// 用户自定义数据
        /// </summary>
        protected object UserData { get; private set; }

        /// <summary>
        /// 界面显示之前触发
        /// </summary>
        public Action<UI> OnShowBeforeAction { get; set; }

        /// <summary>
        /// 界面显示之后触发
        /// </summary>
        public Action<UI> OnShowAfterAction { get; set; }

        /// <summary>
        /// 界面隐藏之前触发
        /// </summary>
        public Action<UI> OnHideBeforeAction { get; set; }

        /// <summary>
        /// 界面隐藏之后触发
        /// </summary>
        public Action<UI> OnHideAfterAction { get; set; }

        /// <summary>
        /// 记录初始化UI是否是显示的状态
        /// </summary>
        protected bool IsInitVisible { get; }

        public UI(GameObject owner, object userData = null, bool isRoot = false)
        {
            UserData = userData;
            Owner = owner;
            IsInitVisible = Owner.activeSelf;
            IsRoot = isRoot;
            InitView();
            // 在初始化的时候先隐藏UI。后续由声明周期控制
            // if (parent == null)
            // {
            // SetVisibleWithNoNotify(false);
            // }

            Init();

            // parent?.Add(this);

            if (owner.name.IsNullOrWhiteSpace())
            {
                Name = GetType().Name;
            }
            else
            {
                Name = owner.name;
            }
        }

        protected virtual void InitView()
        {
        }

        /// <summary>
        /// 界面添加到UI系统之前执行
        /// </summary>
        protected virtual void Init()
        {
            // Log.Info("Init " + Name);
        }

        /// <summary>
        /// 界面显示后执行
        /// </summary>
        protected virtual void OnShow()
        {
            // Log.Info("OnShow " + Name);
        }


        /// <summary>
        /// 界面显示之后执行，设置数据和多语言建议在这里设置
        /// </summary>
        public virtual void Refresh()
        {
            // Log.Info("Refresh " + Name);
        }

        /// <summary>
        /// 界面隐藏之前执行
        /// </summary>
        protected virtual void OnHide()
        {
            // Log.Info("OnHide " + Name);
        }

        /// <summary>
        /// UI 对象销毁之前执行
        /// </summary>
        protected virtual void OnDispose()
        {
            // Log.Info("OnDispose " + Name);
        }

        /// <summary>
        /// 显示UI
        /// </summary>
        public virtual void Show(object userData = null)
        {
            UserData = userData;
            // Log.Info("Show " + Name);
            if (Visible)
            {
                OnShowBeforeAction?.Invoke(this);
                OnShow();
                OnShowAfterAction?.Invoke(this);
                Refresh();
                return;
            }

            Visible = true;
        }


        /// <summary>
        /// 隐藏UI
        /// </summary>
        public virtual void Hide()
        {
            // Log.Info("Hide " + Name);
            if (!Visible)
            {
                OnHideBeforeAction?.Invoke(this);
                OnHide();
                OnHideAfterAction?.Invoke(this);
                return;
            }

            Visible = false;
        }

        /// <summary>
        /// UI 对象
        /// </summary>
        public GameObject Owner { get; }

        /// <summary>
        /// 是否是UI根
        /// </summary>
        public bool IsRoot { get; private set; }

        /// <summary>
        /// UI 变换对象
        /// </summary>
        public Transform Transform
        {
            get { return Owner?.transform; }
        }

        /// <summary>
        /// UI 名称
        /// </summary>
        public sealed override string Name
        {
            get
            {
                if (Owner == null)
                {
                    return string.Empty;
                }

                return Owner.name;
            }

            protected set
            {
                if (Owner == null)
                {
                    return;
                }

                if (Owner.name.IsNotNullOrWhiteSpace() && Owner.name == value)
                {
                    return;
                }

                Owner.name = value;
            }
        }

        protected override void Release(bool isShutdown)
        {
            OnShowBeforeAction = null;
            OnShowAfterAction = null;
            OnHideBeforeAction = null;
            OnHideAfterAction = null;
            Parent = null;
        }

        /// <summary>
        /// 设置UI的显示状态，不发出事件
        /// </summary>
        /// <param name="value"></param>
        public virtual void SetVisibleWithNoNotify(bool value)
        {
            if (Owner.activeSelf == value)
            {
                return;
            }

            Owner.SetActive(value);
        }

        /// <summary>
        /// 获取UI是否显示
        /// </summary>
        public bool IsVisible
        {
            get { return Visible; }
        }

        protected virtual bool Visible
        {
            get
            {
                if (Owner == null)
                {
                    return false;
                }

                return Owner.activeSelf;
            }
            set
            {
                if (Owner == null)
                {
                    return;
                }

                if (Owner.activeSelf == value)
                {
                    return;
                }

                if (value == false)
                {
                    OnHideBeforeAction?.Invoke(this);
                    foreach (var child in this.Children)
                    {
                        child.Value.Visible = value;
                    }

                    OnHide();
                    OnHideAfterAction?.Invoke(this);
                }

                Owner.SetActive(value);
                if (value)
                {
                    OnShowBeforeAction?.Invoke(this);
                    foreach (var child in Children)
                    {
                        child.Value.Visible = value;
                    }

                    OnShow();
                    OnShowAfterAction?.Invoke(this);
                    Refresh();
                }
            }
        }


        /// <summary>
        /// 界面对象是否为空
        /// </summary>
        public bool IsEmpty
        {
            get { return Owner == null; }
        }

        protected readonly Dictionary<string, UI> Children = new Dictionary<string, UI>();

        /// <summary>
        /// 是否从对象池获取
        /// </summary>
        protected bool IsFromPool { get; set; }


        protected bool IsDisposed;

        /// <summary>
        /// 销毁UI对象
        /// </summary>
        public virtual void Dispose()
        {
            if (IsDisposed)
            {
                return;
            }

            IsDisposed = true;
            // 删除所有的孩子
            DisposeChildren();

            // 删除自己的UI
            /*if (!IsRoot)
            {
                RemoveFromParent();
            }#1#

            // 释放UI
            OnDispose();
            // 删除自己的UI
            /*if (!IsRoot)
            {
                GObject.DestroyObject();
            }#1#

            Release(false);
            // isFromFGUIPool = false;
        }

        /// <summary>
        /// 添加UI对象到子级列表
        /// </summary>
        /// <param name="ui"></param>
        /// <param name="index">添加到的目标UI层级索引位置</param>
        /// <exception cref="Exception"></exception>
        public void Add(UI ui, int index = -1)
        {
            if (ui == null || ui.IsEmpty)
            {
                throw new Exception($"ui can not be empty");
            }

            if (string.IsNullOrWhiteSpace(ui.Name))
            {
                throw new Exception($"ui.Name can not be empty");
            }

            if (Children.ContainsKey(ui.Name))
            {
                throw new Exception($"ui.Name({ui.Name}) already exist");
            }

            AddInner(ui, index);
        }

        protected virtual void AddInner(UI ui, int index = -1)
        {
            Children.Add(ui.Name, ui);
            if (index < 0 || index > Children.Count)
            {
                ui.Transform.SetParent(Transform);
            }
            else
            {
                ui.Transform.SetParent(Transform);
                ui.Transform.SetSiblingIndex(index);
            }

            ui.Parent = this;

            if (ui.IsInitVisible)
            {
                // 显示UI
                ui.Show(ui.UserData);
            }
        }

        /// <summary>
        /// UI 父级对象
        /// </summary>
        public UI Parent { get; protected set; }

        /// <summary>
        /// 设置当前UI对象为全屏
        /// </summary>
        public abstract void MakeFullScreen();

        /// <summary>
        /// 将自己从父级UI对象删除
        /// </summary>
        public virtual void RemoveFromParent()
        {
            Parent?.Remove(Name);
        }

        /// <summary>
        /// 删除指定UI名称的UI对象
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public virtual bool Remove(string name)
        {
            if (Children.TryGetValue(name, out var ui))
            {
                Children.Remove(name);

                if (ui != null)
                {
                    ui.RemoveChildren();
                    ui.Hide();
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 销毁所有自己对象
        /// </summary>
        public void DisposeChildren()
        {
            if (Children.Count > 0)
            {
                var children = GetAll();
                foreach (var child in children)
                {
                    child.Dispose();
                }

                Children.Clear();
            }
        }

        /// <summary>
        /// 删除所有子级UI对象
        /// </summary>
        public void RemoveChildren()
        {
            if (Children.Count > 0)
            {
                var children = GetAll();

                foreach (var child in children)
                {
                    child.RemoveFromParent();
                }

                Children.Clear();
            }
        }

        /// <summary>
        /// 根据 UI名称 获取子级UI对象
        /// </summary>
        /// <param name="name">UI名称</param>
        /// <returns></returns>
        public UI Get(string name)
        {
            if (Children.TryGetValue(name, out var child))
            {
                return child;
            }

            return null;
        }

        /// <summary>
        /// 获取所有的子级UI，非递归
        /// </summary>
        /// <returns></returns>
        public UI[] GetAll()
        {
            return Children.Values.ToArray();
        }
    }
}*/