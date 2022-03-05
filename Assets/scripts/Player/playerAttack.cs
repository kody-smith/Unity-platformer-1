using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttack : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private float attackCooldown;
    [SerializeField] private float colliderDistance;

    [SerializeField] private LayerMask enemy;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] public Transform attackPoint;
    [SerializeField] private AudioClip slash;
    
    private Animator ani;
    private playerMove playerMove;
    private Melee meleeEnemy;
    private Health enemyHealth;

    private float cooldown = Mathf.Infinity;
    public float range = 0.5f;

    private void Awake()
    {
        ani = GetComponent<Animator>();
        playerMove = GetComponent<playerMove>();
        enemyHealth = GetComponent<Health>();
    }

    private void Attack()
    {
        if(Input.GetMouseButtonDown(0))
        {
            SoundManager.instance.Play(slash);
        }
        ani.SetTrigger("attack");
        cooldown = 0;       

        Collider2D[] hit = Physics2D.OverlapCircleAll(attackPoint.position,range,enemy); 

        foreach (Collider2D enemies in hit)
        {
            enemies.GetComponent<Health>().TakeDamage(1);
        }
        
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
            Attack();
            

        cooldown += Time.deltaTime;
    }
    
    private void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, range);
    }
}
