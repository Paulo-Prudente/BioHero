using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerTeste : MonoBehaviour
{
    public void LoadSceneGame() 
    {
        int numeroFaseQueTa = PlayerPrefs.GetInt("numeroFase");

        SceneManager.LoadScene(numeroFaseQueTa);
    }

    public void LoadSceneStore() 
    {
        SceneManager.LoadScene(1);
    }

    public void ResetarPlayerPrefs()
    {
        PlayerPrefs.SetFloat("speedModifier", 1.0f);
        PlayerPrefs.SetInt("attackPower", 10);
        PlayerPrefs.SetInt("lifeStealAtivado", 0);
        PlayerPrefs.SetInt("bonusLifeAtivado", 0);
        PlayerPrefs.SetFloat("vidaMaxima", 100.0f);
        PlayerPrefs.SetFloat("defesa", 1.0f);


        PlayerPrefs.SetInt("numeroFase", 5);


        PlayerPrefs.SetString("jaJogouAntes", "sim");
    }
}
