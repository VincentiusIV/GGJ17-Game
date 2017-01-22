using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float speed;
    public float destroyTime = 5f;
    public bool team1;
    private bool isFlying;

    void Start()
    {
        isFlying = true;
        StartCoroutine(DestroyAfterTime());
        Debug.Log("Team " + team1);
    }
	// Update is called once per frame
	void Update () {

        if(isFlying)
            transform.Translate(new Vector3(speed, 0f, 0f));
    }

    IEnumerator OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("bullet collided with" + col.gameObject.tag);

        if (col.gameObject.CompareTag("Team1") || col.gameObject.CompareTag("Team2"))
        {
            isFlying = false;
            if (col.gameObject.CompareTag("Team1") && team1 == true || col.gameObject.CompareTag("Team2") && team1 == false)
                yield break;

            if (team1 && col.gameObject.CompareTag("Team1"))
            {
                GetComponent<SpriteRenderer>().enabled = false;
                GetComponent<Animator>().enabled = false;
                col.gameObject.GetComponent<PlayerController>().hp -= 50;
                yield return StartCoroutine(col.gameObject.GetComponent<VisibilityScript>().Visible());
                yield return StartCoroutine(col.gameObject.GetComponent<VisibilityScript>().Invisible());
            }

            else if (!team1 && col.gameObject.CompareTag("Team2"))
            {
                Debug.Log("fuck team 2");
                GetComponent<SpriteRenderer>().enabled = false;
                GetComponent<Animator>().enabled = false;
                col.gameObject.GetComponent<PlayerController>().hp -= 50;
                yield return StartCoroutine(col.gameObject.GetComponent<VisibilityScript>().Visible());
                yield return StartCoroutine(col.gameObject.GetComponent<VisibilityScript>().Invisible());
            }
            col.gameObject.GetComponent<PlayerController>().SpawnBlood();
        }
        else
            Destroy(gameObject);
    }

    IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(destroyTime);
        Destroy(gameObject);
    }
}
