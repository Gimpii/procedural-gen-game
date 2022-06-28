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
        genbutton.onClick.AddListener(onclick); //Sets up the generate map button to run when clicked
    }

    public void Onreset() //Is run when the respawn button is clicked
    {
        GameObject.Find("PLAYER").GetComponent<playerController>().dead = false;
        player.GetComponent<SpriteRenderer>().enabled = true; //character shown
        respawn.gameObject.SetActive(false); //respawn button is hidden
        spwnActive = true; //enemies can spawn
        player.transform.position = new Vector3(int.Parse(inpMapSize.text) / 2, int.Parse(inpMapSize.text) / 2, -10); //spawn player at centre of map
    }
    void onclick() //Button for radndomizing seed
    {
        Random.InitState((int)System.DateTime.Now.Ticks); //Set internal seed to the current time in ticks
        seed = "";
        for (int i = 0; i < 8; i++)
        {
            seed = seed + Random.Range(0, 10); //set seed to 8 random integers
        }
        genseed.text = seed;
        int b = seed.GetHashCode(); //convert seed to its hash code
        Random.InitState(b); //Set internal seed to the randomised seed
    }
    
    public void Onbegin() //Runs when begin button clicked
    {
        spwnActive = true; //Enemies can spawn
        currentEnemyCount = 0; //Enemies default count set to 0
    }

    void Update()
    {
        if (currentEnemyCount < globalEnemyMax && spwnActive == true) //Only runs if enemies on the map is less than the maximum
        {
            StartCoroutine(spawnEnemy());
        }
        
    }
    IEnumerator spawnEnemy()
    {
        currentEnemyCount += 1; //increments enemy count by 1
        yield return new WaitForSeconds(Random.Range(1.5f, 3));
        Vector3 location = new Vector3(Random.Range(0, int.Parse(inpMapSize.text)) + 0.5f, Random.Range(0, int.Parse(inpMapSize.text)) + 0.5f, -10); 
        Instantiate(enemy, location, Quaternion.identity); //spawns an enemy gameobject somewhere on the map
    }
}
