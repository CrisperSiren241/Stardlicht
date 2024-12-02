using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static InterfaceScript;

public class CollectibleItem : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        QuestManager.Instance.OnItemCollected();
        Destroy(gameObject);
    }

    public void OnFocused() { }
    public void OnDefocused() { }
    public Transform GetTransform() { return transform; }
}
