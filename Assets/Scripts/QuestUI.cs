using UnityEngine;
using TMPro;

public class QuestUI : MonoBehaviour
{
    // Ссылка на QuestManager
    public QuestManager questManager;

    // UI элементы для отображения квестов
    public TextMeshProUGUI questTitleText;
    public TextMeshProUGUI questDescriptionText;
    public TextMeshProUGUI questProgressText;

    void Start()
    {
        // Получаем ссылку на QuestManager
        questManager = FindObjectOfType<QuestManager>();
    }

    void Update()
    {
        // Проверяем, есть ли квесты в списке
        if (questManager != null && questManager.quests.Count > 0)
        {
            Quest currentQuest = questManager.quests[0];  // Выбираем первый квест из списка для отображения

            // Обновляем текстовые поля UI
            questTitleText.text = currentQuest.title;
            questDescriptionText.text = currentQuest.description;
            questProgressText.text = $"Прогресс: {currentQuest.goal.currentAmount}/{currentQuest.goal.requiredAmount}";
        }
        else
        {
            // Если квестов нет, очищаем UI
            questTitleText.text = "";
            questDescriptionText.text = "";
            questProgressText.text = "";
        }
    }
}
