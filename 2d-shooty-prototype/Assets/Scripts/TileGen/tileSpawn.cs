using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;
using UnityEngine.UI;

public class tileSpawn : MonoBehaviour
{
    [SerializeField] private int clusterAmount;
    [SerializeField] private Tilemap groundMap, wallMap;
    [SerializeField] private Tile groundTile, wallTile;
    [SerializeField] private Camera cam;
    [SerializeField] private Button refreshBtn;
    [SerializeField] private InputField inpMapSize, inpSeed;
    [SerializeField] private Slider wallDensityVal, wallSizeVal;
    private string seedInp;
    public void Awake()
    {
        inpMapSize.text = "100";
        int mapS = int.Parse(inpMapSize.text);
        refreshBtn.GetComponent<Button>().onClick.AddListener(onClick);
        cam.orthographicSize = 300;
        cam.transform.position = new Vector3(0 + mapS / 2, 0 + mapS / 2, -20);
        refresh();
    }
    public void mapSizeSubmit()
    {
        if (string.IsNullOrWhiteSpace(inpMapSize.text) || int.Parse(inpMapSize.text) < 100)
        {
            inpMapSize.text = "100";
        }
    }
    public void ReadStringInput(string s)
    {
        if (string.IsNullOrEmpty(s))
        {
            Random.InitState((int)System.DateTime.Now.Ticks);
            refresh();
        }
        else
        {
            seedInp = s;
            Random.InitState(int.Parse(s));
            refresh();
        }
    }
    void onClick()
    {
        if (string.IsNullOrWhiteSpace(inpMapSize.text) || int.Parse(inpMapSize.text) < 100)
        {
            inpMapSize.text = "100";
        }
    }
    public void UIrefresh()
    {
        if (string.IsNullOrWhiteSpace(inpSeed.text))
        {
            Random.InitState((int)System.DateTime.Now.Ticks);
        }
        else
        {
            Random.InitState(int.Parse(inpSeed.text));
        }
        refresh();
    }
    void refresh()
    {
        wallMap.ClearAllTiles();
        groundMap.ClearAllTiles();
        genTilemap();
        genBorder();
        clusterAmount = Mathf.RoundToInt(Mathf.Pow(int.Parse(inpMapSize.text), 2/3f) * wallDensityVal.value);
        for (int a = 0; a < clusterAmount; a++)
        {
            genCluster();
        }
    }
    public void genTilemap() //simple algorithm to generate the ground
    {
        
        int mapS = int.Parse(inpMapSize.text);
        for (int x = 0; x < mapS; x++) //spawns tiles along each x and y position within the size of map
        {
            for (int y = 0; y < mapS; y++)
            {
                groundMap.SetTile(new Vector3Int(x, y, -15), groundTile);
            }
        }
        GameObject.Find("Player Camera").transform.position = new Vector3(mapS / 2, mapS / 2, -20); //centre the camera
    }
    public void genBorder()
    {
        int mapS = int.Parse(inpMapSize.text);
        int Rand = Mathf.RoundToInt(Random.Range(1, 7));
        for (int i = 0; i < mapS; i++) //Gen left border
        {
            Rand = Rand + Mathf.RoundToInt(Random.Range(-5, 5)/3); //random values divided by 3 to get some slightly smoother looking borders
            if (Rand < 5)
                Rand = 5;
            if (Rand > 10)
                Rand = 10;
            for (int o = 0; o < Rand; o++)
            {
                wallMap.SetTile(new Vector3Int(o, i, 1), wallTile);
            }
        }
        Rand = Mathf.RoundToInt(Random.Range(1, 7));
        for (int i = 0; i < mapS; i++) //Gen right border
        {
            Rand = Rand + Mathf.RoundToInt(Random.Range(-5, 5) / 3);
            if (Rand < 5)
                Rand = 5;
            if (Rand > 10)
                Rand = 10;
            for (int o = 0; o > -Rand; o--)
            {
                wallMap.SetTile(new Vector3Int(o + mapS - 1, i, 1), wallTile);
            }
        }
        Rand = Mathf.RoundToInt(Random.Range(1, 7));
        for (int o = 0; o < mapS; o++) //Gen top border
        {
            Rand = Rand + Mathf.RoundToInt(Random.Range(-5, 5) / 3);
            if (Rand < 5)
                Rand = 5;
            if (Rand > 10)
                Rand = 10;
            for (int i = 0; i < Rand; i++)
            {
                wallMap.SetTile(new Vector3Int(o, i, 1), wallTile);
            }
        }
        Rand = Mathf.RoundToInt(Random.Range(1, 7));
        for (int o = 0; o < mapS; o++) //Gen bottom border
        {
            Rand = Rand + Mathf.RoundToInt(Random.Range(-5, 5) / 3);
            if (Rand < 5)
                Rand = 5;
            if (Rand > 10)
                Rand = 10;
            for (int i = 0; i > -Rand; i--)
            {
                wallMap.SetTile(new Vector3Int(o, i + mapS - 1, 1), wallTile);
            }
        }

    }
    public void genCluster()
    {
        int mapS = int.Parse(inpMapSize.text);
        int initPosx = Mathf.RoundToInt(Random.Range(0, mapS - 1));
        int initPosy = Mathf.RoundToInt(Random.Range(0, mapS - 1));
        int maxW = Mathf.RoundToInt(Random.Range(8, Mathf.Pow(int.Parse(inpMapSize.text), 0.65f) * wallSizeVal.value));
        int maxH = Mathf.RoundToInt(Random.Range(8, Mathf.Pow(int.Parse(inpMapSize.text), 0.65f) * wallSizeVal.value)); ;
        int initW = maxW;
        int initH = maxH;
        for (int i = 0; i < maxH; i++) //Generation for the 1st quartile
        {
            maxW = maxW + Random.Range(-1, 1);
            if (i + initPosy < mapS)
            {
                for (int o = 0; o < maxW; o++)
                {
                    if (o + initPosx < mapS)
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
            maxW = maxW + Random.Range(-1, 1);
            if (i + initPosy < mapS)
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
            maxW = maxW + Random.Range(-1, 1);
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
            maxW = maxW + Random.Range(-1, 1);
            if (i + initPosy >= 0)
            {
                for (int o = 0; o < maxW; o++)
                {
                    if (o + initPosx < mapS)
                    {
                        wallMap.SetTile(new Vector3Int(initPosx + o, initPosy + i, 1), wallTile);
                    }
                }
            }
        }
    }
}
