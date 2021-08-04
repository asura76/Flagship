using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlagshipForm
{
    public partial class StartMenu : Form
    {
        public StartMenu()
        {
            InitializeComponent();
        }

        private void newGameButton_Click(object sender, EventArgs e)
        {
            flagshipGame.gameMode = FlagshipLib.GameMode.NewGame;
            Close();
            flagshipGame.Show();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            flagshipGame.Close();
            Close();
        }

        public FlagshipForm flagshipGame;
    }
}
