using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {

    public GameObject playerExplosion;
    public GameController gc;

    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gc = gameControllerObject.GetComponent<GameController>();
        }
        if (gc == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Platform"))
        {
            return;
        }

        if (other.CompareTag("Player"))
        {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            Destroy(other.gameObject);
            gc.GameOver();
        }
    }
}
