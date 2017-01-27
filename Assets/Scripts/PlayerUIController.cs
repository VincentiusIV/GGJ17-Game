using UnityEngine.UI;
using UnityEngine;
using System;

public class PlayerUIController : MonoBehaviour {

    [SerializeField] private Image playerImageView;
    [SerializeField] private Sprite playerImage;
    [SerializeField] private Slider playerHealth;
    [SerializeField] private string playerName;
    [SerializeField] private GameObject AlivePanel;
    [SerializeField] private GameObject DeathPanel;

    void Start()
    {
        playerImageView.sprite = playerImage;
    }

    void FixedUpdate()
    {
        try
        {
            GameObject _player = GameObject.Find(playerName);
            int playerHP = _player.GetComponent<PlayerController>().hp;
            playerHealth.value = playerHP;

            if (playerHP < 1)
            {
                AlivePanel.SetActive(false);
                DeathPanel.SetActive(true);
            }
            else
            {
                AlivePanel.SetActive(true);
                DeathPanel.SetActive(false);
            }
        }catch(Exception e)
        {
            Debug.Log("PlayerUIController ~ Player: " + playerName + " was not found in the scene!");
        }


    }
}
