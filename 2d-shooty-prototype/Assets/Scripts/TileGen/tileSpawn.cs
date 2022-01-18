using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;

public class tileSpawn : MonoBehaviour
{
    [SerializeField] private int tilemapSize, clusterSize, clusterAmount, clusterRandomness, minClusterSize;
    [SerializeField] private Tilemap groundMap, wallMap;
    [SerializeField] private Tile groundTile, wallTile;

    public void Start()
    {
        genTilemap();
        genBorder();
        for (int a = 0; a < clusterAmount; a++)
        {
            genCluster();
        }

        GameObject.Find("PLAYER").transform.position = new Vector3(tilemapSize / 2, tilemapSize / 2, -5); //Place player in the centre of map

        int posx = Mathf.RoundToInt(GameObject.Find("PLAYER").transform.position.x);
        int posy = Mathf.RoundToInt(GameObject.Find("PLAYER").transform.position.y);
        for (int y = posy - 20; y < posy + 20; y++) //NTS: scale tile deletion with mapsizGamee later
        {
            for (int x = posx - 20; x < posx + 20; x++)
            {
                wallMap.SetTile(new Vector3Int(x, y, 1), null);
            }
        }
    }

    public void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.A))
        {
            wallMap.ClearAllTiles();
            genBorder();
            for (int a = 0; a < clusterAmount; a++)
            {
                genCluster();
            }
        }*/
    }
    public void genTilemap() //simple algorithm to generate the ground
    {
        for (int x = 0; x < tilemapSize; x++) //spawns tiles along each x and y position within the size of map
        {
            for (int y = 0; y < tilemapSize; y++)
            {
                groundMap.SetTile(new Vector3Int(x, y, -15), groundTile);
            }
        }
        //GameObject.Find("Main Camera").transform.position = new Vector3(tilemapSize / 2, tilemapSize / 2, -20); //centre the camera

    }

    public void genBorder()
    {
        int Rand = Mathf.RoundToInt(Random.Range(1, 7));
        for (int i = 0; i < tilemapSize; i++) //Gen left border
        {
            Rand = Rand + Mathf.RoundToInt(Random.Range(-5, 5)/3); //random values divided by 3 to get some slightly smoother looking borders
            if (Rand < 5)
            {
                Rand = 5;
            }
            if (Rand > 10)
            {
                Rand = 10;
            }
            for (int o = 0; o < Rand; o++)
            {
                wallMap.SetTile(new Vector3Int(o, i, 1), wallTile);
            }
        }

        Rand = Mathf.RoundToInt(Random.Range(1, 7));
        for (int i = 0; i < tilemapSize; i++) //Gen right border
        {
            Rand = Rand + Mathf.RoundToInt(Random.Range(-5, 5) / 3); 
            if (Rand < 5)
            {
                Rand = 5;
            }
            if (Rand > 10)
            {
                Rand = 10;
            }
            for (int o = 0; o > -Rand; o--)
            {
                wallMap.SetTile(new Vector3Int(o + tilemapSize - 1, i, 1), wallTile);
            }
        }

        Rand = Mathf.RoundToInt(Random.Range(1, 7));
        for (int o = 0; o < tilemapSize; o++) //Gen top border
        {
            Rand = Rand + Mathf.RoundToInt(Random.Range(-5, 5) / 3);
            if (Rand < 5)
            {
                Rand = 5;
            }
            if (Rand > 10)
            {
                Rand = 10;
            }
            for (int i = 0; i < Rand; i++)
            {
                wallMap.SetTile(new Vector3Int(o, i, 1), wallTile);
            }
        }

        Rand = Mathf.RoundToInt(Random.Range(1, 7));
        for (int o = 0; o < tilemapSize; o++) //Gen bottom border
        {
            Rand = Rand + Mathf.RoundToInt(Random.Range(-5, 5) / 3);
            if (Rand < 5)
            {
                Rand = 5;
            }
            if (Rand > 10)
            {
                Rand = 10;
            }
            for (int i = 0; i > -Rand; i--)
            {
                wallMap.SetTile(new Vector3Int(o, i + tilemapSize - 1, 1), wallTile);
            }
        }


    }

    public void genCluster()
    {
        int initPosx = Mathf.RoundToInt(Random.Range(0, tilemapSize - 1));
        int initPosy = Mathf.RoundToInt(Random.Range(0, tilemapSize - 1));
        int maxW = Mathf.RoundToInt(Random.Range(minClusterSize, clusterSize));
        int maxH = Mathf.RoundToInt(Random.Range(minClusterSize, clusterSize));
        int initW = maxW;
        int initH = maxH;

        for (int i = 0; i < maxH; i++) //Generation for the 1st quartile
        {
            maxW = maxW + Mathf.RoundToInt(Random.Range(-clusterRandomness, clusterRandomness));
            if (i + initPosy < tilemapSize)
            {
                for (int o = 0; o < maxW; o++)
                {
                    if (o + initPosx < tilemapSize)
                    {
                        wallMap.SetTile(new Vector3Int(initPosx + o, initPosy + i, 1), wallTile);
                    }
                }
            }  
        }
        maxW = initW;
        maxH = initH;

        for (int i = 0; i < maxH; i++) //Generation for the 2nd quartile
        {
            maxW = maxW + Mathf.RoundToInt(Random.Range(-clusterRandomness, clusterRandomness));
            if (i + initPosy < tilemapSize)
            {
                for (int o = -1; o > -maxW - 1; o--)
                {
                    if (initPosx + o >= 0)
                    {
                        wallMap.SetTile(new Vector3Int(initPosx + o, initPosy + i, 1), wallTile);
                    }
                }
            }
        }
        maxW = initW;
        maxH = initH;

        for (int i = 0; i > -maxH; i--) //Generation for the 3rd quartile
        {
            maxW = maxW + Mathf.RoundToInt(Random.Range(-clusterRandomness, clusterRandomness));
            if (i + initPosy >= 0)
            {
                for (int o = -1; o > -maxW - 1; o--)
                {
                    if (initPosx + o >= 0)
                    {
                        wallMap.SetTile(new Vector3Int(initPosx + o, initPosy + i, 1), wallTile);
                    }
                }
            }
        }
        maxW = initW;
        maxH = initH;

        for (int i = 0; i > -maxH; i--) //Generation for the 4th quartile
        {
            maxW = maxW + Mathf.RoundToInt(Random.Range(-clusterRandomness, clusterRandomness));
            if (i + initPosy >= 0)
            {
                for (int o = 0; o < maxW; o++)
                {
                    if (o + initPosx < tilemapSize)
                    {
                        wallMap.SetTile(new Vector3Int(initPosx + o, initPosy + i, 1), wallTile);
                    }
                }
            }
        }
    }
}
