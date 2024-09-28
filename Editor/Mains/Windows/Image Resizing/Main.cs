#if UNITY_EDITOR && YNL_EDITOR
using System;
using UnityEditor;
using UnityEngine;
using YNL.Editors.Visuals;

namespace YNL.GeneralToolbox.Windows.ImageResizing
{
    [System.Serializable]
    public class Main : IMain
    {
        #region ▶ Editor Properties
        public EditorWindow Root;

        public Visual Visual;
        public Handler Handler;
        #endregion
        #region ▶ Actions
        public static Action<UnityEngine.Object[]> OnAddImage;
        public static Action<Texture2D> OnRemoveImage;
        #endregion

        public Main(EditorWindow root, StyledWindowTagPanel tagPanel)
        {
            Root = root;

            Handler = new(this);
            Visual = new(tagPanel, this);
        }
    }
}
#endif