using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject MisilPrefab;
    private GameObject spawn;

    public float misliRate = 3.5f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn(misliRate, MisilPrefab));
        spawn = GameObject.FindGameObjectWithTag("Spawner");
    } 

    private IEnumerator Spawn(float rate, GameObject Projectile)
    {
        yield return new WaitForSeconds(rate);
        GameObject newMisil = Instantiate(Projectile, new Vector3(spawn.transform.position.x, spawn.transform.position.y + 2), Quaternion.identity);
        StartCoroutine(Spawn(rate, Projectile));
    }
}
