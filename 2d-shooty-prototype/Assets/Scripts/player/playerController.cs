using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;
using UnityEngine.UI;

public class playerController : MonoBehaviour
{
    [SerializeField] private float speed, globalSpeed;
    [SerializeField] private Canvas tileGenUI;
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject player;
    [SerializeField] private Tile wallTile;
    [SerializeField] private Tilemap wallMap;
    [SerializeField] private InputField inpHolder;
    [SerializeField] private Button respawn;

    public bool dead = false;
    bool canDash = true;
    bool isCameraFollow = false;
    bool isCamZoom = false;

    void Start()
    {
        respawn.gameObject.SetActive(false);
    }
    public void buttonBegin() //Hide tile generation UI On Begin
    {
        tileGenUI.gameObject.SetActive(false);
        isCameraFollow = true;
        isCamZoom = true;

        int offset = Mathf.RoundToInt(int.Parse(inpHolder.text) / 2);

        player.transform.position = new Vector3(int.Parse(inpHolder.text) / 2, int.Parse(inpHolder.text) / 2, -10); //Move player to the centre of the map
        for (int i = -15; i < 15; i++)
        {
            for (int o = -15; o < 15; o++)
            {
                wallMap.SetTile(new Vector3Int(offset + i, offset + o, 1), null);//delete walls around player spawn
            }
        }
    }
    void Update()
    {
        if(dead == false)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (canDash == true)
                    StartCoroutine("dash");
            }
            if (isCamZoom == true)
            {
                cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 30, 0.01f);
                //Interpolates between current camera orthographic size and the target orthographic size
                if (cam.orthographicSize == 30)
                    isCamZoom = false;
            }
            if (isCameraFollow == true)
            {
                movePlayer();
                cam.transform.position = Vector3.Lerp(cam.transform.position, new Vector3(player.transform.position.x, player.transform.position.y, -20), 0.03f);
                //Interpolates between the camera position and player position, creating a smooth camera
            }

            RaycastHit2D cast = Physics2D.BoxCast(new Vector2(transform.position.x, transform.position.y), new Vector2(0.5f, 1f), 0, Vector2.zero);
            try
            {
                if (cast.transform.tag == "e_bullet")
                {
                    respawn.gameObject.SetActive(true);
                    die();
                }
            }
            catch (NullReferenceException)
            {
            }
        }
        
    }

    public void die()
    {
        GameObject.Find("tileGen").GetComponent<genManager>().spwnActive = false;
        player.GetComponent<SpriteRenderer>().enabled = false;
        dead = true;
    }
    IEnumerator dash()
    {
        canDash = false;
        speed = 100;
        for (int i = 0; i < 16; i++)
        {
            speed = speed * 0.917004f;
            yield return new WaitForSeconds(0.02f);
        }
        speed = 25;
        yield return new WaitForSeconds(0.6f);
        canDash = true;
    }
    private void movePlayer()
    {
        float xVel = 0f;
        float yVel = 0f;

        if (Input.GetKey(KeyCode.W))
        {
            yVel += 1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            xVel += 1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            xVel += -1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            yVel += -1f;
        }

        Vector3 movement = new Vector3(xVel, yVel).normalized; 

        Vector3 targetMove = transform.position + movement * globalSpeed * speed * Time.deltaTime; //the position the player will move to
        RaycastHit2D detect = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 1), movement, globalSpeed * speed * Time.deltaTime); //Send a ray from position in direction of movement to detect any object with collider
        if (detect.collider == null) //Nothing hit
        {
            transform.position = targetMove;
        }
        else
        {
            Vector3 testPos = new Vector3(movement.x, 0f).normalized;
            targetMove = transform.position + testPos * globalSpeed * speed * 0.75f * Time.deltaTime;
            detect = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 1), testPos, globalSpeed * speed * Time.deltaTime);
            if(testPos.x != 0f && detect.collider == null)//Nothing next to character on horizontal
            {
                transform.position = targetMove;
            }
            else
            {
                testPos = new Vector3(0f, movement.y).normalized;
                targetMove = transform.position + testPos * speed * globalSpeed * 0.75f * Time.deltaTime;
                detect = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 1), testPos, globalSpeed * speed * Time.deltaTime);
                if (testPos.y != 0f && detect.collider == null)//Nothing next to character on vertical
                {
                    transform.position = targetMove;
                }
                else
                {

                }
            }
        }
    }
}
