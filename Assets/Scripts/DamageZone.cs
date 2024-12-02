using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    public int damagePerSecond = 5; // Урон, наносимый за каждую секунду
    private float damageInterval = 1.0f; // Интервал между нанесением урона
    private float damageTimer = 0f;

    private void OnTriggerStay(Collider other)
    {
        CharacterStats characterStats = other.GetComponent<CharacterStats>();
        if (characterStats != null)
        {
            // Проверяем, прошло ли достаточно времени, чтобы нанести урон
            damageTimer += Time.deltaTime;
            if (damageTimer >= damageInterval)
            {
                characterStats.TakeDamage(damagePerSecond);
                Debug.Log("Персонаж получает урон от зоны: " + damagePerSecond);
                damageTimer = 0f; // Сброс таймера
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Сброс таймера при выходе из зоны
        if (other.GetComponent<CharacterStats>() != null)
        {
            damageTimer = 0f;
        }
    }
}
