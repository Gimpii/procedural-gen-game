using System.Collections;
using UnityEngine;

public class enemyProjectileBehaviour : MonoBehaviour
{
    private Vector3 playerPos;
    private Vector2 diff;
    private Vector3 direction;
    [SerializeField] private float speed;
    bool die = false;

    public void Setup() //Is run when an Enemy calls for a projectile to spawn
    {
        playerPos = GameObject.Find("PLAYER").transform.position;
        diff = playerPos - transform.position;
        float angle = Mathf.Atan2(diff.x, diff.y); //Trigonometry to find angle between enemy and player
        direction = new Vector2(Mathf.Sin(angle) + Random.Range(-0.1f, 0.1f), Mathf.Cos(angle) + Random.Range(-0.1f,0.1f)).normalized; //Direction toward player with slight randomness in angle
    }
    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime; //Bullet will move in a single direction each frame
        if (die == false) //To ensure coroutine is not run continuously throughout the liftime of the bullet
        {
            StartCoroutine("destroy");
        }
    }

    IEnumerator destroy()
    {
        die = true;
        yield return new WaitForSeconds(8);//wait 8 seconds
        Destroy(gameObject); //Remove this instance of the gameobject and its scripts from the game
    }
}
