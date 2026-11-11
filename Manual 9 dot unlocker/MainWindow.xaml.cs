using System.Windows;
using System.Windows.Input;
using System.Windows.Shapes;

namespace Manual_9_dot_unlocker
{
    public partial class MainWindow : Window
    {

        private enum AppState { WaitingForStart, BrowsingCombinations }
        private AppState _state = AppState.WaitingForStart;

        private List<string> _combinations = new List<string>();
        private int _currentIndex = 0;


        //lines are all named in the format of "Line" + number from and to,
        //so for ex. Line23 goes from 2 to 3 or vice versa, etc.
        //lines are used to draw combinations for the user.
        private readonly Dictionary<string, Line> _lineMap = new Dictionary<string, Line>();

        //12 23 45 56 78 89 horizontals
        //14 25 36 47 58 69 verticals
        private static readonly Dictionary<int, int[]> AllowedMoves = new Dictionary<int, int[]>
        {
            { 1, new[] { 2, 4 } },
            { 2, new[] { 1, 3, 5 } },
            { 3, new[] { 2, 6 } },
            { 4, new[] { 1, 5, 7 } },
            { 5, new[] { 2, 4, 6, 8 } },
            { 6, new[] { 3, 5, 9 } },
            { 7, new[] { 4, 8 } },
            { 8, new[] { 5, 7, 9 } },
            { 9, new[] { 6, 8 } },
        };

        public MainWindow()
        {
            InitializeComponent();

            BuildLineMap();

            PreviewKeyDown += MainWindow_PreviewKeyDown;

            ResetToStepOne();
        }

        private void BuildLineMap()
        {

            void Add(Line line, int from, int to)
            {
                _lineMap[$"{from}>{to}"] = line;
                _lineMap[$"{to}>{from}"] = line;
            }

            Add(Line12, 1, 2);
            Add(Line23, 2, 3);
            Add(Line45, 4, 5);
            Add(Line56, 5, 6);
            Add(Line78, 7, 8);
            Add(Line89, 8, 9);
            Add(Line14, 1, 4);
            Add(Line25, 2, 5);
            Add(Line36, 3, 6);
            Add(Line47, 4, 7);
            Add(Line58, 5, 8);
            Add(Line69, 6, 9);
        }

        private void ResetToStepOne()
        {
            _state = AppState.WaitingForStart;
            _combinations.Clear();
            _currentIndex = 0;

            HideAllLines();
            SetAllButtonsEnabled(true);

            if (TitleLabel != null)
                TitleLabel.Content = "Click on the first dot";
        }

        private void StartWith(int startDot)
        {
            _combinations = GenerateCombinations(startDot);
            _currentIndex = 0;

            _state = AppState.BrowsingCombinations;
            SetAllButtonsEnabled(false);

            if (_combinations.Count == 0)
            {
                ResetToStepOne();
                return;
            }

            ShowCombination(_combinations[_currentIndex]);
        }

        private List<string> GenerateCombinations(int startDot)
        {
            var results = new List<string>();
            var path = new List<int>();
            var visited = new bool[10];

            void Dfs(int current)
            {
                visited[current] = true;
                path.Add(current);

                if (path.Count >= 4)
                    results.Add(string.Concat(path));

                if (path.Count < 9)
                {
                    foreach (var next in AllowedMoves[current])
                    {
                        if (!visited[next])
                            Dfs(next);
                    }
                }

                path.RemoveAt(path.Count - 1);
                visited[current] = false;
            }

            Dfs(startDot);
            return results;
        }

        private void HideAllLines()
        {
            foreach (var line in _lineMap.Values.Distinct())
                line.Visibility = Visibility.Hidden;
        }

        private void ShowCombination(string code)
        {
            HideAllLines();

            for (int i = 0; i < code.Length - 1; i++)
            {
                int from = code[i] - '0';
                int to = code[i + 1] - '0';
                if (_lineMap.TryGetValue($"{from}>{to}", out var line))
                    line.Visibility = Visibility.Visible;
            }

            int remaining = _combinations.Count - _currentIndex;
            TitleLabel.Content = $"{remaining} combinations left";
        }

        private void SetAllButtonsEnabled(bool enabled)
        {
            Button1.IsEnabled = enabled;
            Button2.IsEnabled = enabled;
            Button3.IsEnabled = enabled;
            Button4.IsEnabled = enabled;
            Button5.IsEnabled = enabled;
            Button6.IsEnabled = enabled;
            Button7.IsEnabled = enabled;
            Button8.IsEnabled = enabled;
            Button9.IsEnabled = enabled;
        }

        private void MainWindow_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (_state == AppState.BrowsingCombinations)
            {
                if (e.Key == Key.Space)
                {
                    e.Handled = true;
                    AdvanceCombination();
                }
                else if (e.Key == Key.Escape)
                {
                    e.Handled = true;
                    ResetToStepOne();
                }
            }
            else
            {
                if (e.Key == Key.Escape)
                {
                    e.Handled = true;
                    Close();
                }
            }
        }

        private void AdvanceCombination()
        {
            _currentIndex++;

            if (_currentIndex >= _combinations.Count)
            {
                ResetToStepOne();
                return;
            }

            ShowCombination(_combinations[_currentIndex]);
        }

        private void Button1_Click(object sender, RoutedEventArgs e) => OnDotClicked(1);
        private void Button2_Click(object sender, RoutedEventArgs e) => OnDotClicked(2);
        private void Button3_Click(object sender, RoutedEventArgs e) => OnDotClicked(3);
        private void Button4_Click(object sender, RoutedEventArgs e) => OnDotClicked(4);
        private void Button5_Click(object sender, RoutedEventArgs e) => OnDotClicked(5);
        private void Button6_Click(object sender, RoutedEventArgs e) => OnDotClicked(6);
        private void Button7_Click(object sender, RoutedEventArgs e) => OnDotClicked(7);
        private void Button8_Click(object sender, RoutedEventArgs e) => OnDotClicked(8);
        private void Button9_Click(object sender, RoutedEventArgs e) => OnDotClicked(9);

        private void OnDotClicked(int dotNumber)
        {
            if (_state != AppState.WaitingForStart) return;
            StartWith(dotNumber);
        }
    }
}