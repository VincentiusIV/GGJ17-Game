using UnityEngine;
using System.Collections;

public class CameraMovements : MonoBehaviour
{
    public Transform[] targets;
    public float camDistance;
    public float lerpIntensity;
    public float pixelToUnits;
    private Vector2 movement;

    void Start()
    {
        GetComponent<Camera>().orthographicSize = (Screen.width / pixelToUnits) / 3.2f;
    }
    void Update()
    {
        
        float xPos = 0f;
        for (int i = 0; i < targets.Length; i++)
        {
            xPos += targets[i].position.x;
        }
        xPos /= (targets.Length * 2);
        Vector3 targetPosition = new Vector3(xPos, 0f, -camDistance);


        transform.position = Vector3.Lerp(transform.position, targetPosition, lerpIntensity);
    }
}
