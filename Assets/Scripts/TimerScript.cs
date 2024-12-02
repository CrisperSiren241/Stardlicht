using UnityEngine;
using TMPro;

public class TimerScript : MonoBehaviour
{
    public float timeLimit = 60f; // Время для выполнения квеста в секундах
    private float remainingTime;
    public TextMeshProUGUI timerText; // UI-элемент для отображения таймера

    private bool questCompleted = false; // Флаг завершения квеста

    void Start()
    {
        remainingTime = timeLimit; // Инициализация оставшегося времени
        UpdateTimerDisplay();
    }

    void Update()
    {
        if (!questCompleted && remainingTime > 0)
        {
            remainingTime -= Time.deltaTime; // Отсчет времени
            UpdateTimerDisplay();

            if (remainingTime <= 0)
            {
                remainingTime = 0;
                FailQuest();
            }
        }
    }

    private void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void CompleteQuest()
    {
        questCompleted = true;
        timerText.text = "Квест завершен!";
        // Дополнительная логика завершения квеста
    }

    private void FailQuest()
    {
        timerText.text = "Квест провален!";
        // Дополнительная логика для провала квеста
    }
}
