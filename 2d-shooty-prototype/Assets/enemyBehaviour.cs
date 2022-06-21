using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBehaviour : MonoBehaviour
{

    public float speed;
    [SerializeField] Transform enemyProj;
    bool projActive = true;
    bool switch_ = true;
    Vector2 direction;
    Vector3 pos, moving;

    void Update()
    {
        pos = GameObject.Find("PLAYER").transform.position;

        Vector3 diff = pos - transform.position;
        float rotation_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        float slowdown = Mathf.Sqrt(Vector2.Distance(pos, transform.position)) - 4.5f;
        Vector2 newPos = Vector2.MoveTowards(transform.position, pos, speed * Time.deltaTime * slowdown);

        transform.position = new Vector3(newPos.x, newPos.y, -10) + moving * Time.deltaTime * speed;
        transform.rotation = Quaternion.Euler(0f, 0f, rotation_z);

        RaycastHit2D cast = Physics2D.BoxCast(new Vector2(transform.position.x, transform.position.y), new Vector2(1.2f, 1.2f), 0, Vector2.zero);
        try
        {
            if (cast.transform.tag == "bullet")
            {
                die();
            }
        }
        catch (NullReferenceException)
        {
        }
        


        if (projActive == true)
        {
            StartCoroutine("fire");
        }
        if (switch_ == true)
        {
            StartCoroutine(moveabout());
        }

    }


    IEnumerator moveabout()
    {
        switch_ = false;
        moving = new Vector3(UnityEngine.Random.Range(-1, 1), UnityEngine.Random.Range(-1, 1), 0);
        yield return new WaitForSeconds(UnityEngine.Random.Range(2.5f, 6));
        switch_ = true;
    }
    public void die()
    {
        GameObject.Find("tileGen").GetComponent<genManager>().globalEnemyMax -= 1;
        Destroy(gameObject);
    }

    IEnumerator fire()
    {
        projActive = false;
        Transform bullet = Instantiate(enemyProj, transform.position, Quaternion.identity);
        bullet.GetComponent<enemyProjectileBehaviour>().Setup();
        yield return new WaitForSeconds(UnityEngine.Random.Range(1.5f,3));
        projActive = true;
    }

    
}
