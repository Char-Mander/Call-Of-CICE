using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class InitializeFolders : Editor {

    [MenuItem("Utilidades/Crear carpetas iniciales")]
    public static void CreateInitialFolders() { 
        Directory.CreateDirectory("Assets/GameAssets");
        Directory.CreateDirectory("Assets/GameAssets/Scripts");
        Directory.CreateDirectory("Assets/GameAssets/Materials");
        Directory.CreateDirectory("Assets/GameAssets/Textures");
        Directory.CreateDirectory("Assets/GameAssets/Prefabs");
        Directory.CreateDirectory("Assets/GameAssets/Resources");
        Directory.CreateDirectory("Assets/GameAssets/Scenes");
        AssetDatabase.Refresh();
    }
}
