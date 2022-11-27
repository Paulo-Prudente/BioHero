using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoisoPlayerPrefsOnce : MonoBehaviour
{
    public string nelson;
    void Start()
    {
        nelson = PlayerPrefs.GetString("jaJogouAntes");

        print("jaJogouAntes (antes de setar) : " + PlayerPrefs.GetString("jaJogouAntes"));


        if (nelson!="sim")
        {
            PlayerPrefs.SetFloat("speedModifier", 1.0f);
            PlayerPrefs.SetInt("attackPower", 10);
            PlayerPrefs.SetInt("lifeStealAtivado", 0);
            PlayerPrefs.SetInt("bonusLifeAtivado", 0);
            PlayerPrefs.SetFloat("vidaMaxima", 100.0f);
            PlayerPrefs.SetFloat("defesa", 1.0f);


            PlayerPrefs.SetInt("numeroFase", 3);


            PlayerPrefs.SetString("jaJogouAntes", "sim");


            print("speedmodifier: " + PlayerPrefs.GetFloat("speedModifier"));
            print("attackPower: " + PlayerPrefs.GetInt("attackPower"));
            print("lifeStealAtivado: " + PlayerPrefs.GetInt("lifeStealAtivado"));
            print("bonusLifeAtivado: " + PlayerPrefs.GetInt("bonusLifeAtivado"));
            print("vidaMaxima: " + PlayerPrefs.GetFloat("vidaMaxima"));
            print("defesa: " + PlayerPrefs.GetFloat("defesa"));
            print("jaJogouAntes (depois de setar) : " + PlayerPrefs.GetString("jaJogouAntes"));
        }

    }
}
