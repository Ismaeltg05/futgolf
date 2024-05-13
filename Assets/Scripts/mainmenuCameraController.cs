using UnityEngine;

public class mainmenuCameraController : MonoBehaviour
{
    Vector3 startPos;
    Vector3 startRot;
    void Update()
    {
        transform.position = startPos + new Vector3(Mathf.PerlinNoise1D(Time.time * 0.15f), Mathf.PerlinNoise1D(Time.time * 0.1f), Mathf.PerlinNoise1D(Time.time * 0.13f)) * 3f;
    }
}
