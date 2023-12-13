using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KameraTakip : MonoBehaviour
{

    [SerializeField] Transform kup;                                                          //inspector kısmından karakterimizi bunun içine atacağım.

    [SerializeField] private Vector3 kameraMesafe = new (0, 5.09f, -12.01f);          //inspector kısmından kamera mesafesini ayarlayacağım.

    // Kamera takip hızı
    [SerializeField] private float kameraTakipHizi = 1.2f;                                  //inspector kısmından kamera takip hızını ayarlayacağım

    private Vector3 kupPozisyon;

    void FixedUpdate()
    {
        //new vector3 diyerek x y z konumunu ayarlıyoruz.
        kupPozisyon = new Vector3(transform.position.x, kup.position.y + kameraMesafe.y, kup.position.z + kameraMesafe.z);
        //transform.position.x ile kameranın x yönünü ayarlandığı gibi kalmasını sağlıyoruz.
        //kup.position.y ile küpümüzün y pozisyonuna eşitliyoruz + kameraMesafesi.y ile de y pozisyonunu değiştiriyoruz(inspectorden ayarladığımız işte..)
        //kup.position.z + kameraMesafesi.z aynı y de olduğu işlemi z pozisyonunda yapıyor.
        //bu işlemlerin hepsini kupPozisyon değişkenine atıyoruz.

        //kupPozisyon değişkenini de aktif hale getirmek için transform.position komutunun içine atıyoruz.
        transform.position = Vector3.Lerp(transform.position, kupPozisyon, kameraTakipHizi * Time.fixedDeltaTime);
        //transform.position ile pozisyonu her fixedupdate framesi boyunca güncelliyoruz.
        //vector3.lerp[1] ile mevcut konumumuzu(transform.position) baz alarak küpün pozisyonunu(kupPozisyon) takip eder.      burası yanlış bilgi olabilir.
        //kameraTakipHizi ile a ile b arasındaki geçiş hızını ayarlıyoruz. daha fazla detay en aşağıdaki [1] notunda.
    }
}

//[1]
//a: Başlangıç noktası. İki değer arasında geçişin başladığı nokta. (transform.position,)
//b: Hedef nokta. İki değer arasında geçişin tamamlandığı nokta.    (kupPozisyon,)
//t: Bu, geçişin ne kadar ilerlediğini kontrol eden bir değerdir. Bu değer 0 ile 1 arasında olmalıdır.  (kameraTakipHizi)