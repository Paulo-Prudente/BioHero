using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyViewStatic : MonoBehaviour
{
    public EnemyStaticAI enemy = null;

    private void OnTriggerEnter(Collider other)
    {
        enemy.VisaoTriggerEnter(other);
    }

    private void OnTriggerExit(Collider other)
    {
        enemy.VisaoTriggerExit(other);
    }
}
