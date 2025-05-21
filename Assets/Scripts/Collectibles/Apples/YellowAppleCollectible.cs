using UnityEngine;
using UnityEngine.UI;
public class YellowAppleCollectible : MonoBehaviour, ICollectible
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
        _playerBoosterTransform = _playerStateUI.GetBoosterSlowTransform;
        _playerBoosterImage = _playerBoosterTransform.GetComponent<Image>();
    }

    public void Collect()
    //Bu ICollectible aray�z�nden gelen metodu uygulayan fonksiyondur.Nesne topland���nda �al��acak davran��� tan�mlar.
    {
        _playerController.SetMovementSpeed(_appleDesignSO.IncreaseDecreaseMultiplier, _appleDesignSO.ResetBoostDuration);

        _playerStateUI.PlayBoosterUIAnimations(_playerBoosterTransform, _playerBoosterImage, _playerStateUI.GetYellowAppleImage,
     _appleDesignSO.ActiveSprite, _appleDesignSO.PassiveSprite, _appleDesignSO.ActiveAppleSprite,
     _appleDesignSO.PassiveAppleSprite, _appleDesignSO.ResetBoostDuration);

        Destroy(gameObject);
        //Oyuncunun hareket h�z�n� ge�ici olarak azaltmak i�in yaz�lm�� bir fonksiyondur.
        //Bu sat�r sayesinde sar� elmay� toplayan oyuncunun h�z� ge�ici olarak de�i�ir.
    }

    
}
