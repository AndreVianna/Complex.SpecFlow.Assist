namespace SpecFlow.Assist.Complex;

internal class Context {
    private const string _regexPattern = @"^([^\[<]+)(\[(\d+)\])*$"; // abc[2][3][4] => $1:abc, #3:[2,3,4]
    private const RegexOptions _regexOptions = Compiled | IgnoreCase | Singleline | IgnorePatternWhitespace;
    private static readonly TimeSpan _regexTimeout = TimeSpan.FromMilliseconds(10);
    private static readonly Regex _regex = new(_regexPattern, _regexOptions, _regexTimeout);

    public Context(IEnumerator<TableRow?> enumerator, int level = 0) {
        Enumerator = enumerator;
        Level = level;
        CurrentKey = Enumerator.Current?[0];
        CurrentValue = Enumerator.Current?[1];
        _basePath = string.Join('.', CurrentKey?.Split('.').Take(Level) ?? Array.Empty<string>());
        SetCurrentPropertyInfo();
    }


    public IEnumerator<TableRow?> Enumerator { get; }
    public string? CurrentKey { get; private set; }
    public string? CurrentValue { get; private set; }
    public int Level { get; }

    public bool HasMore {
        get {
            if (Enumerator.Current is null) return false;
            var currentKey = Enumerator.Current[0];
            var basePath = string.Join('.', currentKey.Split('.').Take(Level));
            return basePath == _basePath;
        }
    }

    public int[] PreviousIndexes { get; private set; } = default!;
    private readonly string _basePath;
    private PropertyInfo? _previousProperty;
    private PropertyInfo? _currentProperty;

    private void SetCurrentPropertyInfo() {
        if (CurrentKey is null) return;
        var relativePath = CurrentKey.Split('.').Skip(Level).ToArray();
        var currentToken = relativePath.First();
        var match = _regex.Match(currentToken);
        var name = match.Groups[1].Value;
        var indexes = match.Groups[3].Captures.Select(c => int.Parse(c.Value)).ToArray();

        _previousProperty = _currentProperty;
        _currentProperty = new PropertyInfo(name, indexes, relativePath.Skip(1).ToArray());
        AdjustPreviousIndexes();
    }

    public PropertyInfo GetCurrentPropertyInfo() {
        return _currentProperty!;
    }

    public void MoveNext() {
        Enumerator.MoveNext();
        UpdateCurrent();
    }

    public void UpdateCurrent() {
        CurrentKey = Enumerator.Current?[0];
        CurrentValue = Enumerator.Current?[1];
        SetCurrentPropertyInfo();
    }

    public void AdjustPreviousIndexes() {
        PreviousIndexes = _previousProperty?.Indexes ?? Array.Empty<int>();
        if (_currentProperty!.Name != _previousProperty?.Name) {
            PreviousIndexes = Enumerable.Repeat(-1, _currentProperty.Indexes.Length).ToArray();
            return;
        }

        if (_currentProperty.Indexes.Length <= 1) return;

        for (var i = 0; i < _currentProperty.Indexes.Length; i++) {
            if (PreviousIndexes[i] == _currentProperty.Indexes[i]) continue;
            for (var j = (i + 1); j < PreviousIndexes.Length; j++) {
                PreviousIndexes[j] = -1;
            }

            return;
        }
    }
}