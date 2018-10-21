using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputHandler : MonoBehaviour {

    [SerializeField] private InputField answer;
    [SerializeField] private Text question;

    public List<char> operators;
    private int operation, num1, num2;
	// Use this for initialization
	void Start () {
        operation = GameManager.instance.operationChosen;
        //operators.Add('+');
        //operators.Add('-');
        //operators.Add('*');
        //operators.Add('/');
    }
	
	// Update is called once per frame
	void Update () {
        if (GameManager.instance.askQuestion)
            AskQuestion();
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
}
