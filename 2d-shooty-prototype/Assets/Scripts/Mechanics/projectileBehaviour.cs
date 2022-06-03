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
        direction = bulletDirection;
    }

    private void Update()
    {
        float speed = 50f;
        transform.position += direction * speed * Time.deltaTime;

        RaycastHit2D detect = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), direction, 0.01f);
        try
        {
            if (detect.transform.gameObject.name == "Wall")
            {
                Destroy(gameObject);
            }
        }
        catch(NullReferenceException)
        {
        }
    }

}
