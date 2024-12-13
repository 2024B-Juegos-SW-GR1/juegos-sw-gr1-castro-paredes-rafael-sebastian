using UnityEngine;

public class Drive : MonoBehaviour
{
    // Se llama al inicio del juego
  //  void Start()
   // {
   //     Debug.Log("¡El script Drive ha sido agregado correctamente!");
  //  }

    // Se llama en cada frame
    void Update()
    {
        // Rotar el objeto 45 grados en el eje Z en cada frame
        transform.Rotate(0, 0, 45 * Time.deltaTime);
    }
}