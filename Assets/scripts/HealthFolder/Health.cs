using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth {get; private set;}
    private Animator ani;
    private bool dead;

    [Header("iFrames")]
    [SerializeField] private float invulnerableFrames;
    [SerializeField] private int numOfFlashes;
    private SpriteRenderer ren;

    [Header("Components")]
    [SerializeField] private Behaviour[] components;
    private bool invulnerable;

    [Header("Sounds")]
    [SerializeField] private AudioClip deathClip;
    [SerializeField] private AudioClip hurtClip;

    private void Awake()
    {
        currentHealth = startingHealth;
        ani = GetComponent<Animator>();
        ren = GetComponent<SpriteRenderer>();
    }
    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            ani.SetTrigger("hurt");
            //iframes
            StartCoroutine(Invulnerability());
            SoundManager.instance.Play(hurtClip);
        }
        else
        {
            if((!dead))
            {
                ani.SetTrigger("die");

                foreach (Behaviour component in components)
                    component.enabled = false;

                dead = true;
                SoundManager.instance.Play(deathClip);

            }
        }
    }
    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }
    private IEnumerator Invulnerability()
    {
        Physics2D.IgnoreLayerCollision(10,11,true);
        //Duration
        for (int i = 0; i < numOfFlashes; i++)
        {
            ren.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(invulnerableFrames / (numOfFlashes * 3));
            ren.color = Color.white;
            yield return new WaitForSeconds(invulnerableFrames / (numOfFlashes * 3));
        }
        Physics2D.IgnoreLayerCollision(10,11,false);
    }

}
