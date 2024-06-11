using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WeaponSO))]
public class WeaponSOCustomEditor : Editor
{
    private SerializedProperty rank;
    private SerializedProperty weaponName;
    private SerializedProperty inventoryWeaponName;
    private SerializedProperty damage;
    private SerializedProperty price;
    private SerializedProperty nextUpgradePercent;
    private SerializedProperty breakPercent;
    private SerializedProperty starCatchSpeed;
    private SerializedProperty starCatchSize;
    private SerializedProperty weaponSprite;
    private SerializedProperty weaponPrefab;
    private SerializedProperty hammerHitCnt;
    
    private GUIStyle textAreaStyle;
    
    private void OnEnable()
    {
        GUIUtility.keyboardControl = 0;
        StyleSetup();
        rank = serializedObject.FindProperty("rank");
        weaponName = serializedObject.FindProperty("weaponName");
        inventoryWeaponName = serializedObject.FindProperty("inventoryWeaponName");
        damage = serializedObject.FindProperty("damage");
        price = serializedObject.FindProperty("price");
        nextUpgradePercent = serializedObject.FindProperty("nextUpgradePercent");
        breakPercent = serializedObject.FindProperty("breakPercent");
        starCatchSpeed = serializedObject.FindProperty("starCatchSpeed");
        starCatchSize = serializedObject.FindProperty("starCatchSize");
        weaponSprite = serializedObject.FindProperty("weaponSprite");
        weaponPrefab = serializedObject.FindProperty("weaponPrefab");
        hammerHitCnt = serializedObject.FindProperty("hammerHitCnt");
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
                GUILayout.Width(70));
            
            EditorGUILayout.BeginVertical();
            {
                EditorGUILayout.LabelField("Weapon Setting", EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(rank);
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
                
                EditorGUILayout.PropertyField(inventoryWeaponName);
                EditorGUILayout.PropertyField(damage);
                EditorGUILayout.PropertyField(price);
                EditorGUILayout.PropertyField(nextUpgradePercent);
                EditorGUILayout.PropertyField(breakPercent);
                EditorGUILayout.PropertyField(weaponPrefab);
                
                EditorGUILayout.Space(10);
                
                EditorGUILayout.LabelField("Star Catch Setting", EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(starCatchSpeed);
                EditorGUILayout.PropertyField(starCatchSize);
                EditorGUILayout.PropertyField(hammerHitCnt);
                
            }
            EditorGUILayout.EndVertical();
        }
        EditorGUILayout.EndHorizontal();
        
        serializedObject.ApplyModifiedProperties();
    }
}