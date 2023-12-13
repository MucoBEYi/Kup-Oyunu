using System;
using UnityEngine;



public class Oyuncu_swerve : MonoBehaviour
{

    [SerializeField] float hareketHizi = .5f;


    //mathf.lerp için gereken 2 kod
    [SerializeField] float xGecisHizi = 12f;
    //mathf.clamp icin gereken 3 kod
    public float yeniXPozisyon;                                         //private yapılacak.
    [SerializeField] float xGecisBoyutu = 3.5f;
    [SerializeField] Vector2 koridor = new(-3.5f, 3.5f);


    //oyun başındaki x pozisyonumuzu hatırlamak için gereken kod
    private float nonFuzuli;

    [SerializeField] float hizlanmaMiktar = 20;
    private float hizlanmaSure = 5;
    private float hizlanmaSureZaman;


    SesEfekt _sesEfekt;

    public Rigidbody rb;

    Tetiklenme _tetiklenme;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        nonFuzuli = rb.position.x;                             //start metodunda kendi mevcut x konumumuzu non fuzuli değişkenine tanımlıyoruz.
        _sesEfekt = FindFirstObjectByType<SesEfekt>();
        _tetiklenme = FindFirstObjectByType<Tetiklenme>();
    }

    void Update()
    {
        Swerve();

    }
    void FixedUpdate()
    {
        Hareket();
    }
    //sağa ve sola hareket
    private void Swerve()
    {
        //vakit olursa swerve sıfırdan yapılacak.
        if (Input.GetButtonDown("Horizontal"))          //düğmeye basıldığında tetiklenir(basılı tutmanın faydası yok)
        {
            if (_tetiklenme.KazanmaKontrol() != true)            //valla bir şeyler yaptım ama bende bilmiyom
            {
                yeniXPozisyon = Mathf.Clamp(rb.position.x + Input.GetAxisRaw("Horizontal") * xGecisBoyutu, nonFuzuli + koridor.x, nonFuzuli + koridor.y);
                //yeniXPozisyon değişkeni bizim pozisyonumuzu güncelleyecek içerisindeki kodlarla.
                //mathf.clamp bir değeri belirli aralıklar içinde sınırlandırmak için kullanılır[1]
                //rb.position.x mevcut konumumuz, Input.GetAxisRaw("Horizontal") keskin bir şekilde sağa sola gitmemizi sağlıyor.
                //xGesicBoyutu ise mevcut konumumuzu baz alarak GetAxis'in raw(-1 yada 1 olabiliyor) değeri ile çarpılıyor. çıkan sonuca göre sağa veya sola hareket ediyor.
                //kordior.x ile sola, koridor.y ile sağa gider. bunu start metodunda tanımladığımız nonFuzuli pozisyonuna göre yapar.

                _sesEfekt.HareketSesi();
            }
        }
    }

    //sağa sola geçiş hızı ve hareket
    private void Hareket()
    {
        if (_tetiklenme.KazanmaKontrol() != true)
        {
            //swerve geçiş hızı(neden hareketin içinde hatırlamıyorum).
            rb.MovePosition(new Vector3(Mathf.Lerp(rb.position.x, yeniXPozisyon, xGecisHizi * Time.fixedDeltaTime), rb.position.y, rb.position.z));

            //ne yazacağımı bilmiyorum hiç bir şey anlamadım 12. saatim..
            hizlanmaSureZaman += Time.fixedDeltaTime;

            if (hizlanmaSure <= hizlanmaSureZaman)
            {
                hareketHizi += hizlanmaMiktar;

                hizlanmaSureZaman = 0;
            }
            rb.AddForce(Vector3.forward * hareketHizi * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }


        //fizik kurallarını ihlal eden kod:
        //rb.MovePosition(new Vector3(Mathf.Lerp(rb.position.x, yeniXPozisyon, xGecisHizi * Time.fixedDeltaTime), rb.position.y, rb.position.z + hareketHizi * Time.fixedDeltaTime));

        //rb.moveposition diyerek hareket etmesini istiyoruz. new vector3 diyerek x y z konumunu belirtmek istiyoruz.
        //mathf.lerp diyerek 3 tane değer gireceğimizi ve bu 3 değer arasında gidip geleceğimizi belirtiyoruz[2] 
        //mathf.lerp() içerisindeki: mevcut konumumuzu baz alarak yeniXpozisyon ile değişen pozisyonumuzu gecisHizi değişkeniyle geçiş hızını ayarlıyor ve time.fixedDeltatime ile dengeleştiriyor. 
        //rb.position.y y konumumuzu her fixupdate framesinde güncelliyor fakat bir işleme bağlamadık.
        //rb.position.z mevcut konumunu hatırlıyor ve hareketHizi ile topluyor. böylelikle mevcut konumunu bizim hareketHizi değişkenin değeriyle her fixupdate framesi başına işlem yapıyor. time de frameye göre hesaplıyor. kısaca sadece ileri gitmemizi sağlıyor.
    }
}

//[1]
//value: Sınırlanacak değer. 
//min: Minimum değer.   buraya hangi sayıyı yazarsanız valuve bu sayının altına inemeyecek
//max: Maksimum değer.  buraya hangi sayıyı yazarsanız valuve bu sayının üstüne çıkamayacak



//[2] =
// a: Başlangıç değeri. İki değer arasında geçişin başladığı nokta.
// b: Bitiş değeri. İki değer arasında geçişin tamamlandığı nokta.
// t: Geçiş oranı. Bu değer 0 ile 1 arasında olmalıdır. 0, tamamen a değerine, 1 ise tamamen b değerine karşılık gelir. özet: bir noktadan diğer noktaya geçiş hızı(sanırım)
//mathf sanırım tek bir pozisyon için kullanılıyor. aksi taktirde vector3.lerp kullanılıyor(kesin bilgi değildir).