using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject Knight;

    void Update()
    {
        Vector3 position = transform.position;
        position.x = Knight.transform.position.x;
        transform.position = position;
    }
}