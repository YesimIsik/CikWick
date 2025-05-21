using UnityEngine;
using UnityEngine.UI;
public class YellowAppleCollectible : MonoBehaviour, ICollectible
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
        _playerBoosterTransform = _playerStateUI.GetBoosterSlowTransform;
        _playerBoosterImage = _playerBoosterTransform.GetComponent<Image>();
    }

    public void Collect()
    //Bu ICollectible arayüzünden gelen metodu uygulayan fonksiyondur.Nesne toplandýðýnda çalýþacak davranýþý tanýmlar.
    {
        _playerController.SetMovementSpeed(_appleDesignSO.IncreaseDecreaseMultiplier, _appleDesignSO.ResetBoostDuration);

        _playerStateUI.PlayBoosterUIAnimations(_playerBoosterTransform, _playerBoosterImage, _playerStateUI.GetYellowAppleImage,
     _appleDesignSO.ActiveSprite, _appleDesignSO.PassiveSprite, _appleDesignSO.ActiveAppleSprite,
     _appleDesignSO.PassiveAppleSprite, _appleDesignSO.ResetBoostDuration);

        Destroy(gameObject);
        //Oyuncunun hareket hýzýný geçici olarak azaltmak için yazýlmýþ bir fonksiyondur.
        //Bu satýr sayesinde sarý elmayý toplayan oyuncunun hýzý geçici olarak deðiþir.
    }

    
}
