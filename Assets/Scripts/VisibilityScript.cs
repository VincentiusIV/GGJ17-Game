using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibilityScript : MonoBehaviour {

    private SpriteRenderer sr;
    private Color color;
    public bool isChanging;
    public bool isVisible;
    public bool isInvisible;
    private Transform hpBar;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
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
        isChanging = true;
        int AmountOfFrames = 30;
        for (int i = 1; i < AmountOfFrames; i++)
        {
            yield return new WaitForSeconds(1 / AmountOfFrames);
            color.a += 0.033f;
            sr.color = color;
        }
        isChanging = false;
        isVisible = true;
        isInvisible = false;
    }

    public void BecomeInvisible()
    {
        Debug.Log("becoming vis");
        color.a = 1f;
        sr.color = color;
        StartCoroutine(Invisible());
    }

    public IEnumerator Invisible()
    {
        isChanging = true;
        Debug.Log("becoming invis");
        int AmountOfFrames = 30;
        for (int i = 1; i < AmountOfFrames; i++)
        {
            yield return new WaitForSeconds(1 / AmountOfFrames);
            color.a -= 0.033f;
            sr.color = color;
        }
        isChanging = false;
        isVisible = false;
        isInvisible = true;
    }
}

