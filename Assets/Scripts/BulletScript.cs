using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [HideInInspector]public float speed;
    [HideInInspector]public float destroyTime;
    [HideInInspector]public bool team1;

    void Start()
    {
        StartCoroutine(DestroyAfterTime());
    }
	// Update is called once per frame
	void Update () {
        transform.Translate(new Vector2(0f, speed));
    }
    IEnumerator OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Team1") || col.CompareTag("Team2"))
        {
            if(team1 && col.gameObject.name == "Player1" || team1 && col.gameObject.name == "Player2")
            {
                col.GetComponent<PlayerController>().hp -= 10;
                yield return StartCoroutine(col.GetComponent<VisibilityScript>().Visible());
                yield return StartCoroutine(col.GetComponent<VisibilityScript>().Invisible());
                Destroy(gameObject);
            }
                
            else if(!team1 && col.gameObject.name == "Player3" || !team1 && col.gameObject.name == "Player4")
            {
                col.GetComponent<PlayerController>().hp -= 10;
                yield return StartCoroutine(col.GetComponent<VisibilityScript>().Visible());
                yield return StartCoroutine(col.GetComponent<VisibilityScript>().Invisible());
                Destroy(gameObject);
            }
        }
        
        else if(col.CompareTag("Environment"))
        {
            Destroy(gameObject);
        }
    }

    IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(destroyTime);
        Destroy(gameObject);
    }
}
