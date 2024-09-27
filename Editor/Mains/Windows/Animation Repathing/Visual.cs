using System.Collections.Generic;
using System.Linq;
using UnityEngine.UIElements;
using UnityEngine;
using YNL.Extensions.Addons;
using YNL.Extensions.Methods;
using YNL.Utilities.Extensions;
using YNL.Editors.Visuals;
using YNL.Editors.Extensions;
using System;

namespace YNL.GeneralToolbox.Windows.AnimationRepathing
{
    public partial class Visual : EVisual
    {
        private const string _styleSheet = "Style Sheets/Animation Repathing/Visual";

        #region ▶ Visual Elements
        private StyledWindowTitle _windowTitlePanel;
        private StyledWindowTagPanel _tagPanel;
        public FlexibleInteractImage PropertyPanel;

        public Button AnimatorPanel;
        private static ScrollView _clipsScroll;

        //private Button _automaticPanel;

        private Label _referencedAnimatorTitle;
        private Label _referencedClipsTitle;
        public static RepaintedComponentField<Animator> ReferencedAnimator;

        public VisualElement HandlerWindow;

        public Image AnimatorWindow;

        private EInputNamePanel _inputNamePanel;
        private static ERootNamePanel _rootNamePanel;

        //private Image _automaticWindow;
        //private static EAutomaticLogPanel _automaticLogPanel;

        public Button ModePanel;
        public Button ManualButton;
        public Image ManualIcon;
        public Label ManualLabel;

        //private static FlexibleInteractButton _enableButton;
        //private static Image _enableIcon;
        //private static Label _enableLabel;
        #endregion
        #region ▶ Style Classes
        private const string _class_root = "RootWindow";

        private const string _class_propertyPanel = "PropertyPanel";
        private const string _class_clipsScroll = "ClipsScroll";

        private const string _class_animatorField = "AnimatorField";

        private const string _class_handlerWindow = "HandlerWindow";
        private const string _class_titlePanel = "TitlePanel";
        #endregion
        #region ▶ General Fields/Properties
        private bool _createdAllElements = false;
        public float TagPanelWidth = 200;
        public MRange PropertyPanelWidth = new MRange(100, 300);

        private Main _main;

        #endregion

        public Visual(StyledWindowTagPanel tagPanel, Main main)
        {
            SetWindowTitle
            (
                "Textures/Animation Repathing/Cracking Bone",
                "Animation - Object Renamer",
                "Easily change or swap your animation objects' name"
            );

            _tagPanel = tagPanel;
            _main = main;

            this.AddStyle(_styleSheet, ESheet.Font);

            CreateElements();
            AddClasses();

            TagPanelHandlers();
            PropertyPanelHandler();
            HandlerWindowCreator();

            this.AddElements(HandlerWindow, _windowTitlePanel, PropertyPanel);

            _createdAllElements = true;

            Variable.OnVisualCreated?.Invoke(this, _main.Handler);
        }

        public void OnGUI()
        {
            if (!_createdAllElements) return;

            ReferencedAnimator.OnGUI();
        }

