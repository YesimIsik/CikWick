using UnityEngine;


    public class PlayerInteractionController : MonoBehaviour
// Oyuncunun �evre ile etkile�imlerini kontrol eder.

{

    private PlayerController _playerController;
    //Bu s�n�fta kullan�lmak �zere saklanan referans. Daha sonra hareket veya �zellik g�ncellemek i�in kullan�l�r.

    private void Awake()
    //Bir script'in ba�l� oldu�u GameObject aktif olurken ilk �al��an fonksiyondur.Burada component referanslar� atan�r.
    {
        _playerController = GetComponent<PlayerController>();
        //Bu sat�r ayn� GameObject �zerinde bulunan PlayerController bile�enini bulur ve _playerController de�i�kenine atar.
    }
    private void OnTriggerEnter(Collider other)
    //Oyuncu bir "Trigger Collider" alan�na girdi�inde �al���r.
    {

        if (other.gameObject.TryGetComponent<ICollectible>(out var collectible))
        //Bu sat�r �arp���lan nesnenin ICollectible aray�z�n� uygulay�p uygulamad���n� kontrol eder.
        {
            collectible.Collect();
            //E�er �arp���lan nesne ICollectible'se, Collect() metodu �al��t�r�l�r.
        }

    }

    private void OnCollisionEnter(Collision other)
    //Bu Unity fonksiyonu, fiziksel bir �arp��ma oldu�unda tetiklenir.



    {
        if (other.gameObject.TryGetComponent<IBoostable>(out var boostable))
        // Oyuncuya ge�ici bir g��lendirme (boost) etkisi verecek olan aray�zd�r.E�er varsa boostable adl� de�i�kende tutulur.
        {
            boostable.Boost(_playerController);
            //Parametre olarak _playerController g�nderilir. B�ylece boost etkisi do�rudan oyuncunun kontrol� �zerinde uygulan�r.
        }
    }
}