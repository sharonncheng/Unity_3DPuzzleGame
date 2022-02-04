using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    Animator animator;
    public GameObject player;
    public LayerMask whatIsPlayer;

    float sightDistance = 20;
    // float sightAngle = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.Find("FPSController");
    }

    private void FixedUpdate()
    {
        Collider[] colliders = Physics.OverlapSphere(this.transform.position, sightDistance, whatIsPlayer);
        if(colliders.Length > 0 && Input.GetKey("e")){
            animator.SetBool("isGivingMagic", true);
            player.GetComponentInChildren<Animator>().SetBool("isMagical", true);
        } else {
            animator.SetBool("isGivingMagic", false);
        }
    }

    // Update is called once per frame
    /*void Update()
    {
        // get distance between player and wizard AI
        float distance = Vector3.Distance(transform.position, player.transform.position);

        // get angle between player and wizard AI
        Vector3 playerDir = player.transform.position - transform.position;
        float angle = Vector3.Angle(playerDir, transform.forward);

        // wizard AI gives magic to player if player is within wizard AI's sight and is waving
        if (distance <= sightDistance && angle <= sightAngle && player.GetComponentInChildren<Animator>().GetBool("isWaving")){
            animator.SetBool("isGivingMagic", true);
            player.SendMessage("ReceivesMagic");
        }
        if (distance > sightDistance || angle > sightAngle || !player.GetComponentInChildren<Animator>().GetBool("isWaving")){
            animator.SetBool("isGivingMagic", false);
        }
    }
    */
}
