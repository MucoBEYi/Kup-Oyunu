using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Tetiklenme : MonoBehaviour
{
    public Rigidbody rb;
    //z�rh
    [SerializeField] int anlikZirh = 1;
    [SerializeField] int maxZirh = 1;
    [SerializeField] Text zirhText;

    //alt�n
    [SerializeField] Text altinText;
    private int altin = 0;


    //kaybetme ve kazanma text
    [SerializeField] Text beceremedinText;


    //kazanma ve kaybetme sonras� hareket etmeyi engellemek i�in
    public bool kazanmaKontrol = true;

    //ses i�in miras alma
    private SesEfekt _sesEfekt;


    void Start()
    {
        kazanmaKontrol = true;
        //oyun ba�lad���mda �a��rd���m textler ve rigidbody
        rb = GetComponent<Rigidbody>();
        _sesEfekt = Object.FindFirstObjectByType<SesEfekt>();
        AnaEkranZirhText();
        AnaEkranAltinText();

    }

    //h�zl� yeniden ba�latma
    private void Update()
    {
        if (Input.GetKey("r"))
        {
            Invoke("GeriSar", 0);
        }
    }

    //etkile�im kontrolleri
    private void OnTriggerEnter(Collider etkilesim)
    {
        //alt�n kontrol
        if (etkilesim.CompareTag("altin"))
        {
            etkilesim.gameObject.SetActive(false);
            altin++;
            AnaEkranAltinText();

            _sesEfekt.AltinSesi();
        }
        if (etkilesim.CompareTag("buyukAltin"))
        {
            etkilesim.gameObject.SetActive(false);
            altin += 2;
            AnaEkranAltinText();

            _sesEfekt.AltinSesi();
        }
        if (etkilesim.CompareTag("devasaAltin"))
        {
            etkilesim.gameObject.SetActive(false);
            altin += 5;
            AnaEkranAltinText();

            _sesEfekt.AltinSesi();
        }
        //z�rh kontrol
        if (etkilesim.CompareTag("zirh"))
        {
            etkilesim.gameObject.SetActive(false);
            ZirhArttir();
        }
    }

    //�arp��ma kontrol
    private void OnCollisionEnter(Collision carpisma)
    {
        if (carpisma.collider.CompareTag("bitisCizgi"))
        {
            Becerdin();
            _sesEfekt.KazanmaSesi();
        }
        //engel nesnesine �arpt���na �al��acak
        if (carpisma.collider.CompareTag("engel"))
        {
            ZirhEksilt();
        }
        //portal nesnesine �arpt���nda �al��acak
        if (carpisma.collider.CompareTag("portal"))
        {
            transform.Translate(Vector3.forward * 122f);
        }
        //ekran1 e ge�i�
        if (carpisma.collider.CompareTag("ekran 1"))
        {
            Ekran1();
        }
        //ekran2 ye ge�i�
        if (carpisma.collider.CompareTag("ekran 2"))
        {
            Ekran2();
        }
    }

    //z�rh ve alt�n eksiltme
    private void ZirhEksilt()
    {
        anlikZirh--;

       rb.AddForce (0, 0, -10, ForceMode.VelocityChange);       

        //ceza alt�n eksilir                                           
        if (altin >= 3)
        {
            altin -= 3;
            AnaEkranAltinText();
        }
        else
        {
            altin = 0;
            AnaEkranAltinText();
        }

        //z�rh -1 olursa oyun kaybedilir fakat ekrana z�rh de�eri yans�t�lmaz.
        if ((anlikZirh <= -1))
        {
            Beceremedin();
        }
        //oyun devam etti�i s�rece �arpt��� engellerden ses ��kmas�n� sa�lar
        if ((anlikZirh >= 0))
        {
            AnaEkranZirhText();

            if (KazanmaKontrol() != true)
            {
                _sesEfekt.CarpmaSes();
            }
        }
    }

    //z�rh artt�rma 
    private void ZirhArttir()
    {

        if ((anlikZirh < maxZirh))
        {
            anlikZirh++;
           _sesEfekt.ZirhSes();
            AnaEkranZirhText();
        }
    }

    //z�rh text g�ncellemesi
    private void AnaEkranZirhText()
    {
        zirhText.text = "Z�rh " + anlikZirh.ToString();

    }
    //alt�n text g�ncellemesi
    private void AnaEkranAltinText()
    {
        altinText.text = "Alt�n " + altin.ToString();

    }

    //kaybedince ne olaca��
    internal void Beceremedin()
    {
        kazanmaKontrol = false;
        altinText.gameObject.SetActive(false);
        zirhText.gameObject.SetActive(false);
        rb.velocity = Vector3.zero;                         //engele �arpt���nda art�k kuvvet uygulayarak geri gitmeyecek.
        rb.angularVelocity = Vector3.zero;
        beceremedinText.text = "Beceremedin \n<size=60>Alt�n " + altin.ToString() + "</size> \n<size=40>Yeniden ba�lamak i�in r tu�una bas</size>";


        _sesEfekt.KaybetmeSesi();

        //Time.timeScale = 0f;                   //zaman du-
    }

    //kazan�nca ne olaca��
    private void Becerdin()
    {
        kazanmaKontrol = false;
        altinText.gameObject.SetActive(false);                  //d�ng� kodunu yapamad�m...
        zirhText.gameObject.SetActive(false);
        rb.velocity = Vector3.zero;                         //engele �arpt���nda art�k kuvvet uygulayarak geri gitmeyecek.
        rb.angularVelocity = Vector3.zero;
        beceremedinText.text = "Kazand�n \n<size=60>Alt�n " + altin.ToString() + "</size> \n<size=40>Yeniden ba�lamak i�in r tu�una bas</size>";

        _sesEfekt.KaybetmeSesi();
    }

    //ekran 2 ye ge�i�
    private void Ekran2()
    {

        SceneManager.LoadSceneAsync("ekran 2");

    }

    //ekran 1 e ge�i�
    private void Ekran1()
    {
        SceneManager.LoadSceneAsync("ekran 1");
    }

    //sahne s�f�rlama
    private void GeriSar()
    {
        AnaEkranAltinText();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);             //�al��ma mant��� ara�t�r�lacak.
    }

    //oyunu kazand�ysa veya kaybettiyse hareket etmeyle ilgili fonksyonlar� kapat�r[miras al�nd�]
    internal bool KazanmaKontrol()
    {
        if (kazanmaKontrol == false)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
