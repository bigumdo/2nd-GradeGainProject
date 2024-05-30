using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

public enum WeaponManagerType
{
    Weapon,
}

public class WeaponSOMaker : EditorWindow
{
    private static int toolbarIndex = 0;
    private static Dictionary<WeaponManagerType, Vector2> scrollPositions 
        = new Dictionary<WeaponManagerType, Vector2>();
    private static Dictionary<WeaponManagerType, Object> selectedItem 
        = new Dictionary<WeaponManagerType, Object>();
    private static Vector2 inspectorScroll = Vector2.zero;

    private string[] _toolbarItemNames;
    private Editor _cachedEditor;
    private Texture2D _selectTexture;
    private GUIStyle _selectStyle;

    #region 각 데이터 테이블 모음
    private readonly string _weaponDirectory = "Assets/10.Database/SO/Weapon";
    private WeaponTreeSO _weaponTable;
    #endregion
    
    [MenuItem("Tool/Weapon/WeaponManager")]
    private static void OpenWindow()
    {
        WeaponSOMaker window = GetWindow<WeaponSOMaker>("WeaponManager");
        window.minSize = new Vector2(700, 500);
        window.Show();
    }
    
    private void CreateDirectory()
    {
        if (Directory.Exists(_weaponDirectory) == false)
        {
            Directory.CreateDirectory(_weaponDirectory);
        }
    }

    private void OnEnable()
    {
        CreateDirectory();
        SetUpUtility();
    }

    private void OnDisable()
    {
        DestroyImmediate(_cachedEditor);
        DestroyImmediate(_selectTexture);
    }

