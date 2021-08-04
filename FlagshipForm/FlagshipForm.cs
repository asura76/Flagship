using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FlagshipLib;
using System.Drawing.Drawing2D;

namespace FlagshipForm
{
    public class FlagshipForm : Form
    {
        public FlagshipForm() : base()
        {
            this.Text = "Flagship";
            const int DEFAULT_HEIGHT = 600;
            Height = DEFAULT_HEIGHT;

            ResizeRedraw = true;

            Controls.Add(menu_);

            const double GOLDEN_RATIO = 1.618;
            Width = (int)(Height * GOLDEN_RATIO);
            N_ROWS = FlagshipMap.GetInstance().NRows;
            N_COLUMNS = FlagshipMap.GetInstance().NColumns;

            theGame = new Game();
            gameMode = GameMode.Neutral;

            ToolStripMenuItem fileMenu = new ToolStripMenuItem();
            fileMenu.Text = "&File";
            //fileMenu.Click += FileMenu_Click;
            menu_.Items.Add(fileMenu);

            ToolStripMenuItem placeShipMenu = new ToolStripMenuItem();
            menu_.Items.Add(placeShipMenu);
            placeShipMenu.Text = "P&laceship";
            //placeShipMenu.Click += PlaceShip_Click;

            ToolStripMenuItem player = new ToolStripMenuItem();
            menu_.Items.Add(player);
            player.Text = "&Player";
            player.Click += Player_Click;

            KeyDown += FlagshipForm_KeyDown;

            ToolStripMenuItem battleship = new ToolStripMenuItem();
            battleship.Text = "Battleship";
            ToolStripMenuItem cruiser = new ToolStripMenuItem();
            cruiser.Text = "Cruiser";
            ToolStripMenuItem aircraftCarrier = new ToolStripMenuItem();
            aircraftCarrier.Text = "Aircraft Carrier";

            placeShipMenu.DropDownItems.Add(battleship);
            placeShipMenu.DropDownItems.Add(cruiser);
            placeShipMenu.DropDownItems.Add(aircraftCarrier);

            //creates click options for each ship
            battleship.Click += Battleship_Click;
            cruiser.Click += Cruiser_Click;
            aircraftCarrier.Click += AircraftCarrier_Click;

            Label UserInfo = new Label();
            //UserInfo.Text = "Place a ship on the map";

            //Button placeShipButton = new Button();
            //placeShipButton.Text = "Place ship";
            //placeShipButton.
           // += Battleship_Click;
            //this.Controls.Add(placeShipButton);

            //Button AttackButton = new Button();
            //AttackButton.Text = "Attack";
            //AttackButton.Click += AttackButton_Click;
            //this.Controls.Add(AttackButton);

            //For loading specific ship info
            ToolStripMenuItem openMenu = new ToolStripMenuItem();
            fileMenu.DropDownItems.Add(openMenu);
            openMenu.Text = "&Open";
            openMenu.Click += FileMenu_Click;

            ToolStripMenuItem newGame = new ToolStripMenuItem();
            newGame.Text = "&New Game";
            newGame.Click += NewGame_Click;
            fileMenu.DropDownItems.Add(newGame);

            ToolStripMenuItem endGame = new ToolStripMenuItem();
            endGame.Text = "End Game";
            endGame.Click += EndGame_Click;
            fileMenu.DropDownItems.Add(endGame);

            ResizeRedraw = true;
            DoubleBuffered = true;


            shipMove.Tick += ShipMove_Tick;
            shipMove.Interval = 1000 / 30;
            //shipMove.Start();

            MouseClick += FlagshipForm_MouseClick;
            MouseMove += UserInfo_MouseMove;
            Paint += FlagshipForm_Paint;

            //currentPlayer = theGame.Players[playerTurn];
            //ShipsOnMap.Add(new BattleShip(10, 10, Direction.CompassPoint.East));
        }

        private void EndGame_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Player_Click(object sender, EventArgs e)
        {
            gameMode = GameMode.Move;
            Invalidate();
        }

