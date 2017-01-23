using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationManager : MonoBehaviour {
    [SerializeField] private Text NotificationText;
    [SerializeField] private Image NotificationIcon;

    [SerializeField] private GameObject panel;

    [SerializeField] private string[] messageList;
    [SerializeField] private Sprite[] iconList;


    public void ShowNotificationBar(int _id, bool _textEffect, float _waitTime)
    {

        Debug.Log("ShowNotificationBar");
        StopAllCoroutines();
        NotificationText.text = "";
        panel.GetComponent<Animation>().Stop();
        panel.SetActive(true);


        NotificationIcon.sprite = iconList[_id];

        string message = messageList[_id];
        char[] chars = message.ToCharArray();
        StartCoroutine(TextEffect(chars, _waitTime));

        panel.GetComponent<Animation>().Play();
    }

    IEnumerator TextEffect(char[] _chars, float _waitTime)
    {
        NotificationText.text = "";
        foreach (char _c in _chars)
        {
            NotificationText.text += _c;
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(_waitTime);
        panel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShowNotificationBar(Random.Range(0, messageList.Length), true, 5f);
        }
    }

}
