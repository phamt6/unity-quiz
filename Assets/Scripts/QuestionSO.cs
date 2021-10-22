using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Question", menuName = "Create New Question", order = 0)]
public class QuestionSO : ScriptableObject {

    [TextArea(2, 6)]
    [SerializeField] string question;
    [SerializeField] string[] answers;
    [SerializeField] int correctAnswerIndex = 0;

    public string GetQuestion(){
        return question;
    }

    public string[] GetAnswers(){
        return answers;
    }

    public bool CheckAnswer(int chosenIndex)
    {
        return chosenIndex == correctAnswerIndex;
    }

    public int GetCorrectAnswer()
    {
        return correctAnswerIndex;
    }
}
