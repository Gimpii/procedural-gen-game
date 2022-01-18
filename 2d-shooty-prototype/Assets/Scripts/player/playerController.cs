using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;

public class playerController : MonoBehaviour
{
    [SerializeField] private int speed;
    void Start()
    {
        
        
    }

    void Update()
    {
        move();
    }

    private void move()
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
