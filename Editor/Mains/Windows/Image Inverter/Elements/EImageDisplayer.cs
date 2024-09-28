#if UNITY_EDITOR && YNL_EDITOR
using UnityEngine;
using UnityEngine.UIElements;
using YNL.Editors.Extensions;
using YNL.Editors.Visuals;

namespace YNL.GeneralToolbox.Windows.ImageInverting
{
    public class EImageDisplayer : Image
    {
        private const string _styleSheet = "Style Sheets/Image Resizing/EImageDisplayer";

        public FlexibleAssetsField<Object> Background;
        public Image Grid;
        public ScrollView Scroll;

        public EImageDisplayer() : base()
        {

            this.AddStyle(_styleSheet).AddClass("Main");

            Grid = new Image().AddClass("Grid").SetName("Grid");
            Scroll = new ScrollView().AddClass("Scroll").AddElements(Grid);
            Background = new FlexibleAssetsField<Object>().AddClass("Background").AddElements(Scroll).SetName("Background");
            Background.OnDragPerform += TryGetAsset;

            this.AddElements(Background);
        }

        public void GenerateImages(Texture2D[] textures, float width)
        {
            Grid.RemoveAllElements();

            foreach (var texture in textures)
            {
                //MDebug.Custom("Image", texture.IsNull() ? "is null" : texture.name);
                EImageBox imageBox = new EImageBox(texture).SetSize(width * 2.5f, width * 1.5f);
                Grid.AddElements(imageBox);
            }
        }
        public void ClearItems() => Grid.RemoveAllElements();
        private void TryGetAsset(Object[] assets)
        {
            Main.OnAddImage?.Invoke(assets);
        }
    }
}
#endif