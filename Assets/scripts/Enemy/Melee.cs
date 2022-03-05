using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private int damage;
    [SerializeField] public int health;
    [SerializeField] private float range;
    [SerializeField] private float colliderDistance;

    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask player;
    [SerializeField] private AudioClip slash;
    
    public Health playerHealth;

    private Animator ani;

    private enemyPatrol enemyPatrol;

    private float cooldown = Mathf.Infinity;
    
    private void Awake()
    {
        ani = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<enemyPatrol>();
    }
    private void Update()
    {
        cooldown += Time.deltaTime;
        //Attack when player is in sight
        if(PlayerInSight())
        {
            if(cooldown >= attackCooldown && playerHealth.currentHealth > 0)
            {
                cooldown = 0;
                SoundManager.instance.Play(slash);
                ani.SetTrigger("meleeAttack");
            }
        }
        if(enemyPatrol != null)
        {
            enemyPatrol.enabled = !PlayerInSight();
        }
        
        
    }
    private bool PlayerInSight()
    {
        RaycastHit2D hit = 
            Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, player);

        if (hit.collider != null)
            playerHealth = hit.transform.GetComponent<Health>();

        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
        new Vector3(boxCollider.bounds.size.x * range,boxCollider.bounds.size.y,boxCollider.bounds.size.z));
    }

    private void DamagePlayer()
    {
        if(PlayerInSight())
        {
            playerHealth.TakeDamage(damage);
        }
    }
    
}
