using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delete : MonoBehaviour
{
    public GameObject swip;
    // Start is called before the first frame update
    private void Awake()
    {
        StartCoroutine(waiter());
    }

    IEnumerator waiter() 
    {
        yield return new WaitForSeconds(3);
        Object.Destroy(this.gameObject);
        swip.SetActive(true);
    }
}
