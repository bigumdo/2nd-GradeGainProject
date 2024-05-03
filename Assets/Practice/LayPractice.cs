using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayPractice : MonoBehaviour
{
    RaycastHit hit;
    [SerializeField]
    float rayDistance;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.DrawRay(transform.position,transform.forward * rayDistance, Color.black);
            if(Physics.Raycast(transform.position, transform.forward, out hit, rayDistance, 1 << LayerMask.NameToLayer("Enemy")))
            {
                hit.transform.GetComponent<MeshRenderer>().material.color = Color.red;
            }
            
        }
    }

}
