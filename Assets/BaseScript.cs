using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseScript : MonoBehaviour {

    GameController gc;
    public GameObject explosion;

    void Start()
    {
        gc = GameObject.Find("GameController").GetComponent<GameController>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Bullet"))
        {
            if (gameObject.name == "Base1")
                gc.base1HP -= 10;
            if (gameObject.name == "Base2")
                gc.base2HP -= 10;

            Instantiate(explosion, transform.position, Quaternion.identity);
        }
    }
}
