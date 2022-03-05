using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform arrowPoint;
    [SerializeField] private GameObject[] arrows;
    private float cooldownTime;

    [Header ("Sound")]
    [SerializeField] private AudioClip arrowClip;

    private void Attack()
    {
        cooldownTime = 0;

        SoundManager.instance.Play(arrowClip);
        arrows[FindArrow()].transform.position = arrowPoint.position;
        arrows[FindArrow()].GetComponent<EnemyProjectile>().Activate();
    }

    private int FindArrow()
    {
        for (int i = 0; i < arrows.Length; i++)
        {
            if(!arrows[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }

    private void Update()
    {
        cooldownTime += Time.deltaTime;

        if(cooldownTime >= attackCooldown)
        {
            Attack();
        }
    }
}
