using UnityEngine;
using UnityEngine.UI;
public class GreenAppleCollectible : MonoBehaviour, ICollectible
//Bu sýnýf ayný zamanda bir ICollectible arayüzünü (interface) uyguluyor.Yani bu nesne bir "toplanabilir" nesne olarak tanýmlý.
{
    [SerializeField] private AppleDesignSO _appleDesignSO;
    //WheatDesignSO adlý ScriptableObject hýz artýrma çarpaný (multiplier) ve süresi gibi bilgileri içerir.
    [SerializeField] private PlayerController _playerController;
    //Oyuncuyu kontrol eden sýnýfýn referansýdýr.Bu nesne üzerinden oyuncunun hareket hýzý gibi verileri deðiþtiriyoruz.
    [SerializeField] private PlayerStateUI _playerStateUI;
    private RectTransform _playerBoosterTransform;
    private Image _playerBoosterImage;

    private void Awake()
    {
        _playerBoosterTransform = _playerStateUI.GetBoosterJumpTransform;
        _playerBoosterImage = _playerBoosterTransform.GetComponent<Image>();
    }

    public void Collect()
    //Bu ICollectible arayüzünden gelen metodu uygulayan fonksiyondur.Nesne toplandýðýnda çalýþacak davranýþý tanýmlar.
    {
        _playerController.SetJumpForce(_appleDesignSO.IncreaseDecreaseMultiplier, _appleDesignSO.ResetBoostDuration);

        _playerStateUI.PlayBoosterUIAnimations(_playerBoosterTransform, _playerBoosterImage, _playerStateUI.GetGreenAppleImage,
      _appleDesignSO.ActiveSprite, _appleDesignSO.PassiveSprite, _appleDesignSO.ActiveAppleSprite,
      _appleDesignSO.PassiveAppleSprite, _appleDesignSO.ResetBoostDuration);
        CameraShake.Instance.ShakeCamera(0.5f, 0.5f);
        AudioManager.Instance.Play(SoundType.PickupGoodSound);
        Destroy(gameObject);
        //Bu satýr oyuncunun zýplama gücünü (jump force) ayarlar.
        //Zýplama gücüne uygulanacak olan çarpan deðeridir (örneðin: 1.5x gibi).
        //Bu etki ne kadar sürecek onu belirten süre deðeridir (örneðin: 5 saniye boyunca yüksek zýplama).
    }


}