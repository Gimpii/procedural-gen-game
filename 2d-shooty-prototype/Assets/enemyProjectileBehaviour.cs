using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyProjectileBehaviour : MonoBehaviour
{
    private Vector3 direction;
    float speed = 50f;
    public void Setup(Vector3 bulletDirection)
    {
        direction = bulletDirection.normalized;
    }
    private void Update()
    {
        
        transform.position += direction * speed * Time.deltaTime;

        //RaycastHit2D detect = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), direction, 0.01f);
    }
}
