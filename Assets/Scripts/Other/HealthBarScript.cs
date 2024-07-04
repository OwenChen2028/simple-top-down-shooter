using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBarScript : MonoBehaviour
{
    public GameObject healthBar;
    public GameObject fillImage;
    public GameObject healthText;
    public GameObject player;
    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider.maxValue = player.GetComponent<PlayerController>().maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            float fillValue = player.GetComponent<PlayerController>().health;
            slider.value = fillValue;
            TextMeshProUGUI displayText = healthText.GetComponent<TextMeshProUGUI>();
            displayText.SetText(slider.value.ToString() + " / " + slider.maxValue.ToString());
        }
        else
        {
            slider.value = 0;
            TextMeshProUGUI displayText = healthText.GetComponent<TextMeshProUGUI>();
            displayText.SetText(slider.value.ToString() + " / " + slider.maxValue.ToString());
        }
    }
}
