﻿#if UNITY_EDITOR && YNL_UTILITIES
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;
using YNL.Extensions.Methods;
using System;

namespace YNL.GeneralToolbox.Windows.AnimationRepathing
{
    [InitializeOnLoad]
    public class Handler
    {
        #region ▶ Fields/Properties
        private Main _main;

        public static Hashtable Paths;
        public static Dictionary<string, string> TempPathOverrides = new();
        public static ArrayList PathKeys = new();

        public static Animator ReferencedAnimator;
        public static List<AnimationClip> AnimationClips = new();

        public static Dictionary<AnimationClip, Color> ClipColors = new();
        public static Dictionary<string, AnimationClip[]> PathColors = new();

        private static string _replacementOldRoot;
        private static string _replacementNewRoot;
        #endregion

        public Handler(Main main)
        {
            _main = main;
        }

        #region ▶ Handle Functions
        public void OnSelectionChange()
        {
            if (Selection.objects.Length > 1)
            {
                foreach (UnityEngine.Object obj in Selection.objects)
                {
                    if (obj is AnimationClip)
                    {
                        if (!AnimationClips.Contains((AnimationClip)obj))
                        {
                            AnimationClips.Add((AnimationClip)obj);
                        }
                        if (!ClipColors.ContainsKey((AnimationClip)obj))
                        {
                            Color bindedColor = MColor.RandomColor(100, 255);
                            ClipColors.Add((AnimationClip)obj, bindedColor);
                        }
                    }
                }

                Visual.PresentAllClips(ClipColors);
                Visual.PresentAllPaths();
            }
            else if (Selection.activeObject is AnimationClip)
            {
                ClipColors.Clear();
                PathColors.Clear();
                AnimationClips.Clear();

                ClipColors.Add((AnimationClip)Selection.activeObject, MColor.RandomColor(100, 255));
                AnimationClips.Add((AnimationClip)Selection.activeObject);

                Visual.PresentAllClips(ClipColors);
                Visual.PresentAllPaths();
            }
            else if (Selection.activeObject == null)
            {
                AnimationClips.Clear();
                ClipColors.Clear();
                PathColors.Clear();

                Visual.PresentAllClips(ClipColors);
                Visual.PresentAllPaths();
            }

            _main.Root.Repaint();
        }

        public static void GetReferencedAnimator()
        {
            ReferencedAnimator = Visual.ReferencedAnimator.ReferencedObject;
        }

        public static void FillModel()
        {
            Paths = new Hashtable();
            PathKeys = new ArrayList();

            foreach (AnimationClip animationClip in AnimationClips)
            {
                FillModelWithCurves(AnimationUtility.GetCurveBindings(animationClip), animationClip);
                FillModelWithCurves(AnimationUtility.GetObjectReferenceCurveBindings(animationClip), animationClip);
            }
        }
        private static void FillModelWithCurves(EditorCurveBinding[] curves, AnimationClip clip)
        {
            foreach (EditorCurveBinding curveData in curves)
            {
                string key = curveData.path;

                if (Paths.ContainsKey(key))
                {
                    ((ArrayList)Paths[key]).Add(curveData);
                }
                else
                {
                    ArrayList newProperties = new ArrayList();
                    newProperties.Add(curveData);
                    Paths.Add(key, newProperties);
                    PathKeys.Add(key);
                }

                if (PathColors.ContainsKey(key))
                {
                    if (!PathColors[key].Contains(clip))
                    {
                        PathColors[key] = PathColors[key].Add(clip);
                    }
                }
                else
                {
                    PathColors.Add(key, new AnimationClip[1] { clip });
                }
            }
        }
        public static GameObject FindObjectInRoot(Animator animator, string path)
        {
            if (animator == null) return null;

            Transform child = animator.transform.Find(path);

            if (child != null) return child.gameObject;
            else return null;
        }
        public static GameObject FindObjectInRoot(string path) => FindObjectInRoot(ReferencedAnimator, path);

