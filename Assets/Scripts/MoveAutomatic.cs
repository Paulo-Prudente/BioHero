using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAutomatic : MonoBehaviour
{
    public Transform Target;
    public float speed = 1;
    public float t = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 a = transform.position;
        Vector3 b = Target.position;
        transform.position = Vector3.MoveTowards(a, Vector3.Lerp(a, b, t), speed);
    }
}
