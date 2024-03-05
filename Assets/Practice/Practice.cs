using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Practice : MonoBehaviour
{
    RaycastHit hit;
    [SerializeField]
    float layDis;
    bool ishit;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.DrawRay(transform.position,transform.forward * layDis, Color.blue);
            if(Physics.Raycast(transform.position, transform.forward, out hit, layDis, 1 << LayerMask.NameToLayer("Enemy")))
            {
                hit.transform.GetComponent<MeshRenderer>().material.color = Color.red;
                Debug.Log(1);
            }
            
            ishit = Physics.Raycast(transform.position, Vector3.forward, layDis, 1 << LayerMask.NameToLayer("Enemy"));
        }
    }

}
