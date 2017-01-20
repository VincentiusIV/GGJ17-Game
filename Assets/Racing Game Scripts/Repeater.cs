using UnityEngine;
using System.Collections;

public class LevelRepeater : MonoBehaviour {

    public GameObject player;
    public GameObject[] platforms;
    public float platformHeight;

    private Vector3 pfPos;
    private float startPos;
	// Update is called once per frame

    void Start()
    {
        startPos = player.transform.position.z;
        pfPos = new Vector3(0, platformHeight, 0);
        Instantiate(platforms[0], pfPos, Quaternion.identity);
    }

	void Update ()
    {
        if(player.transform.position.z == pfPos.z || player.transform.position.z > pfPos.z)
        {
            pfPos.z += 40.0f;
            Instantiate(platforms[Random.Range(1, platforms.Length)], pfPos, Quaternion.identity);
        }
	}
}
