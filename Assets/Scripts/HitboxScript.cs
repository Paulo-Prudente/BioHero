using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxScript : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(AutoDestruir());
    }

    private IEnumerator AutoDestruir()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            gameObject.SetActive(false);

    }

}