        public static void UpdatePath(string oldPath, string newPath)
        {
            if (Paths[newPath] != null)
            {
                throw new UnityException("Path " + newPath + " already exists in that animation!");
            }
            AssetDatabase.StartAssetEditing();
            for (int iCurrentClip = 0; iCurrentClip < AnimationClips.Count; iCurrentClip++)
            {
                AnimationClip animationClip = AnimationClips[iCurrentClip];
                Undo.RecordObject(animationClip, "Animation Hierarchy Change");

                // Recreating all curves one by one to maintain proper order in the editor - slower than just removing old curve and adding a corrected one, but it's more user-friendly
                for (int iCurrentPath = 0; iCurrentPath < PathKeys.Count; iCurrentPath++)
                {
                    string path = PathKeys[iCurrentPath] as string;
                    ArrayList curves = (ArrayList)Paths[path];

                    for (int i = 0; i < curves.Count; i++)
                    {
                        EditorCurveBinding binding = (EditorCurveBinding)curves[i];
                        AnimationCurve curve = AnimationUtility.GetEditorCurve(animationClip, binding);
                        ObjectReferenceKeyframe[] objectReferenceCurve = AnimationUtility.GetObjectReferenceCurve(animationClip, binding);

                        if (curve != null) AnimationUtility.SetEditorCurve(animationClip, binding, null);
                        else AnimationUtility.SetObjectReferenceCurve(animationClip, binding, null);

                        if (path == oldPath) binding.path = newPath;

                        if (curve != null) AnimationUtility.SetEditorCurve(animationClip, binding, curve);
                        else AnimationUtility.SetObjectReferenceCurve(animationClip, binding, objectReferenceCurve);

                        float fChunk = 1f / AnimationClips.Count;
                        float fProgress = (iCurrentClip * fChunk) + fChunk * ((float)iCurrentPath / (float)PathKeys.Count);

                        EditorUtility.DisplayProgressBar("Animation Hierarchy Progress", "How far along the animation editing has progressed.", fProgress);
                    }
                }
            }
            AssetDatabase.StopAssetEditing();
            EditorUtility.ClearProgressBar();

            Visual.PresentAllClips(ClipColors);
        }
        public static string ChildPath(GameObject obj, bool sep = false)
        {
            if (ReferencedAnimator == null)
            {
                throw new UnityException("Please assign Referenced Animator (Root) first!");
            }

            if (obj == ReferencedAnimator.gameObject) return "";
            else
            {
                if (obj.transform.parent == null) throw new UnityException("Object must belong to " + ReferencedAnimator.ToString() + "!");
                else return ChildPath(obj.transform.parent.gameObject, true) + obj.name + (sep ? "/" : "");
            }
        }
        public static string GetPath(GameObject obj, bool sep = false)
        {
            if (obj.transform.parent == null) return "";
            return GetPath(obj.transform.parent.gameObject, true) + obj.name + (sep ? "/" : "");
        }
        public static void ReplaceRoot(string oldRoot, string newRoot, Action done = null)
        {
            float fProgress = 0.0f;
            _replacementOldRoot = oldRoot;
            _replacementNewRoot = newRoot;

            AssetDatabase.StartAssetEditing();

            for (int iCurrentClip = 0; iCurrentClip < AnimationClips.Count; iCurrentClip++)
            {
                AnimationClip animationClip = AnimationClips[iCurrentClip];
                //if (!Variable.IsAutomaticOn) Undo.RecordObject(animationClip, "Animation Hierarchy Root Change");
                Undo.RecordObject(animationClip, "Animation Hierarchy Root Change");

                for (int iCurrentPath = 0; iCurrentPath < PathKeys.Count; iCurrentPath++)
                {
                    string path = PathKeys[iCurrentPath] as string;
                    ArrayList curves = (ArrayList)Paths[path];

                    for (int i = 0; i < curves.Count; i++)
                    {
                        EditorCurveBinding binding = (EditorCurveBinding)curves[i];

                        if (path == _replacementOldRoot)
                        {
                            if (path != _replacementNewRoot)
                            {
                                string sNewPath = Regex.Replace(path, "^" + _replacementOldRoot, _replacementNewRoot);

                                AnimationCurve curve = AnimationUtility.GetEditorCurve(animationClip, binding);
                                if (curve != null)
                                {
                                    AnimationUtility.SetEditorCurve(animationClip, binding, null);
                                    binding.path = sNewPath;
                                    AnimationUtility.SetEditorCurve(animationClip, binding, curve);
                                }
                                else
                                {
                                    ObjectReferenceKeyframe[] objectReferenceCurve = AnimationUtility.GetObjectReferenceCurve(animationClip, binding);
                                    AnimationUtility.SetObjectReferenceCurve(animationClip, binding, null);
                                    binding.path = sNewPath;
                                    AnimationUtility.SetObjectReferenceCurve(animationClip, binding, objectReferenceCurve);
                                }

                                FillModel();
                            }
                        }
                    }

                    float fChunk = 1f / AnimationClips.Count;
                    fProgress = (iCurrentClip * fChunk) + fChunk * ((float)iCurrentPath / (float)PathKeys.Count);

                    EditorUtility.DisplayProgressBar(
                        "Animation Hierarchy Progress",
                        "How far along the animation editing has progressed.",
                        fProgress);
                }

            }
            AssetDatabase.StopAssetEditing();
            EditorUtility.ClearProgressBar();
            Visual.PresentAllClips(ClipColors);

            done?.Invoke();
        }

        //public void ChangeMode(bool toAutomatic)
        //{
        //    Variable.IsAutomaticPanel = toAutomatic;
        //    //_main.Visual.UpdateMode(true);
        //}
        //public void SwitchAutomaticMode()
        //{
        //    Variable.IsAutomaticOn = !Variable.IsAutomaticOn;
        //    //Visual.UpdateAutomatic(true);
        //    Variable.OnModeChanged?.Invoke();
        //}
        #endregion
    }
}
#endif