    private void SetUpUtility()
    {
        _selectTexture = new Texture2D(1, 1); // 1픽셀짜리 텍스쳐 그림
        _selectTexture.SetPixel(0, 0, new Color(0.24f, 0.48f, 0.9f, 0.4f));
        _selectTexture.Apply();

        _selectStyle = new GUIStyle();
        _selectStyle.normal.background = _selectTexture;
        
        _selectTexture.hideFlags = HideFlags.DontSave;
        
        _toolbarItemNames = Enum.GetNames(typeof(WeaponManagerType));
        foreach (WeaponManagerType type in Enum.GetValues(typeof(WeaponManagerType)))
        {
            if (scrollPositions.ContainsKey(type) == false)
                scrollPositions[type] = Vector2.zero;
            if (selectedItem.ContainsKey(type) == false)
                selectedItem[type] = null;
        }

        if (_weaponTable == null)
        {
            _weaponTable = AssetDatabase.LoadAssetAtPath<WeaponTreeSO>
                ($"{_weaponDirectory}/WeaponTree.asset");
            if (_weaponTable == null)
            {
                _weaponTable = CreateInstance<WeaponTreeSO>();
                string fileName = AssetDatabase.GenerateUniqueAssetPath
                    ($"{_weaponDirectory}/WeaponTree.asset");
                
                AssetDatabase.CreateAsset(_weaponTable, fileName);
                Debug.Log($"Create Pooling Table at {fileName}");
            }
        }
        
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    private void OnGUI()
    {
        toolbarIndex = GUILayout.Toolbar(toolbarIndex, _toolbarItemNames);
        EditorGUILayout.Space(5f);

        DrawContent(toolbarIndex);
    }

    private void DrawContent(int toolbarIndex)
    {
        switch (toolbarIndex)
        {
            case 0:
                DrawWeaponItems();
                break;
        }
    }

    private void DrawWeaponItems()
    {
        EditorGUILayout.BeginHorizontal();
        {
            #region Buttons
            GUI.color = new Color(0.19f, 0.76f, 0.08f);
            if(GUILayout.Button("Generate Item"))
            {
                GenerateWeaponItem();
            }
            if(GUILayout.Button("Sort Item By Name"))
            {
                _weaponTable.items.Sort((a, b) => string.Compare(a.weaponName, b.weaponName));
            }
            if(GUILayout.Button("Sort Item By Rank"))
            {
                _weaponTable.items.Sort((a, b) => a.rank.CompareTo(b.rank));
            }
            if (GUILayout.Button("Sort Item By Damage"))
            {
                _weaponTable.items.Sort((a, b) => a.damage.CompareTo(b.damage));
            }
            if (GUILayout.Button("Sort Item By Price"))
            {
                _weaponTable.items.Sort((a, b) => a.price.CompareTo(b.price));
            }
            #endregion
        }
        EditorGUILayout.EndHorizontal();

        GUI.color = Color.white; //원래 색상으로 복귀.

        EditorGUILayout.BeginHorizontal();
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox, GUILayout.Width(300f));
            {
                EditorGUILayout.LabelField("Weapon list", EditorStyles.boldLabel, GUILayout.Height(20f), GUILayout.Width(100));
                EditorGUILayout.Space(3f);

                #region Scroll View
                scrollPositions[WeaponManagerType.Weapon] = EditorGUILayout.BeginScrollView
                    (scrollPositions[WeaponManagerType.Weapon], false, true, 
                        GUIStyle.none, GUI.skin.verticalScrollbar, GUIStyle.none);
                {
                    foreach (WeaponSO item in _weaponTable.items)
                    {
                        // 현재 그릴 item이 선택아이템과 동일하면 스타일 지정
                        GUIStyle style = selectedItem[WeaponManagerType.Weapon] == item
                            ? _selectStyle
                            : GUIStyle.none;
                        EditorGUILayout.BeginHorizontal(style, GUILayout.Height(40f));
                        {
                            EditorGUILayout.LabelField(item.weaponName, 
                                GUILayout.Height(40f), GUILayout.Width(240f));

                            EditorGUILayout.BeginVertical();
                            {
                                #region X 버튼
                                EditorGUILayout.Space(10f);
                                GUI.color = Color.red;
                                if (GUILayout.Button("X", GUILayout.Width(20f)))
                                {
                                    _weaponTable.items.Remove(item);
                                    AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(item));
                                    EditorUtility.SetDirty(_weaponTable);
                                    AssetDatabase.SaveAssets();
                                }
                                GUI.color = Color.white;
                                #endregion
                            }
                            EditorGUILayout.EndVertical();
                            
                        }
                        EditorGUILayout.EndHorizontal();
                        
                        #region 마우스 클릭시 선택
                        // 마지막으로 그린 사각형 정보를 알아옴
                        Rect lastRect = GUILayoutUtility.GetLastRect();

                        if (Event.current.type == EventType.MouseDown
                            && lastRect.Contains(Event.current.mousePosition)) 
                        {
                            inspectorScroll = Vector2.zero;
                            selectedItem[WeaponManagerType.Weapon] = item;
                            Event.current.Use();
                        }
                        
                        // 삭제 확인 break;
                        if (item == null)
                            break;
                        #endregion
                    }
                    // end of foreach
                    
                }
                EditorGUILayout.EndScrollView();
                #endregion
                
            }
            EditorGUILayout.EndVertical();
            
            #region 인스펙터 그리기
            // 인스펙터 그리기
            if (selectedItem[WeaponManagerType.Weapon] != null)
            {
                inspectorScroll = EditorGUILayout.BeginScrollView(inspectorScroll);
                {
                    EditorGUILayout.Space(2f);
                    Editor.CreateCachedEditor(
                        selectedItem[WeaponManagerType.Weapon], null, ref _cachedEditor);
                        
                    _cachedEditor.OnInspectorGUI();
                }
                EditorGUILayout.EndScrollView();
            }
            #endregion
        }
        EditorGUILayout.EndHorizontal();
    }
    
    private void GenerateWeaponItem()
    {
        Guid guid = Guid.NewGuid(); // 고유한 문자열 키 반환
        
        WeaponSO item = CreateInstance<WeaponSO>(); // 메모리에만 생성
        item.weaponName = guid.ToString();
        item.rank = _weaponTable.items[^1].rank + 1;
        
        AssetDatabase.CreateAsset(item, $"{_weaponDirectory}/Weapons/Weapon_{item.weaponName}.asset");
        _weaponTable.items.Add(item);
        
        EditorUtility.SetDirty(_weaponTable);
        AssetDatabase.SaveAssets();
    }
}
