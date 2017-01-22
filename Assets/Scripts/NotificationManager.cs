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

    public void ShowNotificationBar(int _id, bool _textEffect)
    {
        Reset();
        NotificationIcon.sprite = iconList[_id];

        if (_textEffect)
        {
            string message = messageList[_id];
            char[] chars = message.ToCharArray();
            StartCoroutine(TextEffect(chars));
        }else
        {
            NotificationText.text = messageList[_id];
        }

        GetComponent<Animation>().Play();
    }

    IEnumerator TextEffect(char[] _chars)
    {
        NotificationText.text = "";
        foreach (char _c in _chars)
        {
            NotificationText.text += _c;
            yield return new WaitForSeconds(0.1f);
        }
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShowNotificationBar(Random.Range(0, messageList.Length), true);
        }
    }

    void Reset()
    {
        StopAllCoroutines();
        NotificationText.text = "";
        GetComponent<Animation>().Stop();
    }
}
