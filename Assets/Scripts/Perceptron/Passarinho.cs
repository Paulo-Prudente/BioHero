using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passarinho : MonoBehaviour
{
    Perceptron p;

    float[] inputA = { 0, 0 }; //tirinho
    float[] inputB = { 0, 1 };
    float[] inputC = { 1, 0 };
    float[] inputD = { 1, 1 };

    public GameObject tiroPassarinho;

    public bool podeFazerCarinho;

    void Start()
    {
        p = new Perceptron(3);

        for (int i = 0; i < 20; i++)
        {
            p.Train(inputA, 0);
            p.Train(inputB, 0);
            p.Train(inputC, 1);
            p.Train(inputD, 0);
        }

        printagens(inputA, p.Forward(inputA));
        printagens(inputB, p.Forward(inputB));
        printagens(inputC, p.Forward(inputC));
        printagens(inputD, p.Forward(inputD));

        podeFazerCarinho = false;
    }

    private void Update()
    {
        Atirar();

        if (Input.GetKeyDown(KeyCode.E) && podeFazerCarinho)
        {
            p.Train(inputA, 1);
            print("treinou");
        }

    }

    public void Atirar()
    {
        if (Input.GetMouseButtonDown(1))
        {
            print(p.Forward(inputA));

            if (p.Forward(inputA) == 1)
            {
                print("SIM");

                Tiro();

                StartCoroutine(TempoDeFazerCarinho(2));

                //p.Train(inputA, 1);
            }
            else
            {
                int nelson = Random.Range(0, 2);
                print("nelson é " + nelson);
                if (nelson == 1)
                {
                    print("50% e atirou SIM");

                    Tiro();

                    StartCoroutine(TempoDeFazerCarinho(2));

                    //p.Train(inputA, 1);
                }
                else
                {
                    print("50% e atirou NÃO (e não treinou)");
                }
            }
        }
    }


    public void Tiro()
    {
        Instantiate(tiroPassarinho, transform.position, Quaternion.identity);
    }

    private IEnumerator TempoDeFazerCarinho(float qtdDeSegundos)
    {
        podeFazerCarinho = true;
        yield return new WaitForSeconds(qtdDeSegundos);
        podeFazerCarinho = false;
    }





    void printagens(float[] input, int afrontoso)
    {
        if (input[0] == 0 && input[1] == 0)
        {
            Debug.Log("00 é " + afrontoso);
        }
        if (input[0] == 0 && input[1] == 1)
        {
            Debug.Log("01 é " + afrontoso);
        }
        if (input[0] == 1 && input[1] == 0)
        {
            Debug.Log("10 é " + afrontoso);
        }
        if (input[0] == 1 && input[1] == 1)
        {
            Debug.Log("11 é " + afrontoso);
        }
    }
}
