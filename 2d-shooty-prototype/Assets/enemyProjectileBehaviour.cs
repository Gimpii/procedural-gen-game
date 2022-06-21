using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyProjectileBehaviour : MonoBehaviour
{
    private Vector3 playerPos;
    private Vector2 diff;
    private Vector3 direction;
    [SerializeField] private float speed;
    bool die = false;

    public void Setup()
    {
        playerPos = GameObject.Find("PLAYER").transform.position;
        diff = playerPos - transform.position;
        float angle = Mathf.Atan2(diff.x, diff.y);
        direction = new Vector2(Mathf.Sin(angle) + Random.Range(-0.1f, 0.1f), Mathf.Cos(angle) + Random.Range(-0.1f,0.1f)).normalized;
    }
    private void Update()
    {
        
        transform.position += direction * speed * Time.deltaTime;
        if (die == false)
        {
            StartCoroutine("destroy");
        }
        //RaycastHit2D detect = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), direction, 0.01f);
    }

    IEnumerator destroy()
    {
        die = true;
        yield return new WaitForSeconds(8);
        Destroy(gameObject);
    }
}
