using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Cashew.Utility.FileSystem;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

class StratagemIconUpdater : MonoBehaviour
{
    public string IconRoot;
    public string StratagemRoot;

    [Button]
    void Process()
    {
        var stratagemFolders = AssetDatabase.GetSubFolders(StratagemRoot);
        var spritesFolders = AssetDatabase.GetSubFolders(IconRoot);

        foreach (var stratagemFolder in stratagemFolders)
        {
            var iconFolder = MatchFolder(CFile.GetDirectoryName(stratagemFolder), spritesFolders);
            if (iconFolder == null)
                continue;

            var stratagems = GetStratagems(stratagemFolder);
            var sprites = GetSprites(iconFolder);

            foreach (var stratagem in stratagems)
            {
                stratagem.Icon = MatchSprite(stratagem, sprites);
                EditorUtility.SetDirty(stratagem);
            }
        }

        AssetDatabase.SaveAssets();
    }

    string MatchFolder(string stratagemFolderName, string[] folders)
    {
        foreach (var folder in folders)
        {
            var dirName = CFile.GetDirectoryName(folder);
            if (string.Equals(dirName, stratagemFolderName, StringComparison.CurrentCultureIgnoreCase))
                return folder;
        }

        return null;
    }

    Sprite MatchSprite(Stratagem stratagem, Sprite[] sprites)
    {
        return sprites.FirstOrDefault(p =>
            string.Equals(stratagem.name, p.name, StringComparison.CurrentCultureIgnoreCase));
    }

    Sprite[] GetSprites(string folder)
    {
        var dirInfo = new DirectoryInfo(folder);
        var files = dirInfo.GetFiles();
        var sprites = new List<Sprite>();
        foreach (var f in files)
        {
            if (f.Extension != ".png")
                continue;
            
            var path = CFile.MakePathRelative(f.FullName);
            sprites.Add(AssetDatabase.LoadAssetAtPath<Sprite>(path));
        }
        return sprites.ToArray();
    }

    Stratagem[] GetStratagems(string folder)
    {
        var dirInfo = new DirectoryInfo(folder);
        var files = dirInfo.GetFiles();
        var stratagems = new List<Stratagem>();
        foreach (var f in files)
        {
            if (f.Extension != ".asset")
                continue;

            var path = CFile.MakePathRelative(f.FullName);
            stratagems.Add(AssetDatabase.LoadAssetAtPath<Stratagem>(path));
        }

        return stratagems.ToArray();
    }
}