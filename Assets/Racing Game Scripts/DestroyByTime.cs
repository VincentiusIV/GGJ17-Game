using UnityEngine;
using System.Collections;

public class DestroyByTime : MonoBehaviour {

    /* public GameObject player;
    * cannot do this since Unity does not allow the player to be assigned to a prefab
    *
    * public float objectLength;

	// Update is called once per frame
	void Update ()
    {
        float zPos = player.transform.position.z - objectLength;

	    if(player.transform.position.z == zPos)
        {
            Destroy(this);
            Debug.Log("Destroyed platform");
        }
	}*/

    void Start()
    {
        Destroy(this.gameObject, 20.0f);
    }
}
