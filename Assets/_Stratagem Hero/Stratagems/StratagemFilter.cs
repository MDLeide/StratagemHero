using System;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

class StratagemFilter : MonoBehaviour
{
    [Header("Configuration")]
    public StratagemProvider Provider;

    [Header("Settings")]
    public bool FilterByCategory;
    public StratagemCategory[] ValidCategories;
    public bool FilterByMinLength;
    public int MinLength;
    public bool FilterByMaxLength;
    public int MaxLength;

    [Header("State")]
    public Stratagem[] AllStratagem;

    void Start()
    {
        AllStratagem = Provider.GetValidStratagems();
    }

    public Stratagem GetNextStratagem()
    {
        var strat = AllStratagem[Random.Range(0, AllStratagem.Length)];
        var count = 0;
        while (!StratagemIsValid(strat))
        {
            strat = AllStratagem[Random.Range(0, AllStratagem.Length)];
            ++count;
            if (count > 10000)
                throw new InvalidOperationException();
        }

        return strat;
    }

    bool StratagemIsValid(Stratagem strat)
    {
        if (FilterByCategory && !ValidCategories.Contains(strat.Category))
            return false;

        if (FilterByMinLength && strat.Commands.Length < MinLength)
            return false;

        if (FilterByMaxLength && strat.Commands.Length > MaxLength)
            return false;

        return true;
    }
}