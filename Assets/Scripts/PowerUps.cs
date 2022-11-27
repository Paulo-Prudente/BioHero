using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PowerUps : MonoBehaviour
{
    ThirdPersonMovement player;
    public int primeiraOpcao;
    public int segundaOpcao;
    public int terceiraOpcao;
    public int opcaoEscolhida;

    public bool faseDePowerUpPermanente;

    public Canvas canvas;

    public TMP_Text primeiroBotao;
    public TMP_Text segundoBotao;
    public TMP_Text terceiroBotao;

    public GameObject powerUpImagem1;
    public GameObject powerUpImagem2;
    public GameObject powerUpImagem3;
    public GameObject powerUpImagem4;
    public GameObject powerUpImagem5;
    public GameObject powerUpImagem6;


    void Start()
    {
        canvas.gameObject.SetActive(true);


        primeiraOpcao = Random.Range(1, 7);


        segundaOpcao = Random.Range(1, 7);
        while(segundaOpcao==primeiraOpcao)
        {
            segundaOpcao = Random.Range(1, 7);
        }


        terceiraOpcao = Random.Range(1, 7);
        while (terceiraOpcao == primeiraOpcao || terceiraOpcao == segundaOpcao)
        {
            terceiraOpcao = Random.Range(1, 7);
        }




        switch(primeiraOpcao)
        {
            case 1:
                powerUpImagem1.SetActive(true);
                powerUpImagem1.transform.position = primeiroBotao.transform.position;
                primeiroBotao.text = "speed";
                break;
            case 2:
                powerUpImagem2.SetActive(true);
                powerUpImagem2.transform.position = primeiroBotao.transform.position;
                primeiroBotao.text = "ataque";
                break;
            case 3:
                powerUpImagem3.SetActive(true);
                powerUpImagem3.transform.position = primeiroBotao.transform.position;
                primeiroBotao.text = "lifesteal";
                break;
            case 4:
                powerUpImagem4.SetActive(true);
                powerUpImagem4.transform.position = primeiroBotao.transform.position;
                primeiroBotao.text = "bonus life";
                break;
            case 5:
                powerUpImagem5.SetActive(true);
                powerUpImagem5.transform.position = primeiroBotao.transform.position;
                primeiroBotao.text = "vida maxima";
                break;
            case 6:
                powerUpImagem6.SetActive(true);
                powerUpImagem6.transform.position = primeiroBotao.transform.position;
                primeiroBotao.text = "defesa";
                break;
        }




        switch (segundaOpcao)
        {
            case 1:
                powerUpImagem1.SetActive(true);
                powerUpImagem1.transform.position = segundoBotao.transform.position;
                segundoBotao.text = "speed";
                break;
            case 2:
                powerUpImagem2.SetActive(true);
                powerUpImagem2.transform.position = segundoBotao.transform.position;
                segundoBotao.text = "ataque";
                break;
            case 3:
                powerUpImagem3.SetActive(true);
                powerUpImagem3.transform.position = segundoBotao.transform.position;
                segundoBotao.text = "life steal";
                break;
            case 4:
                powerUpImagem4.SetActive(true);
                powerUpImagem4.transform.position = segundoBotao.transform.position;
                segundoBotao.text = "bonus life";
                break;
            case 5:
                powerUpImagem5.SetActive(true);
                powerUpImagem5.transform.position = segundoBotao.transform.position;
                segundoBotao.text = "vida maxima";
                break;
            case 6:
                powerUpImagem6.SetActive(true);
                powerUpImagem6.transform.position = segundoBotao.transform.position;
                segundoBotao.text = "defesa";
                break;
        }



        switch (terceiraOpcao)
        {
            case 1:
                powerUpImagem1.SetActive(true);
                powerUpImagem1.transform.position = terceiroBotao.transform.position;
                terceiroBotao.text = "speed";
                break;
            case 2:
                powerUpImagem2.SetActive(true);
                powerUpImagem2.transform.position = terceiroBotao.transform.position;
                terceiroBotao.text = "ataque";
                break;
            case 3:
                powerUpImagem3.SetActive(true);
                powerUpImagem3.transform.position = terceiroBotao.transform.position;
                terceiroBotao.text = "life steal";
                break;
            case 4:
                powerUpImagem4.SetActive(true);
                powerUpImagem4.transform.position = terceiroBotao.transform.position;
                terceiroBotao.text = "bonus life";
                break;
            case 5:
                powerUpImagem5.SetActive(true);
                powerUpImagem5.transform.position = terceiroBotao.transform.position;
                terceiroBotao.text = "vida maxima";
                break;
            case 6:
                powerUpImagem6.SetActive(true);
                powerUpImagem6.transform.position = terceiroBotao.transform.position;
                terceiroBotao.text = "defesa";
                break;
        }

        primeiroBotao.transform.position = new Vector3(primeiroBotao.transform.position.x, primeiroBotao.transform.position.y - 12, primeiroBotao.transform.position.z);
        segundoBotao.transform.position = new Vector3(segundoBotao.transform.position.x, segundoBotao.transform.position.y - 12, segundoBotao.transform.position.z);
        terceiroBotao.transform.position = new Vector3(terceiroBotao.transform.position.x, terceiroBotao.transform.position.y - 12, terceiroBotao.transform.position.z);

    }

    void Update()
    {
        float nelson;
        int bagulhos;

        switch (opcaoEscolhida)
        {
            case 1:
                ThirdPersonMovement.speedModifierNaoPermanente += 0.1f;
                print("speed" + ThirdPersonMovement.speedModifierNaoPermanente);

                if(faseDePowerUpPermanente)
                {
                    nelson = PlayerPrefs.GetFloat("speedModifier");
                    PlayerPrefs.SetFloat("speedModifier", nelson += 0.1f);
                }

                canvas.gameObject.SetActive(false);
                Destroy(this.gameObject);
                break;
            case 2:
                ThirdPersonMovement.attackPowerNaoPermanente += 1;
                print("ataque" + ThirdPersonMovement.attackPowerNaoPermanente);

                if (faseDePowerUpPermanente)
                {
                    bagulhos = PlayerPrefs.GetInt("attackPower");
                    PlayerPrefs.SetInt("attackPower", bagulhos += 1);
                }

                canvas.gameObject.SetActive(false);
                Destroy(this.gameObject);
                break;
            case 3:
                ThirdPersonMovement.lifeStealAtivadoNaoPermanente = true;
                print("lifesteal" + ThirdPersonMovement.lifeStealAtivadoNaoPermanente);

                canvas.gameObject.SetActive(false);
                Destroy(this.gameObject);
                break;
            case 4:
                ThirdPersonMovement.bonusLifeAtivadoNaoPermanente = true;
                print("bonus life" + ThirdPersonMovement.bonusLifeAtivadoNaoPermanente);

                canvas.gameObject.SetActive(false);
                Destroy(this.gameObject);
                break;
            case 5:
                ThirdPersonMovement.vidaMaximaNaoPermanente *= 1.1f;
                print("vida maxima" + ThirdPersonMovement.vidaMaximaNaoPermanente);

                if (faseDePowerUpPermanente)
                {
                    nelson = PlayerPrefs.GetFloat("vidaMaxima");
                    PlayerPrefs.SetFloat("vidaMaxima", nelson *= 1.1f);
                }

                canvas.gameObject.SetActive(false);
                Destroy(this.gameObject);
                break;
            case 6:
                ThirdPersonMovement.defesaNaoPermanente *= 0.9f;
                print("defesa" + ThirdPersonMovement.defesaNaoPermanente);

                if (faseDePowerUpPermanente)
                {
                    nelson = PlayerPrefs.GetFloat("defesa");
                    PlayerPrefs.SetFloat("defesa", nelson *= 0.9f);
                }

                canvas.gameObject.SetActive(false);
                Destroy(this.gameObject);
                break;
        }
    }

    public void PrimeiroBotao()
    {
        opcaoEscolhida = primeiraOpcao;
    }
    public void SegundoBotao()
    {
        opcaoEscolhida = segundaOpcao;
    }
    public void TerceiroBotao()
    {
        opcaoEscolhida = terceiraOpcao;
    }
}
