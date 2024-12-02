using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // TextMeshPro namespace

public class ARQuestionManager : MonoBehaviour
{
    // Inspector'da görünecek şekilde public değişkenler
    public TextMeshProUGUI questionText; // TextMeshPro bileşeni
    public TMP_InputField answerText; // TextMeshPro bileşeni
    public Button nextButton;
    public Button previousButton;

    // Sorular dizisi
    private string[] questions = new string[]
    {
        "Hem çemberin hem dikdörtgenin içinde yer alan, ancak silindirin dışında kalan sayılar hangileridir?",
        "Sadece silindirin içinde yer alan ve başka hiçbir şeklin içinde olmayan sayılar hangileridir?",
        "Hem silindirin hem çemberin içinde yer alan, ancak dikdörtgenin dışında kalan sayılar hangileridir?",
        "Dikdörtgenin içinde yer alan, ancak çemberin ve silindirin dışında kalan sayılar hangileridir?",
        "Hangi sayılar sadece çemberin içinde yer alır ve diğer hiçbir şeklin içinde değildir?",
        "Hangi sayı tüm şekillerin (çember, dikdörtgen, silindir) içinde yer almaktadır?",
        "Sadece çember ile silindirin kesişiminde yer alan, ancak dikdörtgende bulunmayan sayılar hangileridir?",
        "Silindirin dışında kalan, ancak hem dikdörtgenin hem çemberin içinde yer alan sayılar hangileridir?",
        "Çemberin içinde olup silindirin ve dikdörtgenin dışında kalan sayılar hangileridir?",
        "Silindirin içinde yer alan, ancak çemberin ve dikdörtgenin dışında kalan sayılar hangileridir?",
        "Hem silindirin hem dikdörtgenin içinde yer alan, ancak çemberin dışında kalan sayılar hangileridir?",
        "Çember ve dikdörtgenin kesişiminde yer alan, ancak silindirin dışında kalan sayılar hangileridir?"
    };

    // Cevaplar dizisi
    private string[] answers = new string[]
    {
        "Örnek: 3, 7 (Varsayımsal sayılar)",
        "Örnek: 5, 9 (Varsayımsal sayılar)",
        "Örnek: 2, 8 (Varsayımsal sayılar)",
        "Örnek: 1, 4 (Varsayımsal sayılar)",
        "Örnek: 6 (Varsayımsal sayı)",
        "Örnek: 10 (Varsayımsal sayı)",
        "Örnek: 11 (Varsayımsal sayı)",
        "Örnek: 12 (Varsayımsal sayı)",
        "Örnek: 13 (Varsayımsal sayı)",
        "Örnek: 14 (Varsayımsal sayı)",
        "Örnek: 15 (Varsayımsal sayı)",
        "Örnek: 16 (Varsayımsal sayı)"
    };

    // Geçerli soru indeksi
    private int currentQuestionIndex = 0;

    // Etkinleştirildiğinde ilk soruyu güncelle
    void OnEnable()
    {
        UpdateQuestion();
    }

    // Başlangıçta yapılacak işlemler
    void Start()
    {
        // İlk soruyu güncelle
        UpdateQuestion();

        // Buton tıklama olaylarını ekle
        if (nextButton != null)
            nextButton.onClick.AddListener(NextQuestion);
        
        if (previousButton != null)
            previousButton.onClick.AddListener(PreviousQuestion);
    }

    // Soruyu güncelleme metodu
    void UpdateQuestion()
    {
        // Null kontrolü yaparak metin ve cevapları güncelle
        if (questionText != null)
            questionText.text = questions[currentQuestionIndex];
        
        if (answerText != null)
            answerText.text = answers[currentQuestionIndex];

        // Buton etkileşimlerini güncelle
        if (previousButton != null)
            previousButton.interactable = currentQuestionIndex > 0;
        
        if (nextButton != null)
            nextButton.interactable = currentQuestionIndex < questions.Length - 1;
    }

    // Sonraki soruya geçme metodu
    public void NextQuestion()
    {
        if (currentQuestionIndex < questions.Length - 1)
        {
            currentQuestionIndex++;
            UpdateQuestion();
        }
    }

    // Önceki soruya dönme metodu
    public void PreviousQuestion()
    {
        if (currentQuestionIndex > 0)
        {
            currentQuestionIndex--;
            UpdateQuestion();
        }
    }
}