using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    FPSController fPSController;
    // Start is called before the first frame update
    void Start()
    {
        fPSController = transform.parent.GetComponent<FPSController>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(-fPSController.rotateSpeed * Time.deltaTime * Input.GetAxis("Mouse Y"), 0, 0);
    }
}
