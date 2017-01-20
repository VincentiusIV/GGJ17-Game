using UnityEngine;
using System.Collections;

public class InputTesting : MonoBehaviour {

	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButtonDown("A_1"))
            Debug.Log("Pressed A_1");
        if (Input.GetButtonDown("A_2"))
            Debug.Log("Pressed A_2");
        if (Input.GetButtonDown("A_3"))
            Debug.Log("Pressed A_3");
        if (Input.GetButtonDown("A_4"))
            Debug.Log("Pressed A_4");
    }
}
