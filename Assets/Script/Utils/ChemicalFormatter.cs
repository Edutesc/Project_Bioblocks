using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public static class ChemicalFormatter
{
    public static string Format(string text)
    {
        if (string.IsNullOrEmpty(text))
            return text;

        // Ex: SO4^2- -> SO4<sub>4</sub><sup>2-</sup>
        text = Regex.Replace(
            text,
            @"\^([0-9]+[+-]?|[+-])",
            "<sup>$1</sup>"
        );

        // Ex: NH2 -> NH<sub>2</sub>
        text = Regex.Replace(
            text,
            @"(?<=[A-Za-z\)])(\d+)",
            "<sub>$1</sub>"
        );

        return text;
    }
}