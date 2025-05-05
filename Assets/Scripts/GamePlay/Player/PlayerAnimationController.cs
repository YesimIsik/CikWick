using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private Animator _playerAnimator;
    //Oyuncunun animasyonlarýný kontrol eden Animator bileþenidir.


    private PlayerController _playerController;
    //Oyuncunun hareketlerini yöneten bileþendir.
    private StateController _stateController;
    //Oyuncunun mevcut durumunu yöneten bileþendir.
    private void Update()
    //Her karede çaðrýlýr.

    {
        SetPlayerAnimations();
        //Bu metotta, oyuncunun mevcut durumuna göre uygun animasyonlar ayarlanýr.
    }

    private void Awake()
    //Script yüklendiðinde çaðrýlýr.
    {
        _playerController = GetComponent<PlayerController>();
        _stateController = GetComponent<StateController>();
        //Bu metotta, PlayerController ve StateController bileþenleri alýnýr.
    }

    private void Start()
    //Script baþlatýldýðýnda çaðrýlýr.
    {
        _playerController.OnPlayerJumped += PlayerController_OnPlayerJumped;
        //Bu metotta, oyuncunun zýplama olayý (OnPlayerJumped) dinlenir ve PlayerController_OnPlayerJumped metoduna baðlanýr.

    }

    private void PlayerController_OnPlayerJumped()
    //Oyuncu zýpladýðýnda çaðrýlýr. 
    {
        _playerAnimator.SetBool(Consts.PlayerAnimations.IS_JUMPING, true);
        //IS_JUMPING parametresi true olarak ayarlanýr
        Invoke(nameof(ResetJumping), 0.5f);
        //0.5 saniye sonra ResetJumping metodu çaðrýlarak IS_JUMPING parametresi false yapýlýr.
    }

    private void ResetJumping()
    //Zýplamayý resetler.
    {
        _playerAnimator.SetBool(Consts.PlayerAnimations.IS_JUMPING, false);
        //Bu, zýplama animasyonunun tetiklenmesini ve ardýndan durmasýný saðlar.
    }

    private void SetPlayerAnimations()
    //Bu metot, oyuncunun mevcut durumuna göre uygun animasyonlarý ayarlamak için kullanýlýr.
    {
        var currentState = _stateController.GetCurrentState();
        //currentState deðiþkeni, _stateController aracýlýðýyla oyuncunun mevcut durumunu alýr.

        switch (currentState)
        //currentState deðiþkeni, _stateController aracýlýðýyla oyuncunun mevcut durumunu alýr.
        {
            case PlayerState.Idle:
                //Oyuncu Idle (boþta) durumundaysa
                _playerAnimator.SetBool(Consts.PlayerAnimations.IS_SLIDING, false);
                //IS_SLIDING parametresi false olarak ayarlanýr, yani kayma animasyonu devre dýþý býrakýlýr.
                _playerAnimator.SetBool(Consts.PlayerAnimations.IS_MOVING, false);
                //IS_MOVING parametresi false olarak ayarlanýr, yani hareket animasyonu devre dýþý býrakýlýr.
                break;

            case PlayerState.Move:
                //Oyuncu Move (hareket) durumundaysa
                _playerAnimator.SetBool(Consts.PlayerAnimations.IS_SLIDING, false);
                //IS_SLIDING parametresi false olarak ayarlanýr, yani kayma animasyonu devre dýþý býrakýlýr.
                _playerAnimator.SetBool(Consts.PlayerAnimations.IS_MOVING, true);
                //IS_MOVING parametresi true olarak ayarlanýr, yani hareket animasyonu etkinleþtirilir.
                break;

            case PlayerState.SlideIdle:
                //Oyuncu SlideIdle (kayma bekleme) durumundaysa
                _playerAnimator.SetBool(Consts.PlayerAnimations.IS_SLIDING, true);
                //IS_SLIDING parametresi true olarak ayarlanýr, yani kayma animasyonu etkinleþtirilir.
                _playerAnimator.SetBool(Consts.PlayerAnimations.IS_SLIDING_ACTIVE, false);
                //IS_SLIDIN_ACTIVE parametresi false olarak ayarlanýr, yani aktif kayma animasyonu devre dýþý býrakýlýr
                break;

            case PlayerState.Slide:
                //Oyuncu Slide (kayma) durumundaysa
                _playerAnimator.SetBool(Consts.PlayerAnimations.IS_SLIDING, true);
                //IS_SLIDING parametresi true olarak ayarlanýr, yani kayma animasyonu etkinleþtirilir.
                _playerAnimator.SetBool(Consts.PlayerAnimations.IS_SLIDING_ACTIVE, true);
                //IS_SLIDIN_ACTIVE parametresi true olarak ayarlanýr, yani aktif kayma animasyonu etkinleþtirilir.
                break;
        }
    }
}
