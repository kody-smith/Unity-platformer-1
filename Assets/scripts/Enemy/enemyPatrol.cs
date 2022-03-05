using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyPatrol : MonoBehaviour
{
    [Header("Patrol points")]
    [SerializeField] private Transform left;
    [SerializeField] private Transform right;

    [Header("Movement")]
    [SerializeField] private Transform enemy;
    [SerializeField] private float speed;
    private Vector3 initScale;
    private bool movingLeft;

    [SerializeField] private float idleTime;
    private float idleTimer;

    [Header("Enemy Animator")]
    [SerializeField] private Animator ani;

    private void Awake()
    {
        initScale = enemy.localScale;
    }

    private void OnDisable()
    {
        ani.SetBool("moving", false);
    }

    private void Update()
    {
        if(movingLeft)
        {
            if(enemy.position.x >= left.position.x)
            {
                Move(-1);
            }
            else
            {
                DirectionChange();
            }

        }
        else
        {
            if(enemy.position.x <= right.position.x)
            {
                Move(1);
            }
            else
            {
                DirectionChange();
            }
        }
    }

    private void DirectionChange()
    {
         ani.SetBool("moving", false);
        idleTimer += Time.deltaTime;

        if(idleTimer > idleTime)
        {
            movingLeft = !movingLeft;
        }
    }

    private void Move(int _direction)
    {
        idleTimer =0;
        ani.SetBool("moving", true);
        //Face in direction
        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * _direction, initScale.y, initScale.z);
        //Move in direction
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * _direction * speed, enemy.position.y, enemy.position.z);
    }

}

//27:37