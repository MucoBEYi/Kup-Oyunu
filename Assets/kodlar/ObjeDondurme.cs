using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjeDondurme : MonoBehaviour
{
    public Rigidbody rb;
    private Quaternion dortYonlu;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //otomatik döndürme
        dortYonlu = Quaternion.Euler(Vector3.up * 100 * Time.deltaTime);            //mantýk araþtýrýlacak. öncelik seviyesi: 10/5
        rb.rotation *= dortYonlu;
    }
}