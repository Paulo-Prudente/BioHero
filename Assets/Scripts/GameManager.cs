using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public ThirdPersonMovement player;
    public GameObject portal;
    public GameObject coisoDePowerUp;
    public bool faseDePowerup;

    void Start()
    {
        
    }

    void Update()
    {
        if (player.podeSpawnarPortal)
        {
            if (!coisoDePowerUp)
                portal.SetActive(true);

            if(faseDePowerup)
            {
                coisoDePowerUp.SetActive(true);

            }
            else
            {
                portal.SetActive(true);
            }
        }

    }
}
