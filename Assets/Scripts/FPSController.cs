using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class FPSController : MonoBehaviour
{
    Animator animator;
    [SerializeField] float moveSpeed = 3;
    public float rotateSpeed = 240;
    private int pushCube, AIInteraction;
    private StreamWriter sw;
    public GameObject QuitButton;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        QuitButton = GameObject.Find("Canvas");
        QuitButton.SetActive(false);

        animator.SetBool("isMagical", false);
        

        // if output data file exists, clear the previous data in the file
        if (File.Exists("OutputData.txt"))
        {
            File.Delete("OutputData.txt");
        }
    }

    /*public void ReceivesMagic()
    {
        AIInteraction++;
        isMagical = true;
    }
    */

    // Update is called once per frame
    void Update()
    {
        // moves in world upon wasd keys
        Vector2 axisInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        axisInput.Normalize();
        transform.Translate(moveSpeed * Time.deltaTime * axisInput.x, 0, moveSpeed * Time.deltaTime * axisInput.y);  
        
        // perspective rotates based on horizontal mouse movement
        transform.Rotate(0, rotateSpeed * Time.deltaTime * Input.GetAxis("Mouse X"), 0);

        // 0 for idle and 1 for walking
        animator.SetFloat("Speed", axisInput.magnitude);

        // if pressing left shift on keyboard, player will enter push mode
        if (Input.GetKey("left shift")){
            animator.SetBool("isPushing", true);
        }
        if (!Input.GetKey("left shift")){
            animator.SetBool("isPushing", false);
        }

        // if pressing e on keyboard, player will enter waving mode
        if (Input.GetKey("e")){
            animator.SetBool("isWaving", true);
        }
        if (!Input.GetKey("e")){
            animator.SetBool("isWaving", false);
        }

        // if pressing r on keyboard while player status is magical, player will enter doing magic mode
        if (Input.GetKey("r") && animator.GetBool("isMagical")){
            animator.SetBool("isDoingMagic", true);
        }
        if (!Input.GetKey("r") || !animator.GetBool("isMagical")){
            animator.SetBool("isDoingMagic", false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // pushcube value increments if player successfully pushes a cube 
        if (collision.gameObject.CompareTag("Cube"))
        {
            pushCube++;
        }
    }

   
    private void OnTriggerEnter(Collider other)
    {
        // if player exits maze area by colliding with the boundary trigger collider, data file will be generated
        // player has to be in magical status to destroy glowing cubes 
        // this action also increments pushcube value
        if (other.gameObject.CompareTag("GlowingCube") && animator.GetBool("isMagical"))
        {
            Destroy(other);
            animator.SetBool("isMagical", false);
            pushCube++;
        }
        if (other.gameObject.CompareTag("Boundary"))
        {
            float finalTime = Time.realtimeSinceStartup;
            Vector3 finalPosition = transform.position;

            sw = new StreamWriter("OutputData.txt", true);

            sw.Write("Player pushed " + pushCube.ToString() + " cube(s)!" + "\n");
            sw.Flush();

            sw.Write("Player interacted " + AIInteraction.ToString() + " time(s) with the wizard!" + "\n");
            sw.Flush();

            sw.Write("Player took " + finalTime.ToString() + " seconds to finish the game!" + "\n");
            sw.Flush();

            sw.Write("Player's final position is " + finalPosition.ToString() + "\n");
            sw.Flush();

            sw.Close();

            QuitButton.SetActive(true);
        }
    }
}
