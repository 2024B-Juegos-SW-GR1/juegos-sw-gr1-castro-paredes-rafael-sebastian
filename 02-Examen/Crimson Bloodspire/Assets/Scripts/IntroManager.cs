using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class IntroManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    void Start()
    {
        // Suscribirse al evento de finalización del video
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        // Cargar la escena del juego cuando termine el video
        SceneManager.LoadScene("SampleScene");
    }
}