using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStaticAI : MonoBehaviour
{
    private Transform player = null;
    public GameObject hitboxAtaque;

    public int vida = 30;
    public bool podeAtacar;

    public bool podeOlhar;

    public Animator anim;

    public GameObject particula;

    public AudioClip audioTomandoDano;
    public AudioClip audioAtaque;
    private AudioSource audioSource;

    void Start()
    {
        //hitboxAtaque.SetActive(false);
        podeAtacar = true;
        podeOlhar = true;

        anim = GetComponentInChildren<Animator>();

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

            if (podeAtacar)
                StartCoroutine(Ataque(Random.Range(0.81f, 2.0f)));
            

        }
    }

    private IEnumerator Ataque(float qtdDeSegundos)
    {
        anim.SetBool("ataque", true);
        podeAtacar = false;
        yield return new WaitForSeconds(0.8f);


        podeOlhar = false;

        audioSource.clip = audioAtaque;
        audioSource.Play();

        hitboxAtaque.transform.localPosition = new Vector3(0.0f, 0.31f, 2.11f);
        hitboxAtaque.SetActive(true);
        yield return new WaitForSeconds(1.3f);
        hitboxAtaque.SetActive(false);
        podeOlhar = true;

        anim.SetBool("ataque", false);

        //StartCoroutine(TempoEntreAtaques(4.0f));

        yield return new WaitForSeconds(qtdDeSegundos);

        podeAtacar = true;
    }

    private IEnumerator TempoEntreAtaques(float qtdDeSegundos)
    {
        yield return new WaitForSeconds(qtdDeSegundos);

        podeAtacar = true;
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

            if (ThirdPersonMovement.lifeStealAtivado == true)
            {
                ThirdPersonMovement.vida += (1 * ThirdPersonMovement.attackPower) * 0.05f;
            }



            GameObject obj = Instantiate(particula, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            Destroy(obj, 1);
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