        private void NewGame_Click(object sender, EventArgs e)
        {
            gameMode = GameMode.NewGame;
            SetNewGame();
            Invalidate();
        }

        private void SetNewGame()
        {
            //specify number of rows, columns, players, and ships
            NewGameInfo newGameInfo = new NewGameInfo();
            DialogResult infoResult = newGameInfo.ShowDialog();

            if (infoResult == DialogResult.OK &&
                (int)newGameInfo.RowNum.Value >= 10 &&
                (int)newGameInfo.ColumnNum.Value >= 10)
            {
                //set the game's rows, columns, nPlayers
                FlagshipMap.setDimensions((int)newGameInfo.RowNum.Value,
                   (int)newGameInfo.ColumnNum.Value);

                //recalls the game's map to get instance of changed map
                theGame.theMap = FlagshipMap.GetInstance();
                theGame.theMap = FlagshipMap.GetInstance();

                //sets the game's dimension to 
                //user specified dimensions
                theGame = new Game(
                    (int)newGameInfo.RowNum.Value,
                    (int)newGameInfo.ColumnNum.Value);

                theGame.Players.Add(new Player(theGame));
                theGame.Players.Add(new Player(theGame));
                theGame.Players[0].NavyColor = Color.ForestGreen;
                theGame.Players[1].NavyColor = Color.Black;
                currentPlayer = theGame.Players[0];

                int playerCounter = 1;
                // Launch the NewGameDialog
                foreach (IPlayer player in theGame.Players)
                {
                    player.ShipsOnMap = new List<IShip>();
                    
                    //add ships to navy
                    //10/27: for now, just add battleships
                    for (int i = 0; i < (int)newGameInfo.ShipNumber.Value; i++)
                    {
                        player.Navy.Add(new BattleShip());
                    }

                    string defaultPlayerName = "Player" + playerCounter.ToString();
                    ++playerCounter;
                    NewGameDialog newGame = new NewGameDialog(defaultPlayerName);
                    DialogResult result = newGame.ShowDialog();

                    if (result == DialogResult.OK)
                    {
                        player.Name = newGame.NameTextbox.Text;
                        // player.NavyId = player
                       

                        //change to placeship for each ship in the player's navy
                        newGame.Close();
                    }

                    List<IShip> newNavy = new List<IShip>();

                    foreach (IShip ship in player.Navy)
                    {
                        placeShipDialog = new PlaceShipForm();
                        placeShipDialog.mapForm = this;
                        placingShip = ship;
                        //sets the ship type and name
                        placeShipDialog.ShowDialog();
                        newNavy.Add(placingShip);

                        ShipOnMap shipOnMap = new ShipOnMap();
                        shipOnMap.mapForm = this;
                        //sets the map information for
                        //the ship
                        currentPlayer = player;
                        shipOnMap.ShowDialog();

                        ship.Row = placingShip.Row;
                        ship.Column = placingShip.Column;
                        ship.Name = placingShip.Name;

                        ship.NavyId = player.NavyId;
                        player.ShipsOnMap.Add(ship);

                        //ShipsOnMap.Add(ship);
                    }
                    //creates navy with list of ships
                    //defined by placeshipform
                    player.Navy = newNavy;
                }

                currentPlayer = theGame.Players[0];
                playerTurn = 0;
            }
            else
            {
                if (newGameInfo.RowNum.Value < 10)
                {
                    MessageBox.Show("Too few rows");
                }
                else if (newGameInfo.ColumnNum.Value < 10)
                {
                    MessageBox.Show("Too few columns");
                }
            }

            gameMode = GameMode.Move;
        }

