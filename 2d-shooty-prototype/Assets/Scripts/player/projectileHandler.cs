using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class projectileHandler : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    [SerializeField] private Button inpHolder;
    private bool shootEnabled = false;

    public void buttonBegin()
    {
        shootEnabled = true;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && shootEnabled == true)
        {
            transform.LookAt(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            StartCoroutine(spawnProjectile());
        }
    }

    IEnumerator spawnProjectile()
    {
        GameObject.Instantiate(projectile, transform);
        yield return new WaitForSeconds(1);
    }
}
