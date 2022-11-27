using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;

    public string playerState = "idle";
    public bool isDashing = false;

    public float speed = 10.0f;
    public float speedLimit = 39.0f;
    [SerializeField]
    public static float speedModifier = 1.0f;                                                               //STATIC! Actual valores
    public static float speedModifierNaoPermanente = 0f;                                                         //Nao Permanente
    public float speedModifierPlayerPrefs = 1.0f;                                                           //PLAYERPREFS

    public float turnSmoothTime = 0.1f;
    float turnSmoorthVelocity;

    public Vector3 direction;

    //contador pro dash
    public float waitCounter = 0f;


    //ataque
    public bool attacking = false;
    public bool podeAtacar = true;
    public GameObject hitboxAtaque1;
    public GameObject hitboxAtaque2;
    public GameObject hitboxAtaque3;
    public int comboIncremento;

    [SerializeField]
    public static int attackPower = 10;                                                                     //STATIC! Actual valores
    public static int attackPowerNaoPermanente = 0;                                                             //Nao permanente
    public int attackPowerPlayerPrefs = 10;                                                                 //PLAYERPREFS


    [SerializeField]
    public static bool lifeStealAtivado = false;                                                            //STATIC! Actual valores
    public static bool lifeStealAtivadoNaoPermanente = false;                                                       //Nao permanente
    public bool lifeStealAtivadoPlayerPrefs = false;                                                        //PLAYERPREFS

    [SerializeField]
    public static bool bonusLifeAtivado = false;                                                            //STATIC! Actual valores
    public static bool bonusLifeAtivadoNaoPermanente = false;                                                       //Nao permanente
    public bool bonusLifeAtivadoPlayerPrefs = false;                                                        //PLAYERPREFS


    public float vidaClone = vida;

    //vida
    public static float vida = 100.0f;
    [SerializeField]
    public static float vidaMaxima = 100.0f;                                                                //STATIC Actual valores
    public static float vidaMaximaNaoPermanente = 100.0f;                                                            //Nao permanente
    public float vidaMaximaPlayerPrefs = 100.0f;                                                            //PLAYERPREFS
    [SerializeField]
    public static float defesa = 1.0f; // = porcentagem de defesa mas ao contrario, 1 é 0% e 0 é 100%       //STATIC! Actual valores
    public static float defesaNaoPermanente = 1f;                                                                //Nao permamente
    public float defesaPlayerPrefs = 1.0f;                                                                  //PLAYERPREFS
    public GameObject painelGameOver;
    public GameObject barraVida;
    public GameObject botaoPause;
    //Camera Olhando pro inimigo nelson
    private GameObject inimigo;

    //TOUCH!!
    private bool isSwiping = false;
    private Vector2 startingTouch;


    private string paOnde;

    public bool podeSpawnarPortal;



    //ANIMAÇÃO!!!!!!!!!!!!
    public Animator anim;



    //PARTICULAS!!!!!!!!
    public GameObject particula;


    //SOM!!!!!!!!!!!!!!!
    public AudioClip audioAtaque1;
    public AudioClip audioAtaque2;
    public AudioClip audioAtaque3;

    public AudioClip audioTomandoDano;
    public AudioClip audioMorrendo;


    private AudioSource audioSource;


    private void Start()
    {
        print("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");

        //hitboxAtaque1 = GameObject.Find("Hitbox");
        hitboxAtaque1.SetActive(false);
        hitboxAtaque2.SetActive(false);
        hitboxAtaque3.SetActive(false);
        comboIncremento = 0;

        inimigo = FindClosestEnemy();

        podeSpawnarPortal = false;

        speedModifierPlayerPrefs = PlayerPrefs.GetFloat("speedModifier");
        attackPowerPlayerPrefs = PlayerPrefs.GetInt("attackPower");
        lifeStealAtivadoPlayerPrefs = PlayerPrefs.GetInt("lifeStealAtivado") == 1 ? true : false;
        bonusLifeAtivadoPlayerPrefs = PlayerPrefs.GetInt("bonusLifeAtivado") == 1 ? true : false;
        vidaMaximaPlayerPrefs = PlayerPrefs.GetFloat("vidaMaxima");
        defesaPlayerPrefs = PlayerPrefs.GetFloat("defesa");

        print("speedmodifier: " + PlayerPrefs.GetFloat("speedModifier"));
        print("attackPower: " + PlayerPrefs.GetInt("attackPower"));
        print("lifeStealAtivado: " + PlayerPrefs.GetInt("lifeStealAtivado"));
        print("bonusLifeAtivado: " + PlayerPrefs.GetInt("bonusLifeAtivado"));
        print("vidaMaxima: " + PlayerPrefs.GetFloat("vidaMaxima"));
        print("defesa: " + PlayerPrefs.GetFloat("defesa"));
        print("jaJogouAntes (depois de setar) : " + PlayerPrefs.GetString("jaJogouAntes"));



        speedModifier = speedModifierPlayerPrefs + speedModifierNaoPermanente;
        attackPower = attackPowerPlayerPrefs + attackPowerNaoPermanente;
        lifeStealAtivado = lifeStealAtivadoPlayerPrefs || lifeStealAtivadoNaoPermanente;
        bonusLifeAtivado = bonusLifeAtivadoPlayerPrefs || bonusLifeAtivadoNaoPermanente;
        vidaMaxima = vidaMaximaPlayerPrefs + vidaMaximaNaoPermanente - 100.0f;
        defesa = defesaPlayerPrefs - (1 - defesaNaoPermanente);



        //ANIMAÇÃO
        anim = GetComponentInChildren<Animator>();
        //anim.Play("RunStartP");


        audioSource = GetComponent<AudioSource>();
    }


    private int _paOnde = Animator.StringToHash("paOnde");

    void Update()
    {
        vidaClone = vida;


        ///////////////////////////////ANIMAÇÃO PORRA
        if (isDashing)
            anim.SetBool("isDashing", true);
        else
            anim.SetBool("isDashing", false);

        if (paOnde == "cima")
            anim.SetInteger(_paOnde, 1);
        if (paOnde == "baixo")
            anim.SetInteger(_paOnde, 2);
        if (paOnde == "direita")
            anim.SetInteger(_paOnde, 3);
        if (paOnde == "esquerda")
            anim.SetInteger(_paOnde, 4);

        if (!podeAtacar)
            anim.SetBool("attacking", true);
        else
            anim.SetBool("attacking", false);

        if (comboIncremento == 1)
            anim.SetInteger("comboIncremento", 1);
        if (comboIncremento == 2)
            anim.SetInteger("comboIncremento", 2);
        if (comboIncremento == 3)
            anim.SetInteger("comboIncremento", 3);
        if (comboIncremento == 4)
            anim.SetInteger("comboIncremento", 4);
        /////////////////////////////////////////







        //////////////////////////////////////
        //ATTACK!

        if (Input.GetMouseButtonDown(0))
        {
            if (Vector3.Distance(inimigo.transform.position, transform.position) > 3)
            {
                isDashing = true;

                speed = 60 * speedModifier;
                waitCounter = 1;

                paOnde = "ataque";
            }

            attacking = true;
        }

        if (podeAtacar)
        {
            hitboxAtaque1.SetActive(false);
            hitboxAtaque2.SetActive(false);
            hitboxAtaque3.SetActive(false);

            comboIncremento = 0;
        }


        //////////////////////////////////////
        //PLAYER OLHA PRO INIMIGO E PORTANTO A CAMERA FIXA TB

        if (inimigo)
            transform.LookAt(inimigo.transform.position);
        else
            inimigo = FindClosestEnemy();
        /////////////////////////////////////

    }

    private void FixedUpdate()
    {

        ///////////////////////////////////////////
        //TOUCH!!!!!!!!
        if (Input.touchCount == 1)
        {
            if (isSwiping)
            {
                Vector2 diff = Input.GetTouch(0).position - startingTouch;
                diff = new Vector2(diff.x / Screen.width, diff.y / Screen.width);
                if (diff.magnitude > 0.01f)
                {
                    Vector3 direcao = inimigo.transform.position - transform.position;

                    if (Mathf.Abs(diff.y) > Mathf.Abs(diff.x))
                    {
                        if (diff.y < 0)
                        {
                            isDashing = true;

                            if (Vector3.Distance(inimigo.transform.position, transform.position) < 4)
                                speed = 15 * speedModifier;
                            else
                                speed = 40 * speedModifier;



                            waitCounter = 1;

                            paOnde = "baixo";
                            //Slide();
                        }
                        else
                        {
                            isDashing = true;

                            if (Vector3.Distance(inimigo.transform.position, transform.position) < 4)
                                speed = 15 * speedModifier;
                            else
                                speed = 40 * speedModifier;



                            waitCounter = 1;

                            paOnde = "cima";
                            //Jump();
                        }
                    }
                    else
                    {
                        if (diff.x < 0)
                        {
                            isDashing = true;

                            if (Vector3.Distance(inimigo.transform.position, transform.position) < 4)
                                speed = 15 * speedModifier;
                            else
                                speed = 40 * speedModifier;


                            waitCounter = 1;

                            paOnde = "esquerda";
                            //ChangeLane(-1);
                        }
                        else
                        {
                            isDashing = true;

                            if (Vector3.Distance(inimigo.transform.position, transform.position) < 4)
                                speed = 15 * speedModifier;
                            else
                                speed = 40 * speedModifier;


                            waitCounter = 1;

                            paOnde = "direita";
                            //ChangeLane(1);
                        }
                    }

                    isSwiping = false;
                }
            }

            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                startingTouch = Input.GetTouch(0).position;
                isSwiping = true;
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                isSwiping = false;
            }

        }
        /////////////////////////////////////




        /////////////////////////////////////
        //DASH!

        if (Input.GetKeyDown("w"))
        {
            isDashing = true;

            if (Vector3.Distance(inimigo.transform.position, transform.position) < 4)
                speed = 15 * speedModifier;
            else
                speed = 40 * speedModifier;


            waitCounter = 1;

            paOnde = "cima";
        }

        if (Input.GetKeyDown("s"))
        {
            isDashing = true;


            if (Vector3.Distance(inimigo.transform.position, transform.position) < 4)
                speed = 15 * speedModifier;
            else
                speed = 40 * speedModifier;



            waitCounter = 1;

            paOnde = "baixo";
        }

        if (Input.GetKeyDown("d"))
        {
            isDashing = true;

            if (Vector3.Distance(inimigo.transform.position, transform.position) < 4)
                speed = 15 * speedModifier;
            else
                speed = 40 * speedModifier;


            waitCounter = 1;

            paOnde = "direita";
        }

        if (Input.GetKeyDown("a"))
        {
            isDashing = true;

            if (Vector3.Distance(inimigo.transform.position, transform.position) < 4)
                speed = 15 * speedModifier;
            else
                speed = 40 * speedModifier;


            waitCounter = 1;

            paOnde = "esquerda";
        }

        if (waitCounter > 0.9f && waitCounter < 1.4f)
        {
            waitCounter += Time.deltaTime;

            Vector3 direcao = inimigo.transform.position - transform.position;


            if (paOnde == "cima")
            {
                if (Vector3.Distance(inimigo.transform.position, transform.position) > 15)
                    controller.Move(direcao.normalized * speed * Time.deltaTime);
            }


            if (paOnde == "baixo")
                controller.Move(-direcao.normalized * speed * Time.deltaTime);


            if (paOnde == "direita")
            {
                Vector3 novaDirecao = new Vector3(direcao.z, direcao.y, -direcao.x);

                controller.Move(novaDirecao.normalized * speed * Time.deltaTime);

            }


            if (paOnde == "esquerda")
            {
                Vector3 novaDirecao = new Vector3(-direcao.z, direcao.y, direcao.x);

                controller.Move(novaDirecao.normalized * speed * Time.deltaTime);
            }


            if (paOnde == "ataque")
            {
                if (Vector3.Distance(inimigo.transform.position, transform.position) > 3)
                    controller.Move(direcao.normalized * speed * Time.deltaTime);
            }


        }

        if (waitCounter >= 1.4f)
        {
            speed = 10 * speedModifier;
            waitCounter = 0;

            isDashing = false;
        }
        //////////////////////////////////////
        ///





        //ATAQUE
        if (comboIncremento == 0)
        {
            if (attacking && podeAtacar)
            {
                attacking = false;

                StartCoroutine(TaxaDeLancamento(1f));

                StartCoroutine(AttackWithStartupTime(0.2f));
                comboIncremento++;
                //print("incrementou pra " + comboIncremento);
            }
        }
        else
        {
            if (attacking)
            {
                attacking = false;

                StartCoroutine(TaxaDeLancamento(1f));

                StartCoroutine(AttackWithStartupTime(0.2f));
                if (comboIncremento < 3)
                    comboIncremento++;
                //print("incrementou pra " + comboIncremento);
            }
        }

        /*if(attacking && podeAtacar)
        {
            if(hitboxAtaque1.activeSelf)
            {
                hitboxAtaque2.SetActive(true);

                StartCoroutine(TempoPraDesativarAHitbox(1f));

                hitboxAtaque1.SetActive(false);
                hitboxAtaque3.SetActive(false);
            }

            if (hitboxAtaque2.activeSelf)
            {
                hitboxAtaque3.SetActive(true);

                StartCoroutine(TempoPraDesativarAHitbox(1f));

                hitboxAtaque1.SetActive(false);
                hitboxAtaque2.SetActive(false);
            }

            else if (hitboxAtaque1.activeSelf == false && hitboxAtaque2.activeSelf == false)
            {
                hitboxAtaque1.SetActive(true);

                StartCoroutine(TempoPraDesativarAHitbox(1f));

                hitboxAtaque2.SetActive(false);
                hitboxAtaque3.SetActive(false);
            }

        }*/

    }

    private IEnumerator TempoPraDesativarAHitbox(float qtdDeSegundos)
    {
        podeAtacar = false;
        yield return new WaitForSeconds(qtdDeSegundos);
        attacking = false;
        podeAtacar = true;
        yield return new WaitForSeconds(qtdDeSegundos / 2);
        hitboxAtaque1.SetActive(false);
        hitboxAtaque2.SetActive(false);
        hitboxAtaque3.SetActive(false);
    }


    private IEnumerator TaxaDeLancamento(float qtdDeSegundos)   //Taxa de lançamento do combo de ataque
    {
        podeAtacar = false;
        yield return new WaitForSeconds(qtdDeSegundos);
        if (comboIncremento == 2)
            yield return new WaitForSeconds(qtdDeSegundos);
        if (comboIncremento == 3)
            yield return new WaitForSeconds(qtdDeSegundos);

        podeAtacar = true;
    }

    private IEnumerator AttackWithStartupTime(float qtdDeSegundos)    //Aqui é onde realmente acontece a magia do combo de ataque
    {
        //speed = 4;

        yield return new WaitForSeconds(qtdDeSegundos);

        //GameObject hitboxAtaqueInstanciado = Instantiate(hitboxAtaque, transform.position + Vector3.Cross(transform.right, new Vector3(0, 6, -5)), Quaternion.identity) as GameObject;

        if (comboIncremento == 1)
        {
            hitboxAtaque1.SetActive(true);
            //print("combo1, incrementou pra " + comboIncremento);

            audioSource.clip = audioAtaque1;
            audioSource.Play();
        }
        if (comboIncremento == 2)
        {
            hitboxAtaque1.SetActive(false);
            hitboxAtaque2.SetActive(true);
            //print("combo2, incrementou pra " + comboIncremento);

            audioSource.clip = audioAtaque2;
            audioSource.Play();
        }
        if (comboIncremento == 3)
        {
            hitboxAtaque2.SetActive(false);
            hitboxAtaque3.SetActive(true);
            //print("combo3, incrementou pra " + comboIncremento);
            StartCoroutine(EsperarPraZerarOComboIncremento(0.2f));

            //comboIncremento = 0;

            audioSource.clip = audioAtaque3;
            audioSource.Play();
        }

        // speed = 10;

    }

    private IEnumerator EsperarPraZerarOComboIncremento(float qtdDeSegundos)
    {
        yield return new WaitForSeconds(qtdDeSegundos);
        comboIncremento = 0;
        podeAtacar = true;
    }



    public void OnTriggerEnter(Collider other)    //Se encostar na hitbox do inimigo, perde vida
    {
        if (other.tag == "HitboxEnemy")
        {
            other.gameObject.SetActive(false);
            vida -= 10 * defesa;

            audioSource.clip = audioTomandoDano;
            audioSource.Play();

            GameObject obj = Instantiate(particula, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            Destroy(obj, 1);



            if (vida <= 0)
            {
                if (bonusLifeAtivado == false)
                {
                    audioSource.clip = audioMorrendo;
                    audioSource.Play();

                    vida = vidaMaxima;
                    Destroy(gameObject);

                    Time.timeScale = 0;
                    painelGameOver.SetActive(true);
                    barraVida.SetActive(false);
                    botaoPause.SetActive(false);
                }
            }

            //StartCoroutine(KnockbackRoutine(2f, other));
        }
    }


    public GameObject FindClosestEnemy()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }

        if (closest == null)
        {
            podeSpawnarPortal = true;
            vida = vidaMaxima;
        }

        return closest;
    }



    //Como era o movimento antes de ser tudo dodge

    /*public void CalculoMovimentoSeila(float h, float v)
    {
        Vector3 dir;

        dir = new Vector3(h, 0f, v).normalized; ///////////////trocar o terceiro 0f pra "vertical" se quiser voltar ao normal

        playerState = "walking";

        float targetAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoorthVelocity, turnSmoothTime);

        transform.rotation = Quaternion.Euler(0f, angle, 0f);



        Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        controller.Move(moveDir.normalized * speed * Time.deltaTime);
    }*/

}
