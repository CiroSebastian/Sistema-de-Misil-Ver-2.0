using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    public float speed;
    public float jump;

    private float Move;

    float inputHorizontal;
    bool facingRight;

    int JumpCount = 1;

    public Rigidbody2D rb;

    public bool isJumping;

    public GameObject Sprite;

    // Start is called before the first frame update
    void Start()
    {
        Sprite.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        Move = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(speed * Move, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.W) && isJumping == false && JumpCount < 1)
        {
            rb.AddForce(new Vector2(rb.velocity.x, jump));
            isJumping = true;

            JumpCount++;
        }
    }
    void FixedUpdate()
    {
        inputHorizontal = Input.GetAxisRaw("Horizontal");

        if (inputHorizontal < 0 && !facingRight)
        {
            Flip();
        }

        if (inputHorizontal > 0 && facingRight)
        {
            Flip();

        }
    }

    void Flip()
    {
        Vector3 currentScale = transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        facingRight = !facingRight;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            isJumping = false;
            JumpCount--;
        }
       
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Kill"))
        {
            Sprite.SetActive(false);
            StartCoroutine(Lose());
        }
    }

    IEnumerator Lose()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
