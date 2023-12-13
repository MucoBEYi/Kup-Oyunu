using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Donguler : MonoBehaviour
{
    public Transform[] dongu;
    public float mesafe;
    void Start()
    {

    }

    private void OnTriggerEnter(Collider etkilesim)
    {
        if (etkilesim.CompareTag("dongu"))
        {
            dongu[0].localPosition += new Vector3(0, 0, dongu[0].localScale.z * mesafe);
            Array.Reverse(dongu);
        }
    }

}
