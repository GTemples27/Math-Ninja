using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardController : MonoBehaviour {

    [SerializeField] private float scrollSpeedDefault = 10;
    [SerializeField] private float scrollSpeed = 10;

    // Use this for initialization
    void Start() {
        scrollSpeed = 10;
}

    // Update is called once per frame
    void Update()
    {
        //slows down when box is in jump apex
        if (PlayerController.timeSlow)
        {
            scrollSpeed = scrollSpeedDefault / 5;
        }
        else{
            scrollSpeed = scrollSpeedDefault;
        }

        if (!GameManager.instance.isGameOver)
            transform.Translate(Vector2.left * scrollSpeed * Time.deltaTime);
        
    }
}
