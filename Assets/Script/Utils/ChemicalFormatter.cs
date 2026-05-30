using System.Collections.Generic;
using System.Text.RegularExpressions;

public static class ChemicalFormatter
{
    private static readonly Dictionary<char, char> SuperscriptChars = new Dictionary<char, char>
    {
        ['\u2070'] = '0',
        ['\u00B9'] = '1',
        ['\u00B2'] = '2',
        ['\u00B3'] = '3',
        ['\u2074'] = '4',
        ['\u2075'] = '5',
        ['\u2076'] = '6',
        ['\u2077'] = '7',
        ['\u2078'] = '8',
        ['\u2079'] = '9',
        ['\u207A'] = '+',
        ['\u207B'] = '-',
        ['\u207D'] = '(',
        ['\u207E'] = ')'
    };

    private static readonly Dictionary<char, char> SubscriptChars = new Dictionary<char, char>
    {
        ['\u2080'] = '0',
        ['\u2081'] = '1',
        ['\u2082'] = '2',
        ['\u2083'] = '3',
        ['\u2084'] = '4',
        ['\u2085'] = '5',
        ['\u2086'] = '6',
        ['\u2087'] = '7',
        ['\u2088'] = '8',
        ['\u2089'] = '9',
        ['\u208A'] = '+',
        ['\u208B'] = '-',
        ['\u208D'] = '(',
        ['\u208E'] = ')'
    };

    public static string Format(string text)
    {
        if (string.IsNullOrEmpty(text))
            return text;

        var protectedSubs = new List<string>();
        var protectedSups = new List<string>();

        text = Regex.Replace(text, @"_\{([^}]+)\}", match =>
        {
            int index = protectedSubs.Count;
            protectedSubs.Add($"<sub>{match.Groups[1].Value}</sub>");
            return $"[[SUB|{index}]]";
        });

        text = Regex.Replace(text, @"\^\{([^}]+)\}", match =>
        {
            int index = protectedSups.Count;
            protectedSups.Add($"<sup>{match.Groups[1].Value}</sup>");
            return $"[[SUP|{index}]]";
        });

        text = ReplaceUnicodeScripts(text, SuperscriptChars, "sup");
        text = ReplaceUnicodeScripts(text, SubscriptChars, "sub");

        text = Regex.Replace(text, @"\^([0-9]+[+-]?|[+-])", "<sup>$1</sup>");
        text = Regex.Replace(text, @"(?<=[A-Za-z\)])(\d+)", "<sub>$1</sub>");

        for (int i = 0; i < protectedSubs.Count; i++)
            text = text.Replace($"[[SUB|{i}]]", protectedSubs[i]);

        for (int i = 0; i < protectedSups.Count; i++)
            text = text.Replace($"[[SUP|{i}]]", protectedSups[i]);

        return text;
    }

    private static string ReplaceUnicodeScripts(string text, Dictionary<char, char> map, string tag)
    {
        var pattern = BuildCharacterClass(map);

        return Regex.Replace(text, pattern, match =>
        {
            var normalized = new char[match.Value.Length];

            for (int i = 0; i < match.Value.Length; i++)
                normalized[i] = map[match.Value[i]];

            return $"<{tag}>{new string(normalized)}</{tag}>";
        });
    }

    private static string BuildCharacterClass(Dictionary<char, char> map)
    {
        var chars = new char[map.Count];
        int index = 0;

        foreach (char c in map.Keys)
            chars[index++] = c;

        return $"[{Regex.Escape(new string(chars))}]+";
    }
}
