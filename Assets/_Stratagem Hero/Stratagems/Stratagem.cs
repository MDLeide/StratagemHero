using System.Collections;
using System.ComponentModel;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "stratagem", menuName = "stratagem")]
class Stratagem : ScriptableObject
{
    public Sprite Icon;
    public CommandKey[] Commands;
    public StratagemCategory Category;
}