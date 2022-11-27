using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : MonoBehaviour
{
    private Transform player = null;
    public GameObject hitboxAtaqueCabecada;
    public GameObject hitboxAtaquePaulada;
    public GameObject hitboxAtaqueMorteiro;

    public int vida = 300;
    public bool podeAtacarPaulada;
    public bool podeAtacarCabecada;
    public bool podeAtacarMorteiro;

    public Animator animL;
    public Animator animM;
    public Animator animR;

    public bool podeOlhar;

    public Transform praOndeOlhar;

    public AudioClip audioTomandoDano;
    public AudioClip audioAtaque1;
    public AudioClip audioAtaque2;
    public AudioClip audioAtaque3;

    private AudioSource audioSource;

    public GameObject particula;


    void Start()
    {
        podeOlhar = true;

        hitboxAtaqueCabecada.SetActive(false);
        hitboxAtaquePaulada.SetActive(false);
        hitboxAtaqueMorteiro.SetActive(false);

        podeAtacarPaulada = true;
        podeAtacarCabecada = true;
        podeAtacarMorteiro = true;

        audioSource = GetComponent<AudioSource>();

    }

    void Update()
    {
        if (player != null)
        {

            var targetRotation = Quaternion.LookRotation(player.transform.position - transform.position);

            if (podeOlhar)
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5 * Time.deltaTime);            // Smoothly rotate towards the target point.
            //transform.LookAt(player.position);


            if (podeAtacarCabecada)
                StartCoroutine(AtaqueCabecada(1.5f));
            if(podeAtacarPaulada)
                StartCoroutine(AtaquePaulada(1f));
            if(podeAtacarMorteiro)
                StartCoroutine(AtaqueMorteiro(1f));

        }
    }

    private IEnumerator AtaqueCabecada(float qtdDeSegundos)
    {
        animR.SetBool("RAtacando", true);

        podeOlhar = false;


        yield return new WaitForSeconds(4);

        audioSource.clip = audioAtaque1;
        audioSource.Play();

        hitboxAtaqueCabecada.SetActive(true);
        hitboxAtaqueCabecada.transform.localPosition = new Vector3(0.0f, 0.31f, 2.11f);
        podeAtacarCabecada = false;
        yield return new WaitForSeconds(qtdDeSegundos);
        hitboxAtaqueCabecada.SetActive(false);

        podeOlhar = true;

        animR.SetBool("RAtacando", false);

        StartCoroutine(TempoEntreAtaques(5.0f, 1));
    }

    private IEnumerator AtaquePaulada(float qtdDeSegundos)
    {
        animL.SetBool("LAtacando", true);

        podeOlhar = false;

        yield return new WaitForSeconds(4);

        audioSource.clip = audioAtaque2;
        audioSource.Play();

        hitboxAtaquePaulada.SetActive(true);
        hitboxAtaquePaulada.transform.localPosition = new Vector3(-3.42f, 0.31f, 0f);
        podeAtacarPaulada = false;
        yield return new WaitForSeconds(qtdDeSegundos);
        hitboxAtaquePaulada.SetActive(false);

        podeOlhar = true;

        animL.SetBool("LAtacando", false);

        StartCoroutine(TempoEntreAtaques(6.0f, 2));
    }

    private IEnumerator AtaqueMorteiro(float qtdDeSegundos)
    {
        animM.SetBool("MAtacando", true);

        yield return new WaitForSeconds(4);

        audioSource.clip = audioAtaque3;
        audioSource.Play();

        hitboxAtaqueMorteiro.SetActive(true);
        hitboxAtaqueMorteiro.transform.position = new Vector3(player.position.x, player.position.y+8, player.position.z);
        podeAtacarMorteiro = false;
        yield return new WaitForSeconds(qtdDeSegundos);
        hitboxAtaqueMorteiro.SetActive(false);

        animM.SetBool("MAtacando", false);

        StartCoroutine(TempoEntreAtaques(7.0f, 3));
    }

    private IEnumerator TempoEntreAtaques(float qtdDeSegundos, int qualPodeAtacar)
    {
        yield return new WaitForSeconds(qtdDeSegundos);

        if (qualPodeAtacar == 1)
            podeAtacarCabecada = true;
        if (qualPodeAtacar == 2)
            podeAtacarPaulada = true;
        if (qualPodeAtacar == 3)
            podeAtacarMorteiro = true;
    }



    public void VisaoTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //state = States.SEGUIR;
            player = other.transform;
        }

    }

    public void VisaoTriggerExit(Collider other)
    {
        //state = States.PATRULHAR;
        player = null;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "HitboxPlayer")
        {
            print("enemy tomando do player");
            vida -= 1 * ThirdPersonMovement.attackPower;

            audioSource.clip = audioTomandoDano;
            audioSource.Play();

            GameObject obj = Instantiate(particula, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            Destroy(obj, 1);

            if (ThirdPersonMovement.lifeStealAtivado == true)
            {
                ThirdPersonMovement.vida += (1 * ThirdPersonMovement.attackPower) * 0.05f;
            }
        }





        if (other.tag == "HitboxPassarinho")
        {
            print("enemy tomando do passarinho");
            vida -= 5;

        }

        if (vida <= 0)
            Destroy(gameObject);
    }
}
