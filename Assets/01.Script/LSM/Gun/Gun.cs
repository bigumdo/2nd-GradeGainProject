using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    [SerializeField] private float _gunDistance;
    [SerializeField] private LayerMask targetMask;

    private Transform _firePos;
    private LineRenderer _lineRenderer;
    private Player _plyaer;

    private void Awake()
    {
        _firePos = transform.Find("FirePos");
        _lineRenderer = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        _plyaer = GameManager.Instance.player;
        _lineRenderer.positionCount = 2;
        _lineRenderer.SetPosition(0, _firePos.position);
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {

            StartCoroutine(GunFire());
        }
    }

    private IEnumerator GunFire()
    {
        Vector3 mouse = GameManager.Instance.mainCam.transform.forward;
        Vector3 vec = mouse * _gunDistance;
        _lineRenderer.enabled = true;
        _lineRenderer.SetPosition(1, vec);
        if (Physics.Raycast(_firePos.position, _plyaer.transform.forward, _gunDistance, targetMask))
            Debug.Log(1);
        yield return new WaitForSeconds(0.3f);
        _lineRenderer.enabled = false;

    }

}
