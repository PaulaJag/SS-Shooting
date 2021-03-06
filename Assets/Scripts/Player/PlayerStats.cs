using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private Image healthStats, staminaStats;

    [SerializeField] private Text killStats;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayHealthStats(float healthValue)
    {
        healthValue /= 100f;

        healthStats.fillAmount = healthValue;
    }

    public void DisplayStaminaStats(float staminaValue)
    {
        staminaValue /= 100f;

        staminaStats.fillAmount = staminaValue;
    }

    public void DisplayKills(int kills)
    {
        killStats.text = kills.ToString();
    }
}
