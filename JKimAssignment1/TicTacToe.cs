/* TicTacToe.cs
 * Assignment 1
 * Revision History
 *      Jisung Kim, 2021.09.28: Created
 *      Jisung Kim, 2021.10.01: Modified code
 *      
 */

using System;
using System.Drawing;
using System.Windows.Forms;

namespace JKimAssignment1
{
    /// <summary>
    /// A class to use images to play Tic-Tac-Toe game
    /// </summary>
    public partial class TicTacToe : Form
    {
        // Declaring class variables and constants
        private const int SIZE = 3;

        private readonly Image X = Properties.Resources.x;
        private readonly Image O = Properties.Resources.o;

        private Image[,] marks;

        private bool isPlaying;
        private bool isTurnOfX;
        private int numberOfMarks;

        /// <summary>
        /// Default constructor of the TicTacToe class
        /// </summary>
        public TicTacToe()
        {
            InitializeComponent();
        }

        private void TicTacToe_Load(object sender, EventArgs e)
        {
            initialize();
        }

        /// <summary>
        /// Initialize variables for game start or restart
        /// </summary>
        private void initialize()
        {
            marks = new Image[SIZE, SIZE];

            isPlaying = true;
            isTurnOfX = true;
            numberOfMarks = 0;

            foreach (var control in this.Controls)
            {
                if (control is PictureBox pictureBox)
                {
                    pictureBox.Image = null;
                }
            }
        }

        // Click event handler that set the image in the selected picture box
        private void setImage(object sender, EventArgs e)
        {
            if (sender is PictureBox pictureBox)
            {
                if (pictureBox.Image != null)
                {
                    return;
                }

                if (isTurnOfX)
                {
                    pictureBox.Image = X;
                    isTurnOfX = false;
                }
                else
                {
                    pictureBox.Image = O;
                    isTurnOfX = true;
                }

                addImageToArray(pictureBox);

                numberOfMarks++;
                
                displayResults(checkGameEnded());
            }
        }

        /// <summary>
        /// Add the image set in the picture box to the array
        /// </summary>
        /// <param name="pictureBox">The picture box clicked by user</param>
        private void addImageToArray(PictureBox pictureBox)
        {
            switch (pictureBox.Name)
            {
                case "picFirstRowLeft":
                    marks[0, 0] = pictureBox.Image;
                    break;
                case "picFirstRowCenter":
                    marks[0, 1] = pictureBox.Image;
                    break;
                case "picFirstRowRight":
                    marks[0, 2] = pictureBox.Image;
                    break;
                case "picSecondRowLeft":
                    marks[1, 0] = pictureBox.Image;
                    break;
                case "picSecondRowCenter":
                    marks[1, 1] = pictureBox.Image;
                    break;
                case "picSecondRowRight":
                    marks[1, 2] = pictureBox.Image;
                    break;
                case "picThirdRowLeft":
                    marks[2, 0] = pictureBox.Image;
                    break;
                case "picThirdRowCenter":
                    marks[2, 1] = pictureBox.Image;
                    break;
                case "picThirdRowRight":
                    marks[2, 2] = pictureBox.Image;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Check if the game is ended
        /// </summary>
        /// <returns>The image of the winner or null</returns>
        private Image checkGameEnded()
        {
            // The winner can be determined after at least 5 turns
            if (numberOfMarks < 5)
            {
                return null;
            }

            isPlaying = true;
            Image previousMark = null;

            // Check all the rows
            for (int row = 0; row < SIZE; row++)
            {
                previousMark = null;
                for (int column = 0; column < SIZE; column++)
                {
                    if (marks[row, column] == null)
                    {
                        break;
                    }

                    if (previousMark == null)
                    {
                        previousMark = marks[row, column];
                    }
                    else if (previousMark != marks[row, column])
                    {
                        break;
                    }

                    if (isLastIndex(column))
                    {
                        isPlaying = false;
                        return previousMark;
                    }
                }
            }

            // Check all the columns
            for (int row = 0; row < SIZE; row++)
            {
                previousMark = null;
                for (int column = 0; column < SIZE; column++)
                {
                    if (marks[column, row] == null)
                    {
                        break;
                    }

                    if (previousMark == null)
                    {
                        previousMark = marks[column, row];
                    }
                    else if (previousMark != marks[column, row])
                    {
                        break;
                    }

                    if (isLastIndex(column))
                    {
                        isPlaying = false;
                        return previousMark;
                    }
                }
            }

            // Check all the diagonal lines
            previousMark = null;
            for (int row = 0; row < SIZE; row++)
            {
                if (marks[row, row] == null)
                {
                    break;
                }

                if (previousMark == null)
                {
                    previousMark = marks[row, row];
                }
                else if (previousMark != marks[row, row])
                {
                    break;
                }

                if (isLastIndex(row))
                {
                    isPlaying = false;
                    return previousMark;
                }
            }
            previousMark = null;
            for (int row = 0; row < SIZE; row++)
            {
                if (marks[row, Math.Abs(row - 2)] == null)
                {
                    break;
                }

                if (previousMark == null)
                {
                    previousMark = marks[row, Math.Abs(row - 2)];
                }
                else if (previousMark != marks[row, Math.Abs(row - 2)])
                {
                    break;
                }

                if (isLastIndex(row))
                {
                    isPlaying = false;
                    return previousMark;
                }
            }

            // Check the empty space
            foreach (var mark in marks)
            {
                if (mark == null)
                {
                    return null;
                }
            }

            isPlaying = false;
            return null;
        }

        /// <summary>
        /// Check if it is the last index of a row or column
        /// </summary>
        /// <param name="index"></param>
        /// <returns>true if the number is the last index; otherwise false</returns>
        private bool isLastIndex(int index)
        {
            return index == (SIZE - 1);
        }

        /// <summary>
        /// Show the game result message
        /// </summary>
        /// <param name="mark">The image of the winner</param>
        private void displayResults(Image mark)
        {
            if (isPlaying)
            {
                return;
            }
            
            if (mark == X)
            {
                MessageBox.Show("X Wins");
            }
            else if (mark == O)
            {
                MessageBox.Show("O Wins");
            }
            else
            {
                MessageBox.Show("Draw");
            }

            initialize();
        }
    }
}
