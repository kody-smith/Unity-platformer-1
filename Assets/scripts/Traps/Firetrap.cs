using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firetrap : MonoBehaviour
{

    [SerializeField] private float damage;

    [Header ("Firetrap Timers")]
    [SerializeField] private float activationDelay;
    [SerializeField] private float activeTime;
    private Animator ani;
    private SpriteRenderer ren;

    [Header ("Sound")]
    [SerializeField] private AudioClip fireClip;

    private bool triggered; // Trap active
    private bool active; // Can hurt player

    private void Awake()
    {
        ani = GetComponent<Animator>();
        ren = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Player")
        {
            if(!triggered)
            {
                //trigger firetrap
                StartCoroutine(ActivateFiretrap());

            }
            if(active)
                collider.GetComponent<Health>().TakeDamage(damage);
        }
    }
    private IEnumerator ActivateFiretrap()
    {
        triggered = true;
        ren.color = Color.red; 
        yield return new WaitForSeconds(activationDelay);
        SoundManager.instance.Play(fireClip);
        ren.color = Color.white; 
        active = true;

        ani.SetBool("activated", true);

        yield return new WaitForSeconds(activeTime);
        active = false;
        triggered = false;

        ani.SetBool("activated", false);
    }

}