        private void FlagshipForm_KeyDown(object sender, KeyEventArgs e)
        {
            const int ROTATE_COMMAND = (int)'R';
            //const int ESC_COMMAND = 27;
            const int ENTER_COMMAND = 13;
            switch (gameMode)
            {
                
                case GameMode.Move:

                    if (shipSelectedIndex < currentPlayer.ShipsOnMap.Count)
                    {
                        if (e.KeyValue == ROTATE_COMMAND)
                        {
                            switch (shipSelected.Direction.Current)
                            {
                                case Direction.CompassPoint.East:
                                    shipSelected.Direction.Current =
                                        Direction.CompassPoint.South;
                                    break;

                                case Direction.CompassPoint.South:
                                    shipSelected.Direction.Current =
                                        Direction.CompassPoint.West;
                                    break;

                                case Direction.CompassPoint.West:
                                    shipSelected.Direction.Current =
                                        Direction.CompassPoint.North;
                                    break;

                                case Direction.CompassPoint.North:
                                    shipSelected.Direction.Current =
                                        Direction.CompassPoint.East;
                                    break;
                            }
                        }
                        //if (e.KeyValue == ESC_COMMAND)
                        //{
                        //    gameMode = GameMode.Neutral;
                        //    // Add any placed ship to user's navy.
                        //}
                        if (e.KeyValue == ENTER_COMMAND)
                        {
                            //sets the new data after the timesteps current player's data
                            int count = 0;
                            while (count < theGame.Players.Count &&
                                theGame.Players[count].Name != currentPlayer.Name)
                            {
                                if (theGame.Players[count].Name != currentPlayer.Name)
                                {
                                    ++count;
                                }
                            }

                            if (theGame.Players[count] != null)
                            {
                                theGame.Players[count] = currentPlayer;
                            }

                            count = 0;

                            ++shipSelectedIndex;
                        }
                    }
                    if(shipSelectedIndex >= currentPlayer.ShipsOnMap.Count)
                    {
                        shipSelectedIndex = 0;

                        gameMode = GameMode.Attack;
                    }

                    break;
            }

            Invalidate();
        }

        private void AircraftCarrier_Click(object sender, EventArgs e)
        {
            PlaceShipForm dialog = new PlaceShipForm();
            DialogResult result = dialog.ShowDialog();
            placingShip = new AircraftCarrier();
            placeShipDialog.ShowDialog();
            Text = "Click on map to place ship";
            gameMode = GameMode.PlaceShip;
        }

        private void Cruiser_Click(object sender, EventArgs e)
        {
            placingShip = new Cruiser();

            placeShipDialog.ShowDialog();

            Text = "Click on map to place ship";
            gameMode = GameMode.PlaceShip;
        }

        private void Battleship_Click(object sender, EventArgs e)
        {
            placingShip = new BattleShip();
            placeShipDialog.ShowDialog();
            // AttackPrompt.Controls.Add(batButton);
            Text = "Click on map to place ship";
            gameMode = GameMode.PlaceShip;
        }

        private void Draw_Ship(Graphics graphics, IShip theShip)
        {
            // Graphics graphics = CreateGraphics();
            //Multiple different ships determined by players' navy

            //SetWorldCoordinates(graphics);

            //For now, test with one ship
            //IShip ship = new AircraftCarrier();
            if (theShip.Image != null)
            {
                theShip.Image.MakeTransparent(theShip.Image.GetPixel(0, 0));
                graphics.DrawImage(theShip.Image, theShip.shipsquares_[0].Column,
                    theShip.shipsquares_[0].Row, theShip.LengthInSquares, 1);
            }
            else
            {
                PlaceShipOnBoard(graphics, theShip);
            }

            //theGame.CurrentPlayer.Navy.Add(theShip);
        }

        private void PlaceShipOnBoard(Graphics graphics, IShip ship)
        {
            // Graphics graphics = CreateGraphics();
            
            foreach (ShipSquare square in ship.shipsquares_)
            {
                //get row and column for filling rectangle
                //from ShipSquare row and column member variables
                //which accomodate the direction of the ship
                // SetWorldCoordinates(graphics);
                if (square.Row >= N_ROWS) { square.Row -= N_ROWS; }
                if (square.Column >= N_COLUMNS) { square.Column -= N_COLUMNS; }
                if (square.Row < 0) { square.Row += N_ROWS; }
                if (square.Column < 0) { square.Column += N_COLUMNS; }


                SolidBrush shipBrush = new SolidBrush(currentPlayer.NavyColor);
                
                //shows square as red if hit
                if (square.Hit != true)
                {
                    graphics.FillRectangle(shipBrush, square.Column,
                        square.Row, 1, 1);
                }
                else
                {
                    graphics.FillRectangle(Brushes.Crimson, square.Column,
                        square.Row, 1, 1);
                }

            }

            //Invalidate();
        }



