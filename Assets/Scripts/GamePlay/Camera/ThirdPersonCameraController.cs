using UnityEngine;

public class ThirdPersonCameraController : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private Transform _playerTransform;
    //Oyuncunun pozisyonunu temsil eder.
    [SerializeField] private Transform _orientationTransform;
    //Oyuncunun yönünü belirlemek için kullanýlýr.
    [SerializeField] private Transform _playerVisualTransform;
    //Oyuncunun görsel modelinin transformu için kullanýlýr.


    [Header("Settings")]
    [SerializeField] private float _rotationSpeed;
    //Oyuncunun dönüþ hýzýný belirler.


    private void Update()
    {
        if(GameManager.Instance.GetCurrentGameState() !=GameState.Play
            &&GameManager.Instance.GetCurrentGameState() != GameState.Resume)
        {
            return;
        }

        Vector3 viewDirection
            //
            = _playerTransform.position - new Vector3(transform.position.x, _playerTransform.position.y, transform.position.z);
        //Kameranýn yatay düzlemde ki oyuncuya bakýþ yönünü hesaplar.

        _orientationTransform.forward = viewDirection.normalized;
        //Bu yön, oyuncunun bakýþ yönü olarak ayarlanýr.

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        //Kullanýcýnýn yatay giriþlerini alýr. (W-A-S-D)
        float verticalInput = Input.GetAxisRaw("Vertical");
        //Kullanýcýnýn dikey giriþlerini alýr. (W-A-S-D)

        Vector3 inputDirection
            = _orientationTransform.forward * verticalInput + _orientationTransform.right * horizontalInput;
        //Oyuncunun hareket yönünü belirler.

        if (inputDirection != Vector3.zero)
        {
            _playerVisualTransform.forward
            = Vector3.Slerp(_playerVisualTransform.forward, inputDirection.normalized, Time.deltaTime * _rotationSpeed);
            //Eðer inputDirection sýfýr deðilse (yani oyuncu hareket ediyorsa), oyuncunun görsel modeli (_playerVisualTransform)
            //hareket yönüne doðru yumuþak bir þekilde döner. 
        }

    }
}
