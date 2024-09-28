﻿#if UNITY_EDITOR && YNL_EDITOR && YNL_UTILITIES
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using YNL.Editors.Extensions;
using YNL.Extensions.Methods;

namespace YNL.GeneralToolbox.Windows.ImageResizing
{
    public class EImageBox : VisualElement
    {
        private const string _styleSheet = "Style Sheets/Image Resizing/EImageBox";

        public Texture2D AssignedImage;
        public Vector2 NewAssignedSize;

        public Button Background;
        public Image Image;
        public Button Delete;
        public Button Name;

        public Image SizeBox;
        public Button OriginSize;
        public Button NewSize;

        public EImageBox(Texture2D image, Vector2 newSize) : base()
        {
            if (image.IsNull())
            {
                MDebug.Notify("Image not found, something is wrong!");
                return;
            }

            AssignedImage = image;
            NewAssignedSize = newSize;

            this.AddStyle(_styleSheet, ESheet.Font).AddClass("Main");

            Image = new Image().SetBackgroundImage(image).AddClass("Image");
            Delete = new Button().AddClass("Delete");
            Delete.clicked += () => 
            {
                this.RemoveFromHierarchy();
                Main.OnRemoveImage?.Invoke(AssignedImage); 
            };

            Name = new Button().AddClass("Name").SetText(image.name);
            Name.clicked += () => EditorGUIUtility.PingObject(image);

            OriginSize = new Button().AddClass("Size", "OriginSize").SetText($"{image.width} x {image.height}");//.AddElements(new Image().AddClass("Arrow"));
            NewSize = new Button().AddClass("Size", "NewSize").SetText($"{newSize.x} x {newSize.y}");
            SizeBox = new Image().AddClass("SizeBox").AddElements(OriginSize, new Image().AddClass("Arrow"), NewSize);

            Background = new Button().AddClass("Background").AddElements(Name, Delete, Image);
            Background.AddElements(new VisualElement().AddClass("Space"), SizeBox);

            this.AddElements(Background);
        }

        public void SetOriginSize(Vector2 originSize) => OriginSize.SetText($"{originSize.x} x {originSize.y}");
        public void SetNewSize(Vector2 newSize)
        {
            NewSize.SetText($"{newSize.x} x {newSize.y}");
            NewAssignedSize = newSize;
        }
        public void SetNewAspectedSize(int value, bool isWidth)
        {
            Vector2 imageSize = new(AssignedImage.width, AssignedImage.height);
            if (isWidth) SetNewSize(new(value, (int)((imageSize.y / imageSize.x) * value)));
            else SetNewSize(new((int)((imageSize.x / imageSize.y) * value), value));
        }

        public void RefreshOriginSizeLabel() => SetOriginSize(new(AssignedImage.width, AssignedImage.height));
    }
}
#endif