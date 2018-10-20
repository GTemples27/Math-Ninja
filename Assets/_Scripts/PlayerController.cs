using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float jumpStrength = 3;
    [SerializeField] private AudioClip jumpNoise;
    [SerializeField] private float slowTime = 2;

    private bool isTouchingGround = true;
    private bool isInSloMo = false;
    private AudioSource jumpSource;
    private float timer;
    public List<float> velocityValues;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        jumpSource = GetComponent<AudioSource>();
        timer = 0;
        velocityValues = new List<float>();
        velocityValues.Add(rb.velocity.y);
        velocityValues.Add(rb.velocity.y);
    }
	
	// Update is called once per frame
	void Update () {
        velocityValues.Add(rb.velocity.y);
        velocityValues.RemoveAt(0);
        if (Input.GetKeyDown(KeyCode.Space))
            Jump();
        
        //Debug.Log(rb.velocity.y);
        if (!isTouchingGround && (velocityValues[0] * velocityValues[1]) <= 0)
        {
            Debug.Log("APEX");
            timer = 0;
            isInSloMo = true;
            //StartSloMo();
        }

        if (isInSloMo)
            timer += Time.deltaTime;

        if (timer >= slowTime)
        {
            //StopSloMo();
        }
        //Debug.Log("Last:" + velocityValues[0].ToString() + "; This: " + velocityValues[1].ToString());
    }
    

    public void StartSloMo()
    {
        Time.timeScale = 0.2f;
        Debug.Log("Start");
    }

    public void StopSloMo()
    {
        Time.timeScale = 1f;
        Debug.Log("End");
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
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ground")
            isTouchingGround = false;
    }
}
