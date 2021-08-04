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

namespace FlagshipForm
{
    public partial class PlaceShipForm : Form
    {
        public PlaceShipForm()
        {
            InitializeComponent();
        }



        private void OKButton_Click(object sender, EventArgs e)
        {

            mapForm.placingShip = shipToCreate;

            int shipId = Ship.NextShipId();
            mapForm.placingShip.NavyId = shipId;

            string shipName = ShipNameTextbox.Text;
            mapForm.placingShip.Name = shipName;

            // string direction = ShipDirectionTextbox.Text;
            // mapForm.placingShip.changeDirByString(direction, 0);

            DialogResult = DialogResult.OK;
            this.Close();
        }


        public FlagshipForm mapForm;
        private IShip shipToCreate;

        private void BRadio_CheckedChanged(object sender, EventArgs e)
        {
            if(bRadio.Checked == true)
            {
                shipToCreate = new BattleShip();
            }
        }

        private void CRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (cRadio.Checked == true)
            {
                shipToCreate = new Cruiser();
            }
        }

        private void AcRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (acRadio.Checked == true)
            {
                shipToCreate = new AircraftCarrier();
            }
        }
    }
}
