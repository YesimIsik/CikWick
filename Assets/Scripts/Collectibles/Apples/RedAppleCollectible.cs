using UnityEngine.UI;
using UnityEngine;

public class RedAppleCollectible : MonoBehaviour, ICollectible
{
    //Bu sýnýf ayný zamanda bir ICollectible arayüzünü (interface) uyguluyor.Yani bu nesne bir "toplanabilir" nesne olarak tanýmlý.

    [SerializeField] private AppleDesignSO _appleDesignSO;
    //WheatDesignSO adlý ScriptableObject hýz artýrma çarpaný (multiplier) ve süresi gibi bilgileri içerir.
    [SerializeField] private PlayerController _playerController;
    //Oyuncuyu kontrol eden sýnýfýn referansýdýr.Bu nesne üzerinden oyuncunun hareket hýzý gibi verileri deðiþtiriyoruz.
    [SerializeField] private PlayerStateUI _playerStateUI;
    private RectTransform _playerBoosterTransform;

    private Image _playerBoosterImage;

    private void Awake()
    {
        _playerBoosterTransform = _playerStateUI.GetBoosterSpeedTransform;
        _playerBoosterImage = _playerBoosterTransform.GetComponent<Image>();
    }

    public void Collect()
    //Bu ICollectible arayüzünden gelen metodu uygulayan fonksiyondur.Nesne toplandýðýnda çalýþacak davranýþý tanýmlar.

    {
        _playerController.SetMovementSpeed(_appleDesignSO.IncreaseDecreaseMultiplier, _appleDesignSO.ResetBoostDuration);

        _playerStateUI.PlayBoosterUIAnimations(_playerBoosterTransform,_playerBoosterImage,_playerStateUI.GetRedAppleImage,
        _appleDesignSO.ActiveSprite,_appleDesignSO.PassiveSprite,_appleDesignSO.ActiveAppleSprite,
        _appleDesignSO.PassiveAppleSprite,_appleDesignSO.ResetBoostDuration);

        CameraShake.Instance.ShakeCamera(0.5f, 0.5f);
        Destroy(gameObject);
        //Oyuncu kontrolcüsünün metodunu çaðýrýr.Bu metot oyuncunun hareket hýzýný ayarlamak için kullanýlýr.
        //Hýz çarpaný: Oyuncunun hareket hýzý bu oranla artýrýlýr veya azaltýlýr.
        //Bu etki ne kadar süre devam edecek onu belirtir (örneðin, 5 saniye boyunca hýzlý koþma gibi).

    }


}

