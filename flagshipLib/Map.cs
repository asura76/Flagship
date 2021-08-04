using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlagshipLib
{
    public class Map
    {
        public Map(int nRows, int nColumns)
        {
            NRows = nRows;
            NColumns = nColumns;

            // Create the map
            theMap = new List<IMapObject>[nRows, nColumns];

            //shipMap = new IMapObject[nRows, nColumns];

            // Set all the map entries to water
            for (int row = 0; row < nRows; ++row)
            {
                for (int column = 0; column < nColumns; ++column)
                {
                    theMap[row, column] = new List<IMapObject>();
                    theMap[row, column].Add(new Water(row, column));
                }
            }


            //Set one ship to new map
           // shipMap[1, 2] = new Ship(1, 2);

        }

        private void resolveCollisions(List<IMapObject> objects,
            int row, int column)
        {
            for(int i=0;i<objects.Count;i++)
            {
                for(int j=i+1;j<objects.Count;j++)
                {
                    objects[i].CollideWith(objects[j], row, column);
                    objects[j].CollideWith(objects[i], row, column);
                }
            }
        }

        public void resolveCollisions()
        {
            for(int row=0;row<NRows;++row)
            {
                for(int column=0;column<NColumns;++column)
                {
                    if (theMap[row, column].Count > 1)
                    { 
                      resolveCollisions(theMap[row, column], row, column);
                    }
                }
            }
        }



        //draws theMap and shipMap together
        //each map does not effect status of the other,
        //but effects the appearance of drawn map
        //public void drawMap()
        //{
        //    for (int row = 0; row < NRows; row++)
        //    {

        //        for (int column = 0; column < NColumns; column++)
        //        {
                    
        //        }
        //    }
        //}


        public List<IMapObject> this[int row, int column]
        {
            get { return theMap[row, column]; }
            set { theMap[row, column] = value; }
        }

    public int NRows { get; private set; }
    public int NColumns { get; private set; }
    public List<IMapObject>[,] theMap;
    //public IMapObject[,] shipMap;
    }
}