        //To be called after ship goes through timesteps and updated on board
        private void moveTo(IShip shipToMove, PointF destination)
        {
            //From ship's current drawing,
            //move from said position to the new
            prevShipPositions = currentShipPositions;
            currentShipPositions = new List<Tuple<IShip, Point>>();
            foreach(IShip ship in currentPlayer.ShipsOnMap)
            {
                currentShipPositions.Add(new Tuple<IShip, Point>
                    (ship, new Point(ship.Column, ship.Row)));
            }
            //call tick for animation
        }

        private void ShipMove_Tick(object sender, EventArgs e)
        {
            Graphics graphics = CreateGraphics();
            if (gameMode == GameMode.Move)
            {
                foreach (IShip ship in currentPlayer.ShipsOnMap)
                {
                    ship.PreTimeStep();
                    moveTo(ship, new Point(ship.Column, ship.Row));

                    //SetWorldCoordinates(graphics);
                    //Draw_Ship(ship);
                }
            }

            Invalidate();
        }

        private void FlagshipForm_MouseClick(object sender, MouseEventArgs e)
        {
            Point mouseCoord;
            Graphics graphics = getWorldGraphics(e, out mouseCoord);
            SetWorldCoordinates(graphics);

            if (mouseCoord.X == N_COLUMNS)
            {
                mouseCoord.X = N_COLUMNS - 1;
            }

            if (mouseCoord.Y == N_ROWS)
            {
                mouseCoord.Y = N_ROWS - 1;
            }

            switch (gameMode)
            {
                case GameMode.PlaceShip:
                    if (currentPlayer.ShipsOnMap.Count != 0)
                    {
                        placingShip.Row = mouseCoord.Y;
                        placingShip.Column = mouseCoord.X;
                        currentPlayer.ShipsOnMap.Add(placingShip);
                        Invalidate();
                    }

                    break;

                case GameMode.NewGame:
                    //SetNewGame();

                    /*foreach (IPlayer player in theGame.Players)
                    {
                        currentPlayer = player;
                        currentPlayer.ShipsOnMap = new List<IShip>();

                        foreach (IShip ship in player.Navy)
                        {
                            placingShip = ship;
                            //11/13
                            //go through placeship process
                            placeShipDialog.ShowDialog();
                            ShipOnMap shipOnMap = new ShipOnMap();
                            shipOnMap.mapForm = this;
                            shipOnMap.ShowDialog();
                            //placeshipDialog to make ship into whatever
                            //type of ship

                            ship.Row = placingShip.Row;
                            ship.Column = placingShip.Column;
                            ship.NavyId = player.NavyId;
                            currentPlayer.ShipsOnMap.Add(ship);

                            //ShipsOnMap.Add(ship);
                        }
                    }*/

                    //gameMode = GameMode.Attack;

                    Invalidate();

                    break;


                case GameMode.Attack:

                    int nShipsAttacked = 0;

                    foreach(IPlayer player in theGame.Players)
                    {
                        if(player != currentPlayer)
                        {
                            foreach (IShip ship in player.Navy)
                            {
                                if (ship.IsShipDestroyed() != true)
                                {
                                    foreach (ShipSquare square in ship.shipsquares_)
                                    {
                                        if (square.Row == mouseCoord.Y &&
                                            square.Column == mouseCoord.X &&
                                            square.Hit != true)
                                        {
                                            //ship[mouseCoord.Y, mouseCoord.X].Hit = true;

                                            ship.squaresPendingDestruction.Add(ship[mouseCoord.Y, mouseCoord.X]);
                                            ship[mouseCoord.Y, mouseCoord.X].Hit = true;

                                            currentPlayer.HitsOnMap.Add(new Tuple<Point, int>
                                                (new Point(mouseCoord.X, mouseCoord.Y), 0));

                                            //adds message of ship that was hit to the
                                            //messages for the attacking player
                                            string message = "You hit " + player.Name + " at (" +
                                                mouseCoord.X + " , " + mouseCoord.Y + ") !";

                                            currentPlayer.MessageString.Add(message);

                                            //adds message of ship that was hit to the 
                                            //messages for the attacked player
                                            message = ship.Name + " was hit by "
                                                + currentPlayer.Name + " at "
                                                + mouseCoord.X + " , " + mouseCoord.Y + ") !";

                                            player.MessageString.Add(message);

                                            ++nShipsAttacked;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    
                    if(nShipsAttacked == 0)
                    {
                        currentPlayer.MissesOnMap.Add(new Tuple<Point, int>
                                        (new Point(mouseCoord.X, mouseCoord.Y), 0));
                    }

                    //if (playerTurn == theGame.Players.Count - 1)
                    //{
                    //    gameMode = GameMode.Move;
                    //}

                    //12/3
                    //Need to set up messageBox for next player
                    //after currentPlayer's turn is up
                    updatePlayerPoints();

                    FormTimeStep();

                    MessageBox.Show(currentPlayer.Name + ", look away!");

                    //++playerMessageCounter;

                    playerTurn = (playerTurn == 0) ?
                        1 : 0;

                    gameMode = GameMode.Move;

                    Invalidate();
                    break;
            }
        }

        private void updatePlayerPoints()
        {
            List<Tuple<Point, int>> newList = new List<Tuple<Point, int>>();

            foreach (Tuple<Point, int> point in currentPlayer.HitsOnMap)
            {
                int nTurns = point.Item2 + 1;
                Tuple<Point, int> updatedPoint = new Tuple<Point, int>(point.Item1, nTurns);
                newList.Add(updatedPoint);
            }

            currentPlayer.HitsOnMap = newList;

            newList = new List<Tuple<Point, int>>();

            foreach (Tuple<Point, int> point in currentPlayer.MissesOnMap)
            {
                int nTurns = point.Item2 + 1;
                Tuple<Point, int> updatedPoint = new Tuple<Point, int>(point.Item1, nTurns);
                newList.Add(updatedPoint);
            }

            currentPlayer.MissesOnMap = newList;

        }

        private void FormTimeStep()
        {
            foreach (IShip ship in currentPlayer.Navy)
            {
                if (ship.IsShipDestroyed() == false)
                {
                    //if (ship.squaresPendingDestruction.Count > 0)
                    //{
                    //    for (int i = 0; i < ship.squaresPendingDestruction.Count; i++)
                    //    {
                    //        string message = "Ship: " + ship.Name + " was hit at \n" +
                    //            "(" + ship.squaresPendingDestruction[i].Row + "," +
                    //            ship.squaresPendingDestruction[i].Column + ")";

                    //        currentPlayer.MessageString.Add(message);

                    //        ship.squaresPendingDestruction = new List<ShipSquare>();
                    //    }
                    //}

                    ship.PreTimeStep();
                    //ship.TimeStep();
                }
                //else
                //{
                //    string message = "Ship: " + ship.Name + " was destroyed at \n (" +
                //        ship.Row + ship.Column + ")";

                //    currentPlayer.MessageString.Add(message);
                //}
            }
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



        private void FileMenu_Click(object sender, EventArgs e)
        {

        }

        private void UserInfo_MouseMove(object sender, MouseEventArgs e)
        {
            if (gameMode != GameMode.NewGame)
            {
                //Text = e.Location.ToString();

                Graphics graphics = CreateGraphics();

                //graphics.FillEllipse(Brushes.DodgerBlue, e.X, e.Y, 4, 4);

                int width = ClientRectangle.Width / N_COLUMNS;
                int height = ClientRectangle.Height / N_ROWS;

                Point mouseCoord = new Point(e.X / width, e.Y / height);

                if (mouseCoord.X == N_COLUMNS)
                {
                    mouseCoord.X = N_COLUMNS - 1;
                }

                if (mouseCoord.Y == N_ROWS)
                {
                    mouseCoord.Y = N_ROWS - 1;
                }

                switch (gameMode)
                {
                    case GameMode.Attack:
                        Text = currentPlayer.Name + "'s turn to attack. Click where you want to attack";
                        break;

                    case GameMode.Move:

                        if (shipSelectedIndex < currentPlayer.Navy.Count)
                        {
                            shipSelected = currentPlayer.Navy[shipSelectedIndex];
                        }

                        Text = currentPlayer.Name + 
                            "'s turn. " + shipSelected.Name + " headed " 
                            + shipSelected.Direction.Current.ToString() 
                            + ". Press R to rotate ship at (" 
                            + shipSelected.Row + ", " + shipSelected.Column + ") ENTER to confirm ";
                        break;
                }

                Text += mouseCoord.ToString();

            }
        }

        private void AttackButton_Click(object sender, EventArgs e)
        {

        }

        private void FirstForm_Resize(object sender, PaintEventArgs e)
        {
            Invalidate();
        }

        private void SetWorldCoordinates(Graphics graphics)
        {
            int width = ClientRectangle.Width;
            int height = ClientRectangle.Height - menu_.Height;

            N_ROWS = theGame.theMap.NRows;
            N_COLUMNS = theGame.theMap.NColumns;

            //Draws the board
            //int boardWidth = Math.Min(ClientRectangle.Width, ClientRectangle.Height);

            squareWidth = width / N_COLUMNS;
            squareHeight = height / N_ROWS;

            //Change where 0,0 is defined in client area (under menu bar and in middle)
            //int totalMargin = width - boardWidth;
            //int leftMargin = totalMargin / 2;
            graphics.TranslateTransform(0, menu_.Height);

            //specifies number of pixels per world coordinate
            graphics.ScaleTransform(squareWidth, squareHeight);
        }


        private void DrawFlagShipMap(Graphics graphics,
            Color ocean, Color land, Pen graphLine)
        {
            BackColor = ocean;

            //int boardWidth = Math.Min(ClientRectangle.Width, ClientRectangle.Height),
            //   squareWidth = N_ROWS / boardWidth;

            //N_ROWS = FlagshipMap.GetInstance().NRows;
            //N_COLUMNS = FlagshipMap.GetInstance().NColumns;

            // Draw N_ROWS - 1 lines for the grid rows
            for (int i = 0; i < N_ROWS; i++)
            {
                graphics.DrawLine(graphLine, new Point(0, i),
                    new Point(N_COLUMNS, i));
            }

            graphics.DrawLine(graphLine, new Point(0, N_ROWS),
                    new Point(N_COLUMNS, N_ROWS));

            // Draw N_COLUMN - 1 lines for grid columns
            for (int i = 0; i < N_COLUMNS; i++)
            {
                graphics.DrawLine(graphLine, new Point(i, 0),
                    new Point(i, N_ROWS));
            }

            graphics.DrawLine(graphLine, new Point(N_COLUMNS, 0),
                    new Point(N_COLUMNS, N_ROWS));


            ////thin lines to indicate each part of graph
            //for(int row = 0; row < N_ROWS; row++)
            //{
            //    for(int column = 0; column < N_COLUMNS; column++)
            //    {
            //        graphics.DrawRectangle(graphLine, row, column, squareWidth, squareWidth);
            //    }
            //}
        }

        //Function for drawing ships:
        //  input image?
        //  Ship for different color per player
        //private void DrawShips(Graphics graphics, Brush playerOneColor, Brush playerTwoColor
        //Brush playerThreeColor, Brush playerFourColor)
        //{
        //}


        Point toDevice(Graphics graphics, Point worldPoint)
        {
            Point[] pointArray = new Point[1] { worldPoint };
            graphics.TransformPoints(CoordinateSpace.Device, CoordinateSpace.World,
                pointArray);

            return pointArray[0];
        }

        private void FlagshipForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;

            if (theGame.Players.Count <= 1 ||
                    N_ROWS < 10 ||
                    N_COLUMNS < 10)
            {
                startMenu = new StartMenu();
                startMenu.flagshipGame = this;
                startMenu.ShowDialog();
                if (gameMode == GameMode.NewGame)
                {
                    SetNewGame();
                }
            }

            if (theGame.GameOver() != true)
            {
                currentPlayer = theGame.Players[playerTurn];

                SetWorldCoordinates(graphics);

                //10/31 for now, sets game mode back to 
                //neutral (after invalidate())
                //gameMode = GameMode.Neutral;

                Pen gridPen = new Pen(Color.Black, 0.0f);

                DrawFlagShipMap(graphics, Color.LightBlue,
                    Color.ForestGreen, gridPen);

                Font font = new Font(FontFamily.GenericSansSerif, 12);

                foreach (Tuple<Point, int> point in currentPlayer.MissesOnMap)
                {

                    Point devicePoint = toDevice(graphics, point.Item1);
                    graphics.FillRectangle(Brushes.WhiteSmoke, point.Item1.X, point.Item1.Y,
                        1, 1);

                    graphics.ResetTransform();
                    graphics.DrawString(point.Item2.ToString(), font,
                        Brushes.Black, devicePoint);
                    SetWorldCoordinates(graphics);
                }

                foreach (Tuple<Point, int> point in currentPlayer.HitsOnMap)
                {
                    graphics.FillRectangle(Brushes.Red, point.Item1.X, point.Item1.Y,
                        1, 1);

                    Point devicePoint = toDevice(graphics, point.Item1);
                    graphics.ResetTransform();

                    graphics.DrawString(point.Item2.ToString(), font,
                        Brushes.Black, devicePoint);
                    SetWorldCoordinates(graphics);
                }

                foreach (IShip ship in currentPlayer.Navy)
                {
                    Draw_Ship(graphics, ship);
                }

                if(gameMode == GameMode.Move)
                {
                    if (currentPlayer.MessageString.Count > 0)
                    {
                        //shows after results of last player's turn
                        foreach (string message in currentPlayer.MessageString)
                        {
                            MessageBox.Show(message);
                        }

                        currentPlayer.MessageString = new List<string>();
                    }
                }
            }
            else
            {
                if (theGame.Winners.Count != 0)
                {
                    MessageBox.Show(theGame.Winners[0].Name + " Wins!");
                    //Back to start menu
                    this.Close();
                    startMenu.ShowDialog();
                }
            }
        }

        private void FirstForm_MouseMove(object sender, MouseEventArgs e)
        {
            Text = e.Location.ToString();

            Graphics graphics = CreateGraphics();

            //graphics.FillEllipse(Brushes.DodgerBlue, e.X, e.Y, 4, 4);

            int nRows = N_ROWS;
            int width = Math.Min(ClientRectangle.Width, ClientRectangle.Height);
            width = width / nRows;

            Point coordinate = new Point(e.X / width, e.Y / width);
            Text = coordinate.ToString();

        }

        public int N_ROWS, N_COLUMNS;
        int squareWidth, squareHeight;
        
        public IGameModel theModel_;
        //private Image ShipModel = new Image("")
        private MenuStrip menu_ = new MenuStrip();
        //List<Point> HitsOnMap = new List<Point>();

        Timer shipMove = new Timer();
        public GameMode gameMode = GameMode.Move;
        PlaceShipForm placeShipDialog;
        ShipOnMap placeTheShip;
        StartMenu startMenu;
        public IShip placingShip;
        int shipSelectedIndex = 0;
        public IShip shipSelected;
        //List<Button> ShipButtons;
        AttackForm AttackDialog;
        List<Tuple<IShip, Point>> currentShipPositions;
        List<Tuple<IShip,Point>> prevShipPositions;
        public IPlayer currentPlayer;
        int playerTurn = 0;
        int playerMessageCounter = 0;
        public IGame theGame;
        Timer timer = new Timer();

    }
}

