using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBarUI : MonoBehaviour
{
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private PlayerStats playerStats;

    public void Start()
    {
        healthText.text = "HEATLH: " + playerStats.currentHealth;
    }

    public void Update()
    {
        healthText.text = "HEATLH: " + playerStats.currentHealth;
    }
}