        #region ▶ Editor Initializing
        private void CreateElements()
        {
            ManualIcon = new Image().SetName("ModeIcon").SetBackgroundImage("Textures/Animation Repathing/Manual").SetMarginLeft(90).SetLeft(-62.5f);
            ManualLabel = new Label().SetName("ModeLabel").SetText("Manual");
            ManualButton = new Button().SetName("ModeButton").AddElements(ManualIcon, ManualLabel).SetBackgroundColor("#1f1f1f").SetWidth(100, true);

            ModePanel = new Button().AddClass("ModePanel").AddElements(ManualButton);

            #region Animator Panel
            _referencedClipsTitle = new Label("Animation Clips:").AddClass("ClipsTitle");
            _clipsScroll = new ScrollView().SetWidth(100, true).SetHeight(100, true);

            _referencedAnimatorTitle = new Label("Referenced Animator:").AddClass("AnimatorTitle");
            ReferencedAnimator = new RepaintedComponentField<Animator>(Handler.ReferencedAnimator);
            ReferencedAnimator.Background.OnDragPerform += (obj) => PresentAllPaths();

            AnimatorPanel = new Button()
                .AddSpace(0, 10).AddClass("AnimatorPanel")
                .AddElements(_referencedAnimatorTitle, ReferencedAnimator, _referencedClipsTitle, _clipsScroll);
            #endregion

            //_propertyPanel = new FlexibleInteractImage().AddElements(_modePanel);
            PropertyPanel = new FlexibleInteractImage().AddElements(ModePanel, AnimatorPanel);

            _windowTitlePanel = new(_windowIcon.LoadResource<Texture2D>(), _windowTitle, _windowSubtitle);
        }
        private void AddClasses()
        {
            this.AddClass(_class_root);

            PropertyPanel.AddClass(_class_propertyPanel);
            _clipsScroll.AddClass(_class_clipsScroll);

            ReferencedAnimator.AddClass(_class_animatorField);

            _windowTitlePanel.AddClass(_class_titlePanel);
        }
        #endregion
        #region ▶ Specific Elements Handlers
        private void TagPanelHandlers()
        {
            _tagPanel.OnPointerEnter += () =>
            {
                PropertyPanel.SetMarginLeft(TagPanelWidth);
                _windowTitlePanel.Panel.SetMarginLeft(TagPanelWidth - 50);
                AnimatorWindow.SetMarginLeft(TagPanelWidth + 102);

                Variable.OnTagPanelExpanded?.Invoke();
            };
            _tagPanel.OnPointerExit += () =>
            {
                PropertyPanel.SetMarginLeft(50);
                _windowTitlePanel.Panel.SetMarginLeft(0);
                AnimatorWindow.SetMarginLeft(152);

                Variable.OnTagPanelCollapsed?.Invoke();
            };
        }
        private void PropertyPanelHandler()
        {
            PropertyPanel.OnPointerEnter = () =>
            {
                PropertyPanel.SetWidth(PropertyPanelWidth.Max);
                _windowTitlePanel.Panel.SetMarginLeft(PropertyPanelWidth.Max - 100);
                AnimatorWindow.SetMarginLeft(TagPanelWidth + 152);

                ManualIcon.SetLeft(0);

                ManualLabel.SetColor("#ffffff");

                Variable.OnPropertyPanelExpanded?.Invoke();
            };
            PropertyPanel.OnPointerExit = () =>
            {
                PropertyPanel.SetWidth(PropertyPanelWidth.Min);
                _windowTitlePanel.Panel.SetMarginLeft(PropertyPanelWidth.Min - 100);
                AnimatorWindow.SetMarginLeft(PropertyPanelWidth.Min + 52);

                ManualIcon.SetLeft(-62.5f);

                ManualLabel.SetColor("#00000000");

                Variable.OnPropertyPanelCollapsed?.Invoke();
            };
        }

