using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    Text healthtext;
    Player player;
    int health = 0;


    void Start()
    {
        player = FindObjectOfType<Player>();
        healthtext = GetComponent<Text>();
        health = player.GetHealth();
    }


    void Update()
    {
        if(health < 0) { player.SetPlayerHealth(0); }
        healthtext.text = player.GetHealth().ToString();
    }
}
