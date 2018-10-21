using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour {
    public static GameManager instance = null;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public InputField answer;
    public bool isGameOver;
    public bool askQuestion;
    //0 = add, 1 = sub, 2 = mult, 3 = div
    public int operationChosen;

    //Button Functions
    public void ChangeScene(int scene)
    {
        SceneManager.LoadSceneAsync(scene);
    }

    public void ChooseOperation(int choice)
    {
        operationChosen = choice;
        ChangeScene(1);
    }

    //turns an input field to an integer [can be used in other files]
    //outputs the integer of the input field
    public static int GetResponse(InputField Answer)
    {
        int input = 0;
        //instance of correct answer = 0
        if (Answer.text.CompareTo("") == 0)
        {
            input = -1;
        }
        else { 
            Int32.TryParse(Answer.text, out input);
            //Debug.Log("Answer:" + input.ToString());
        }
        return input;
    }
}
