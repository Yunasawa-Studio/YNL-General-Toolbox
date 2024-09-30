#if UNITY_EDITOR && YNL_EDITOR
using UnityEditor;
using UnityEngine;
using YNL.Extensions.Methods;
using YNL.Editors.Visuals;
using YNL.Editors.Extensions;
using UnityEngine.UIElements;
using YNL.GeneralToolbox.Settings;

namespace YNL.GeneralToolbox.Windows
{
    public class GeneralToolbox : EditorWindow
    {
        #region ▶ Editor Asset Fields/Properties
        private const string _windowIconPath = "Textures/General/Editor Window Icon";
        private static ToolboxSettings _toolboxSettings => ToolboxSettingsProvider.Settings;
        #endregion

        #region ▶ Visual Elements
        public StyledWindowTagPanel WindowTagPanel;

        public ImageResizing.Main ImageResizerWindow;
        public ImageInverting.Main ImageInverterWindow;
        public AnimationRepathing.Main ObjectRenamerWindow;

        private StyledWindowTag _imageResizingTag;
        private StyledWindowTag _imageInvertingTag;
        private StyledWindowTag _animationRepathingTag;
        #endregion

        #region ▶ General Fields/Properties
        private float _tagPanelWidth = 200;

        private IMain _selectedWindow;
        #endregion


        [MenuItem("🔗 YのL/▷ YNL - General Toolbox/🎲 Editor Window")]
        public static void ShowWindow()
        {
            GeneralToolbox window = GetWindow<GeneralToolbox>("General Toolbox");
            Texture2D texture = Resources.Load<Texture2D>(_windowIconPath);

            window.titleContent.image = texture;
            window.maxSize = new Vector2(800, 500);
            window.minSize = window.maxSize;
        }

        #region ▶ Editor Messages
        private void OnSelectionChange()
        {
            if (!_selectedWindow.IsNull()) _selectedWindow.OnSelectionChange();
        }

        public void CreateGUI()
        {
            Texture2D windowIcon = Resources.Load<Texture2D>("Textures/General/Editor Icon");

            Texture2D textureImageResizerIcon = Resources.Load<Texture2D>("Textures/Image Resizing/Image Resizer Icon");
            Texture2D textureImageInverterIcon = Resources.Load<Texture2D>("Textures/Image Inverting/Image Inverter Icon 2");
            Texture2D animationObjectRenamerIcon = Resources.Load<Texture2D>("Textures/Animation Repathing/Cracking Bone");

            Texture2D waitIcon = Resources.Load<Texture2D>("Textures/Icons/Time1");

            _imageResizingTag = new StyledWindowTag(textureImageResizerIcon, "Image Resizer", "Texture", Color.white, _tagPanelWidth - 25, () => SwitchWindow(WindowType.ImageResizing));
            _imageInvertingTag = new StyledWindowTag(textureImageInverterIcon, "Image Inverter", "Texture", Color.white, _tagPanelWidth - 25, () => SwitchWindow(WindowType.ImageInverting));
            _animationRepathingTag = new StyledWindowTag(animationObjectRenamerIcon, "Object Renamer", "Animation", Color.white, _tagPanelWidth - 25, () => SwitchWindow(WindowType.AnimationRepathing));

            WindowTagPanel = new(windowIcon, "General Toolbox", "Center", _tagPanelWidth, new StyledWindowTag[]
            {
                _imageResizingTag,
                _imageInvertingTag,
                _animationRepathingTag,
                new StyledWindowTag(waitIcon, "Coming Soon", "", Color.white, _tagPanelWidth - 25, () => SwitchWindow(WindowType.None))
            });

            ImageResizerWindow = new ImageResizing.Main(this, WindowTagPanel);
            ObjectRenamerWindow = new AnimationRepathing.Main(this, WindowTagPanel);
            ImageInverterWindow = new ImageInverting.Main(this, WindowTagPanel);

            SwitchWindow(_toolboxSettings.CurrentWindow);
            SelectTag(_toolboxSettings.CurrentWindow);
        }

        public void OnGUI()
        {
            if (!_selectedWindow.IsNull()) _selectedWindow.OnGUI();
        }
        #endregion

        private void SwitchWindow(WindowType windowTag)
        {
            rootVisualElement.RemoveAllElements();
            switch (windowTag)
            {
                case WindowType.ImageResizing:
                    SwitchWindow(ImageResizerWindow);
                    rootVisualElement.Add(ImageResizerWindow.Visual);
                    break;
                case WindowType.ImageInverting:
                    SwitchWindow(ImageInverterWindow);
                    rootVisualElement.Add(ImageInverterWindow.Visual);
                    break;
                case WindowType.AnimationRepathing:
                    SwitchWindow(ObjectRenamerWindow);
                    rootVisualElement.Add(ObjectRenamerWindow.Visual);
                    break;
            }
            rootVisualElement.Add(WindowTagPanel);

            _toolboxSettings.CurrentWindow = windowTag;
            _toolboxSettings.SaveSettings();
        }

        private void SelectTag(WindowType type)
        {
            switch (type)
            {
                case WindowType.ImageResizing: _imageResizingTag.Select(); break;
                case WindowType.ImageInverting: _imageInvertingTag.Select(); break;
                case WindowType.AnimationRepathing: _animationRepathingTag.Select(); break;
            }
        }

        private void SwitchWindow(IMain window)
        {
            _selectedWindow = window;
            //WindowTagPanel.Tutorial.clicked += _selectedWindow.OpenInstruction;
        }
    }

    public enum WindowType
    {
        None, AnimationRepathing, ImageResizing, ImageInverting
    }

    public interface IMain
    {
        public virtual void OnSelectionChange() { }
        public virtual void CreateGUI() { }
        public virtual void OnGUI() { }
    }

    public abstract class EVisual : VisualElement
    {
        protected string _windowIcon;
        protected string _windowTitle;
        protected string _windowSubtitle;

        public void SetWindowTitle(string icon, string title, string subtitle)
        {
            _windowIcon = icon;
            _windowTitle = title;
            _windowSubtitle = subtitle;
        }
    }
}
#endif
