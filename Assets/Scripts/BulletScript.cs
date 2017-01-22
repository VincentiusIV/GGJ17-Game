using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float speed;
    public float destroyTime = 5f;
    public bool team1;

    void Start()
    {
        //StartCoroutine(DestroyAfterTime());
    }
	// Update is called once per frame
	void Update () {
        if (Time.time > destroyTime)
            Destroy(this.gameObject);

        transform.Translate(new Vector3(speed, 0f, 0f));
    }

    IEnumerator OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("bullet collided with" + col.gameObject.tag);

        if (col.gameObject.CompareTag("Team1") || col.gameObject.CompareTag("Team2"))
        {
            if (col.gameObject.CompareTag("Team1") && team1 == true || col.gameObject.CompareTag("Team2") && team1 == false)
                yield break;

            if (team1 && col.gameObject.name == "Player1" || team1 && col.gameObject.name == "Player2")
            {
                GetComponent<SpriteRenderer>().enabled = false;
                col.gameObject.GetComponent<PlayerController>().hp -= 50;
                yield return StartCoroutine(col.gameObject.GetComponent<VisibilityScript>().Visible());
                yield return StartCoroutine(col.gameObject.GetComponent<VisibilityScript>().Invisible());
            }

            else if (!team1 && col.gameObject.name == "Player3" || !team1 && col.gameObject.name == "Player4")
            {
                GetComponent<SpriteRenderer>().enabled = false;
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
