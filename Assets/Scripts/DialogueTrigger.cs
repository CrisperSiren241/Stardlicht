using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public PlayerMovement playerMovement;
    private bool hasBeenTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TriggerDialogue();
        }
    }

public void TriggerDialogue() 
    { 
        DialogueManager dialogueManager = FindObjectOfType<DialogueManager>(); 
 
        if (!hasBeenTriggered) 
        { 
            dialogueManager.StartDialogue(dialogue); // Начало полного диалога 
            hasBeenTriggered = true; 
        } 
        else 
        { 
            string lastSentence = dialogue.sentences[dialogue.sentences.Length - 1]; 
            dialogueManager.ShowShortMessage(lastSentence, dialogue); // Отображение последней реплики 
        } 
    }

    void OnTriggerExit(Collider other)
    {
        playerMovement.isDialogueActive = false;
    }
}