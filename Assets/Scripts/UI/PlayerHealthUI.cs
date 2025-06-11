using DG.Tweening;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UI;
public class PlayerHealthUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image[] _playerHealthImages;
    // Oyuncunun saðlýk durumunu gösteren UI’daki kalp veya saðlýk ikonlarýnýn Image bileþenleri.

    [Header("Sprites")]
    [SerializeField] private Sprite _playerHealthSprite;
    // Saðlýklý (dolu) kalp ikonunun sprite'ý.
    [SerializeField] private Sprite _playerUnhealthySprite;
    // Hasar aldýðýnda gösterilecek boþ kalp ikonunun sprite'ý.

    [Header("Settings")]
    [SerializeField] private float _scaleDuration;
    // Hasar animasyonunda ölçek deðiþiminin süresi.

    private RectTransform[] _playerHealthTransforms;
    // Her bir saðlýk ikonunun RectTransform bileþenini saklamak için dizi.

    private void Awake()
    {
        // UI ikonlarýnýn RectTransform bileþenlerini dizide sakla.
        _playerHealthTransforms = new RectTransform[_playerHealthImages.Length];

        for (int i = 0; i < _playerHealthImages.Length; i++)
        {
            _playerHealthTransforms[i] = _playerHealthImages[i].gameObject.GetComponent<RectTransform>();
        }
    }

    // FOR TESTING - Test amaçlý klavye ile animasyon tetiklemeleri
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            AnimateDamage();  // Tek bir saðlýklý kalpte hasar animasyonu oynat
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            AnimateDamageForAll();  // Tüm kalplerde hasar animasyonu oynat
        }
    }

    public void AnimateDamage()
    {
        // Saðlýklý kalpler içinde ilk saðlýklý olaný bul ve animasyon oynat
        for (int i = 0; i < _playerHealthImages.Length; i++)
        {
            if (_playerHealthImages[i].sprite == _playerHealthSprite)
            {
                AnimateDamageSprite(_playerHealthImages[i], _playerHealthTransforms[i]);
                break;  // Sadece ilk saðlýklý kalpte animasyon oynatýlýr, döngüden çýkýlýr
            }
        }
    }

    public void AnimateDamageForAll()
    {
        // Tüm kalpler için hasar animasyonunu baþlat
        for (int i = 0; i < _playerHealthImages.Length; i++)
        {
            AnimateDamageSprite(_playerHealthImages[i], _playerHealthTransforms[i]);
        }
    }

    private void AnimateDamageSprite(Image activeImage, RectTransform activeImageTransform)
    {
        // Hasar animasyonu: önce ölçek küçültülür (0’a)
        activeImageTransform.DOScale(0f, _scaleDuration).SetEase(Ease.InBack).OnComplete(() =>
        {
            // Ölçek küçülünce sprite boþ kalp olarak deðiþtirilir
            activeImage.sprite = _playerUnhealthySprite;

            // Sonra tekrar eski boyutuna ölçeklendirilir (1f)
            activeImageTransform.DOScale(1f, _scaleDuration).SetEase(Ease.OutBack);
        });
    }
}
