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
    //zýrh
    [SerializeField] int anlikZirh = 1;
    [SerializeField] int maxZirh = 1;
    [SerializeField] Text zirhText;

    //altýn
    [SerializeField] Text altinText;
    private int altin = 0;


    //kaybetme ve kazanma text
    [SerializeField] Text beceremedinText;


    //kazanma ve kaybetme sonrasý hareket etmeyi engellemek için
    public bool kazanmaKontrol = true;

    //ses için miras alma
    private SesEfekt _sesEfekt;


    void Start()
    {
        kazanmaKontrol = true;
        //oyun baþladýðýmda çaðýrdýðým textler ve rigidbody
        rb = GetComponent<Rigidbody>();
        _sesEfekt = Object.FindFirstObjectByType<SesEfekt>();
        AnaEkranZirhText();
        AnaEkranAltinText();

    }

    //hýzlý yeniden baþlatma
    private void Update()
    {
        if (Input.GetKey("r"))
        {
            Invoke("GeriSar", 0);
        }
    }

    //etkileþim kontrolleri
    private void OnTriggerEnter(Collider etkilesim)
    {
        //altýn kontrol
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
        //zýrh kontrol
        if (etkilesim.CompareTag("zirh"))
        {
            etkilesim.gameObject.SetActive(false);
            ZirhArttir();
        }
    }

    //çarpýþma kontrol
    private void OnCollisionEnter(Collision carpisma)
    {
        if (carpisma.collider.CompareTag("bitisCizgi"))
        {
            Becerdin();
            _sesEfekt.KazanmaSesi();
        }
        //engel nesnesine çarptýðýna çalýþacak
        if (carpisma.collider.CompareTag("engel"))
        {
            ZirhEksilt();
        }
        //portal nesnesine çarptýðýnda çalýþacak
        if (carpisma.collider.CompareTag("portal"))
        {
            transform.Translate(Vector3.forward * 122f);
        }
        //ekran1 e geçiþ
        if (carpisma.collider.CompareTag("ekran 1"))
        {
            Ekran1();
        }
        //ekran2 ye geçiþ
        if (carpisma.collider.CompareTag("ekran 2"))
        {
            Ekran2();
        }
    }

    //zýrh ve altýn eksiltme
    private void ZirhEksilt()
    {
        anlikZirh--;

       rb.AddForce (0, 0, -10, ForceMode.VelocityChange);       

        //ceza altýn eksilir                                           
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

        //zýrh -1 olursa oyun kaybedilir fakat ekrana zýrh deðeri yansýtýlmaz.
        if ((anlikZirh <= -1))
        {
            Beceremedin();
        }
        //oyun devam ettiði sürece çarptýðý engellerden ses çýkmasýný saðlar
        if ((anlikZirh >= 0))
        {
            AnaEkranZirhText();

            if (KazanmaKontrol() != true)
            {
                _sesEfekt.CarpmaSes();
            }
        }
    }

    //zýrh arttýrma 
    private void ZirhArttir()
    {

        if ((anlikZirh < maxZirh))
        {
            anlikZirh++;
           _sesEfekt.ZirhSes();
            AnaEkranZirhText();
        }
    }

    //zýrh text güncellemesi
    private void AnaEkranZirhText()
    {
        zirhText.text = "Zýrh " + anlikZirh.ToString();

    }
    //altýn text güncellemesi
    private void AnaEkranAltinText()
    {
        altinText.text = "Altýn " + altin.ToString();

    }

    //kaybedince ne olacaðý
    internal void Beceremedin()
    {
        kazanmaKontrol = false;
        altinText.gameObject.SetActive(false);
        zirhText.gameObject.SetActive(false);
        rb.velocity = Vector3.zero;                         //engele çarptýðýnda artýk kuvvet uygulayarak geri gitmeyecek.
        rb.angularVelocity = Vector3.zero;
        beceremedinText.text = "Beceremedin \n<size=60>Altýn " + altin.ToString() + "</size> \n<size=40>Yeniden baþlamak için r tuþuna bas</size>";


        _sesEfekt.KaybetmeSesi();

        //Time.timeScale = 0f;                   //zaman du-
    }

    //kazanýnca ne olacaðý
    private void Becerdin()
    {
        kazanmaKontrol = false;
        altinText.gameObject.SetActive(false);                  //döngü kodunu yapamadým...
        zirhText.gameObject.SetActive(false);
        rb.velocity = Vector3.zero;                         //engele çarptýðýnda artýk kuvvet uygulayarak geri gitmeyecek.
        rb.angularVelocity = Vector3.zero;
        beceremedinText.text = "Kazandýn \n<size=60>Altýn " + altin.ToString() + "</size> \n<size=40>Yeniden baþlamak için r tuþuna bas</size>";

        _sesEfekt.KaybetmeSesi();
    }

    //ekran 2 ye geçiþ
    private void Ekran2()
    {

        SceneManager.LoadSceneAsync("ekran 2");

    }

    //ekran 1 e geçiþ
    private void Ekran1()
    {
        SceneManager.LoadSceneAsync("ekran 1");
    }

    //sahne sýfýrlama
    private void GeriSar()
    {
        AnaEkranAltinText();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);             //çalýþma mantýðý araþtýrýlacak.
    }

    //oyunu kazandýysa veya kaybettiyse hareket etmeyle ilgili fonksyonlarý kapatýr[miras alýndý]
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
