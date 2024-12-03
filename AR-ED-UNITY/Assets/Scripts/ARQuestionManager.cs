using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // TextMeshPro namespace

public class ARQuestionManager : MonoBehaviour
{
    public TextMeshProUGUI questionText;  
    public TMP_InputField answerText;  
    public Button nextButton;
    public Button previousButton;

    public TextMeshProUGUI timerText; 
    public TextMeshProUGUI scoreText; 
    public TextMeshProUGUI endGameText; 

    // Sorular dizisi
    private string[] questions = new string[]
{
    "Hem elmasın hem dikdörtgenin içinde yer alan, ancak silindirin dışında kalan sayılar hangileridir?",
    "Sadece silindirin içinde yer alan ve başka hiçbir şeklin içinde olmayan sayılar hangileridir?",
    "Hem silindirin hem kürenin içinde yer alan, ancak dikdörtgenin dışında kalan sayılar hangileridir?",
    "Dikdörtgenin içinde yer alan, ancak kürenin ve silindirin dışında kalan sayılar hangileridir?",
    "Hangi sayılar sadece kürenin içinde yer alır ve diğer hiçbir şeklin içinde değildir?",
    "Hangi sayı tüm şekillerin (kürenin, dikdörtgenin, silindirin) içinde yer almaktadır?",
    "Sadece küre ile silindirin kesişiminde yer alan, ancak dikdörtgende bulunmayan sayılar hangileridir?",
    "Silindirin dışında kalan, ancak hem dikdörtgenin hem kürenin içinde yer alan sayılar hangileridir?",
    "Kürenin içinde olup silindirin ve dikdörtgenin dışında kalan sayılar hangileridir?",
    "Silindirin içinde yer alan, ancak kürenin ve dikdörtgenin dışında kalan sayılar hangileridir?",
    "Hem silindirin hem dikdörtgenin içinde yer alan, ancak kürenin dışında kalan sayılar hangileridir?",
    "Küre ve dikdörtgenin kesişiminde yer alan, ancak silindirin dışında kalan sayılar hangileridir?"
};

private string[] answers = new string[]
{
    "11", // Hem elmasın hem dikdörtgenin içinde yer alan, ancak silindirin dışında kalan sayılar.
    "10", // Sadece silindirin içinde yer alan ve başka hiçbir şeklin içinde olmayan sayılar.
    "8",  // Hem silindirin hem kürenin içinde yer alan, ancak dikdörtgenin dışında kalan sayılar.
    "11", // Dikdörtgenin içinde yer alan, ancak kürenin ve silindirin dışında kalan sayılar.
    "15", // Hangi sayılar sadece kürenin içinde yer alır ve diğer hiçbir şeklin içinde değildir.
    "8",  // Hangi sayı tüm şekillerin (kürenin, dikdörtgenin, silindirin) içinde yer almaktadır.
    "0",  // Sadece küre ile silindirin kesişiminde yer alan, ancak dikdörtgende bulunmayan sayılar.
    "11", // Silindirin dışında kalan, ancak hem dikdörtgenin hem kürenin içinde yer alan sayılar.
    "15", // Kürenin içinde olup silindirin ve dikdörtgenin dışında kalan sayılar.
    "10", // Silindirin içinde yer alan, ancak kürenin ve dikdörtgenin dışında kalan sayılar.
    "0",  // Hem silindirin hem dikdörtgenin içinde yer alan, ancak kürenin dışında kalan sayılar.
    "11"  // Küre ve dikdörtgenin kesişiminde yer alan, ancak silindirin dışında kalan sayılar.
};

    private int currentQuestionIndex = 0;

    private int score = 0;
    private bool gameStarted = false;  

    private float gameDuration = 300f; // 5 dakika
    private float remainingTime;

    void Start()
    {
        remainingTime = gameDuration;

        if (nextButton != null) nextButton.gameObject.SetActive(false);
        if (answerText != null) answerText.gameObject.SetActive(false);
        if (questionText != null) questionText.gameObject.SetActive(false);
        if (scoreText != null) scoreText.text = "Puan: 0";
        if (endGameText != null) endGameText.gameObject.SetActive(false);
        if (timerText != null) timerText.gameObject.SetActive(false);

        if (previousButton != null)
        {
            previousButton.GetComponentInChildren<TextMeshProUGUI>().text = "Başla";
            previousButton.onClick.AddListener(StartGame);
        }
    }

    
    void Update()
    {
        if (gameStarted)
        {

            timerText.gameObject.SetActive(true);
            if (remainingTime > 0)
            {
                remainingTime -= Time.deltaTime;
                UpdateTimerText();
            }
            else
            {
                EndGame();
            }
        }
    }

    void UpdateQuestion()
    {
        if (questionText != null)
            questionText.text = questions[currentQuestionIndex];


        if (previousButton != null)
            previousButton.interactable = currentQuestionIndex > 0;

        if (nextButton != null)
            nextButton.interactable = currentQuestionIndex < questions.Length;
    }

    public void StartGame()
    {
        if (!gameStarted)
        {
            gameStarted = true;

            if (previousButton != null)
            {
                previousButton.GetComponentInChildren<TextMeshProUGUI>().text = "Geri";
                previousButton.onClick.RemoveListener(StartGame);
                previousButton.onClick.AddListener(PreviousQuestion);
                
            }

            if (nextButton != null) {
                nextButton.gameObject.SetActive(true);
                nextButton.onClick.AddListener(CheckAnswerAndNext);
            }
            if (answerText != null) answerText.gameObject.SetActive(true);

            questionText.gameObject.SetActive(true);
            UpdateQuestion();
        }
    }

    void EndGame()
    {
        gameStarted = false;

        if (questionText != null)
            questionText.gameObject.SetActive(false);

        if (answerText != null)
            answerText.gameObject.SetActive(false);

        if (nextButton != null)
            nextButton.gameObject.SetActive(false);

        if (previousButton != null)
            previousButton.gameObject.SetActive(false);

        if (endGameText != null)
        {


            float timeUsed = gameDuration - remainingTime;
            int minutesUsed = Mathf.FloorToInt(timeUsed / 60);
            int secondsUsed = Mathf.FloorToInt(timeUsed % 60);

            endGameText.text = $"Oyun Bitti!\n" +
                           $"Toplam Puan: {score}\n" +
                           $"Toplam Süre: 5:00\n" +
                           $"Kalan Süre: {Mathf.FloorToInt(remainingTime / 60):00}:{Mathf.FloorToInt(remainingTime % 60):00}\n" +
                           $"Kullanılan Süre: {minutesUsed:00}:{secondsUsed:00}";
            endGameText.gameObject.SetActive(true);
        }
    }

    public void CheckAnswerAndNext()
    {
        string playerAnswer = answerText.text.Trim();
        answerText.text = "";

        string correctAnswer = answers[currentQuestionIndex];

        if (playerAnswer == correctAnswer)
        {
            score += 10;
            if (scoreText != null)
                scoreText.text = $"Puan: {score}";
        }

        NextQuestion();
    }

    void NextQuestion()
    {
        if (currentQuestionIndex < questions.Length - 1)
        {
            currentQuestionIndex++;
            UpdateQuestion();
        }
        else
        {
            EndGame();
        }
    }

    public void PreviousQuestion()
    {
        if (currentQuestionIndex > 0)
        {
            currentQuestionIndex--;
            UpdateQuestion();
        }
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
}