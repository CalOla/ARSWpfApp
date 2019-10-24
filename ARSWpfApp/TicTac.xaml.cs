using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ARSWpfApp
{
    /// <summary>
    /// Interaction logic for TicTac.xaml
    /// </summary>
    public partial class TicTac : Window
    {
        #region Private Members
        private MarkType[] mResults;
        private bool mPlayer1Turn;
        private bool mGameEnded;
        #endregion
        public TicTac()
        {
            InitializeComponent();
            NewGame();
        }

        private void NewGame()
        {
            mResults = new MarkType[9];

            for (var i = 0; i < mResults.Length; i++)
            {
                mResults[i] = MarkType.Free;
            }

            mPlayer1Turn = true;

            Container.Children.Cast<Button>().ToList().ForEach(button => {
                button.Content = String.Empty;
                button.Background = Brushes.White;
                button.Foreground = Brushes.Blue;
            });

            mGameEnded = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (mGameEnded)
            {
                NewGame();
                return;
            }

            var button = (Button)sender;
            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);

            var index = column + (row * 3);

            if (mResults[index] != MarkType.Free)
            {
                return;
            }

            mResults[index] = mPlayer1Turn ? MarkType.Cross : MarkType.Nought;

            button.Content = mPlayer1Turn ? 'X' : 'O';

            mPlayer1Turn ^= true;
            if (mPlayer1Turn)
            {
                button.Foreground = Brushes.Red;
            }

            CheckForWinner();
        }

        private void CheckForWinner()
        {
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[1] & mResults[2]) == mResults[0])
            {
                mGameEnded = true;
                Button_0_0.Background = Button_1_0.Background = Button_2_0.Background = Brushes.Green;
            }

            if (mResults[3] != MarkType.Free && (mResults[3] & mResults[4] & mResults[5]) == mResults[3])
            {
                mGameEnded = true;
                Button_0_1.Background = Button_1_1.Background = Button_2_1.Background = Brushes.Green;
            }

            if (mResults[6] != MarkType.Free && (mResults[6] & mResults[7] & mResults[8]) == mResults[6])
            {
                mGameEnded = true;
                Button_0_2.Background = Button_1_2.Background = Button_2_2.Background = Brushes.Green;
            }

            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[3] & mResults[6]) == mResults[0])
            {
                mGameEnded = true;
                Button_0_0.Background = Button_0_1.Background = Button_0_2.Background = Brushes.Green;
            }

            if (mResults[1] != MarkType.Free && (mResults[1] & mResults[4] & mResults[7]) == mResults[1])
            {
                mGameEnded = true;
                Button_1_0.Background = Button_1_1.Background = Button_1_2.Background = Brushes.Green;
            }

            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[5] & mResults[8]) == mResults[2])
            {
                mGameEnded = true;
                Button_2_0.Background = Button_2_1.Background = Button_2_2.Background = Brushes.Green;
            }

            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[4] & mResults[8]) == mResults[0])
            {
                mGameEnded = true;
                Button_0_0.Background = Button_1_1.Background = Button_2_2.Background = Brushes.Green;
            }

            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[4] & mResults[6]) == mResults[2])
            {
                mGameEnded = true;
                Button_2_0.Background = Button_1_1.Background = Button_0_2.Background = Brushes.Green;
            }

            if (!mResults.Any(f => f == MarkType.Free))
            {
                mGameEnded = true;

                Container.Children.Cast<Button>().ToList().ForEach(button => {
                    button.Background = Brushes.Orange;
                });
            }
        }
    }
}
