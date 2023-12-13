using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDongu : MonoBehaviour
{
    public Tetiklenme _tetiklenme;

    public GameObject engeller;

    public float zaman;

    void Start()
    {
        _tetiklenme = FindAnyObjectByType<Tetiklenme>();                //sanýrým herhangi bir objeden tetikleme scriptini kendine yerleþtiriyor.

        StartCoroutine(DonguObjeler(zaman));
    }


    public IEnumerator DonguObjeler(float _zaman)
    {
        //engelleri klonluyoruz
        while (_tetiklenme.kazanmaKontrol)
        {
            Instantiate(engeller, new Vector3(Random.Range(-3.5f, 3.5f), 1.58f, Random.Range(-0f, 1000.5f)), Quaternion.identity);

            yield return new WaitForSeconds(_zaman);
        }
    }
}
