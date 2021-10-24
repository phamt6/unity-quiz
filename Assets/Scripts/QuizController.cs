using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuizController : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] QuestionSO[] questions;
    [SerializeField] TextMeshProUGUI questionText;

    [Header("Answer Buttons")]
    [SerializeField] GameObject[] AnswerButtons;

    [Header("Images")]
    [SerializeField] Sprite correctAnswerSprite;
    [SerializeField] Sprite defaultAnswerSprite;

    [Header("Timer")]
    [SerializeField] Image timer;

    [Header("Score")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    const string CORRECT_ANSWER = "Correct answer!";
    const string INCORRECT_ANSWER = "Incorrect. The correct answer is: ";

    private int currentQuestionIndex = 0;
    private QuestionSO questionObj;
    private Timer timerObj;

    private void Start()
    {
        timerObj = FindObjectOfType<Timer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        FetchNewQuestion();
    }

    public void HandleAnswerBtnClick(int selectedBtnIdx)
    {
        if (questionObj.CheckAnswer(selectedBtnIdx))
        {
            questionText.text = CORRECT_ANSWER;
            Image SelectedBtnSprite = AnswerButtons[selectedBtnIdx].GetComponent<Image>();
            SelectedBtnSprite.sprite = correctAnswerSprite;
            scoreKeeper.IncrementCorrectAnswers();
        }
        else
        {
            questionText.text = INCORRECT_ANSWER + questionObj.GetCorrectAnswer();
        }

        scoreText.text = "Score: " + scoreKeeper.CalculateScore() + "%";

        SetBtnInteraction(false);
        timerObj.DisplayAnswer();

        // Do nothing else if detected last question
        if (currentQuestionIndex < questions.Length - 1)
        {
            currentQuestionIndex++;
            Invoke("FetchNewQuestion", timerObj.GetAnswerDisplayDelay());
        }
    }

    private void FetchNewQuestion()
    {
        questionObj = questions[currentQuestionIndex];
        questionText.text = questionObj.GetQuestion();
        scoreKeeper.IncrementQuestionSeen();

        string[] answers = questionObj.GetAnswers();

        for (int i = 0; i < answers.Length; i++)
        {
            TextMeshProUGUI answerOption = AnswerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            answerOption.text = answers[i];
        }

        SetBtnInteraction(true);
        ResetBtnSprite();

        timerObj.StartAnsweringNewQuestion();
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

    void Update()
    {
        timer.fillAmount = timerObj.GetFillFraction();
    }
}
