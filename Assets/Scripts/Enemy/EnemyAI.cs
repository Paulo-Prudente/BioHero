using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform[] locations;
    private int nextLocation = 0;
    private Vector3 currentPosition;
    private States state = States.PATRULHAR;
    private Transform player = null;

    public GameObject hitboxAtaque;

    public int vida = 30;

    public bool agressive;
    public bool ciriculaPraEsquerda;

    public Animator anim;

    public GameObject particula;
    public GameObject particula2;

    //public GameObject particula2;


    public bool podeAtacar;

    public AudioClip audioAtaque;
    public AudioClip audioTomandoDano;
    private AudioSource audioSource;

    enum States
    {
        PATRULHAR,
        SEGUIR,
        ATACAR,
        //FUGIR,
        CIRCULAR,
        KNOCKBACK
    }

    // Start is called before the first frame update
    void Start()
    {
        currentPosition = locations[0].position;
        hitboxAtaque.SetActive(false);


        podeAtacar = true;


        anim = GetComponentInChildren<Animator>();

        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        /*if (vida <= 10)
            state = States.FUGIR;*/

        switch (state)
        {
            case States.PATRULHAR:
                Patrulhar();
                break;
            case States.SEGUIR:
                Seguir();
                break;
            case States.ATACAR:
                Atacar();
                break;
            /*case States.FUGIR:
                Fugir();
                break;*/
            case States.CIRCULAR:
                Circular();
                break;
            case States.KNOCKBACK:
                Knockback();
                break;
        }
        
    }

    public void Patrulhar()
    {
        Vector3 target = locations[nextLocation].position;
        transform.LookAt(target);
        transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * 30);


        if (Vector3.Distance(transform.position, target) < 1f)
        {
            nextLocation++;
            if (nextLocation >= locations.Length)
            {
                nextLocation = 0;
            }
        }
        //print("enemy patrulhando");
    }

    public void Seguir()
    {
        transform.LookAt(player.position);
        transform.position = Vector3.Lerp(transform.position, player.position, Time.deltaTime * 1);
        //print("enemy Seguindo");

        if (Vector3.Distance(transform.position, player.position) < 3f)
        {
            state = States.ATACAR;
        }

    }

    public void Atacar()
    {
        if(player != null)
            transform.LookAt(player.position);
        else
            state = States.PATRULHAR;

        //print("enemy Atacando");

        if (podeAtacar)
            StartCoroutine(Ataque(0.81f));

        //anim.SetBool("ataque", true);


        if (Vector3.Distance(transform.position, player.position) >= 3f)
        {
            //hitboxAtaque.SetActive(false);
            state = States.SEGUIR;

            anim.SetBool("ataque", false);

        }
    }

    /*public void Fugir()
    {
        print("enemy fugindo");

        if(player != null)
        {
            transform.LookAt(player.position);

            Vector3 escapePosition;

            escapePosition.y = transform.position.y;

            if (transform.position.x > player.position.x)
                escapePosition.x = transform.position.x + 4;
            else
                escapePosition.x = transform.position.x - 4;

            if (transform.position.z > player.position.z)
                escapePosition.z = transform.position.z + 4;
            else
                escapePosition.z = transform.position.z - 4;

            transform.position = Vector3.MoveTowards(transform.position, escapePosition, Time.deltaTime * 15);
        }
    }*/

    public void Circular()
    {
        print("circular");

        Vector3 ladinhoDoPlayer;

        if (ciriculaPraEsquerda)
            ladinhoDoPlayer = new Vector3(player.position.x+10, player.position.y, player.position.z);
        else
            ladinhoDoPlayer = new Vector3(player.position.x-10, player.position.y, player.position.z-10);

        transform.position = Vector3.Lerp(transform.position, ladinhoDoPlayer, Time.deltaTime * 1);
        
        transform.LookAt(player.position);

        StartCoroutine(TempoDoCircularProAtacar(2f));
        //state = States.ATACAR;
    }

    public void Knockback()
    {
        print("knockback)");

        //Vector3 moveDirection = transform.position - player.transform.position;
        //enemyRigidBody.AddForce(moveDirection.normalized * 500f);

        //transform.position = Vector3.MoveTowards(transform.position, moveDirection * 5000, Time.deltaTime * 30);

        StartCoroutine(KnockbackRoutine(0.15f));

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

            state = States.KNOCKBACK;
        }

        



        if (other.tag == "HitboxPassarinho")
        {
            print("enemy tomando do passarinho");
            vida -= 5;

        }

        if (vida <= 0)
            Destroy(gameObject);
    }

    public void VisaoTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            state = States.SEGUIR;
            player = other.transform;
        }

    }

    public void VisaoTriggerExit(Collider other)
    {
        state = States.PATRULHAR;
        player = null;
    }


    public void OnTriggerExit(Collider other)
    {
        
    }


    private IEnumerator TempoDoCircularProAtacar(float qtdDeSegundos)
    {
        yield return new WaitForSeconds(qtdDeSegundos);
        state = States.ATACAR;
    }

    private IEnumerator KnockbackRoutine(float qtdDeSegundos)
    {
        transform.LookAt(player.position);

        //transform.position = Vector3.MoveTowards(transform.position, player.position*5, Time.deltaTime * 60);

        Vector3 knockbackPosition;

        knockbackPosition.y = transform.position.y;

        if (transform.position.x > player.position.x)
            knockbackPosition.x = transform.position.x + 4;
        else
            knockbackPosition.x = transform.position.x - 4;

        if (transform.position.z > player.position.z)
            knockbackPosition.z = transform.position.z + 4;
        else
            knockbackPosition.z = transform.position.z - 4;

        transform.position = Vector3.MoveTowards(transform.position, knockbackPosition, Time.deltaTime * 60);

        yield return new WaitForSeconds(qtdDeSegundos);
        

        if (!agressive)
            state = States.CIRCULAR;
        else
            state = States.SEGUIR;
    }


    private IEnumerator Ataque(float qtdDeSegundos)
    {
        anim.SetBool("ataque", true);
        podeAtacar = false;
        yield return new WaitForSeconds(0.8f);

        audioSource.clip = audioAtaque;
        audioSource.Play();

        /*GameObject obj2 = Instantiate(particula2, new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z), Quaternion.identity);
        Destroy(obj2, 1);*/

        hitboxAtaque.transform.localPosition = new Vector3(0.0f, 0.3f, 0.3f);
        hitboxAtaque.SetActive(true);

        GameObject obj2 = Instantiate(particula2, new Vector3(hitboxAtaque.transform.position.x, hitboxAtaque.transform.position.y, hitboxAtaque.transform.position.z), Quaternion.identity);
        Destroy(obj2, 1);


        /*GameObject obj = Instantiate(particula2, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        Destroy(obj, 1);*/


        yield return new WaitForSeconds(1.3f);
        hitboxAtaque.SetActive(false);

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
}
