using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    private Image HealthBar;
    public float currentHealth;
    public float MaxHealth = 100f;
    ThirdPersonMovement Player;
    // Start is called before the first frame update
    void Start()
    {
        HealthBar = GetComponent<Image>();
        Player = FindObjectOfType<ThirdPersonMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth = Player.vidaClone;
        HealthBar.fillAmount = currentHealth / MaxHealth;
    }
}
