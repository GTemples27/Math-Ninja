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

    public int GetResponse()
    {
        int input = 0;
        Int32.TryParse(answer.text, out input);
        return input;
    }
}
