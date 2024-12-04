using UnityEngine;
using UnityEngine.SceneManagement;

public class MainScreen : MonoBehaviour
{
    // İlk sahneye geçiş yapan metod
    public void LoadScene1()
    {
        SceneManager.LoadScene("SampleScene");
    }

    // İkinci sahneye geçiş yapan metod
    public void LoadScene2()
    {
        SceneManager.LoadScene("Cow_X");
    }

    public void QuitApplication()
    {
        // Oyun editörde çalışıyorsa
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            // Derlenmiş uygulamada
            Application.Quit();
        #endif
    }
}