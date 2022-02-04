namespace Complex.SpecFlow.Assist.Models;

internal sealed class PropertyCollection {

    private readonly PropertyEnumerator _enumerator;

    private PropertyCollection(IEnumerator<TableRow?> enumerator, int level = 0) {
        _enumerator = new PropertyEnumerator(enumerator, level);
    }

    public static PropertyCollection Create(Table table) => new(table.Rows.GetEnumerator());
    public PropertyCollection LevelUp() => new(_enumerator.Source, _enumerator.Level + 1);


    public PropertyEnumerator GetEnumerator() => _enumerator;
    public void DoNotMoveNext() => _enumerator.DoNotMoveNext();

    public sealed class PropertyEnumerator {
        private const string _regexPattern = @"^([^\[<]+)(\[([^]]+)\])*$"; // abc[2][-3][df] => $1:abc, $3:[2,-3,df]
        private const RegexOptions _regexOptions = Compiled | IgnoreCase | Singleline | IgnorePatternWhitespace;
        private static readonly TimeSpan _regexTimeout = TimeSpan.FromMilliseconds(10);
        private static readonly Regex _regex = new(_regexPattern, _regexOptions, _regexTimeout);
        private readonly string _basePath;
        private bool _canMoveNext;

        public PropertyEnumerator(IEnumerator<TableRow?> source, int level) {
            Source = source;
            Level = level;
            _canMoveNext = level == 0;
            _basePath = GetBasePath();
        }

        public IEnumerator<TableRow?> Source { get; }
        public int Level { get; }
        public Property Current { get; private set; } = new();

        public void DoNotMoveNext() {
            _canMoveNext = false;
        }

        public bool MoveNext() {
            if (_canMoveNext) Source.MoveNext();
            UpdateCurrent();
            _canMoveNext = true;
            return Source.Current is not null && _basePath == GetBasePath();
        }

        private string GetBasePath() => Source.Current is not null ? string.Join('.', Source.Current[0].Split('.').Take(Level)) : string.Empty;

        private void UpdateCurrent() {
            var previousName = Current.Name;
            var previousIndexes = Current.Indexes;
            var key = Source.Current?[0];
            if (key is null) return;

            var value = Source.Current![1];
            var line = new TableLine(key, value);

            var relativePath = key.Split('.').Skip(Level).ToArray();
            var (name, keys) = DeconstructToken(relativePath.First());
            var indexes = ConvertKeys(keys, previousIndexes, previousName == name);
            var children = relativePath.Skip(1).ToArray();
            Current = new Property(name, indexes, children, line);
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
                throw new InvalidDataException($"Invalid array index at '{Source.Current![0]}'.");
            switch (index - previous) {
                case < 0:
                case > 1:
                case 0 when isLast:
                    throw new InvalidDataException($"Invalid array index at '{Source.Current![0]}'.");
            }

            return index;
        }
    }
}
