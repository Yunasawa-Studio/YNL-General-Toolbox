#if UNITY_EDITOR && YNL_EDITOR && YNL_UTILITIES
using UnityEditor;
using YNL.Editors.Visuals;
using YNL.Extensions.Methods;

namespace YNL.GeneralToolbox.Windows.AnimationRepathing
{
    [System.Serializable]
    public class Main : IMain
    {
        public EditorWindow Root;

        public Visual Visual;
        public Handler Handler;

        public Main(EditorWindow root, StyledWindowTagPanel tagPanel)
        {
            Root = root;

            Handler = new(this);
            Visual = new(tagPanel, this);
        }

        public void OnSelectionChange()
        {
            Handler.OnSelectionChange();
        }

        public void OnGUI()
        {
            if (!Visual.IsNull()) Visual.OnGUI();
        }
    }
}
#endif