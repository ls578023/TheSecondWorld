using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using GameFramework;

[InitializeOnLoad]
[CustomEditor(typeof(UILangText))]
public class UILangTextEditor : Editor
{
    protected UILangText Target;
    protected override void OnHeaderGUI()
    {
        base.OnHeaderGUI();
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        Target = target as UILangText;
        if (Application.isPlaying)
            return;
        if(!string.IsNullOrEmpty(Target.Key))
            Target.Value = LangService.Instance.Get(Target.Key);
    }
}
