using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;

public class playerController : MonoBehaviour
{
    [SerializeField] private int speed;
    [SerializeField] private Canvas tileGenUI;
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject player;

    private bool isCameraFollow = false;
    private bool isCamZoom = false;
    void Start()
    {
        
    }
    public void buttonBegin() //Hide tile generation UI On Begin
    {
        tileGenUI.gameObject.SetActive(false);
        isCameraFollow = true;
        isCamZoom = true;
    }
    void Update()
    {
        if (isCamZoom == true)
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 30, 0.01f);
            if (cam.orthographicSize == 30)
                isCamZoom = false;
        }
        if (isCameraFollow == true)
        {
            movePlayer();
            cam.transform.position = Vector3.Lerp(cam.transform.position, new Vector3(player.transform.position.x, player.transform.position.y, -10), 0.03f);
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
        transform.position += movement * speed * Time.deltaTime;
    }
}
