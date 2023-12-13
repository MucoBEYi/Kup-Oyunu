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
        //otomatik d�nd�rme
        dortYonlu = Quaternion.Euler(Vector3.up * 100 * Time.deltaTime);            //mant�k ara�t�r�lacak. �ncelik seviyesi: 10/5
        rb.rotation *= dortYonlu;
    }
}