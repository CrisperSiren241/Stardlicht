using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cinemachine;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public GameObject dialogueBox;

    private Queue<string> sentences;

    public PlayerMovement playerMovement;
    public CinemachineFreeLook freeLookCamera;

    void Start()
    {
        sentences = new Queue<string>();
        dialogueBox.SetActive(false); // Скрываем диалоговое окно в начале игры
    }

    public void StartDialogue(Dialogue dialogue)
    {
        playerMovement.isDialogueActive = true;
        LockCamera(true);

        dialogueBox.SetActive(true);
        nameText.text = dialogue.name;
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForEndOfFrame();
        }
    }

    void EndDialogue()
    {
        playerMovement.isDialogueActive = false;
        LockCamera(false);

        dialogueBox.SetActive(false);
    }
    public void NextSentence()
    {
        DisplayNextSentence();
    }

    public void ShowShortMessage(string message, Dialogue dialogue)
    {
        playerMovement.isDialogueActive = true;
        dialogueBox.SetActive(true);
        nameText.text = dialogue.name;
        dialogueText.text = message;
    }

    void LockCamera(bool isLocked)
    {
        if (isLocked)
        {
            freeLookCamera.m_XAxis.m_MaxSpeed = 0;
            freeLookCamera.m_YAxis.m_MaxSpeed = 0;
        }
        else
        {
            freeLookCamera.m_XAxis.m_MaxSpeed = 300; 
            freeLookCamera.m_YAxis.m_MaxSpeed = 2; 
        }
    }

}
