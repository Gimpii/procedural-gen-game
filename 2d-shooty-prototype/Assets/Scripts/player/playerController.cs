using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;
using UnityEngine.UI;

public class playerController : MonoBehaviour
{
    [SerializeField] private int speed;
    [SerializeField] private Canvas tileGenUI;
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject player;
    [SerializeField] private InputField inpHolder;

    bool isCameraFollow = false;
    bool isCamZoom = false;
    public void buttonBegin() //Hide tile generation UI On Begin
    {
        tileGenUI.gameObject.SetActive(false);
        isCameraFollow = true;
        isCamZoom = true;

        player.transform.position = new Vector3(int.Parse(inpHolder.text) / 2, int.Parse(inpHolder.text) / 2, -10); //Move player to the centre of the map
    }
    void Update()
    {
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

        Vector3 targetMove = transform.position + movement * speed * Time.deltaTime; //the position the player will move to
        RaycastHit2D detect = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 1), movement, speed * Time.deltaTime); //Send a ray from position in direction of movement to detect any object with collider
        if (detect.collider == null) //Nothing hit
        {
            transform.position = targetMove;
        }
        else
        {
            Vector3 testPos = new Vector3(movement.x, 0f).normalized;
            targetMove = transform.position + testPos * speed * 0.75f * Time.deltaTime;
            detect = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 1), testPos, speed * Time.deltaTime);
            if(detect.collider == null)//Nothing next to character on horizontal
            {
                transform.position = targetMove;
            }
            else
            {
                testPos = new Vector3(0f, movement.y).normalized;
                targetMove = transform.position + testPos * speed * 0.75f * Time.deltaTime;
                detect = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 1), testPos, speed * Time.deltaTime);
                if (detect.collider == null)//Nothing next to character on vertical
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
