using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CustomEditor : MonoBehaviour
{
    [MenuItem("Dialogue/ImageFolder")]
    static void GetImageFolder()
    {
        OpenDialoguePath("/Resources/Dialogue/Image");
    }
    [MenuItem("Dialogue/CSVFolder")]
    static void GetCSVFolder()
    {
        OpenDialoguePath("/Resources/Dialogue/Scene");
    }

    static void OpenDialoguePath(string _path)
	{
        string path = Application.dataPath + _path;
        string tempPath = "";
        foreach (var item in path)
        {
            if(item != '/')
            tempPath += item;
            else
            tempPath += '\\';   
        }
        try
		{
			System.Diagnostics.Process.Start("explorer.exe", tempPath);
		}
		catch
		{
			Debug.LogError("Fail to Enter FolderPath");
		}
    }
}
