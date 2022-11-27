using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedAttackScript : MonoBehaviour
{
    void FixedUpdate()
    {
        if (isActiveAndEnabled)
        {
            transform.Translate(Vector3.forward * 30f * Time.fixedDeltaTime);
        }
    }
}
