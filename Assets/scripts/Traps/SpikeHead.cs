using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeHead : EnemyDamage
{
    [SerializeField] private float speed;
    [SerializeField] private float sight;
    [SerializeField] private float checkDelay;
    [SerializeField] private LayerMask playerLayer;
    private Vector3[] directions = new Vector3[4];
    private Vector3 destination = new Vector3();
    private float checkTimer;
    private bool attack;

    [Header ("Sounds")]
    [SerializeField] private AudioClip impactClip;
    

    private void onEnable()
    {
        Stop();
    }

    private void Update()
    {
        //Move only if attacking
        if(attack)
        {
            transform.Translate(destination * Time.deltaTime * speed);
        }
        else
        {
            checkTimer += Time.deltaTime;
            if(checkTimer > checkDelay)
            {
                CheckPlayer();
            }
        }
    }
    private void CheckPlayer()
    {
        CalculateDirection();
        //Check if player is visible to enemy
        for (int i = 0; i < directions.Length; i++)
        {
            Debug.DrawRay(transform.position, directions[i],Color.red);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directions[i], sight, playerLayer);
            if (hit.collider != null && !attack)
            {
                attack = true;
                destination = directions[i];
                checkTimer = 0;
            }
        }
    }
    private void CalculateDirection()
    {
        directions[0] = transform.right * sight; // Go right
        directions[1] = -transform.right * sight; // Go left
        directions[2] = transform.up * sight; // Go up
        directions[1] = -transform.up * sight; // Go down
    }
    private void Stop()
    {
        destination = transform.position; //Set destination as current position so it doesn't move
        attack = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SoundManager.instance.Play(impactClip);
        base.OnTriggerEnter2D(collision);
        Stop(); //Stop spikehead once hits something
    }
}
