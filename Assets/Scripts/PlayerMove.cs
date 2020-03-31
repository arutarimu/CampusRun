using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerMove : MonoBehaviour
{
    public float moveSpeed, jumpHeight;
    static public int lvlreached = 1;
    public Animator animator;
    private bool onGround;
    private bool isRight;
    private Rigidbody2D rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        isRight = true;
        onGround = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        flip(horizontal);
        animator.SetFloat("Speed", Mathf.Abs(horizontal));
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            transform.Translate(Vector2.right * Time.deltaTime * moveSpeed);
        }

        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            transform.Translate(Vector2.left * Time.deltaTime * moveSpeed);
        }

        if (Input.GetAxisRaw("Vertical") > 0 && onGround)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpHeight);
            onGround = false;
        }

        if (rigidBody.velocity.y == 0)
        {
            onGround = true;
        }
    }

    void flip(float horizontal)
    {
        if (horizontal > 0 && !isRight || horizontal < 0 && isRight)
        {
            isRight = !isRight;
            Vector3 scaling = transform.localScale;
            scaling.x *= -1;
            transform.localScale = scaling;
        }
    }

    void LevelProgress(int level)
    {
        if (PlayerPrefs.GetInt("reset") == 1)
        {
            lvlreached = 1;
            PlayerPrefs.SetInt("reset", 0);
        }

        if (lvlreached == level)
        {
            lvlreached += 1;
        }

        PlayerPrefs.SetInt("levelReached", lvlreached);
        SceneManager.LoadScene("LevelSelection");
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "DeathTrigger")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        else if (col.gameObject.tag == "Sandwich")
        {
            LevelProgress(1);

        }
        else if (col.gameObject.tag == "Coffee")
        {
            LevelProgress(2);

        }
    }
}