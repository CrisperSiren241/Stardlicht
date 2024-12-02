using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance { get; private set; }

    // Список квестов в игре
    public List<Quest> quests = new List<Quest>();
    public TimerScript timerScript;

    // Переменные для UI квестов
    public TextMeshProUGUI questTitleText;
    public TextMeshProUGUI questDescriptionText;
    public TextMeshProUGUI questProgressText;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        // Обнуляем количество собранных предметов для всех квестов при запуске
        foreach (Quest quest in quests)
        {
            quest.goal.currentAmount = 0;
        }
        UpdateQuestUI(); // Обновляем UI после обнуления
    }

    // Метод для добавления нового квеста и обновления UI
    public void AddQuest(Quest quest)
    {
        quests.Add(quest);
        UpdateQuestUI(); // Обновляем UI после добавления квеста
    }

    // Метод для обработки сбора предметов
    public void OnItemCollected()
    {
        // Находим активный квест типа "Gather"
        Quest activeQuest = quests.Find(q => q.isActive && q.goal.goalType == QuestGoal.GoalType.Gather);
        if (activeQuest != null)
        {
            activeQuest.goal.currentAmount++; // Увеличиваем прогресс квеста
            UpdateQuestUI(); // Обновляем UI

            // Проверяем, достигнута ли цель
            if (activeQuest.goal.IsReached())
            {
                activeQuest.isActive = false;
                Debug.Log("Квест завершен! Вы получили " + activeQuest.experienceReward + " опыта.");
                timerScript.CompleteQuest();
                UpdateQuestUI(); // Обновляем UI после завершения квеста
            }
        }
    }

    // Метод для обновления UI с информацией о квесте
    public void UpdateQuestUI()
    {
        Quest activeQuest = quests.Find(q => q.isActive && q.goal.goalType == QuestGoal.GoalType.Gather);
        if (activeQuest != null)
        {
            questTitleText.text = activeQuest.title;
            questDescriptionText.text = activeQuest.description;
            questProgressText.text = "Собрано предметов: " + activeQuest.goal.currentAmount + "/" + activeQuest.goal.requiredAmount;
        }
        else
        {
            // Очищаем UI, если нет активного квеста
            questTitleText.text = "";
            questDescriptionText.text = "";
            questProgressText.text = "";
        }
    }
}
