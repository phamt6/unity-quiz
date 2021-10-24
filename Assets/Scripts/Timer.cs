using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    float timeToAnswerQuestion = 30f;
    float timeToShowCorrectAnswer = 1.5f;

    bool isAnsweringQuestion = false;
    bool showCorrectAnswer = false;

    float timerValue = 0;

    float fillFraction = 0;
    public void StartAnsweringNewQuestion()
    {
        timerValue = timeToAnswerQuestion;
        isAnsweringQuestion = true;
    }

    public void DisplayAnswer()
    {
        timerValue = timeToShowCorrectAnswer;
        isAnsweringQuestion = false;
        showCorrectAnswer = true;
    }

    // Update is called once per frame
    void Update()
    {
        timerValue -= Time.deltaTime;

        if (isAnsweringQuestion)
        {
            if (timerValue > 0)
            {
                fillFraction = timerValue / timeToAnswerQuestion;
            }
            else
            {
                isAnsweringQuestion = false;
                showCorrectAnswer = true;
                timerValue = timeToShowCorrectAnswer;
            }
        } else if (showCorrectAnswer)
        {
            if (timerValue > 0)
            {
                fillFraction = timerValue / timeToShowCorrectAnswer;
            } else
            {
                showCorrectAnswer = false;
            }
        } else
        {
            fillFraction = 1;
        }
    }

    public float GetFillFraction()
    {
        return fillFraction;
    }

    public float GetAnswerDisplayDelay()
    {
        return timeToShowCorrectAnswer;
    }
}
