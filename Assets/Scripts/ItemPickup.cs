using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour, InterfaceScript.IInteractable
{
    public Item item;  // Ссылка на предмет, который будет подобран

    // Реализация метода Interact() из интерфейса IInteractable
    public void Interact()
    {
        PickUp();  // Подбор предмета
    }

    // Реализация метода OnFocused() из интерфейса IInteractable
    public void OnFocused()
    {
        Debug.Log("Объект в фокусе: " + item.itemName);
    }

    // Реализация метода OnDefocused() из интерфейса IInteractable
    public void OnDefocused()
    {
        Debug.Log("Объект вне фокуса");
    }

    // Реализация метода GetTransform() из интерфейса IInteractable
    public Transform GetTransform()
    {
        return transform;
    }

    // Логика подбора предмета
    void PickUp()
    {
        Debug.Log("Подобран предмет: " + item.name);

        Inventory inventory = FindObjectOfType<Inventory>();  // Поиск инвентаря в сцене

        // Добавление предмета в инвентарь
        if (inventory != null && inventory.Add(item))
        {
            Destroy(gameObject);  // Удаление объекта после подбора
        }
    }


}
