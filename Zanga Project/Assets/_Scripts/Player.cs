using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    public Text keysText;
    public GameObject Door;
    public AudioSource audio_source;
    public AudioClip deathSound,jumpSound;
    public static bool isAlive;
    public float speed,jumpForce;
    GameObject [] Keys;
    int numberOfKeys;
    float vecrtical,timer;
    bool isJumping;
    Rigidbody2D rb;
    Light l;
    void Start()
    {
        Keys = GameObject.FindGameObjectsWithTag("Collectables");
        rb =gameObject.GetComponent<Rigidbody2D>();
        isAlive = true;
        isJumping  = false;
        timer = 2;
        int s = Keys.Length;
        keysText.text = "Keys "+numberOfKeys + " : " + Keys.Length;
    }

    void Update()
    {
        if(isAlive)
        {
            Move();
            Jump();
        }
        else
        {
            timer -= Time.deltaTime;
            if(timer<=0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
        if(numberOfKeys == Keys.Length)
        {
            Color Dc = Color.red;
            Dc.r = 255;
            Door.GetComponent<SpriteRenderer>().color = Dc;
        }
    }

    void Move()
    {
        if(Input.GetKey(KeyCode.D))
        {
            rb.AddForce(Vector2.right * speed);
        }else if(Input.GetKey(KeyCode.A))
        {
            rb.AddForce(Vector2.left * speed);
        }
        if(rb.velocity.x >0 || rb.velocity.x <0)
        {
            rb.velocity = new Vector2(0,rb.velocity.y);
        }
    } 

    void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            audio_source.clip = jumpSound;
            audio_source.Play();
            rb.AddForce(Vector2.up * jumpForce);
            audio_source.clip = jumpSound;
            audio_source.Play();
            isJumping = true;
        }
        if(isJumping && rb.velocity.y <=0)
        {
            rb.AddForce(Vector2.down * jumpForce / 27);
        }
    }

    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        if(collisionInfo.gameObject.tag == "Platform")
        {
            isJumping = false;
        }
        if(collisionInfo.gameObject.tag == "Collectables")
        {
            Destroy(collisionInfo.gameObject);
            numberOfKeys++;
            keysText.text = "Keys "+numberOfKeys + " : " + Keys.Length;
            print(numberOfKeys);
        }
        if(collisionInfo.gameObject.tag == "Obstacles")
        {
            collisionInfo.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            isAlive = false;
            audio_source.clip = deathSound;
            audio_source.Play();
            print("Death");
        }
        if(collisionInfo.gameObject.tag == "Door")
        {
            if(numberOfKeys == Keys.Length)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }
}
