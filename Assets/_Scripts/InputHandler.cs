using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputHandler : MonoBehaviour {

    [SerializeField] private InputField answer;
    [SerializeField] private Text question;

    public List<char> operators;
    private int operation, num1, num2;
    public static bool questionAsked;
	// Use this for initialization
	void Start () {
        operation = GameManager.instance.operationChosen;
        answer.gameObject.SetActive(false);
        //operators.Add('+');
        //operators.Add('-');
        //operators.Add('*');
        //operators.Add('/');
    }
	
	// Update is called once per frame
	void Update () {
        //if it can ask a question, it will
        if (GameManager.instance.askQuestion && !GameManager.instance.isGameOver) { 
            AskQuestion();
            answer.gameObject.SetActive(true);
            questionAsked = true;
        }

        //after question asked, it will check if the answer is correct
        if (questionAsked)
        {
            GetAnswer();
        }
    }


    private void GetAnswer()
    {
        answer.gameObject.SetActive(true);
        /*int answerInt = 0;
        while (!Input.GetKeyDown("enter"))
        {
            answerInt = GameManager.instance.GetResponse();
        }*/

        //it just compares the int to the correct answer
        //if it is correct, the warp effect takes place and time is reset
        if (GameManager.GetResponse(answer) == getCorrectAnswer()){
            answer.gameObject.SetActive(false);
            questionAsked = false;
            answer.text = "";
            PlayerController.timeSlow = false;
            Debug.Log("time back");

            // try to work out warping here
            PlayerController.isWarping = true;
            PlayerController.warpingTimer = 0;
        }
    }

    private void AskQuestion()
    {
        switch (operation)
        {
            case 0:
                num1 = Random.Range(0, 51);
                num2 = Random.Range(0, 51);
                break;
            case 1:
                num1 = Random.Range(0, 51);
                num2 = Random.Range(0, num1 + 1);
                break;
            case 2:
                num1 = Random.Range(0, 13);
                num2 = Random.Range(0, 13);
                break;
            default:
                num1 = Random.Range(1, 13);
                num2 = Random.Range(1, 13);
                num1 *= num2;
                break;
        }
        question.text = num1.ToString() + operators[operation] + num2.ToString();
        GameManager.instance.askQuestion = false;
    }

    //gets the correct answer to the operation according to the operation used
    //returns the correct answer as an int
    private int getCorrectAnswer()
    {
        int correctAnswer = -1;
        switch (operation)
        {
            case 0:
                correctAnswer = num1 + num2;
                break;
            case 1:
                correctAnswer = num1 - num2;
                break;
            case 2:
                correctAnswer = num1 * num2;
                break;
            default:
                correctAnswer = num1 / num2;
                break;
        }
        return correctAnswer;
    }


}
