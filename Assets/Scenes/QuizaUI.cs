using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizaUI : MonoBehaviour
{
    [SerializeField] private QuizManager1 quizManager1;
    [SerializeField] private Text questionText;
    [SerializeField] private Image questionImage;
    [SerializeField] private UnityEngine.Video.VideoPlayer questionVideo;
    [SerializeField] private AudioSource questionAudio;
    [SerializeField] private List<Button> options;
    [SerializeField] private Color correctCol, wrongCol, normalCol;

    private Question question;
    private bool answered;
    private float audioLength;

    // Start is called before the first frame update
    void Awake()
    {
        for(int i = 0; i < options.Count; i++)
        {
            Button localBtn = options[i];
            localBtn.onClick.AddListener(()=>OnClick(localBtn));
        }
    }

    public void SetQuestion(Question question)
    {
        this.question = question;

        switch (question.questionType)
            {
                case QuestionType.TEXT:
                    questionImage.transform.parent.gameObject.SetActive(false);   //deactivate image holder
                    break;
                    
                case QuestionType.IMAGE:
                    ImageHolder();
                    questionImage.transform.gameObject.SetActive(true);           //activate questionImg

                    questionImage.sprite = question.questionImage;                //set the image sprite
                    break;
                    
                case QuestionType.AUDIO:
                    ImageHolder();
                    questionAudio.transform.gameObject.SetActive(true);           
                    
                    audioLength = question.audioClip.length;                    //set audio clip
                    StartCoroutine(PlayAudio());                                //start Coroutine
                    break;

                case QuestionType.VIDEO:
                    ImageHolder();
                    questionVideo.transform.gameObject.SetActive(true);           //activate questionImg

                    questionVideo.clip = question.videoClip;                    //set video clip
                    questionVideo.Play();                                       //play video
                    break;
            }

        questionText.text = question.questionInfo;                      //set the question text

        //suffle the list of options
        List<string> ansOptions = ShuffleList.ShuffleListItems<string>(question.options);

        //assign options to respective option buttons
        for (int i = 0; i < options.Count; i++)
        {
            //set the child text
            options[i].GetComponentInChildren<Text>().text = ansOptions[i];
            options[i].name = ansOptions[i];    //set the name of button
            options[i].image.color = normalCol; //set color of button to normal
        }

        answered = false; 
    }

    void ImageHolder()
    {
        questionImage.transform.parent.gameObject.SetActive(true);
        questionVideo.transform.gameObject.SetActive(false);        //deactivate questionVideo
        questionImage.transform.gameObject.SetActive(false);           //activate questionImg
        questionAudio.transform.gameObject.SetActive(false);        //deactivate questionAudio
    }

    IEnumerator PlayAudio()
    {
        //if questionType is audio
        if (question.questionType == QuestionType.AUDIO)
        {
            //PlayOneShot
            questionAudio.PlayOneShot(question.audioClip);
            //wait for few seconds
            yield return new WaitForSeconds(audioLength + 0.5f);
            //play again
            StartCoroutine(PlayAudio());
        }
        else //if questionType is not audio
        {
            //stop the Coroutine
            StopCoroutine(PlayAudio());
            //return null
            yield return null;
        }
    }

    private void OnClick(Button btn)
    {
        if (!answered)
        {
            answered = true;
            bool val = quizManager1.Answer(btn.name);

            if(val)
            {
                btn.image.color = correctCol;
            }
            else
            {
                btn.image.color = wrongCol;
            }
        }
    }
}