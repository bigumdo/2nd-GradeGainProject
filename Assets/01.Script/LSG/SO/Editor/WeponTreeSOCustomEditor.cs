using System;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[CustomEditor(typeof(WeaponTreeSO))]
public class WeponTreeSOCustomEditor : Editor
{
    private ReorderableList list;

    private void OnEnable()
    {
        var prop = serializedObject.FindProperty("items");
        list = new ReorderableList(serializedObject, prop,
            true, true, true, true);
        
        list.drawElementCallback = (rect, index, isActive, isFocused) =>
        {
            var element = prop.GetArrayElementAtIndex(index);
            rect.height -= 4;
            rect.y += 2;
            EditorGUI.PropertyField(rect, element, GUIContent.none);
        };
        
        list.drawHeaderCallback = rect =>
        {
            EditorGUI.LabelField(rect, "Weapon List");
        };
        
        list.onAddDropdownCallback = (rect, l) =>
        {
            var menu = new GenericMenu();
            menu.AddItem(new GUIContent("Sort SO by Rank"), false, () =>
            {
                var so = target as WeaponTreeSO;
                so.items.Sort((a, b) => a.rank.CompareTo(b.rank));
                EditorUtility.SetDirty(target);
            });
            menu.ShowAsContext();
        };
    }
    
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        list.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
    }
}