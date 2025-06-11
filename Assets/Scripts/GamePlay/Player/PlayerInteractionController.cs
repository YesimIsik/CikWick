using UnityEngine;


    public class PlayerInteractionController : MonoBehaviour
// Oyuncunun �evre ile etkile�imlerini kontrol eder.

{
    [SerializeField] private Transform _playerVisualTransform;

    private PlayerController _playerController;
    //Bu s�n�fta kullan�lmak �zere saklanan referans. Daha sonra hareket veya �zellik g�ncellemek i�in kullan�l�r.

    private Rigidbody _playerRigidbody;

    private void Awake()
    //Bir script'in ba�l� oldu�u GameObject aktif olurken ilk �al��an fonksiyondur.Burada component referanslar� atan�r.
    {
        _playerController = GetComponent<PlayerController>();
        //Bu sat�r ayn� GameObject �zerinde bulunan PlayerController bile�enini bulur ve _playerController de�i�kenine atar.
        _playerRigidbody = GetComponent<Rigidbody>();
            

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

    private void OnParticleCollision(GameObject other)//Bir Particle System bu objeyle �arp��t���nda otomatik olarak �a�r�l�r.
    {
        if(other.TryGetComponent<IDamageable>(out var damagable))//other objesinde IDamageable aray�z�n� uygulayan bir bile�en olup olmad���n� kontrol eder.
                                                                 //Varsa, damagable de�i�kenine referans� aktar�r.
                                                                 //Bu sayede �arp�lan objeye zarar verilip verilemeyece�i anla��l�r.


        {
            damagable.GiveDamage(_playerRigidbody,_playerVisualTransform);//E�er obje zarar alabiliyorsa (IDamageable), GiveDamage metodu �a�r�l�r.
            CameraShake.Instance.ShakeCamera(1f, 0.5f);//Kamera sallanma efekti ba�lat�l�r.
                                                       //1f: Sallanman�n �iddeti.
                                                       //0.5f: Sallanman�n s�resi.

        }
    }


}
