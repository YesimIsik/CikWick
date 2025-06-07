using UnityEngine.UI;
using UnityEngine;

public class RedAppleCollectible : MonoBehaviour, ICollectible
{
    //Bu s�n�f ayn� zamanda bir ICollectible aray�z�n� (interface) uyguluyor.Yani bu nesne bir "toplanabilir" nesne olarak tan�ml�.

    [SerializeField] private AppleDesignSO _appleDesignSO;
    //WheatDesignSO adl� ScriptableObject h�z art�rma �arpan� (multiplier) ve s�resi gibi bilgileri i�erir.
    [SerializeField] private PlayerController _playerController;
    //Oyuncuyu kontrol eden s�n�f�n referans�d�r.Bu nesne �zerinden oyuncunun hareket h�z� gibi verileri de�i�tiriyoruz.
    [SerializeField] private PlayerStateUI _playerStateUI;
    private RectTransform _playerBoosterTransform;

    private Image _playerBoosterImage;

    private void Awake()
    {
        _playerBoosterTransform = _playerStateUI.GetBoosterSpeedTransform;
        _playerBoosterImage = _playerBoosterTransform.GetComponent<Image>();
    }

    public void Collect()
    //Bu ICollectible aray�z�nden gelen metodu uygulayan fonksiyondur.Nesne topland���nda �al��acak davran��� tan�mlar.

    {
        _playerController.SetMovementSpeed(_appleDesignSO.IncreaseDecreaseMultiplier, _appleDesignSO.ResetBoostDuration);

        _playerStateUI.PlayBoosterUIAnimations(_playerBoosterTransform,_playerBoosterImage,_playerStateUI.GetRedAppleImage,
        _appleDesignSO.ActiveSprite,_appleDesignSO.PassiveSprite,_appleDesignSO.ActiveAppleSprite,
        _appleDesignSO.PassiveAppleSprite,_appleDesignSO.ResetBoostDuration);

        CameraShake.Instance.ShakeCamera(0.5f, 0.5f);
        Destroy(gameObject);
        //Oyuncu kontrolc�s�n�n metodunu �a��r�r.Bu metot oyuncunun hareket h�z�n� ayarlamak i�in kullan�l�r.
        //H�z �arpan�: Oyuncunun hareket h�z� bu oranla art�r�l�r veya azalt�l�r.
        //Bu etki ne kadar s�re devam edecek onu belirtir (�rne�in, 5 saniye boyunca h�zl� ko�ma gibi).

    }


}

