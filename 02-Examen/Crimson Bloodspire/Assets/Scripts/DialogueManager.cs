using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public GameObject dialoguePanel;
    public float typingSpeed = 0.05f;
    public Move playerMovement; // Referencia al script del jugador

    private string[] dialogues;
    private int currentDialogue = 0;
    private bool isTyping = false; 

    void Start()
    {
        // Desactivar movimiento del jugador al inicio
        if(playerMovement != null)
            playerMovement.enabled = false;

        // Array de diálogos
        dialogues = new string[]
        {
            "Aldrick: DRAVEN SE QUE ESTAS AHI Y SE QUE PUEDES ESCUCHARME!!!",
            "Aldrick: Me arrebataste a Madeleine y la asesinaste sin piedad ",
            "Aldrick: Para que, todo para traer a la vida a tu señor Dracula",
            "Aldrick: No eres mas que un peon al servicio de tu amo",
            "Aldrick: Dracula te dejara de lado cuando ya no le seas util",
            "Aldrick: Pero antes que eso pase sere yo quien acabe contigo",
            "Aldrick: No importa donde te escondas, no podras huir de mi",
            "Aldrick: VOY POR TI!!!"
            
            
        };

        StartDialogue();
    }

    void StartDialogue()
    {
        dialoguePanel.SetActive(true);
        DisplayNextDialogue();
    }

    void DisplayNextDialogue()
    {
        if (currentDialogue < dialogues.Length)
        {
            StartCoroutine(TypeDialogue(dialogues[currentDialogue]));
        }
        else
        {
            EndDialogue();
        }
    }

    IEnumerator TypeDialogue(string dialogue)
    {
        isTyping = true;
        dialogueText.text = "";
        foreach (char letter in dialogue.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        isTyping = false;
    }

    void Update()
    {
        // Avanzar al siguiente diálogo con Space o Enter
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            if (!isTyping)
            {
                currentDialogue++;
                DisplayNextDialogue();
            }
        }
    }

    void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        // Activar movimiento del jugador cuando terminen los diálogos
        if(playerMovement != null)
            playerMovement.enabled = true;
    }
}