using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MisilChase : MonoBehaviour
{
   
    private GameObject Player;
    public float speed;
    public float launchForce;
    public float wait;
    public string target;
    public float lifeTime;

    public Rigidbody2D rb;
    public bool IsLaunching;

    private float distance;
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag(target);
    }


    void Awake()
    {
        rb.AddForce(new Vector2(rb.velocity.x, launchForce));
        StartCoroutine(Launch());
        StartCoroutine(Destroy());
    }

    // Update is called once per frame
    void Update()
    {
        if (IsLaunching == true)
        {
            Chase();
        }
    }

    IEnumerator Launch()
    {
        yield return new WaitForSeconds(wait);
        IsLaunching = true;
    }
    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }

    private void Chase()
    {
        distance = Vector2.Distance(transform.position, Player.transform.position);
        Vector2 direction = Player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.position = Vector2.MoveTowards(this.transform.position, Player.transform.position, speed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(Vector3.forward * angle);
    }   

       private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(target))
        {
            Destroy(gameObject); 
        }
    }
}
