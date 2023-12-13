using Unity.VisualScripting;
using UnityEngine;

public class SesEfekt : MonoBehaviour
{
    [SerializeField] AudioClip hareketSesi;
    [SerializeField] AudioClip carpmaSesi;
    [SerializeField] AudioClip kazanmaSesi;
    [SerializeField] AudioClip kaybetmeSesi;
    [SerializeField] AudioClip altinSesi;
    [SerializeField] AudioClip zirhSesi;


    private AudioSource audioKaynak;

    void Start()
    {
        // AudioSource bileþenini alýyoruz veya ekliyoruz
        audioKaynak = GetComponent<AudioSource>();
    }

    //swerve yapýnca çalacak ses
    internal void HareketSesi()
    {
        audioKaynak.PlayOneShot(hareketSesi);
    }
    //kazanýnca çalacak ses
    internal void KazanmaSesi()
    {
        audioKaynak.PlayOneShot(kazanmaSesi);
    }
    //kaybedince çalacak ses
    internal void KaybetmeSesi()
    {
        audioKaynak.PlayOneShot(kaybetmeSesi);
    }
    //çarpýnca çalacak ses
    internal void CarpmaSes()
    {
        audioKaynak.PlayOneShot(carpmaSesi);
    }
    internal void AltinSesi()
    {
        audioKaynak.PlayOneShot(altinSesi);
    }
    internal void ZirhSes()
    {
        audioKaynak.PlayOneShot(zirhSesi);
    }
}