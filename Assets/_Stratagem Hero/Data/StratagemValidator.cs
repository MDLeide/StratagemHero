using System.IO;
using Cashew.Utility.FileSystem;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

class StratagemValidator : MonoBehaviour
{
    public StratagemProvider Provider;
    public string StratagemRootPath;

    [Button]
    void Validate()
    {
        var subFolders = AssetDatabase.GetSubFolders(StratagemRootPath);
        foreach (var folder in subFolders)
            ProcessFolder(folder);
    }

    public bool IsValid(Stratagem stratagem)
    {
        return stratagem.Icon != null;
    }

    void ProcessFolder(string folderPath)
    {
        var files = CFile.GetFiles(folderPath);
        var folderName = CFile.GetDirectoryName(folderPath);
        foreach (var f in files)
        {
            if (Path.GetExtension(f) != ".asset")
                continue;
            var stratagem = AssetDatabase.LoadAssetAtPath<Stratagem>(f);
            CheckStratagem(folderName, stratagem);
        }
    }

    void CheckStratagem(string folderName, Stratagem stratagem)
    {
        var hasIcon = stratagem.Icon != null;
        var iconMatchesName = false;
        if (hasIcon)
            iconMatchesName = stratagem.Icon.name == stratagem.name;

        var categoryMatches = true;

        if (TryParseCategory(folderName, out var category))
            categoryMatches = stratagem.Category == category;

        if (!hasIcon || !iconMatchesName || !categoryMatches)
        {
            if (!hasIcon && !categoryMatches)
                Debug.Log($"[{stratagem.name}] No Icon, Bad Category");
            else if (!hasIcon)
                Debug.Log($"[{stratagem.name}] No Icon");
            else if (!categoryMatches)
                Debug.Log($"[{stratagem.name}] Bad Category");
            else if (hasIcon && !iconMatchesName)
                Debug.Log($"[{stratagem.name}] Check Icon");
        }
    }
    bool TryParseCategory(string folder, out StratagemCategory category)
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