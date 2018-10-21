using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float jumpStrength = 3;
    [SerializeField] private readonly AudioClip jumpNoise;


    private bool isTouchingGround = true;
    private AudioSource jumpSource;
    private float timer;
    public List<float> velocityValues;
    private float deltaTime;

    // Use this for initialization
    void Start () {
        isTouchingGround = true;
        GameManager.instance.isGameOver = false;
        GameManager.instance.askQuestion = false;
        rb = GetComponent<Rigidbody2D>();
        jumpSource = GetComponent<AudioSource>();
        timer = 0;
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

        if (Input.GetKeyDown(KeyCode.Space) && !GameManager.instance.isGameOver)
        {
            Jump();
            Debug.Log("pew");
        }

        //Debug.Log(rb.velocity.y);
        if (!isTouchingGround && (velocityValues[0] * velocityValues[1]) <= 0 && timer != deltaTime)
        {
            Debug.Log("APEX");
            GameManager.instance.askQuestion = true;
            timer = 0;
            //FreezeTime();
        }

        timer += Time.deltaTime;
        deltaTime = Time.deltaTime;
        
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
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ground")
            isTouchingGround = false;
    }
}
