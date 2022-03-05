using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float speed;
    private float currX;
    private Vector3 velocity = Vector3.zero;

    //Follow player
    [SerializeField] private Transform player;
    [SerializeField] private float ahead;
    [SerializeField] private float camSpeed;
    private float lookAhead;
    private void Update()
    {
        //Room Camera
        // transform.position = Vector3.SmoothDamp(transform.position, new Vector3(currX, transform.position.y, transform.position.z),ref velocity, speed);
        transform.position = new Vector3(player.position.x + lookAhead, transform.position.y, transform.position.z);
        lookAhead = Mathf.Lerp(lookAhead, (ahead * player.localScale.x), Time.deltaTime * camSpeed);
    }

    public void MoveToNewRoom(Transform _newRoom)
    {
        currX = _newRoom.position.x;
    }
}
