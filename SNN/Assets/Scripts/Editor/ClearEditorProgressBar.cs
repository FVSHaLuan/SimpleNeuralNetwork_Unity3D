using UnityEditor;


public static class ClearEditorProgressBar
{
    [MenuItem("SNN/ClearEditorProgressBar")]
    public static void Clear()
    {
        EditorUtility.ClearProgressBar();
    }

}
