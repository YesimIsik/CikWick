using UnityEngine;


    public class PlayerInteractionController : MonoBehaviour
// Oyuncunun çevre ile etkileþimlerini kontrol eder.

{
    [SerializeField] private Transform _playerVisualTransform;

    private PlayerController _playerController;
    //Bu sýnýfta kullanýlmak üzere saklanan referans. Daha sonra hareket veya özellik güncellemek için kullanýlýr.

    private Rigidbody _playerRigidbody;

    private void Awake()
    //Bir script'in baðlý olduðu GameObject aktif olurken ilk çalýþan fonksiyondur.Burada component referanslarý atanýr.
    {
        _playerController = GetComponent<PlayerController>();
        //Bu satýr ayný GameObject üzerinde bulunan PlayerController bileþenini bulur ve _playerController deðiþkenine atar.
        _playerRigidbody = GetComponent<Rigidbody>();
            

    }
    private void OnTriggerEnter(Collider other)
    //Oyuncu bir "Trigger Collider" alanýna girdiðinde çalýþýr.
    {

        if (other.gameObject.TryGetComponent<ICollectible>(out var collectible))
        //Bu satýr çarpýþýlan nesnenin ICollectible arayüzünü uygulayýp uygulamadýðýný kontrol eder.
        {
            collectible.Collect();
            //Eðer çarpýþýlan nesne ICollectible'se, Collect() metodu çalýþtýrýlýr.
        }

    }

    private void OnCollisionEnter(Collision other)
    //Bu Unity fonksiyonu, fiziksel bir çarpýþma olduðunda tetiklenir.



    {
        if (other.gameObject.TryGetComponent<IBoostable>(out var boostable))
        // Oyuncuya geçici bir güçlendirme (boost) etkisi verecek olan arayüzdür.Eðer varsa boostable adlý deðiþkende tutulur.
        {
            boostable.Boost(_playerController);
            //Parametre olarak _playerController gönderilir. Böylece boost etkisi doðrudan oyuncunun kontrolü üzerinde uygulanýr.
        }
    }

    private void OnParticleCollision(GameObject other)//Bir Particle System bu objeyle çarpýþtýðýnda otomatik olarak çaðrýlýr.
    {
        if(other.TryGetComponent<IDamageable>(out var damagable))//other objesinde IDamageable arayüzünü uygulayan bir bileþen olup olmadýðýný kontrol eder.
                                                                 //Varsa, damagable deðiþkenine referansý aktarýr.
                                                                 //Bu sayede çarpýlan objeye zarar verilip verilemeyeceði anlaþýlýr.


        {
            damagable.GiveDamage(_playerRigidbody,_playerVisualTransform);//Eðer obje zarar alabiliyorsa (IDamageable), GiveDamage metodu çaðrýlýr.
            CameraShake.Instance.ShakeCamera(1f, 0.5f);//Kamera sallanma efekti baþlatýlýr.
                                                       //1f: Sallanmanýn þiddeti.
                                                       //0.5f: Sallanmanýn süresi.

        }
    }


}
