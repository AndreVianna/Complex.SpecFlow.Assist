namespace SpecFlow.Assist.Complex;

internal class Context {
    private const string _regexPattern = @"^([^\[<]+)(\[([^]]+)\])*$"; // abc[2][-3][df] => $1:abc, #3:[2,-3,df]
    private const RegexOptions _regexOptions = Compiled | IgnoreCase | Singleline | IgnorePatternWhitespace;
    private static readonly TimeSpan _regexTimeout = TimeSpan.FromMilliseconds(10);
    private static readonly Regex _regex = new(_regexPattern, _regexOptions, _regexTimeout);
    private readonly string _basePath;

    public Context(IEnumerator<TableRow?> enumerator, int level = 0) {
        Enumerator = enumerator;
        Level = level;
        if (level == 0) MoveNext();
        else UpdateCurrent();
        _basePath = string.Join('.', Current!.Key.Split('.').Take(Level));
    }


    public IEnumerator<TableRow?> Enumerator { get; }
    public Property? Current { get; private set; }

    public int Level { get; }
    public bool HasMore =>
        Enumerator.Current is not null &&
        _basePath == string.Join('.', Enumerator.Current[0].Split('.').Take(Level));


    public void MoveNext() {
        Enumerator.MoveNext();
        UpdateCurrent();
    }

    public void UpdateCurrent() {
        var previousName = Current?.Name ?? string.Empty;
        var previousIndexes = Current?.Indexes ?? Array.Empty<int>();
        Current = null;
        var key = Enumerator.Current?[0];
        if (key is null) return;

        var value = Enumerator.Current![1];
        var relativePath = key.Split('.').Skip(Level).ToArray();
        var currentToken = relativePath.First();
        var match = _regex.Match(currentToken);
        var name = match.Groups[1].Value;
        var keys = match.Groups[3].Captures.Select(c => c.Value).ToArray();
        var indexes = ConvertKeys(keys, previousIndexes, previousName == name);
        var children = relativePath.Skip(1).ToArray();
        Current = new Property(key, value, name, indexes, children);
    }

    private int[] ConvertKeys(IReadOnlyList<string> keys, int[] previousIndexes, bool sameProperty) {
        var result = new List<int>();
        for (var i = 0; i < keys.Count; i++) {
            var key = keys[i];
            var previous = sameProperty && i < previousIndexes.Length ? previousIndexes[i] : -1;
            var index = GetIndex(key, previous, i == keys.Count - 1);
            if (index > previous && index > 0)
                for (var j = i + 1; j < previousIndexes.Length; j++)
                    previousIndexes[j] = -1;
            result.Add(index);
        }
        return result.ToArray();
    }

    private int GetIndex(string key, int previous, bool isLast) {
        if (!int.TryParse(key, out var index) || index < 0)
            throw new InvalidDataException($"Invalid array index at '{Enumerator.Current![0]}'.");
        switch (index - previous) {
            case < 0:
            case > 1:
            case 0 when isLast:
                throw new InvalidDataException($"Invalid array index at '{Enumerator.Current![0]}'.");
        }

        return index;
    }
}