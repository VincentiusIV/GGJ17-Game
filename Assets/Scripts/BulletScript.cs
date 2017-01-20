using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [HideInInspector]public float speed;
    [HideInInspector]public float destroyTime;

    void Start()
    {
        StartCoroutine(DestroyAfterTime());
    }
	// Update is called once per frame
	void Update () {
        transform.Translate(new Vector2(0f, speed));
    }

    IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(destroyTime);
        Destroy(gameObject);
    }
}
