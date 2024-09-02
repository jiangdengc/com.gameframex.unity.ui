using System;

namespace GameFrameX.UI.Runtime
{
    public sealed class OpenUIFormInfo : IReference
    {
        private int m_SerialId = 0;
        private UIGroup m_UIGroup = null;
        private bool m_PauseCoveredUIForm = false;
        private object m_UserData = null;
        private Type m_FormType;

        public Type FormType
        {
            get { return m_FormType; }
        }

        public int SerialId
        {
            get { return m_SerialId; }
        }

        public UIGroup UIGroup
        {
            get { return m_UIGroup; }
        }

        public bool PauseCoveredUIForm
        {
            get { return m_PauseCoveredUIForm; }
        }

        public object UserData
        {
            get { return m_UserData; }
        }

        public static OpenUIFormInfo Create(int serialId, UIGroup uiGroup, Type uiFormType, bool pauseCoveredUIForm, object userData)
        {
            OpenUIFormInfo openUIFormInfo = ReferencePool.Acquire<OpenUIFormInfo>();
            openUIFormInfo.m_SerialId = serialId;
            openUIFormInfo.m_UIGroup = uiGroup;
            openUIFormInfo.m_PauseCoveredUIForm = pauseCoveredUIForm;
            openUIFormInfo.m_UserData = userData;
            openUIFormInfo.m_FormType = uiFormType;
            return openUIFormInfo;
        }

        public void Clear()
        {
            m_SerialId = 0;
            m_UIGroup = null;
            m_PauseCoveredUIForm = false;
            m_UserData = null;
        }
    }
}