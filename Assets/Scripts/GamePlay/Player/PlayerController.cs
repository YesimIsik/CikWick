using System;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public event Action OnPlayerJumped;
    //Oyuncu zıpladığında tetiklenecek bir olaydır.Diğer bileşenler bu olaya abone olarak zıplama anında belirli işlemleri gerçekleştirebilir.
    public event Action<PlayerState> OnPlayerStateChanged;

    [Header("References")]
    //Unity editöründe ilgili değişkenleri gruplamak için kullanılır.
    [SerializeField] private Transform _orientationTransform;
    //Oyuncunun yönünü belirlemek için kullanılan referans transform.

    [Header("Movement Settings")]
    //Unity editöründe ilgili değişkenleri gruplamak için kullanılır.
    [SerializeField] private KeyCode _movomentKey;
    //
    [SerializeField] private float _movementSpeed;
    //

    [Header("Jump Settings")]
    //Unity editöründe ilgili değişkenleri gruplamak için kullanılır.
    [SerializeField] private KeyCode _jumpKey;
    //Oyuncunun zıplama eylemini tetikleyeceği klavye tuşunu belirler.SerializeField sayesinde bu özel (private) değişken Unity Editor'de görünür ve ayarlanabilir hale gelir.
    [SerializeField] private float _jumpForce;
    //Zıplama sırasında uygulanacak kuvvetin büyüklüğünü belirler.Daha yüksek bir değer daha yüksek bir zıplama sağlar.
    [SerializeField] private float _jumpCooldown;
    //Zıplama işlemleri arasındaki bekleme süresini saniye cinsinden belirler.Bu oyuncunun sürekli zıplamasını engeller.
    [SerializeField] private bool _canJump;
    //Oyuncunun şu anda zıplayıp zıplayamayacağını belirten bayrak.Zıplama sonrası false yapılır ve bekleme süresi sonunda tekrar true olur.
    [SerializeField] private float _airMultiplier;
    //Havadayken hareket kuvvetine uygulanan çarpan.Bu havadayken oyuncunun hareket hızını kontrol eder.
    [SerializeField] private float _airDrag;
    //Havadayken oyuncuya uygulanan sürüklenme miktarı.Daha yüksek bir değer havadayken hareketin daha hızlı yavaşlamasını sağlar.

    [Header("Sliding Settings")]
    //Unity editöründe ilgili değişkenleri gruplamak için kullanılır.
    [SerializeField] private KeyCode _slideKey;
    //Oyuncunun kayma eylemini başlatacağı klavye tuşunu belirler.
    [SerializeField] private float _slideMultiplier;
    //Kayma sırasında hareket kuvvetine uygulanan çarpan.Bu kayma hızını kontrol eder.
    [SerializeField] private float _slideDrag;
    //Kayma sırasında oyuncuya uygulanan sürüklenme miktarı.Daha yüksek bir değer kayma sırasında daha hızlı yavaşlamayı sağlar.

    [Header("Ground Check Settings")]
    //Unity editöründe ilgili değişkenleri gruplamak için kullanılır.
    [SerializeField] private float _playerHeight;
    //Oyuncunun yüksekliğini temsil eder. Bu değer zeminle temasın kontrolünde kullanılır.
    [SerializeField] private LayerMask _graundLayer;
    //Zemin olarak kabul edilen katmanları belirtir.Bu oyuncunun zeminle temasını kontrol ederken hangi katmanların zemin olarak kabul edileceğini belirler.
    [SerializeField] private float _groundDrag;
    //Oyuncu zemindeyken uygulanan sürüklenme miktarı.Bu zeminde hareket ederken oyuncunun hızının nasıl etkileneceğini kontrol eder.


    private StateController _stateController;
    //Oyuncunun mevcut durumunu yöneten bileşen.
    private Rigidbody _playerRigidbody;
    //Oyuncunun fiziksel hareketlerini kontrol eden Rigidbody bileşeni.
    private float _startingMovementSpeed, _startingJumpForce;

    private float _horizontalInput, _verticalInput;
    //Oyuncunun yatay ve dikey girişlerini saklar.
    private Vector3 _movementDirection;
    // Oyuncunun hareket yönünü belirler.
    private bool _isSliding;
    //Oyuncunun kayma durumunu belirtir.


    private void Awake()
    //Awake metodu script yüklendiğinde ilk çalışan metottur.
    {
        _stateController = GetComponent<StateController>();
        _playerRigidbody = GetComponent<Rigidbody>();
        //GetComponent<T>() metodu ile aynı GameObject üzerindeki StateController ve Rigidbody bileşenleri alınır.
        _playerRigidbody.freezeRotation = true;
        //Rigidbody'nin fiziksel etkileşimlerle dönmesini engeller.
        _startingMovementSpeed = _movementSpeed;
        _startingJumpForce = _jumpForce;


    }

    private void Update()
    //Update metodu her karede bir kez çağrılır.
    {
        SetInputs();
        //Kullanıcı girişlerini işler.
        SetState();
        //Oyuncunun durumunu günceller.
        SetPlayerDrag();
        //Oyuncunun sürtünme değerini ayarlar.
        LimitPlayerSpeed();
        //Oyuncunun hızını sınırlar.
    }

    private void FixedUpdate()
    //FixedUpdate metodu sabit zaman aralıklarında çağrılır ve fiziksel işlemler için kullanılır.
    {
        SetPlayerMovement();
        //Oyuncunun hareketini fiziksel olarak uygular.
    }

    private void SetInputs()
    //Bu oyuncu girdilerini işlemek için tanımlanmış özel bir metottur.Oyuncunun hareket, kayma ve zıplama gibi eylemlerini algılar.
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        //Belirtilen eksende yatay giriş değerini alır.
        _verticalInput = Input.GetAxisRaw("Vertical");
        //Belirtilen eksende dikey giriş değerini alır.


        if (Input.GetKeyDown(_slideKey))
        //Oyuncu _slideKey olarak tanımlanan tuşa bastıysa kod bloğu çalışır.Input.GetKeyDown fonksiyonu tuşa basıldığı anı algılar ve sadece o karede true döner.
        {
            _isSliding = true;
            //Oyuncunun kayma durumunu belirten _isSliding true olur.Bu oyuncunun kayma eylemini başlattığını gösterir.

        }
        else if (Input.GetKeyDown(_movomentKey))
        //Eğer oyuncu _movomentKey olarak tanımlanan tuşa bastıysa kod bloğu çalışır.Bu kayma durumundan çıkmak veya normal harekete dönmek için kullanılabilir.
        {
            _isSliding = false;
            //Oyuncunun kayma durumunu belirten _isSliding false olur.Bu oyuncunun kayma eylemini sonlandırdığını gösterir.

        }

        else if (Input.GetKey(_jumpKey) && _canJump && IsGrounded())
        //Eğer oyuncu _jumpKey olarak tanımlanan tuşa basılı tutuyorsa, oyuncu zıplayabiliyorsa ve oyuncu zemindeyse kod bloğu çalışır.
        //Input.GetKey fonksiyonu tuşa basılı tutulduğu sürece true döner.
        {
            _canJump = false;
            //Oyuncunun zıplama yeteneğini geçici olarak devre dışı bırakır.Bu zıplama sonrası bir bekleme süresi (cooldown) uygulanmasını sağlar.
            SetPlayerJumping();
            //Oyuncunun zıplama eylemini gerçekleştiren fonksiyonu çağırır.Bu fonksiyon genellikle zıplama kuvvetini uygular ve ilgili animasyonları tetikler.
            Invoke(nameof(ResetJumping), _jumpCooldown);
            //Belirtilen _jumpCooldown süresi sonunda ResetJumping fonksiyonunu çağırır.Bu zıplama bekleme süresi sona erdiğinde oyuncunun tekrar zıplayabilmesini sağlar.
        }
    }
    private void SetState()
    //Bu kod oyuncunun mevcut hareket durumunu belirlemek ve gerektiğinde bu durumu güncellemek için tanımlanmış özel bir metottur.
    {
        var movementDirection = GetMovementDirection();
        //Oyuncunun hareket yönünü alır.Bu oyuncunun hangi yöne doğru hareket ettiğini belirlemek için kullanılır.
        var isGrounded = IsGrounded();
        //Oyuncunun zeminde olup olmadığını kontrol eder.Bu oyuncunun havada mı yoksa yerde mi olduğunu belirlemek için kullanılır.
        var isSliding = IsSliding();
        //Oyuncunun kayma durumunda olup olmadığını kontrol eder.Bu oyuncunun şu anda kayma eylemi yapıp yapmadığını belirlemek için kullanılır.
        var currentState = _stateController.GetCurrentState();
        //Oyuncunun mevcut hareket durumunu alır.Bu oyuncunun şu anda hangi hareket durumunda olduğunu belirlemek için kullanılır.

        var newState = currentState switch
        //Oyuncunun yeni hareket durumunu belirler.
        {
            _ when movementDirection == Vector3.zero && isGrounded && !isSliding => PlayerState.Idle,
            //Oyuncu hareket etmiyor, zeminde ve kayma durumunda değilse, durum Idle (boşta) olarak ayarlanır.
            _ when movementDirection != Vector3.zero && isGrounded && !isSliding => PlayerState.Move,
            //Oyuncu hareket ediyor, zeminde ve kayma durumunda değilse, durum Move (hareket) olarak ayarlanır.
            _ when movementDirection != Vector3.zero && isGrounded && isSliding => PlayerState.Slide,
            //Oyuncu hareket ediyor, zeminde ve kayma durumundaysa, durum Slide (kayma) olarak ayarlanır.
            _ when movementDirection == Vector3.zero && isGrounded && isSliding => PlayerState.SlideIdle,
            //Oyuncu hareket etmiyor, zeminde ve kayma durumundaysa, durum SlideIdle (kayma halinde boşta) olarak ayarlanır.
            _ when !_canJump && !isGrounded => PlayerState.Jump,
            //Oyuncu zıplama yeteneğine sahip değil (muhtemelen zıpladı) ve zeminde değilse, durum Jump (zıplama) olarak ayarlanır.
            _ => currentState
            //Yukarıdaki durumların hiçbiri geçerli değilse, mevcut durum korunur.
        };

        if (newState != currentState)
        //Yeni belirlenen durum, mevcut durumdan farklıysa, aşağıdaki kod bloğu çalışır.
        {
            _stateController.ChangeState(newState);
            //Oyuncunun durumunu yeni belirlenen duruma günceller.Bu genellikle animasyonları tetiklemek veya belirli hareket davranışlarını değiştirmek için kullanılır.
            OnPlayerStateChanged?.Invoke(newState);
        }


    }
    private void SetPlayerMovement()
    {
        _movementDirection = _orientationTransform.forward * _verticalInput
            + _orientationTransform.right * _horizontalInput;
        //Bu satır, oyuncunun hareket yönünü belirler.

        float forceMultiplier = _stateController.GetCurrentState() switch
        //Bu kod bloğu, oyuncunun mevcut durumuna göre kuvvet çarpanını belirler.
        {
            PlayerState.Move => 1f,
            //Oyuncu hareket ediyorsa, çarpan 1 olarak ayarlanır.
            PlayerState.Slide => _slideMultiplier,
            //Oyuncu kayma durumundaysa, _slideMultiplier değeri kullanılır.
            PlayerState.Jump => _airMultiplier,
            //Oyuncu zıplama durumundaysa, _airMultiplier değeri kullanılır.
            _ => 1f
            //Diğer durumlar için varsayılan olarak 1 kullanılır.

        };
        _playerRigidbody.AddForce(_movementDirection.normalized * _movementSpeed * forceMultiplier, ForceMode.Force);
        //Bu satır, oyuncunun Rigidbody bileşenine kuvvet uygular. 

    }

    private void SetPlayerDrag()
    //Bu oyuncunun hareket durumuna bağlı olarak Rigidbody bileşeninin lineer sürtünme katsayısını (linearDamping) ayarlayan özel bir metottur.
    {
        _playerRigidbody.linearDamping = _stateController.GetCurrentState() switch
        //Bu kod mevcut hareket durumunu alır ve buna göre linearDamping değerini belirler.switch ifadesi farklı durumlar için farklı sürtünme katsayıları atamak amacıyla kullanılır.
        {
            PlayerState.Move => _groundDrag,
            //Eğer oyuncu hareket ediyorsa (Move durumu) zemin üzerindeki sürtünme katsayısı (_groundDrag) uygulanır.Bu oyuncunun hareketini yavaşlatmak için kullanılır.
            PlayerState.Slide => _slideDrag,
            //Eğer oyuncu kayma durumundaysa kayma sırasında uygulanacak sürtünme katsayısı kullanılır.Bu kayma hareketinin süresini ve mesafesini kontrol etmek için önemlidir.
            PlayerState.Jump => _airDrag,
            //Eğer oyuncu zıplama durumundaysa (Jump durumu) havadayken uygulanacak sürtünme katsayısı (_airDrag) kullanılır.Bu havadaki hareketin kontrolünü sağlar.
            _ => _playerRigidbody.linearDamping
            //Yukarıdaki durumların hiçbiri geçerli değilse mevcut linearDamping değeri korunur.Bu beklenmeyen durumlarda sürtünme katsayısının değişmemesini sağlar.
        };



    }
    private void LimitPlayerSpeed()
    //Bu oyuncunun yatay düzlemdeki (X ve Z eksenleri) hızını sınırlamak için kullanılan özel bir metottur.
    {
        Vector3 flatVelocity = new Vector3(_playerRigidbody.linearVelocity.x, 0f, _playerRigidbody.linearVelocity.z);
        //Bu Rigidbody bileşeninin yatay düzlemdeki hızını temsil eden vektör oluşturur.Düşey hareketi göz ardı edilir.Bu karakterin yatay hareket hızını sınırlamak için kullanılır.

        if (flatVelocity.magnitude > _movementSpeed)
        //Bu koşul eğer oyuncunun yatay hızı (flatVelocity.magnitude) belirlenen maksimum hareket hızından (_movementSpeed) büyükse çalışır.
        {
            Vector3 limitedVelocity = flatVelocity.normalized * _movementSpeed;
            //Bu satır mevcut yatay hız vektörünün yönünü koruyarak büyüklüğünü sınırlar.
            _playerRigidbody.linearVelocity = new Vector3(limitedVelocity.x, _playerRigidbody.linearVelocity.y, limitedVelocity.z);
            //Bu satır oyuncunun Rigidbody bileşeninin linearVelocity özelliğini günceller.yatay hız bileşenleri (x ve z),sınırlı hız vektöründen (limitedVelocity) alınır.
            //Düşey hız bileşeni (y), mevcut linearVelocity.y değeri korunarak değiştirilmez. Bu, zıplama veya düşme gibi düşey hareketlerin etkilenmemesini sağlar.
        }
    }


    private void SetPlayerJumping()
    //
    {
        OnPlayerJumped?.Invoke();
        //Bu genellikle oyuncunun zıplama eylemini gerçekleştirdiğinde diğer sistemlerin (animasyonlar,ses efektleri,kullanıcı arayüzü güncellemeleri) bilgilendirilmesi için kullanılır.
        _playerRigidbody.linearVelocity = new Vector3(_playerRigidbody.linearVelocity.x, 0f, _playerRigidbody.linearVelocity.z);
        //Bu oyuncunun Rigidbody bileşeninin düşey hızını sıfırlar,zıplama sırasında mevcut düşey hareketi etkisini ortadan kaldırarak zıplamanın tutarlı ve öngörülebilir olmasını sağlar.
        _playerRigidbody.AddForce(transform.up * _jumpForce, ForceMode.Impulse);
        //Bu satır oyuncunun Rigidbody bileşenine yukarı yönlü bir kuvvet uygular.
    }


    private void ResetJumping()
    //Oyuncunun tekrar zıplayabilmesini sağlamak için kullanılan metodtur.
    {
        _canJump = true;
    //Genellikle bir zıplama işleminden sonra belirli bir süre beklenir ve bu süre sonunda bu metod çağrılarak oyuncunun tekrar zıplamasına izin verilir.

    }
    #region Helpers Functions
    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, _playerHeight * 0.5f + 0.2f, _graundLayer);
        //Oyuncunun zemine temas edip etmediğini kontrol eder.
    }

    private Vector3 GetMovementDirection()
    //Oyuncunun hareket yönünü birim vektör olarak döndürür.
    {
        return _movementDirection.normalized;
        //Birim vektör olarak hareket yönünü döndürür.Bu genellikle hareket hesaplamalarında yönü belirlemek için kullanılır.
    }

    private bool IsSliding()
    //Oyuncunun şu anda kayma (slide) durumunda olup olmadığını kontrol eder.
    {
        return _isSliding;
        //Oyuncunun kayma durumunu belirten bir boole değişkenidir. true ise oyuncu kayıyor, false ise kaymıyor demektir.
    }
    public void SetMovementSpeed(float speed, float duration)//Bu yöntem, oyuncunun hareket hızını geçici olarak artırmak için kullanılır.
    {
        _movementSpeed += speed;//Mevcut hareket hızına (_movementSpeed) speed değeri eklenir. Bu, geçici bir hız artışı sağlar.
        Invoke(nameof(ResetMovementSpeed), duration);//Unity’nin Invoke() fonksiyonu, burada belirtilen süre (duration) sonra ResetMovementSpeed fonksiyonunu çalıştırır.
        // Yani belli bir süre sonra hız normale döner.
    }

    private void ResetMovementSpeed()//Bu, SetMovementSpeed() metoduyla artırılan hareket hızını sıfırlamak için çağrılan özel (private) bir metottur.
    {
        _movementSpeed = _startingMovementSpeed;//Hareket hızı, başlangıç değeri olan _startingMovementSpeed'e geri ayarlanır.

    }

    public void SetJumpForce(float force,float duration)//Bu metod, oyuncunun zıplama gücünü geçici olarak artırır.
    {
        _jumpForce += force;//Zıplamaya eklenecek ekstra kuvvet.
        Invoke(nameof(ResetJumpForce), duration);//Belirtilen duration süresi sonunda ResetJumpForce() metodu çalıştırılır.
    }
    private void ResetJumpForce()//Bu metod, geçici olarak artırılan zıplama gücünü eski haline döndürür.
    {
        _jumpForce = _startingJumpForce;//Zıplama gücü, başlangıç değeri olan _startingJumpForce’a geri ayarlanır.
    }

    public Rigidbody GetPlayerRigidbody()//Bu metod, oyuncunun Rigidbody bileşenini dışarıdan erişilebilir kılmak için kullanılır.
    {
        return _playerRigidbody;//_playerRigidbody adındaki fiziksel bileşen dışarıya geri döndürülür. Böylece başka sınıflar bu Rigidbody ile işlem yapabilir.
    }
    #endregion
}



