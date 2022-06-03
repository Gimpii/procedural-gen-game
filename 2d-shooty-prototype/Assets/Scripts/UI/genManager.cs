using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class genManager : MonoBehaviour
{

    [SerializeField] private Button genbtn;
    [SerializeField] private InputField genseed;
    [SerializeField] private InputField inpMapSize;
    [SerializeField] private TileBase[] wallTile;
    [SerializeField] private Tilemap wallMap;
    [SerializeField] private GameObject player;
    [SerializeField] private Transform enemy;

    private bool spwnActive = false;
    private string seed;
    private int globalEnemyMax, currentEnemyCount;

    private void Start()
    {
        Button genbutton = genbtn.GetComponent<Button>();
        genbutton.onClick.AddListener(onclick);
        globalEnemyMax = 5;
    }
    void onclick()
    {
        Random.InitState((int)System.DateTime.Now.Ticks);
        seed = "";
        for (int i = 0; i < 8; i++)
        {
            seed = seed + Random.Range(0, 10);
        }
        genseed.text = seed;
        int b = seed.GetHashCode();
        print(b);
        Random.InitState(b);
    }
    
    public void Onbegin()
    {
        spwnActive = true;
        currentEnemyCount = 0;
    }

    void Update()
    {
        Vector3 playerpos = player.transform.position;

        if (currentEnemyCount < globalEnemyMax && spwnActive == true)
        {
            spawnEnemy();
        }
    }
    void spawnEnemy()
    {
        Vector3 location = new Vector3(Random.Range(0, int.Parse(inpMapSize.text)) + 0.5f, Random.Range(0, int.Parse(inpMapSize.text)) + 0.5f, -10);
        Instantiate(enemy, location, Quaternion.identity);
        currentEnemyCount += 1;
    }
}
