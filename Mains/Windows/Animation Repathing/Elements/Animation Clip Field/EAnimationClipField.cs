#if UNITY_EDITOR && YNL_UTILITIES
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using YNL.Editors.Extensions;
using YNL.Editors.Visuals;

namespace YNL.GeneralToolbox.Windows.AnimationRepathing
{
    public class EAnimationClipField : Button
    {
        private const string USS_StyleSheet = "Style Sheets/Animation Repathing/EAnimationClipField";

        private const string _uss_field = "e-animation-clip-field__field";
        private const string _uss_color = "e-animation-clip-field__color";
        private const string _uss_clip = "e-animation-clip-field__clip";

        public Image Color;
        public RepaintedAssetField<AnimationClip> Clip;

        public EAnimationClipField(KeyValuePair<AnimationClip, Color> pair) : base()
        {
            this.AddStyle(USS_StyleSheet);

            Color = new Image().AddClass(_uss_color).SetBackgroundColor(pair.Value);
            Clip = new RepaintedAssetField<AnimationClip>(pair.Key).AddClass(_uss_clip);

            this.AddClass(_uss_field).AddElements(Clip, Color);
        }
    }
}
#endif