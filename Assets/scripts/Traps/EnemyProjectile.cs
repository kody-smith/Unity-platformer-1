using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : EnemyDamage
{
    [SerializeField] private float speed;
    [SerializeField] private float reset;
    private float lifeTime;

    public void Activate()
    {
        lifeTime = 0;
        gameObject.SetActive(true);
    }
    public void Update()
    {
        float moveSpeed = speed * Time.deltaTime;
        transform.Translate(moveSpeed, 0,0);

        lifeTime += Time.deltaTime;
        if(lifeTime > reset)
        {
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision); //Execute logic from parent script first
        gameObject.SetActive(false); //When this hits any object deactivate arrow
    }
}
