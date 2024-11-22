using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Batteries : MonoBehaviour
{
    [Header("“d’r‚Ì“–‚½‚è”»’è"), SerializeField]
    private BoxCollider batterie;

    private void Update()
    {
        if (this.transform.parent)
        {
            batterie.isTrigger = true;
        }
        else
        {
            batterie.isTrigger = false;
        }
    }
}
