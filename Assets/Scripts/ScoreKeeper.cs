using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    int questionsSeen = 0;
    int correctAnswers = 0;
    public void IncrementCorrectAnswers()
    {
        correctAnswers++;
    }

    public int GetCorrectAnswers()
    {
        return correctAnswers;
    }

    public void IncrementQuestionSeen()
    {
        questionsSeen++;
    }

    public int GetQuestionSeen()
    {
        return questionsSeen;
    }

    public float CalculateScore()
    {
        return Mathf.RoundToInt(correctAnswers / (float)questionsSeen * 100) ;
    }
}
