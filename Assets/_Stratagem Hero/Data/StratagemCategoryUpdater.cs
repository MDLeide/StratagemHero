using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cashew.Utility.FileSystem;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;


class StratagemCategoryUpdater : MonoBehaviour
{
    public string StratagemRootDirectory;

    [Button]
    public void Process()
    {
        var subFolders = AssetDatabase.GetSubFolders(StratagemRootDirectory);
        foreach (var sub in subFolders)
        {
            if (!TryParse(CFile.GetDirectoryName(sub).ToLower(), out var category))
                continue;

            var stratagems = LoadStratagems(sub);
            foreach (var s in stratagems)
            {
                s.Category = category;
                EditorUtility.SetDirty(s);
            }
        }

        AssetDatabase.SaveAssets();
    }

    Stratagem[] LoadStratagems(string path)
    {
        var files = CFile.GetFiles(path);
        var stratagems = new List<Stratagem>();
        foreach (var f in files)
        {
            if (Path.GetExtension(f) != ".asset")
                continue;

            stratagems.Add(AssetDatabase.LoadAssetAtPath<Stratagem>(f));
        }

        return stratagems.ToArray();
    }

    bool TryParse(string folder, out StratagemCategory category)
    {
        category = StratagemCategory.Orbital;

        switch (folder)
        {
            case "backpack":
                category = StratagemCategory.Backpack;
                return true;
            case "eagle":
                category = StratagemCategory.Eagle;
                return true;
            case "emplacement":
                category = StratagemCategory.Emplacement;
                return true;
            case "orbital":
                category = StratagemCategory.Orbital;
                return true;
            case "support":
                category = StratagemCategory.Support;
                return true;
            case "weapon":
                category = StratagemCategory.Weapon;
                return true;
        }

        return false;
    }
}