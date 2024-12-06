using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAnimation : MonoBehaviour
{
   
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        if (animator != null)
            animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
