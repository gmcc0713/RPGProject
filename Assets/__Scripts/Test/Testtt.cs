using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testtt : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.forward * 15, Color.red);
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            if (hit.collider.CompareTag("Ground"))
            {
                Debug.Log("Ãæµ¹");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
