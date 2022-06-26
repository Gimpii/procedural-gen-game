using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class genManager : MonoBehaviour
{

    [SerializeField] private Button genbtn, respawn;
    [SerializeField] private InputField genseed;
    [SerializeField] private InputField inpMapSize;
    [SerializeField] private Transform enemy;
    [SerializeField] private GameObject player;

    public bool spwnActive = false;
    private string seed;
    public int globalEnemyMax, currentEnemyCount;

    public void Start()
    {
        Button genbutton = genbtn.GetComponent<Button>();
        genbutton.onClick.AddListener(onclick);
    }

    public void Onreset()
    {
        GameObject.Find("PLAYER").GetComponent<playerController>().dead = false;
        player.GetComponent<SpriteRenderer>().enabled = true;
        respawn.gameObject.SetActive(false);
        spwnActive = true;
        player.transform.position = new Vector3(int.Parse(inpMapSize.text) / 2, int.Parse(inpMapSize.text) / 2, -10);
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
        if (currentEnemyCount < globalEnemyMax && spwnActive == true)
        {
            StartCoroutine(spawnEnemy());
        }
        
    }
    IEnumerator spawnEnemy()
    {
        currentEnemyCount += 1;
        yield return new WaitForSeconds(Random.Range(1.5f, 3));
        Vector3 location = new Vector3(Random.Range(0, int.Parse(inpMapSize.text)) + 0.5f, Random.Range(0, int.Parse(inpMapSize.text)) + 0.5f, -10);
        Instantiate(enemy, location, Quaternion.identity);
    }
}
