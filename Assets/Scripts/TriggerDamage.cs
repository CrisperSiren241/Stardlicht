using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDamage : MonoBehaviour
{
    public int damageAmount = 20; // Количество урона, наносимого ловушкой

    private void OnTriggerEnter(Collider other)
    {
        // Проверка, является ли объект, с которым столкнулась ловушка, персонажем
        CharacterStats characterStats = other.GetComponent<CharacterStats>();
        if (characterStats != null)
        {
            // Наносим урон персонажу
            characterStats.TakeDamage(damageAmount);
            Debug.Log("Персонаж получил урон от ловушки: " + damageAmount);
        }
    }
}
