using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WeaponSO))]
public class WeaponSOCustomEditor : Editor
{
    private SerializedProperty weaponName;
    private SerializedProperty damage;
    private SerializedProperty price;
    private SerializedProperty weaponSprite;
    
    private GUIStyle textAreaStyle;
    
    private void OnEnable()
    {
        GUIUtility.keyboardControl = 0;
        StyleSetup();
        weaponName = serializedObject.FindProperty("weaponName");
        damage = serializedObject.FindProperty("damage");
        price = serializedObject.FindProperty("price");
        weaponSprite = serializedObject.FindProperty("weaponSprite");
    }

    private void StyleSetup()
    {
        if (textAreaStyle == null)
        {
            textAreaStyle = new GUIStyle(EditorStyles.textArea);
            textAreaStyle.wordWrap = true;
            textAreaStyle.active.textColor = Color.white;
        }
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.BeginHorizontal("HelpBox");
        {
            weaponSprite.objectReferenceValue = EditorGUILayout.ObjectField(GUIContent.none,
                weaponSprite.objectReferenceValue,
                typeof(Sprite),
                false,
                GUILayout.Width(65));
            EditorGUILayout.BeginVertical();
            {
                EditorGUI.BeginChangeCheck();
                string prevName = weaponName.stringValue;
                EditorGUILayout.DelayedTextField(weaponName);
                
                if (EditorGUI.EndChangeCheck())
                {
                    string assetPath = AssetDatabase.GetAssetPath(target);
                    string newName = $"Weapon_{weaponName.stringValue}";
                    serializedObject.ApplyModifiedProperties();
                    
                    string msg = AssetDatabase.RenameAsset(assetPath, newName);
                    
                    if (string.IsNullOrEmpty(msg))
                    {
                        target.name = newName;
                        EditorGUILayout.EndVertical();
                        EditorGUILayout.EndHorizontal();
                        return;
                    }
                    
                    weaponName.stringValue = prevName;
                }
                
                EditorGUILayout.PropertyField(damage);
                EditorGUILayout.PropertyField(price);
                
            }
            EditorGUILayout.EndVertical();
        }
        EditorGUILayout.EndHorizontal();
        
        serializedObject.ApplyModifiedProperties();
    }
}