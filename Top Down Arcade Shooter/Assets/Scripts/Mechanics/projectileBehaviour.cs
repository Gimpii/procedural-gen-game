using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class projectileBehaviour : MonoBehaviour
{
    private Vector3 direction;
    public void Setup(Vector3 bulletDirection)
    {
        direction = bulletDirection; //Gets the bullet direction set when spawning a bullet
    }

    private void Update()
    {
        float speed = 50f;
        transform.position += direction * speed * Time.deltaTime; //move bullet in its direction each frame

        RaycastHit2D detect = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), direction, 0.01f); //Send a ray in the direction of the bullet
        try
        {
            if (detect.transform.gameObject.name == "Wall") //If the ray hits a wall, then destroy the bullet
            {
                Destroy(gameObject);
                
            }
        }
        catch(NullReferenceException) //Prevents error messages about the ray not finding any interactable object
        {
        }
    }

}
