using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class projectileHandler : MonoBehaviour
{
    [SerializeField] Transform projectile, player;
    [SerializeField] private Button inpHolder;
    [SerializeField] Vector3 mousepos, bulletDirection;
    private bool shootEnabled = false;

    public void buttonBegin()
    {
        shootEnabled = true;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) && shootEnabled == true)
        {
            StartCoroutine(spawnProjectile("down"));
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && shootEnabled == true)
        {
            StartCoroutine(spawnProjectile("up"));
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && shootEnabled == true)
        {
            StartCoroutine(spawnProjectile("left"));
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && shootEnabled == true)
        {
            StartCoroutine(spawnProjectile("right"));
        }
    }

    IEnumerator spawnProjectile(string keyPressed)
    {
        Transform bullet = Instantiate(projectile, transform.position, Quaternion.identity);
        switch (keyPressed)
        {
            case "right":
                bulletDirection = Vector2.right;
                break;
            case "left":
                bulletDirection = Vector2.left;
                break;
            case "up":
                bulletDirection = Vector2.up;
                break;
            case "down":
                bulletDirection = Vector2.down;
                break;
        }
        bullet.GetComponent<projectileBehaviour>().Setup(bulletDirection);
        shootEnabled = false;
        yield return new WaitForSeconds(0.4f);
        shootEnabled = true;
    }
}
