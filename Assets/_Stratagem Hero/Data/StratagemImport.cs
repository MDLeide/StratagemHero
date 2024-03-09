using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;


class StratagemImport : MonoBehaviour
{
    public char QuoteSymbol = '"';
    public char SeparatorSymbol = ',';

    public string GenerateIntoFolderPath;
    public TextAsset Source;

    [Button]
    public void Process()
    {
        var txt = Source.text;
        var lines = txt.Split(Environment.NewLine);
        var strats = new List<Stratagem>();
        foreach (var line in lines)
        {
            Stratagem strategem;

            try
            {
                strategem = ProcessLine(line);
            }
            catch (Exception e)
            {
                Debug.Log($"Failed to process line: {line}");
                Debug.Log(e);
                continue;
            }

            strats.Add(strategem);
        }

        SaveStratagems(strats);
    }

    void SaveStratagems(List<Stratagem> stratagems)
    {
        foreach (var strat in stratagems)
            AssetDatabase.CreateAsset(strat, Path.Combine(GenerateIntoFolderPath, $"{strat.name}.asset"));
    }

    Stratagem ProcessLine(string line)
    {
        var parts = SplitLine(line);
        var stratagem = ScriptableObject.CreateInstance<Stratagem>();
        stratagem.name = parts[0];
        var cmds = SplitLine(parts[1]);
        var keys = new List<CommandKey>();
        foreach (var cmd in cmds)
            keys.Add(GetKey(cmd));
        stratagem.Commands = keys.ToArray();
        return stratagem;
    }

    CommandKey GetKey(string keyName)
    {
        keyName = keyName.Trim().ToLower();

        switch (keyName)
        {
            case "up":
                return CommandKey.Up;
            case "right":
                return CommandKey.Right;
            case "down":
                return CommandKey.Down;
            case "left":
                return CommandKey.Left;
            default:
                throw new ArgumentOutOfRangeException(nameof(keyName), keyName, "Failed to parse command key.");
        }
    }

    string[] SplitLine(string line)
    {
        var openedLiteral = 0;
        var inLiteral = false;
        var lastSeparatorIndex = -1;
        var parts = new List<string>();

        for (int i = 0; i < line.Length; i++)
        {
            var character = line[i];

            if (character == QuoteSymbol)
            {
                if (inLiteral)
                {
                    inLiteral = false;
                    var len = i - openedLiteral - 1;
                    if (len <= 0)
                        continue;

                    var part = line.Substring(openedLiteral + 1, len).Trim(QuoteSymbol);
                    parts.Add(part);
                }
                else
                {
                    inLiteral = true;
                    openedLiteral = i;
                }
            }
            else if (!inLiteral && character == SeparatorSymbol)
            {
                var len = i - lastSeparatorIndex - 1;
                if (len <= 0)
                {
                    lastSeparatorIndex = i;
                    continue;
                }

                var part = line.Substring(lastSeparatorIndex + 1, i - lastSeparatorIndex - 1);
                parts.Add(part);
                lastSeparatorIndex = i;
            }
        }
        
        var finalPart = line.Substring(lastSeparatorIndex + 1);

        if (!string.IsNullOrEmpty(finalPart))
            parts.Add(finalPart);

        return parts.ToArray();
    }
}