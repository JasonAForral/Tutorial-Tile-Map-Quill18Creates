using UnityEngine;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace TileData
{
    public class Map : ScriptableObject
    {
        //public static Map instance = null;
        protected Tile [] mapTiles;

        int mapHeight;
        int mapWidth;
        private Transform mapHolder;
        private List <Vector3> gridPositions = new List<Vector3>();

        
        public Map (int width = 10, int height = 10)
        {
            mapWidth = width;
            mapHeight = height;

            mapTiles = new Tile[mapWidth * mapHeight];
        }

        void InitializeList ()
        {
            gridPositions.Clear();
            for (int x = 0; x < mapWidth; x ++)
            {
                for (int y = 0; y < mapHeight; y++)
                {
                    gridPositions.Add(new Vector3(x, 0f, y));
                }
            }
        }

        void MapSetup ()
        {
            mapHolder = new GameObject("Map").transform;

            //instance.transform.SetParent(mapHolder);
        }

        public Tile GetTile (int x, int y)
        {
            if (0 > x || x > mapWidth)
                return null;
            return mapTiles[x + y * mapWidth];
        }

    }   
}