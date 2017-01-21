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
        GetComponent<Camera>().orthographicSize = (Screen.height / pixelToUnits) / 2;
    }
    void Update()
    {
        Vector3 targetPosition = new Vector3();
        for (int i = 0; i < targets.Length; i++)
        {
            targetPosition += targets[i].position;
        }
        targetPosition /= targets.Length;
        targetPosition += new Vector3(0f, 0f, -camDistance);


        transform.position = Vector3.Lerp(transform.position, targetPosition, lerpIntensity);
    }
}
