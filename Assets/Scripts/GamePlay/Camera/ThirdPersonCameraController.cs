using UnityEngine;

public class ThirdPersonCameraController : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private Transform _playerTransform;
    //Oyuncunun pozisyonunu temsil eder.
    [SerializeField] private Transform _orientationTransform;
    //Oyuncunun y�n�n� belirlemek i�in kullan�l�r.
    [SerializeField] private Transform _playerVisualTransform;
    //Oyuncunun g�rsel modelinin transformu i�in kullan�l�r.


    [Header("Settings")]
    [SerializeField] private float _rotationSpeed;
    //Oyuncunun d�n�� h�z�n� belirler.


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
        //Kameran�n yatay d�zlemde ki oyuncuya bak�� y�n�n� hesaplar.

        _orientationTransform.forward = viewDirection.normalized;
        //Bu y�n, oyuncunun bak�� y�n� olarak ayarlan�r.

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        //Kullan�c�n�n yatay giri�lerini al�r. (W-A-S-D)
        float verticalInput = Input.GetAxisRaw("Vertical");
        //Kullan�c�n�n dikey giri�lerini al�r. (W-A-S-D)

        Vector3 inputDirection
            = _orientationTransform.forward * verticalInput + _orientationTransform.right * horizontalInput;
        //Oyuncunun hareket y�n�n� belirler.

        if (inputDirection != Vector3.zero)
        {
            _playerVisualTransform.forward
            = Vector3.Slerp(_playerVisualTransform.forward, inputDirection.normalized, Time.deltaTime * _rotationSpeed);
            //E�er inputDirection s�f�r de�ilse (yani oyuncu hareket ediyorsa), oyuncunun g�rsel modeli (_playerVisualTransform)
            //hareket y�n�ne do�ru yumu�ak bir �ekilde d�ner. 
        }

    }
}
