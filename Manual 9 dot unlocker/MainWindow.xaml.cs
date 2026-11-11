using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
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
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button4_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button5_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button6_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button7_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button8_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button9_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}