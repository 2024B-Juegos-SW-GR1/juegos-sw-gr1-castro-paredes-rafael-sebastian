using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    void Update()
    {
        // Verifica si se presiona Enter
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartGame();
        }
    }

    public void StartGame()
    {
        Debug.Log("Iniciando juego");
        SceneManager.LoadScene("IntroScene"); // Asegúrate que este sea el nombre de tu escena
    }
}