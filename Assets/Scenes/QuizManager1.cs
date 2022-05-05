using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizManager1 : MonoBehaviour
{
    [SerializeField] private QuizaUI quizaUI;
    [SerializeField] private QuizData quizData;

    private List<Question> questions;
    private Question selectQuestion;

    // Start is called before the first frame update
    void Start()
    {
        questions = quizData.questions;
        
        SelectQuestion();
    }

    void SelectQuestion()
    {
        int val = Random.Range(0, questions.Count);
        selectQuestion = questions[val];

        quizaUI.SetQuestion(selectQuestion);
    }

    public bool Answer(string answered)
    {
        bool correctAns = false;

        if(answered == selectQuestion.correctAns)
        {
            //Yes
            correctAns = true;
        }
        else
        {
            //No
        }

        Invoke("SelectQuestion", 0.4f);

        return correctAns;
    }
}

[System.Serializable]
public class Question
{
    public string questionInfo;         //question text
    public QuestionType questionType;   //type
    public Sprite questionImage;        //image for Image Type
    public AudioClip audioClip;         //audio for audio type
    public UnityEngine.Video.VideoClip videoClip;   //video for video type
    public List<string> options;        //options to select
    public string correctAns;           //correct option
}

[System.Serializable]
public enum QuestionType
{
    TEXT,
    IMAGE,
    AUDIO,
    VIDEO
}