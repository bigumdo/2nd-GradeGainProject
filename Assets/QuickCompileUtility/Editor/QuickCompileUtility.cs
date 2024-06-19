using UnityEngine;
using UnityEditor;

namespace QuickCompileUtility.Editor
{
    public class QuickCompileUtility : EditorWindow
    {
        [MenuItem("Tools/Quick Compile %#q")] // % (Ctrl), # (Shift), & (Alt)
        public static void ShowWindow()
        {
            GetWindow<QuickCompileUtility>("Quick Compile");
        }

        private void OnGUI()
        {
            if (GUILayout.Button("Force Compile"))
            {
                ForceCompile();
            }

            GUILayout.Label("Use Ctrl+Shift+Alt+f to force compile");
        }

        [MenuItem("Tools/Force Compile %#&f")] // % (Ctrl), # (Shift), & (Alt)
        public static void ForceCompile()
        {
            Debug.Log("Forcing script compilation...");
            AssetDatabase.Refresh();
            // Trigger recompilation
            UnityEditor.Compilation.CompilationPipeline.RequestScriptCompilation();
        }
    }

}