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

    public void ShowNotificationBar(int _id)
    {
        GetComponent<Animation>().Play();
        NotificationText.text = messageList[_id];
        NotificationIcon.sprite = iconList[_id];
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShowNotificationBar(Random.Range(0, messageList.Length));
        }
    }
}
