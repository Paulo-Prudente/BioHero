using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyViewBoss : MonoBehaviour
{
    public EnemyBoss enemy = null;

    private void OnTriggerEnter(Collider other)
    {
        enemy.VisaoTriggerEnter(other);
    }

    private void OnTriggerExit(Collider other)
    {
        enemy.VisaoTriggerExit(other);
    }
}
