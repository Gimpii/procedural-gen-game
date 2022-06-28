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
    Vector3 pos, moving;

    void Update()
    {
        pos = GameObject.Find("PLAYER").transform.position;

        Vector3 diff = pos - transform.position;
        float rotation_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg; //angle between enemy and player
        float slowdown = Mathf.Sqrt(Vector2.Distance(pos, transform.position)) - 4.5f; //prevent enemy from gliding into player
        Vector2 newPos = Vector2.MoveTowards(transform.position, pos, speed * Time.deltaTime * slowdown); 

        transform.position = new Vector3(newPos.x, newPos.y, -10) + moving * Time.deltaTime * speed; //move enemy towards player, but not on top of them
        transform.rotation = Quaternion.Euler(0f, 0f, rotation_z); //rotate enemy to face player

        RaycastHit2D cast = Physics2D.BoxCast(new Vector2(transform.position.x, transform.position.y), new Vector2(1.1f, 1.1f), rotation_z, Vector2.zero); //Hitbox for the enemy, will destroy it if touched by a player projectile
        try
        {
            if (cast.transform.tag == "bullet") //destroys enemy instance if touching player bullet
            {
                die();
            }
        }
        catch (NullReferenceException) //Prevent errors from appearing if the enemy is not touching anything
        {
        }
        if (GameObject.Find("PLAYER").GetComponent<playerController>().dead == true) //Will destroy all enemy instances if player is killed
        {
            die();
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

    IEnumerator moveabout() //adds random jolts of movement to enemy
    {
        switch_ = false;
        moving = new Vector3(UnityEngine.Random.Range(-1, 1), UnityEngine.Random.Range(-1, 1), 0);
        yield return new WaitForSeconds(UnityEngine.Random.Range(2.5f, 6));
        switch_ = true;
    }
    public void die() //destroys enemy instance
    {
        GameObject.Find("tileGen").GetComponent<genManager>().currentEnemyCount -= 1;
        Destroy(gameObject);
    }

    IEnumerator fire() //fires a projectile every 1.5 to 3 seconds
    {
        projActive = false;
        Transform bullet = Instantiate(enemyProj, transform.position, Quaternion.identity);
        bullet.GetComponent<enemyProjectileBehaviour>().Setup();
        yield return new WaitForSeconds(UnityEngine.Random.Range(1.5f,3));
        projActive = true;
    }

    
}
