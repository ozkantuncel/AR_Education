using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestionAnswerManager : MonoBehaviour
{
    private string selectedQuestion = "*";
    private string selectedAnswer = "*";

     private QuestionAnswer questionAnswerScript;


    // Metot: Soru ya da cevap objesine tıklandığında çalışır
    private void OnMouseDown()
    {
        // Tıklanan objeyi al
        GameObject clickedObject = gameObject;

        // Eğer obje bir soru ise
        if (clickedObject.CompareTag("Question"))
        {
            SelectQuestion(clickedObject);
        }
        // Eğer obje bir cevap ise
        else if (clickedObject.CompareTag("Answer"))
        {
            SelectAnswer(clickedObject);
        }
    }

    void Start()
    {
       questionAnswerScript = FindObjectOfType<QuestionAnswer>();
        
        if (questionAnswerScript == null)
        {
            Debug.LogError("QuestionAnswer script not found in the scene!");
        }
    }

    // Soru seçildiğinde çağrılır
    private void SelectQuestion(GameObject questionObject)
    {
        selectedQuestion = questionObject.name;
        questionAnswerScript.setSelectedQuestion(selectedQuestion);
    }

    // Cevap seçildiğinde çağrılır
    private void SelectAnswer(GameObject answerObject)
    {
        selectedAnswer = answerObject.name;

        questionAnswerScript.setSelectedAnswer(selectedAnswer);
    }
    

    
}
