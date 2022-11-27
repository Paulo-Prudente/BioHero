using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalScript : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "HitboxPlayer")
        {
            int numeroFaseQueTava = PlayerPrefs.GetInt("numeroFase");
            PlayerPrefs.SetInt("numeroFase", numeroFaseQueTava+1);

            SceneManager.LoadScene(numeroFaseQueTava+1);
        }
    }

}
