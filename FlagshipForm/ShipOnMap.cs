using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace FlagshipForm
{
    public partial class ShipOnMap : Form
    {
        public ShipOnMap()
        {
            InitializeComponent();

            MouseClick += ShipOnMap_MouseClick;

            MouseMove += ShipOnMap_MouseMove;

            Paint += ShipOnMap_Paint;

            ResizeRedraw = true;

            DoubleBuffered = true;
        }

        private void ShipOnMap_MouseMove(object sender, MouseEventArgs e)
        {
            Text = e.Location.ToString();

            Graphics graphics = CreateGraphics();

            //graphics.FillEllipse(Brushes.DodgerBlue, e.X, e.Y, 4, 4);

            int width = ClientRectangle.Width / mapForm.N_COLUMNS;
            int height = ClientRectangle.Height / mapForm.N_ROWS;

            Point coordinate = new Point(e.X / width, e.Y / height);
            Text = coordinate.ToString();

            Text += mapForm.currentPlayer.Name + ", click on board to place ship";
        }

        private Graphics getWorldGraphics(MouseEventArgs e, out Point mouseCoord)
        {
            Graphics graphics = CreateGraphics();
            SetWorldCoordinates(graphics);

            PointF[] pointsToConvert = new PointF[1] { new Point(e.X, e.Y) };
            graphics.TransformPoints(CoordinateSpace.World,
                CoordinateSpace.Device, pointsToConvert);
            mouseCoord = new Point((int)pointsToConvert[0].X,
                (int)pointsToConvert[0].Y);
            Text = mouseCoord.ToString();

            return graphics;
        }

        private void ShipOnMap_MouseClick(object sender, MouseEventArgs e)
        {
            Point mouseCoord;
            Graphics graphics = getWorldGraphics(e, out mouseCoord);

            int width = ClientRectangle.Width / mapForm.N_COLUMNS;
            int height = ClientRectangle.Height / mapForm.N_ROWS;

            mapForm.placingShip.Row = mouseCoord.Y;
            mapForm.placingShip.Column = mouseCoord.X;



            this.Close();
        }

        private void SetWorldCoordinates(Graphics graphics)
        {
            int width = ClientRectangle.Width;
            int height = ClientRectangle.Height;

            mapForm.N_ROWS = mapForm.theGame.theMap.NRows;
            mapForm.N_COLUMNS = mapForm.theGame.theMap.NColumns;

            //Draws the board
            //int boardWidth = Math.Min(ClientRectangle.Width, ClientRectangle.Height);

            int squareWidth = width / mapForm.N_COLUMNS;
            int squareHeight = height / mapForm.N_ROWS;

            //Change where 0,0 is defined in client area (under menu bar and in middle)
            //int totalMargin = width - boardWidth;
            //int leftMargin = totalMargin / 2;
            graphics.TranslateTransform(0, 0);

            //specifies number of pixels per world coordinate
            graphics.ScaleTransform(squareWidth, squareHeight);
        }

        private void DrawMap(Graphics graphics,
            Color ocean, Color land, Pen graphLine)
        {
            BackColor = ocean;

            //int boardWidth = Math.Min(ClientRectangle.Width, ClientRectangle.Height),
            //   squareWidth = N_ROWS / boardWidth;

            //N_ROWS = FlagshipMap.GetInstance().NRows;
            //N_COLUMNS = FlagshipMap.GetInstance().NColumns;

            // Draw N_ROWS - 1 lines for the grid rows
            for (int i = 0; i <= mapForm.N_ROWS; i++)
            {
                graphics.DrawLine(graphLine, new Point(0, i),
                    new Point(mapForm.N_COLUMNS, i));
            }

            // Draw N_COLUMN - 1 lines for grid columns
            for (int i = 0; i <= mapForm.N_COLUMNS; i++)
            {
                graphics.DrawLine(graphLine, new Point(i, 0),
                    new Point(i, mapForm.N_ROWS));
            }
        }

        private void ShipOnMap_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;

            SetWorldCoordinates(e.Graphics);

            DrawMap(e.Graphics, Color.LightBlue, 
                Color.ForestGreen, new Pen(Color.Black, 0.0f));
        }

        public FlagshipForm mapForm;
    }
}
