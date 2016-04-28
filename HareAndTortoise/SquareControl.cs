using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Square_Class_Library;
using Player_Class_Library;



namespace HareAndTortoise {
    public partial class SquareControl : PictureBox {

        public const int SQUARE_SIZE = 100;

        private Square square;  // A reference to the corresponding square object

        private BindingList<Player> players;  // References the players in the overall game.

        private bool[] containsPlayers = new bool[6];//HareAndTortoiseGame.MAX_PLAYERS];
       
        public bool[] ContainsPlayers {
            get {
                return containsPlayers;
            }
            set {
                containsPlayers = value;
            }
        }
        
       

        // Font and brush for displaying text inside the square.
        private Font textFont = new Font("Microsoft Sans Serif", 8);
        private Brush textBrush = Brushes.White;

        public SquareControl(Square square, BindingList<Player> players) {

            this.square = square;
            this.players = players;

            //  Set GUI properties of the whole square.
            Size = new Size(SQUARE_SIZE, SQUARE_SIZE);
            Margin = new Padding(0);  // No spacing around the cell. (Default is 3 pixels.)
            Dock = DockStyle.Fill;
            BorderStyle = BorderStyle.FixedSingle;
            BackColor = Color.CornflowerBlue;

            SetImageWhenNeeded();
        }

        private void SetImageWhenNeeded() {

            if (square is Win_Square) {
                LoadImageFromFile("Win.png");
                textBrush = Brushes.Black;
            } else if (square is Lose_Square) {
                LoadImageFromFile("Lose.png");
                textBrush = Brushes.Red;
            } else if (square is Chance_Square) {
                LoadImageFromFile("monster-green.png"); 
            } else if (square.Name == "Finish") {
                LoadImageFromFile("checkered-flag.png");
            } else {
                // No image needed.
            }

        }

        private void LoadImageFromFile(string fileName) {
            Image image = Image.FromFile(@"Images\" + fileName);
            Image = image;
            SizeMode = PictureBoxSizeMode.StretchImage;  // Zoom is also ok.
        }

        protected override void OnPaint(PaintEventArgs e) {

            //  Due to a limitation in WinForms, don't use base.OnPaint(e) here.

            if (Image != null)
                e.Graphics.DrawImage(Image, e.ClipRectangle);

            string name = square.Name;

            // Create rectangle for drawing.
            float textWidth = textFont.Size * name.Length;
            float textHeight = textFont.Height;
            float textX = e.ClipRectangle.Right - textWidth;
            float textY = e.ClipRectangle.Bottom - textHeight;
            RectangleF drawRect = new RectangleF(textX, textY, textWidth, textHeight);

            // When debugging this method, show the drawing-rectangle on the screen.
            //Pen blackPen = new Pen(Color.Black);
            //e.Graphics.DrawRectangle(blackPen, textX, textY, textWidth, textHeight);

            // Set format of string.
            StringFormat drawFormat = new StringFormat();
            drawFormat.Alignment = StringAlignment.Far;  // Right-aligned.

            // Draw string to screen.
            e.Graphics.DrawString(name, textFont, textBrush, drawRect, drawFormat);

            //  Draw player tokens (when any players are on this square).
            const int PLAYER_TOKENS_PER_ROW = 3;
            const int PLAYER_TOKEN_SIZE = 30;  // pixels.
            const int PLAYER_TOKEN_SPACING = (SQUARE_SIZE - (PLAYER_TOKEN_SIZE * PLAYER_TOKENS_PER_ROW)) / (PLAYER_TOKENS_PER_ROW - 1);

            for (int i = 0; i < containsPlayers.Length; i++) {
                if (containsPlayers[i]) {
                    int xPosition = i % PLAYER_TOKENS_PER_ROW;
                    int yPosition = i / PLAYER_TOKENS_PER_ROW;
                    int xPixels = xPosition * (PLAYER_TOKEN_SIZE + PLAYER_TOKEN_SPACING);
                    int yPixels = yPosition * (PLAYER_TOKEN_SIZE + PLAYER_TOKEN_SPACING);
                    Brush playerTokenColour = players[i].PlayerTokenColour;
                    e.Graphics.FillEllipse(playerTokenColour, xPixels, yPixels, PLAYER_TOKEN_SIZE, PLAYER_TOKEN_SIZE);
                }
            }//endfor
        }

    }
}
