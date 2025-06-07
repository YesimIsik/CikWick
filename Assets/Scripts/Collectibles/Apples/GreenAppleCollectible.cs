using UnityEngine;
using UnityEngine.UI;
public class GreenAppleCollectible : MonoBehaviour, ICollectible
//Bu s�n�f ayn� zamanda bir ICollectible aray�z�n� (interface) uyguluyor.Yani bu nesne bir "toplanabilir" nesne olarak tan�ml�.
{
    [SerializeField] private AppleDesignSO _appleDesignSO;
    //WheatDesignSO adl� ScriptableObject h�z art�rma �arpan� (multiplier) ve s�resi gibi bilgileri i�erir.
    [SerializeField] private PlayerController _playerController;
    //Oyuncuyu kontrol eden s�n�f�n referans�d�r.Bu nesne �zerinden oyuncunun hareket h�z� gibi verileri de�i�tiriyoruz.
    [SerializeField] private PlayerStateUI _playerStateUI;
    private RectTransform _playerBoosterTransform;
    private Image _playerBoosterImage;

    private void Awake()
    {
        _playerBoosterTransform = _playerStateUI.GetBoosterJumpTransform;
        _playerBoosterImage = _playerBoosterTransform.GetComponent<Image>();
    }

    public void Collect()
    //Bu ICollectible aray�z�nden gelen metodu uygulayan fonksiyondur.Nesne topland���nda �al��acak davran��� tan�mlar.
    {
        _playerController.SetJumpForce(_appleDesignSO.IncreaseDecreaseMultiplier, _appleDesignSO.ResetBoostDuration);

        _playerStateUI.PlayBoosterUIAnimations(_playerBoosterTransform, _playerBoosterImage, _playerStateUI.GetGreenAppleImage,
      _appleDesignSO.ActiveSprite, _appleDesignSO.PassiveSprite, _appleDesignSO.ActiveAppleSprite,
      _appleDesignSO.PassiveAppleSprite, _appleDesignSO.ResetBoostDuration);
        CameraShake.Instance.ShakeCamera(0.5f, 0.5f);
        AudioManager.Instance.Play(SoundType.PickupGoodSound);
        Destroy(gameObject);
        //Bu sat�r oyuncunun z�plama g�c�n� (jump force) ayarlar.
        //Z�plama g�c�ne uygulanacak olan �arpan de�eridir (�rne�in: 1.5x gibi).
        //Bu etki ne kadar s�recek onu belirten s�re de�eridir (�rne�in: 5 saniye boyunca y�ksek z�plama).
    }


}