using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private Animator _playerAnimator;
    //Oyuncunun animasyonlar�n� kontrol eden Animator bile�enidir.


    private PlayerController _playerController;
    //Oyuncunun hareketlerini y�neten bile�endir.
    private StateController _stateController;
    //Oyuncunun mevcut durumunu y�neten bile�endir.
    private void Update()
    //Her karede �a�r�l�r.

    {
        SetPlayerAnimations();
        //Bu metotta, oyuncunun mevcut durumuna g�re uygun animasyonlar ayarlan�r.
    }

    private void Awake()
    //Script y�klendi�inde �a�r�l�r.
    {
        _playerController = GetComponent<PlayerController>();
        _stateController = GetComponent<StateController>();
        //Bu metotta, PlayerController ve StateController bile�enleri al�n�r.
    }

    private void Start()
    //Script ba�lat�ld���nda �a�r�l�r.
    {
        _playerController.OnPlayerJumped += PlayerController_OnPlayerJumped;
        //Bu metotta, oyuncunun z�plama olay� (OnPlayerJumped) dinlenir ve PlayerController_OnPlayerJumped metoduna ba�lan�r.

    }

    private void PlayerController_OnPlayerJumped()
    //Oyuncu z�plad���nda �a�r�l�r. 
    {
        _playerAnimator.SetBool(Consts.PlayerAnimations.IS_JUMPING, true);
        //IS_JUMPING parametresi true olarak ayarlan�r
        Invoke(nameof(ResetJumping), 0.5f);
        //0.5 saniye sonra ResetJumping metodu �a�r�larak IS_JUMPING parametresi false yap�l�r.
    }

    private void ResetJumping()
    //Z�plamay� resetler.
    {
        _playerAnimator.SetBool(Consts.PlayerAnimations.IS_JUMPING, false);
        //Bu, z�plama animasyonunun tetiklenmesini ve ard�ndan durmas�n� sa�lar.
    }

    private void SetPlayerAnimations()
    //Bu metot, oyuncunun mevcut durumuna g�re uygun animasyonlar� ayarlamak i�in kullan�l�r.
    {
        var currentState = _stateController.GetCurrentState();
        //currentState de�i�keni, _stateController arac�l���yla oyuncunun mevcut durumunu al�r.

        switch (currentState)
        //currentState de�i�keni, _stateController arac�l���yla oyuncunun mevcut durumunu al�r.
        {
            case PlayerState.Idle:
                //Oyuncu Idle (bo�ta) durumundaysa
                _playerAnimator.SetBool(Consts.PlayerAnimations.IS_SLIDING, false);
                //IS_SLIDING parametresi false olarak ayarlan�r, yani kayma animasyonu devre d��� b�rak�l�r.
                _playerAnimator.SetBool(Consts.PlayerAnimations.IS_MOVING, false);
                //IS_MOVING parametresi false olarak ayarlan�r, yani hareket animasyonu devre d��� b�rak�l�r.
                break;

            case PlayerState.Move:
                //Oyuncu Move (hareket) durumundaysa
                _playerAnimator.SetBool(Consts.PlayerAnimations.IS_SLIDING, false);
                //IS_SLIDING parametresi false olarak ayarlan�r, yani kayma animasyonu devre d��� b�rak�l�r.
                _playerAnimator.SetBool(Consts.PlayerAnimations.IS_MOVING, true);
                //IS_MOVING parametresi true olarak ayarlan�r, yani hareket animasyonu etkinle�tirilir.
                break;

            case PlayerState.SlideIdle:
                //Oyuncu SlideIdle (kayma bekleme) durumundaysa
                _playerAnimator.SetBool(Consts.PlayerAnimations.IS_SLIDING, true);
                //IS_SLIDING parametresi true olarak ayarlan�r, yani kayma animasyonu etkinle�tirilir.
                _playerAnimator.SetBool(Consts.PlayerAnimations.IS_SLIDING_ACTIVE, false);
                //IS_SLIDIN_ACTIVE parametresi false olarak ayarlan�r, yani aktif kayma animasyonu devre d��� b�rak�l�r
                break;

            case PlayerState.Slide:
                //Oyuncu Slide (kayma) durumundaysa
                _playerAnimator.SetBool(Consts.PlayerAnimations.IS_SLIDING, true);
                //IS_SLIDING parametresi true olarak ayarlan�r, yani kayma animasyonu etkinle�tirilir.
                _playerAnimator.SetBool(Consts.PlayerAnimations.IS_SLIDING_ACTIVE, true);
                //IS_SLIDIN_ACTIVE parametresi true olarak ayarlan�r, yani aktif kayma animasyonu etkinle�tirilir.
                break;
        }
    }
}