        private void HandlerWindowCreator()
        {
            #region Animator Window
            _inputNamePanel = new EInputNamePanel();
            _inputNamePanel.SwapButton.OnPointerDown += () => ReplaceClipPathItem(_inputNamePanel.OriginField.text, _inputNamePanel.NewField.text, out bool isSucceeded);

            _rootNamePanel = new ERootNamePanel();

            AnimatorWindow = new Image().AddClass("MainWindow");
            AnimatorWindow.AddElements(_inputNamePanel, _rootNamePanel.SetMarginTop(0));
            #endregion

            HandlerWindow = new VisualElement().AddClass(_class_handlerWindow).AddElements(AnimatorWindow);
        }
        #endregion
        #region ▶ Editor Functions Handlers
        public static void PresentAllClips(Dictionary<AnimationClip, Color> clips)
        {
            _clipsScroll.RemoveAllElements();

            foreach (var clip in clips)
            {
                _clipsScroll.AddElements(new EAnimationClipField(clip));
            }
        }
        public static void PresentAllPaths()
        {
            Handler.GetReferencedAnimator();
            Handler.FillModel();
            _rootNamePanel.ClearAllClipItem();
            if (Handler.Paths != null && !Handler.AnimationClips.IsEmpty())
            {
                if (Handler.PathKeys.Count > 0)
                {
                    foreach (string path in Handler.PathKeys)
                    {
                        CreateClipPathItem(path);
                    }
                }
                else
                {
                    _rootNamePanel.AddBoard("No animation clip found!");
                }
            }
            else
            {
                _rootNamePanel.AddBoard("No animation clip found!");
            }
        }
        public static void ReplaceClipPathItem(string oldPath, string newPath, out bool isSucceeded, bool isAuto = false)
        {
            isSucceeded = true;

            if (!Handler.AnimationClips.IsEmpty() && Handler.PathKeys.Count > 0)
            {
                List<string> paths = new();

                foreach (var path in Handler.PathKeys) paths.Add((string)path);

                if (paths.Contains(oldPath) && paths.Contains(newPath))
                {
                    ReplaceRoot();

                    if (!isAuto) MDebug.Custom("Swap", $"{oldPath} ▶ {newPath}", CColor.Macaroon.ToHex());
                }
                else
                {
                    Handler.ReplaceRoot(oldPath, newPath, () => ChangeVisuals(oldPath, newPath));

                    if (!isAuto) MDebug.Custom("Rename", $"{oldPath} ▶ {newPath}", CColor.Flamingo.ToHex());
                }
            }

            void ReplaceRoot()
            {
                Handler.ReplaceRoot(oldPath, "Temporary Root", () => ChangeVisuals(oldPath, "Temporary Root"));
                Handler.ReplaceRoot(newPath, oldPath, () => ChangeVisuals(newPath, oldPath));
                Handler.ReplaceRoot("Temporary Root", newPath, () => ChangeVisuals("Temporary Root", newPath));
            }

            void ChangeVisuals(string originalRoot, string newRoot)
            {
                EClipNameField clipNameField = _rootNamePanel.ClipPanel.Query<EClipNameField>().ToList().FirstOrDefault(i => i.Name.text == originalRoot);

                if (!clipNameField.IsNull())
                {
                    clipNameField.Name.SetText(newRoot);

                    GameObject returnedObject = Handler.FindObjectInRoot(newRoot);
                    clipNameField.Object.DragPerformOnField(returnedObject);

                    clipNameField.UpdateArrowColor();
                }
            }
        }
        public static void CreateClipPathItem(string path)
        {
            string newPath = path;
            GameObject gameObject = Handler.FindObjectInRoot(path);

            string pathOverride = path;
            string currentPath = path;

            List<Color> referencedColor = new();

            if (Handler.TempPathOverrides.ContainsKey(path)) pathOverride = Handler.TempPathOverrides[path];
            if (pathOverride != path) Handler.TempPathOverrides[path] = pathOverride;

            if (Handler.PathColors.ContainsKey(path))
            {
                foreach (var clip in Handler.AnimationClips)
                {
                    if (Handler.ClipColors.ContainsKey(clip))
                    {
                        if (Handler.PathColors[path].Contains(clip)) referencedColor.Add(Handler.ClipColors[clip]);
                        else referencedColor.Add(Color.clear);
                    }
                    //else MDebug.Caution("Do nothing");
                }
            }

            Color arrowColor = "#BF4040".ToColor();
            if (!gameObject.IsNull()) arrowColor = "#40BF8F".ToColor();

            EClipNameField clipNameField = new(pathOverride, gameObject, referencedColor.ToArray(), arrowColor, null);

            clipNameField.Object.Background.OnDragPerform += (newObject) =>
            {
                ClipPathObjectChanged(clipNameField, gameObject, newObject, ref currentPath);
            };

            clipNameField.ChangeButton.OnPointerDown += () =>
            {
                ClipPathRootChanged(clipNameField, ref currentPath, ref newPath, clipNameField.Name.text, null);
            };

            clipNameField.UndoButton.OnPointerDown += () =>
            {
                ClipPathRootChanged(clipNameField, ref currentPath, ref newPath, clipNameField.LastRoot, () =>
                {
                    clipNameField.Name.SetText(currentPath);
                });
            };

            _rootNamePanel.AddClipItem(clipNameField);
        }
        public static void ClipPathObjectChanged(EClipNameField clipNameField, GameObject gameObject, GameObject newObject, ref string currentPath)
        {
            gameObject = Handler.FindObjectInRoot(currentPath);

            try
            {
                if (gameObject != newObject)
                {
                    //MDebug.Caution($"Start: {gameObject.CheckNull("gameObject")?.name} - {newObject.CheckNull("newObject")?.name} | {clipNameField.CheckNull("clipNameField")?.Name.text} - {currentPath.CheckNull("currentPath")}");

                    currentPath = Handler.ChildPath(newObject);
                    Handler.UpdatePath(clipNameField.Name.text, currentPath);
                    clipNameField.Name.SetText(currentPath);
                    gameObject = newObject;
                    Handler.FillModel();

                    //MDebug.Caution($"End: {gameObject.CheckNull("gameObject")?.name} - {newObject.CheckNull("newObject")?.name} | {clipNameField.CheckNull("clipNameField")?.Name.text} - {currentPath.CheckNull("currentPath")}");

                }
            }
            catch (UnityException)
            {
                MDebug.Caution($"<color=#c7ff96><b>{currentPath}</b></color> already exits in animation!");
                GameObject returnedObject = Handler.FindObjectInRoot(clipNameField.Name.text);
                clipNameField.Object.DragPerformOnField(returnedObject);
            }
            clipNameField.UpdateArrowColor();
        }
        public static void ClipPathRootChanged(EClipNameField clipNameField, ref string currentPath, ref string newPath, string setNewPath, Action additionAction)
        {
            newPath = setNewPath;
            Handler.TempPathOverrides.Remove(currentPath);

            try
            {
                if (newPath != currentPath)
                {
                    clipNameField.LastRoot = currentPath;

                    Handler.UpdatePath(currentPath, newPath);
                    currentPath = newPath;

                    GameObject getObject = Handler.FindObjectInRoot(currentPath);

                    clipNameField.Object.DragPerformOnField(getObject);
                    clipNameField.UpdateArrowColor();

                    additionAction?.Invoke();

                    Handler.FillModel();
                }
            }
            catch (UnityException)
            {
                MDebug.Caution($"<color=#c7ff96><b>{currentPath}</b></color> already exits in animation!");
                clipNameField.Name.SetText(currentPath);
            }
        }
        #endregion
    }
}