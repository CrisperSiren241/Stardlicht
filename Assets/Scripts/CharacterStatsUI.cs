using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CharacterStatsUI : MonoBehaviour
{
    // Start is called before the first frame update
    public CharacterStats characterStats; // Ссылка на скрипт характеристик

    public Slider healthSlider;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI flaskCountText;


    void Start()
    {
        // Устанавливаем максимальные значения для слайдеров
        healthSlider.maxValue = characterStats.maxHealth;
        // Начальное обновление UI
        UpdateUI();

    }

    // Update is called once per frame
    void Update()
    {
        UpdateUI();
    }


    void UpdateUI()
    {
        // Обновление значений здоровья
        healthSlider.value = characterStats.currentHealth;
        healthText.text = characterStats.currentHealth + " / " + characterStats.maxHealth;

        if (flaskCountText != null)
        {
            flaskCountText.text = "Фляги: " + characterStats.currentFlasks; // Обновляем текст в Canvas
        }
    }

}
