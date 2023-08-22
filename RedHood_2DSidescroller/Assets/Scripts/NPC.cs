using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public GameObject dialoguePanel; // to reference the dialogue panel
    public Text dialogueText; // to pull the actual dialogue text
    public string[] dialogue; 
    private int index;

    public GameObject contButton;
    public float wordSpeed; // how fast dialogue text appears
    public bool playerIsClose; //to check if player is close to NPC

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerIsClose) {
            if (dialoguePanel.activeInHierarchy) {
                zeroText();
            }
            else {
                dialoguePanel.SetActive(true);
                StartCoroutine(Typing());
            }
        }

        if (dialogueText.text == dialogue[index])
        {
            contButton.SetActive(true);
        }
    }

    public void zeroText() {
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
    }

    IEnumerator Typing()
    {
        foreach(char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void NextLine()
    {
        contButton.SetActive(false);

        if(index < dialogue.Length - 1)
        {
            index++;
            dialogueText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            zeroText();
        }
    }

        private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
                playerIsClose = true;
            }
        }

        private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
                playerIsClose = false;
                zeroText();
            }
        }
}
