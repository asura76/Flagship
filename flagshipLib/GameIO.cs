using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;

namespace FlagshipLib
{
    public class GameIO : IGameIO
    {


        public bool LoadPlayerInfo(XmlTextWriter xMLWrite, 
            IPlayer navy, string fileName, string navyID)
        {
            int Counter = 1;
            bool FoundPlayer = false;
            IShip NewShip = new Ship();
            XmlTextReader reader = new XmlTextReader(fileName);

            while (reader.Read() &&
                FoundPlayer == false)
            {
                //if(reader.Name.ToString() == "ID: ")
                //{
                //    if (reader.ReadInnerXml() == navyID)
                //    {
                //        foundPlayer = true;
                //        reader.MoveToNextAttribute();
                //        reader.MoveToNextAttribute();

                //    }
                //}

                // if found selected id is correct, 
                // take each ship listed in attribute
                reader.MoveToAttribute("ID" + Counter + ": ");
                if(reader.ReadInnerXml() == navyID)
                {
                    FoundPlayer = true;
                    reader.MoveToNextAttribute();
                    while (reader.Name.ToString() != "Battleship" + ": " ||
                    reader.Name.ToString() != "Cruiser" + ": " ||
                    reader.Name.ToString() != "Battleship" + ": ")
                    {
                        //add new ship to player's navy
                        StringShipFactory(NewShip, reader);
                        navy.PlaceShip(NewShip);
                        reader.MoveToNextAttribute();
                    }
                }
            }
            return FoundPlayer;
        }

        public void Load(string fileName, IGame game)
        {
            int PlayerCount = 1;
            XmlTextReader reader = new XmlTextReader(fileName);

            IPlayer newPlayer;
            IShip newShip = new Ship();

            while (reader.Read())
            {
                //needs to read a player name/navyID
                // 'mark' for when a player is listed
                newPlayer = new Player(game);

                if (reader.Name.ToString() == "Player" + PlayerCount + ": ")
                {
                    newPlayer.Name = reader.ReadInnerXml();
                }

                //each ships' class and name
                if (reader.Name.ToString() == "Battleship" + ": " ||
                    reader.Name.ToString() == "Cruiser" + ": " ||
                    reader.Name.ToString() == "Battleship" + ": ")
                {
                    StringShipFactory(newShip, reader);
                }

            }
        }

        

        //writes the player's info to xml
        ///(doesn't make new player, just loads navy into a player's navy)
        public void writePlayerInfo(XmlTextWriter xMLWrite, IPlayer thePlayer)
        {
            int counter = 1;
            while (thePlayer != null && thePlayer.Navy != null)
            {
                xMLWrite.WriteElementString("Player" + counter + ": ",
                    thePlayer.Name);
                xMLWrite.WriteElementString("ID" + counter + ": ", thePlayer.NavyId.ToString());

                foreach (IShip ship in thePlayer.Navy)
                {
                    xMLWrite.WriteElementString(ship.Class + ": ", ship.Name);

                    ++counter;
                }
            }

        }

        public void Save(string fileName, IGame game)
        {
            string directory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            using (FileStream fileStream = new FileStream(directory + "\\" + fileName, FileMode.Create))
            using (StreamWriter sw = new StreamWriter(fileStream))
            using (XmlTextWriter xmlWriter = new XmlTextWriter(sw))
            {

                XmlWriterSettings settings = new XmlWriterSettings();
                xmlWriter.Formatting = Formatting.Indented;

                foreach (IPlayer player in game.Players)
                {
                    xmlWriter.Indentation = 4;
                    writePlayerInfo(xmlWriter, player);
                }

                xmlWriter.Close();
            }

        }

        public void StringShipFactory(IShip shipToPlace, XmlReader read)
        {
            IShip newShip = new Ship();
            switch (read.Name.ToString())
            {
                case "BattleShip":
                    newShip = new BattleShip();
                    break;
                case "Cruiser":
                    newShip = new Cruiser();
                    break;
                case "AircraftCarrier":
                    newShip = new AircraftCarrier();
                    break;
            }

            newShip.Name = read.ReadInnerXml();
            shipToPlace = newShip;
        }

        


    }
}
