﻿using UnityEngine;
using UnityEditor;
using GameFramework;

[CustomPropertyDrawer(typeof(EnumFlags))]
public class EnumFlagsAttributeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        property.intValue = EditorGUI.MaskField(position, label, property.intValue
                                                , property.enumNames);
    }
}