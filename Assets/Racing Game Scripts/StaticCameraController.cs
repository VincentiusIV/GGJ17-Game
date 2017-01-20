using UnityEngine;
using System.Collections;

public class StaticCameraController : MonoBehaviour {

    public GameObject player;
    public float cameraHeight;
    public float distanceFromPlayer;

	// Update is called once per frame
	void Update ()
    {
        if(player != null)
        {
            Vector3 cameraPosition = player.transform.position + Vector3.right * distanceFromPlayer;
            transform.position = new Vector3(cameraPosition.x, 0.0f, cameraPosition.z);
            transform.Translate(Vector3.up * cameraHeight);
            //transform.LookAt(player.transform);
        }
    }
}
