using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [HideInInspector] public Animator animator;
    [HideInInspector] public AudioSource audioSource;

    private void Awake()
    {
        animator = transform.Find("Visual").GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

    }



}
