using UnityEditor;
using UnityEngine;

public class UnityWindow : EditorWindow {

    static string defaultName = "Enemy";
    bool createBool = false;

    [MenuItem("Editor/Window")]
    public static void ShowWindow()
    {
        GetWindow<UnityWindow>("Editor");
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Create"))
        {
            createBool = !createBool;
        }

        EditorGUI.indentLevel++;
        if (createBool)
        {
            defaultName = EditorGUILayout.TextField("Name:", defaultName);
            GUILayout.BeginHorizontal();
            GUILayout.Space(EditorGUI.indentLevel * 20);
            if (GUILayout.Button("Create " + defaultName))
            {
                CreatePrefab();
            }
            GUILayout.EndHorizontal();
        }
        EditorGUI.indentLevel--;



        /* Example
        GUILayout.Label("Description");
        string text = "123";
        text = EditorGUILayout.TextField(text);
        if(GUILayout.Button("Do something"))
        {
            //Function
        }
        */
    }

    static void CreatePrefab()
    {
        string localPath = "Assets/Prefab/Enemy/" + defaultName + ".prefab";

        GameObject go = new GameObject();
        go.name = "Enemy";
        go.tag = "Enemy";
        go.layer = 8;
        go.AddComponent<SpriteRenderer>();
        go.AddComponent<BoxCollider2D>().size = new Vector2(1.15f, 2.3f);
        go.AddComponent<Rigidbody2D>().gravityScale = 0f;
        go.AddComponent<Enemy>();


        #region CzyNadpisac zakomentowane
        /*
        if (AssetDatabase.LoadAssetAtPath(localPath, typeof(GameObject)))
        {
            if (EditorUtility.DisplayDialog("Are you sure?",
                    "The prefab already exists. Do you want to overwrite it?",
                    "Yes",
                    "No"))
            {
                CreateNew(gameObject, localPath);
            }
        }
        */
        #endregion
        CreateNew(go, localPath);
        DestroyImmediate(go);
    }

    /* Sprawdza czy jest cos zaznaczone i dopiero wtedy uruchamia MenuItem
    [MenuItem("Examples/Create Prefab", true)]
    static bool ValidateCreatePrefab()
    {
        return Selection.activeGameObject != null;
    }
    */

    static void CreateNew(GameObject obj, string localPath)
    {
        Object prefab = PrefabUtility.CreatePrefab(localPath, obj);
        PrefabUtility.ReplacePrefab(obj, prefab, ReplacePrefabOptions.ConnectToPrefab);
    }
}
