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
        // AudioSource bile�enini al�yoruz veya ekliyoruz
        audioKaynak = GetComponent<AudioSource>();
    }

    //swerve yap�nca �alacak ses
    internal void HareketSesi()
    {
        audioKaynak.PlayOneShot(hareketSesi);
    }
    //kazan�nca �alacak ses
    internal void KazanmaSesi()
    {
        audioKaynak.PlayOneShot(kazanmaSesi);
    }
    //kaybedince �alacak ses
    internal void KaybetmeSesi()
    {
        audioKaynak.PlayOneShot(kaybetmeSesi);
    }
    //�arp�nca �alacak ses
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