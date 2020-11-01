using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Members
        //Calculates number of turns playes
        private int TurnsPlayed = 0;
        //Bool to check if game has ended
        private bool GameEnded = false;
        //Stores what cell has what value, X, O or Free
        private CellInfo[,] GameResult = new CellInfo[3, 3];
        #endregion
        public MainWindow()
        {
            InitializeComponent();
            StartGame();
        }
        /// <summary>
        /// Initializes the start of the game
        /// </summary>
        private void StartGame()
        {
            //Gets current clicked button
            var Buttons = Container.Children.Cast<Button>().ToList();
            //Clears Tuns Played and Set GameEnded to Flase
            TurnsPlayed = 0;
            GameEnded = false;
            //Clears all Button Content
            foreach (Button i in Buttons)
            {
                i.Content = "";
                i.Background = Brushes.White;
            }

            //Sets all CellInfo as free cells
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    GameResult[i, j] = CellInfo.Free;
        }

        /// <summary>
        /// Handles the button click event
        /// </summary>
        /// <param name="sender">Sender is the current clicked button</param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button ClickedButton = (Button)sender;
            if (ButtonValid(ClickedButton))
            {
                if (NextTurn() == "P1")
                {
                    int col = Grid.GetColumn(ClickedButton);
                    int row = Grid.GetRow(ClickedButton);
                    GameResult[row, col] = CellInfo.X;
                    ClickedButton.Content = "X";
                }
                else
                {
                    int col = Grid.GetColumn(ClickedButton);
                    int row = Grid.GetRow(ClickedButton);
                    GameResult[row, col] = CellInfo.O;
                    ClickedButton.Content = "O";
                }
            }
            CheckGame();
        }
        /// <summary>
        /// Checks if the game is finished
        /// </summary>
        private void CheckGame()
        {
            CellInfo[] Row1 = new CellInfo[3];
            CellInfo[] Row2 = new CellInfo[3];
            CellInfo[] Row3 = new CellInfo[3];

            CellInfo[] Col1 = new CellInfo[3];
            CellInfo[] Col2 = new CellInfo[3];
            CellInfo[] Col3 = new CellInfo[3];

            CellInfo[] Dia1 = new CellInfo[3];
            CellInfo[] Dia2 = new CellInfo[3];

            //Winner Text String. Contains if a player won or its a tie
            String WinText = "";

            for (int i = 0; i < 3; i++) 
            {
                Row1[i] = GameResult[0, i];
                Row2[i] = GameResult[1, i];
                Row3[i] = GameResult[2, i];

                Col1[i] = GameResult[i, 0];
                Col2[i] = GameResult[i, 1];
                Col3[i] = GameResult[i, 2];

                Dia1[i] = GameResult[i, i];
                Dia2[i] = GameResult[i, 2 - i];

            }

            //Check Row
            if (Row1.All(Element => Element == CellInfo.X) || Row1.All(Element => Element == CellInfo.O))
            {
                WinText = Row1[0] == CellInfo.X ? "Player 1 Wins!" : "Player 2 Wins!";
                Button1.Background = Button4.Background = Button7.Background = Brushes.LightGreen;
                GameEnded = true;
            }
            if (Row2.All(Element => Element == CellInfo.X) || Row2.All(Element => Element == CellInfo.O))
            {
                WinText = Row2[0] == CellInfo.X ? "Player 1 Wins!" : "Player 2 Wins!";
                Button2.Background = Button5.Background = Button8.Background = Brushes.LightGreen;
                GameEnded = true;
            }
            if (Row3.All(Element => Element == CellInfo.X) || Row3.All(Element => Element == CellInfo.O))
            {
                WinText = Row3[0] == CellInfo.X ? "Player 1 Wins!" : "Player 2 Wins!";
                Button3.Background = Button6.Background = Button9.Background = Brushes.LightGreen;
                GameEnded = true;
            }

            //Check Coloumn
            if (Col1.All(Element => Element == CellInfo.X) || Col1.All(Element => Element == CellInfo.O))
            {
                WinText = Col1[0] == CellInfo.X ? "Player 1 Wins!" : "Player 2 Wins!";
                Button1.Background = Button2.Background = Button3.Background = Brushes.LightGreen;
                GameEnded = true;
            }
            if (Col2.All(Element => Element == CellInfo.X) || Col2.All(Element => Element == CellInfo.O))
            {
                WinText = Col2[0] == CellInfo.X ? "Player 1 Wins!" : "Player 2 Wins!";
                Button4.Background = Button5.Background = Button6.Background = Brushes.LightGreen;
                GameEnded = true;
            }
            if (Col3.All(Element => Element == CellInfo.X) || Col3.All(Element => Element == CellInfo.O))
            {
                WinText = Col3[0] == CellInfo.X ? "Player 1 Wins!" : "Player 2 Wins!";
                Button7.Background = Button8.Background = Button9.Background = Brushes.LightGreen;
                GameEnded = true;
            }

            //Check Diagonal
            if (Dia1.All(Element => Element == CellInfo.X) || Dia1.All(Element => Element == CellInfo.O))
            {
                WinText = Dia1[0] == CellInfo.X ? "Player 1 Wins!" : "Player 2 Wins!";
                Button1.Background = Button5.Background = Button9.Background = Brushes.LightGreen;
                GameEnded = true;
            }
            if (Dia2.All(Element => Element == CellInfo.X) || Dia2.All(Element => Element == CellInfo.O))
            {
                WinText = Dia2[0] == CellInfo.X ? "Player 1 Wins!" : "Player 2 Wins!";
                Button3.Background = Button5.Background = Button7.Background = Brushes.LightGreen;
                GameEnded = true;
            }

            if (TurnsPlayed == 9)
            {
                GameEnded = true;
                if (WinText == "") WinText = "Tie Game!";
            }

            if (GameEnded)
            {
                MessageBox.Show(WinText);
                StartGame();
            }
        }

        /// <summary>
        /// Checks if the current Button click / Cell is a valid cell
        /// </summary>
        /// <param name="ClickedButton"></param>
        /// <returns></returns>
        private bool ButtonValid(Button ClickedButton)
        {
            int col = Grid.GetColumn(ClickedButton);
            int row = Grid.GetRow(ClickedButton);

            if (GameResult[row, col] != CellInfo.Free)
                return false;
            else return true;
        }
        /// <summary>
        /// Returns which player plays next
        /// </summary>
        /// <returns></returns>
        private string NextTurn()
        {
            // If TurnsPlayed is even P1 has plays else P2 plays
            if (TurnsPlayed % 2 == 0)
            {
                TurnsPlayed++; 
                return "P1";
            }
            else
            {
                TurnsPlayed++; 
                return "P2";
            }
        }
    }
}