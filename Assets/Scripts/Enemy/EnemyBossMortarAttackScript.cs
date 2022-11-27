using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossMortarAttackScript : MonoBehaviour
{
    void Update()
    {
        if (isActiveAndEnabled)
        {
            transform.Translate(Vector3.down * 5 * Time.deltaTime);
        }
    }
}
