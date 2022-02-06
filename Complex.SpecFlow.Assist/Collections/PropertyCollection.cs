namespace Complex.SpecFlow.Assist.Collections;

internal sealed class PropertyCollection : IDisposable {

    private readonly PropertyEnumerator _enumerator;
    internal PropertyCollection(IEnumerator<TableLine> enumerator, int level = 0) {
        _enumerator = new PropertyEnumerator(enumerator, level);
    }

    public PropertyCollection LevelUp() => new(_enumerator.Source!, _enumerator.Level + 1);


    public PropertyEnumerator GetEnumerator() => _enumerator;
    public void DoNotMoveNext() => _enumerator.DoNotMoveNext();

    public void Dispose() => _enumerator.Source.Dispose();

    public sealed class PropertyEnumerator {
        private const string _regexPattern = @"^([^\[<]+)(\[([^]]+)\])*$"; // abc[2][-3][df] => $1:abc, $3:[2,-3,df]
        private const RegexOptions _regexOptions = Compiled | IgnoreCase | Singleline | IgnorePatternWhitespace;
        private static readonly TimeSpan _regexTimeout = TimeSpan.FromMilliseconds(10);
        private static readonly Regex _regex = new(_regexPattern, _regexOptions, _regexTimeout);
        private readonly string _basePath;
        private bool _canMoveNext;

        public PropertyEnumerator(IEnumerator<TableLine> source, int level) {
            Source = source;
            Level = level;
            _canMoveNext = level == 0;
            _basePath = GetBasePath();
        }

        public IEnumerator<TableLine?> Source { get; }
        public int Level { get; }
        public Property Current { get; private set; } = default!;

        public void DoNotMoveNext() {
            _canMoveNext = false;
        }

        public bool MoveNext() {
            if (_canMoveNext) Source.MoveNext();
            _canMoveNext = true;
            if (Source.Current is null) return false;
            SetCurrent();
            return _basePath == GetBasePath();
        }

        private string GetBasePath() => string.Join('.', Source.Current?.Key.Split('.').Take(Level) ?? Array.Empty<string>());

        private void SetCurrent() {
            var key = Source.Current!.Key;
            var relativePath = key.Split('.').Skip(Level).ToArray();
            if (!relativePath.Any()) return;

            var previousName = Current?.Name ?? string.Empty;
            var (name, keys) = DeconstructToken(relativePath.First());

            var previousIndexes = Current?.Indexes ?? Array.Empty<int>();
            var indexes = ConvertKeys(keys, previousIndexes, previousName == name);
            var children = relativePath.Skip(1).ToArray();

            Current = new Property(name, indexes, children, Source.Current);
        }

        private static (string name, string[] keys) DeconstructToken(string token) {
            var match = _regex.Match(token);
            var name = match.Groups[1].Value;
            var keys = match.Groups[3].Captures.Select(c => c.Value).ToArray();
            return (name, keys);
        }

        private int[] ConvertKeys(IReadOnlyList<string> keys, IList<int> previousIndexes, bool isSameProperty) {
            var result = new List<int>();
            for (var i = 0; i < keys.Count; i++) {
                var previous = (isSameProperty && i < previousIndexes.Count) ? previousIndexes[i] : -1;
                var index = GetIndex(keys[i], previous, i == keys.Count - 1);
                if (index > 0 && index > previous) for (var j = i + 1; j < previousIndexes.Count; j++) previousIndexes[j] = -1;
                result.Add(index);
            }
            return result.ToArray();
        }

        private int GetIndex(string key, int previous, bool isLast) {
            if (!int.TryParse(key, out var index) || index < 0)
                throw new InvalidDataException($"Invalid array index at '{Source.Current!.Key}'.");
            switch (index - previous) {
                case < 0:
                case > 1:
                case 0 when isLast:
                    throw new InvalidDataException($"Invalid array index at '{Source.Current!.Key}'.");
            }

            return index;
        }
    }
}
