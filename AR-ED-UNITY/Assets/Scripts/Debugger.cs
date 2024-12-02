using UnityEngine;
using Vuforia;

public class Debugger : MonoBehaviour
{
    // ObserverBehaviour bileşenine referans
    private ObserverBehaviour observerBehaviour;

    void Start()
    {
        // GameObject'teki ObserverBehaviour bileşenini al
        observerBehaviour = GetComponent<ObserverBehaviour>();
        
        if (observerBehaviour != null)
        {
            // Target durumu değiştiğinde çağrılacak metoda abone ol
            observerBehaviour.OnTargetStatusChanged += OnTargetStatusChanged;
        }
        else
        {
            Debug.LogError("ObserverBehaviour bulunamadı! Lütfen bu scripti bir Image Target objesine bağlayın.");
        }
    }

    // Target durumu değiştiğinde çağrılan metod
    private void OnTargetStatusChanged(ObserverBehaviour behaviour, TargetStatus targetStatus)
    {
        // Target algılandıysa (TRAKED veya EXTENDED_TRACKED durumu)
        if (targetStatus.Status == Status.TRACKED || targetStatus.Status == Status.EXTENDED_TRACKED)
        {
            Debug.Log($"Target {behaviour.TargetName} algılandı!");
        }
    }

    void OnDestroy()
    {
        if (observerBehaviour != null)
        {
            observerBehaviour.OnTargetStatusChanged -= OnTargetStatusChanged;
        }
    }
}