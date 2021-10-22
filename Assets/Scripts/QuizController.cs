using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuizController : MonoBehaviour
{
    [SerializeField] QuestionSO[] questions;
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] GameObject[] AnswerButtons;

    [SerializeField] Sprite correctAnswerSprite;
    [SerializeField] Sprite defaultAnswerSprite;

    const string CORRECT_ANSWER = "Correct answer!";
    const string INCORRECT_ANSWER = "Incorrect. The correct answer is: ";
    const float NEW_QUESTION_FETCH_DELAY = 1.5f;

    private int currentQuestionIndex = 0;
    private QuestionSO questionObj;

    private void Start()
    {
        FetchNewQuestion();
    }

    public void HandleAnswerBtnClick(int selectedBtnIdx)
    {
        if (questionObj.CheckAnswer(selectedBtnIdx))
        {
            questionText.text = CORRECT_ANSWER;
            Image SelectedBtnSprite = AnswerButtons[selectedBtnIdx].GetComponent<Image>();
            SelectedBtnSprite.sprite = correctAnswerSprite;
        }
        else
        {
            questionText.text = INCORRECT_ANSWER + questionObj.GetCorrectAnswer();
        }

        SetBtnInteraction(false);

        // Do nothing else if detected last question
        if (currentQuestionIndex < questions.Length - 1)
        {
            currentQuestionIndex++;
            Invoke("FetchNewQuestion", NEW_QUESTION_FETCH_DELAY);
        }
    }

    private void FetchNewQuestion()
    {
        questionObj = questions[currentQuestionIndex];
        questionText.text = questionObj.GetQuestion();

        string[] answers = questionObj.GetAnswers();

        for (int i = 0; i < answers.Length; i++)
        {
            TextMeshProUGUI answerOption = AnswerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            answerOption.text = answers[i];
        }

        SetBtnInteraction(true);
        ResetBtnSprite();
    }

    private void ResetBtnSprite()
    {
        for (int i = 0; i < AnswerButtons.Length; i++)
        {
            AnswerButtons[i].GetComponent<Image>().sprite = defaultAnswerSprite;
        }
    }

    public void SetBtnInteraction(bool state)
    {
        for (int i = 0; i < AnswerButtons.Length; i++)
        {
            AnswerButtons[i].GetComponent<Button>().interactable = state;
        }
    }
}
