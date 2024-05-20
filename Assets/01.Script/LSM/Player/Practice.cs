using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Practice : MonoBehaviour
{
    public Rigidbody rigid;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            //rigid.velocity = Vector3.zero;
            rigid.AddForce(new Vector3(0, 10, 0), ForceMode.Impulse);
        }
    }
}
