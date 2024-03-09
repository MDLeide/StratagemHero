using System.Collections.Generic;
using System.IO;
using System.Linq;
using Cashew.Utility.FileSystem;
using UnityEditor;
using UnityEngine;

class StratagemProvider : MonoBehaviour
{
    public string StratagemRootPath;
    public StratagemValidator Validator;

    public Stratagem[] GetAllStratagems()
    {
        var categoryFolders = AssetDatabase.GetSubFolders(StratagemRootPath);
        var stratagems = new List<Stratagem>();
        foreach (var folder in categoryFolders)
        {
            stratagems.AddRange(LoadDirectory(folder));
        }

        return stratagems.ToArray();
    }

    public Stratagem[] GetValidStratagems()
    {
        return GetAllStratagems().Where(p => Validator.IsValid(p)).ToArray();
    }

    IEnumerable<Stratagem> LoadDirectory(string path)
    {
        var files = CFile.GetFiles(path);
        var stratagems = new List<Stratagem>();
        foreach (var file in files)
        {
            if (Path.GetExtension(file) != ".asset")
                continue;
            stratagems.Add(AssetDatabase.LoadAssetAtPath<Stratagem>(file));
        }

        return stratagems;
    }
}