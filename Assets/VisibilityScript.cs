using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibilityScript : MonoBehaviour {

    private Material mat;

    private SpriteRenderer sr;
    private Color color;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        mat = sr.material;
        color = sr.color;
    }
    public void BecomeVisible()
    {
        color.a = 0;
        sr.color = color;
        StartCoroutine(Visible());
        
    }
    public IEnumerator Visible()
    {
        int AmountOfFrames = 30;
        for (int i = 1; i < AmountOfFrames; i++)
        {
            yield return new WaitForSeconds(1 / AmountOfFrames);
            color.a += 0.033f;
            sr.color = color;
        }
    }

    public void BecomeInvisible()
    {
        color.a = 1f;
        sr.color = color;
        StartCoroutine(Invisible());
    }

    public IEnumerator Invisible()
    {
        int AmountOfFrames = 30;
        for (int i = 1; i < AmountOfFrames; i++)
        {
            yield return new WaitForSeconds(1 / AmountOfFrames);
            color.a -= 0.033f;
            sr.color = color;
        }
    }
}

