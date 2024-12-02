using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static InterfaceScript;

public class PlayerInteraction : MonoBehaviour
{
    public Transform rightHandBone; // Ссылка на правую руку
    public KeyCode interactKey = KeyCode.E;  // Клавиша взаимодействия
    public float handReachDuration = 1.0f;   // Время движения руки
    private bool isHandMoving = false;       // Флаг движения руки

    private InterfaceScript.IInteractable currentFocus = null; // Текущий объект взаимодействия
    private Animator animator;  // Аниматор персонажа

    void Start()
    {
        animator = GetComponent<Animator>();
        rightHandBone = animator.GetBoneTransform(HumanBodyBones.RightHand);
    }

    void Update()
    {
        if (Input.GetKeyDown(interactKey) && currentFocus != null)
        {
            StartCoroutine(RemoveObject());  // Запуск корутины для удаления объекта
        }
    }

    private IEnumerator RemoveObject()
    {
        isHandMoving = true;  // Активация IK для руки

        yield return new WaitForSeconds(handReachDuration);  // Ждем, пока рука дойдет до объекта

        if (currentFocus != null)
        {
            currentFocus.Interact();  // Взаимодействие с объектом
            RemoveFocus();            // Убираем фокус
        }

        isHandMoving = false;  // Отключаем IK после завершения
    }

    private void SetFocus(IInteractable newFocus)
    {
        if (newFocus != currentFocus)  // Если новый фокус отличается от текущего
        {
            if (currentFocus != null)
            {
                currentFocus.OnDefocused();  // Если был предыдущий фокус, вызываем его завершение
            }

            currentFocus = newFocus;
            currentFocus.OnFocused();       // Уведомляем новый объект о фокусе
        }
    }

    private void RemoveFocus()
    {
        if (currentFocus != null)
        {
            currentFocus.OnDefocused();
            currentFocus = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var interactable = other.GetComponent<IInteractable>();
        if (interactable != null)
        {
            SetFocus(interactable);  // Устанавливаем фокус на предмет
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var interactable = other.GetComponent<IInteractable>();
        if (interactable != null && interactable == currentFocus)
        {
            RemoveFocus();
        }
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (currentFocus != null)
        {
            // Персонаж смотрит на объект
            animator.SetLookAtWeight(1f, 0f, 1f, 0.5f, 0.5f);
            animator.SetLookAtPosition(currentFocus.GetTransform().position);

            // Если рука движется, активируем IK
            if (isHandMoving)
            {
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
                animator.SetIKPosition(AvatarIKGoal.RightHand, currentFocus.GetTransform().position);
            }
            else
            {
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
            }
        }
        else
        {
            // Отключаем IK, если нет фокуса
            animator.SetLookAtWeight(0f);
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
        }
    }
}
