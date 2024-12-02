using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    public Image icon;  // Иконка предмета
    public TextMeshProUGUI itemNameText;  // Ссылка на TextMeshPro элемент для отображения названия
    public Button removeButton;  // Кнопка для удаления предмета из инвентаря

    private Item item;  // Предмет, который находится в слоте
    private Inventory inventory;  // Ссылка на инвентарь

     void Start()
    {
        inventory = FindObjectOfType<Inventory>();
        removeButton.onClick.AddListener(OnRemoveButton);  // Привязываем метод к кнопке
    }

    // Добавить предмет в слот
    public void AddItem(Item newItem)
    {
        item = newItem;

        icon.sprite = item.icon;
        icon.enabled = true;

        itemNameText.text = item.itemName;  // Отобразить название предмета
        itemNameText.enabled = true;
    }

    // Очистить слот
    public void ClearSlot()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;

        itemNameText.text = "";
        itemNameText.enabled = false;
    }

    public void OnRemoveButton()
    {
        if (item != null && inventory != null)
        {
            inventory.Remove(item);  // Удаляем предмет из инвентаря
            ClearSlot();  // Очищаем слот в интерфейсе
        }
    }
}
