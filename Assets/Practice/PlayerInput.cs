using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    public float speed;
    public GameObject plan;

    private Rigidbody rigid;
    private Ray ray;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        rigid.velocity = new Vector3(h, 0, v) * speed;

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //transform.forward = new Vector3(h, 0, v);
        RaycastHit rayHit;
        if (Physics.Raycast(ray, out rayHit, 100, LayerMask.NameToLayer("Floor")))
        {
            Vector3 nextDirection = new Vector3(rayHit.point.x, transform.position.y, rayHit.point.z) - transform.position;
            transform.forward = nextDirection;
        }
        //if (Physics.Raycast(ray, out rayHit, 100, LayerMask.NameToLayer("A"))&&Input.GetMouseButtonDown(0))
        //{
        //    if(rayHit.transform.CompareTag("Player"))
        //        plan.SetActive(true);

        //}

    }
}
