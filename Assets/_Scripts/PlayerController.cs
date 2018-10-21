using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float jumpStrength = 3;
    [SerializeField] private readonly AudioClip jumpNoise;
    [SerializeField] private readonly AudioClip KONoise;
    private SpriteRenderer rend;


    private bool isTouchingGround = true;
    private AudioSource jumpSource;
    private AudioSource KoSource;
    private float timer;
    public List<float> velocityValues;
    private float deltaTime;

    public static bool timeSlow;
    public static bool isWarping;
    public static float warpingTimer;
    private int GRAVITY;

    // Use this for initialization
    void Start () {
        isTouchingGround = true;
        GameManager.instance.isGameOver = false;
        GameManager.instance.askQuestion = false;
        isWarping = false;
        rb = GetComponent<Rigidbody2D>();
        rend = gameObject.GetComponent<SpriteRenderer>();
        jumpSource = GetComponent<AudioSource>();
        KoSource = GetComponent<AudioSource>();
        timer = 0;
        warpingTimer = 0;
        rb.gravityScale = 2;
        velocityValues = new List<float>
        {
            rb.velocity.y,
            rb.velocity.y
        };
    }
	
	// Update is called once per frame
	void Update () {
        velocityValues.Add(rb.velocity.y);
        velocityValues.RemoveAt(0);

        //can exit the game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.instance.isGameOver = true;
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.Space) && !GameManager.instance.isGameOver)
        {
            Jump();
            Debug.Log("pew");
        }

        //time slow affects velocity
        if (timeSlow)
        {
            rb.drag = 75;
        }

        //Debug.Log(rb.velocity.y);
        if (!isTouchingGround && (velocityValues[0] * velocityValues[1]) <= 0 && timer != deltaTime)
        {
            Debug.Log("APEX");
            GameManager.instance.askQuestion = true;
            timer = 0;
            //FreezeTime();
            //try to make time slow down
            timeSlow = true;
        }
        if (isTouchingGround /*|| !InputHandler.questionAsked */)
        {
            timeSlow = false;
        }

        if (!timeSlow)
        {
            rb.drag = 0;
        }

        timer += Time.deltaTime;
        deltaTime = Time.deltaTime;

        //warp effect
        if (isWarping){
            Debug.Log("Warp");
            rend.enabled = false;
            warpingTimer += Time.deltaTime;
            //keep y value at same
            rb.gravityScale = 0;
        }
        //stop warping after X seconds
        if (warpingTimer >= .5)
        {
            rend.enabled = true;
            isWarping = false;
            rb.gravityScale = 2;
        }        
    }

    public void FreezeTime()
    {
        Debug.Log("Start");
        Time.timeScale = 0f;       
    }

    public void StopFreeze()
    {
        Debug.Log("End");
        Time.timeScale = 1f;
    }

    public void Jump()
    {
        if (!isTouchingGround)
            return;

        rb.AddForce(Vector2.up * jumpStrength);
        jumpSource.PlayOneShot(jumpNoise);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ground")
            isTouchingGround = true;

        if (collision.transform.tag == "Obstacle")
        {
            GameManager.instance.isGameOver = true;
            PolygonCollider2D collider = collision.gameObject.GetComponent<PolygonCollider2D>();
            collider.enabled = false;
            KoSource.PlayOneShot(KONoise);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ground")
            isTouchingGround = false;
    }
}
