using UnityEngine;
using System.Collections;

public class Repeater : MonoBehaviour {

    public GameObject deathFloorObject;
    public GameObject[] platforms;
    public float platformHeight;

    private Vector3 dfPos;
    private Vector3 pfPos;

	// Update is called once per frame

    void Start()
    {
        dfPos = new Vector3(0, 0, 0);
        pfPos = new Vector3(0, platformHeight, 0);
        Instantiate(deathFloorObject, dfPos , Quaternion.identity);
        Instantiate(platforms[0], pfPos, Quaternion.identity);
    }
}
