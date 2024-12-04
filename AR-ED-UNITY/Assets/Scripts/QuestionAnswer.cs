using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // TextMeshPro namespace

public class QuestionAnswer : MonoBehaviour
{

    public Button questionButton;

    public TextMeshProUGUI timerText; 
    public TextMeshProUGUI scoreText; 

    public TextMeshProUGUI gameText;
   

    private QuestionAnswerManager questionAnswerManager;
    private string selectedKey = "";
    private string selectedAnswer = "";

    private string selectedQuestionWithKey = "";


    private List<string> questions = new List<string>();  
    private Dictionary<string, string> questionAnswerPairs = new Dictionary<string, string>();

    private Dictionary<string, string> questionObjectPairs = new Dictionary<string, string>();

    private bool gameStarted = false;

    private int score = 0;    

    private float gameDuration = 300f; // 5 dakika
    private float remainingTime;
    void Start()
    {
        remainingTime = gameDuration;


        if (scoreText != null) scoreText.text = "Puan: 0";
        if (timerText != null) timerText.gameObject.SetActive(false);
        if (scoreText != null) scoreText.gameObject.SetActive(false);
        if (questionButton != null) questionButton.onClick.AddListener(StartGame);
        

        questionAnswerPairs.Add("q_1", "225");
        questionAnswerPairs.Add("q_2", "256");
        questionAnswerPairs.Add("q_3", "110");
        questionAnswerPairs.Add("q_4", "110");
        questionAnswerPairs.Add("q_5", "121");
        questionAnswerPairs.Add("q_6", "43");

        questionObjectPairs.Add("q_1", "T");
        questionObjectPairs.Add("q_2", "K");
        questionObjectPairs.Add("q_3", "A");
        questionObjectPairs.Add("q_4", "O");
        questionObjectPairs.Add("q_5", "U");
        questionObjectPairs.Add("q_6", "G");
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStarted)
        {
            remainingTime -= Time.deltaTime;
            UpdateTimerText();

            if (remainingTime <= 0)
            {
                //EndGame();
            }
        }
        
    }

    void StartGame()
    {
        if (!gameStarted)
        {
            gameStarted = true;

            if (questionButton != null)
            {
                questionButton.GetComponentInChildren<TextMeshProUGUI>().text = "Kontol Et";
                questionButton.onClick.RemoveListener(StartGame);
                questionButton.onClick.AddListener(CheckQuestion);
                
            }

        }
    }

    void CheckQuestion()
    {

        if (questionAnswerPairs.TryGetValue(selectedKey, out string value))
        {
            if(selectedAnswer == value)
            {
                score += 10;
                scoreText.text = "Puan: " + score;
                gameText.text = "Doğru Cevap!";
            }
            else
            {
                gameText.text = "Yanlış Cevap!";
            }
            ResetSelection();
        }
    }

    private void ResetSelection()
    {
        selectedQuestionWithKey = "*";
        selectedAnswer = "*";
    }
    

    void UpdateTimerText()
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(remainingTime / 60);
            int seconds = Mathf.FloorToInt(remainingTime % 60);
            timerText.text = $"{minutes:00}:{seconds:00}";
        }
    }

    /*void EndGame()
    {
        gameStarted = false;

        if (timerText != null)
            timerText.gameObject.SetActive(false);

        if (scoreText != null)
            scoreText.gameObject.SetActive(false);

        if (questionButton != null)
            questionButton.gameObject.SetActive(false);

        if (gameText != null)
        {
            gameText.text = $"Oyun Bitti!\n" +
                           $"Toplam Puan: {score}\n" +
                           $"Toplam Süre: 5:00\n" +
                           $"Kalan Süre: 0:00\n" +
                           $"Kullanılan Süre: 5:00";
            gameText.gameObject.SetActive(true);
        }
    }*/

    public string getSelectedQuestion()
    {
        return selectedQuestionWithKey;
    }

    public string getSelectedAnswer()
    {
        return selectedAnswer;
    }

    public void setSelectedQuestion(string question)
    {

        selectedKey = question;
        Debug.Log("Selected Answer: " + selectedKey);
        selectedQuestionWithKey = questionObjectPairs[question];

        string result = "Harf: " + selectedQuestionWithKey + " | Cevap: " + selectedAnswer;

        updateGameText(result);

        Debug.Log(result);
    }

    public void setSelectedAnswer(string answer)
    {
        selectedAnswer = answer;

        string result = "Harf: " + selectedQuestionWithKey + " | Cevap: " + answer;

        gameText.text = result;

        Debug.Log(result);

    }

    private void updateGameText(string result)
    {
        gameText.text = result;
    }
}
