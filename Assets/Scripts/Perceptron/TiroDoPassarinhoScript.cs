using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiroDoPassarinhoScript : MonoBehaviour
{
    private GameObject inimigo;
    void Update()
    {
        inimigo = FindClosestEnemy();

        if (inimigo != null)
        {
            transform.LookAt(inimigo.transform.position);
            transform.position = Vector3.MoveTowards(transform.position, inimigo.transform.position, Time.deltaTime * 30);
        }
        else
            Destroy(this.gameObject);

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
            Destroy(this.gameObject);

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
        return closest;
    }
}
