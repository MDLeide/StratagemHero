using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

static class Extensions
{
    public static void SetAlpha(this TMP_Text text, float alpha)
    {
        var c = text.color;
        c.a = alpha;
        text.color = c;
    }

    public static void SetColor(this TMP_Text text, Color color)
    {
        text.color = color;
    }
}