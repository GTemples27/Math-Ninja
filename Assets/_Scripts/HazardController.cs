using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardController : MonoBehaviour {

    [SerializeField] private float scrollSpeed = 10;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.isGameOver)
            transform.Translate(Vector2.left * scrollSpeed * Time.deltaTime);
    }
}
