using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBehaviour : MonoBehaviour
{

    public float speed = 1.5f;
    [SerializeField] Transform enemyProj;
    bool projActive = true;
    Vector2 direction;
    Vector3 pos;

    void Update()
    {
        pos = GameObject.Find("PLAYER").transform.position;

        Vector2 diff = pos - transform.position;
        float rotation_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        float slowdown = Mathf.Sqrt(Vector2.Distance(pos, transform.position)) - 4.5f;
        Vector2 newPos = Vector2.MoveTowards(transform.position, pos, speed * Time.deltaTime * slowdown);
        direction = newPos;

        transform.position = new Vector3(newPos.x, newPos.y, -10);
        transform.rotation = Quaternion.Euler(0f, 0f, rotation_z);

        /*if (projActive == true)
        {
            StartCoroutine(fire());
        }*/
    }

    IEnumerator fire()
    {
        projActive = false;

        Transform bullet = Instantiate(enemyProj, transform.position, Quaternion.identity);
        bullet.GetComponent<projectileBehaviour>().Setup(direction);

        yield return new WaitForSeconds(1);
        projActive = true;
    }

    
}
