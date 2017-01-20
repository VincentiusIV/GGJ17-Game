using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public int playerID;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("A_"+playerID))
            Debug.Log("Pressed A_"+playerID);

        float xPos = Input.GetAxis("L_XAxis_" + playerID);
        float yPos = 1f;
        Vector3 movement = new Vector3(xPos, yPos, 0f);
        rb.velocity = movement;
    }
}
