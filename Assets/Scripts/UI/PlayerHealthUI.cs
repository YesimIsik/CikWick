using DG.Tweening;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UI;
public class PlayerHealthUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image[] _playerHealthImages;
    // Oyuncunun sa�l�k durumunu g�steren UI�daki kalp veya sa�l�k ikonlar�n�n Image bile�enleri.

    [Header("Sprites")]
    [SerializeField] private Sprite _playerHealthSprite;
    // Sa�l�kl� (dolu) kalp ikonunun sprite'�.
    [SerializeField] private Sprite _playerUnhealthySprite;
    // Hasar ald���nda g�sterilecek bo� kalp ikonunun sprite'�.

    [Header("Settings")]
    [SerializeField] private float _scaleDuration;
    // Hasar animasyonunda �l�ek de�i�iminin s�resi.

    private RectTransform[] _playerHealthTransforms;
    // Her bir sa�l�k ikonunun RectTransform bile�enini saklamak i�in dizi.

    private void Awake()
    {
        // UI ikonlar�n�n RectTransform bile�enlerini dizide sakla.
        _playerHealthTransforms = new RectTransform[_playerHealthImages.Length];

        for (int i = 0; i < _playerHealthImages.Length; i++)
        {
            _playerHealthTransforms[i] = _playerHealthImages[i].gameObject.GetComponent<RectTransform>();
        }
    }

    // FOR TESTING - Test ama�l� klavye ile animasyon tetiklemeleri
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            AnimateDamage();  // Tek bir sa�l�kl� kalpte hasar animasyonu oynat
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            AnimateDamageForAll();  // T�m kalplerde hasar animasyonu oynat
        }
    }

    public void AnimateDamage()
    {
        // Sa�l�kl� kalpler i�inde ilk sa�l�kl� olan� bul ve animasyon oynat
        for (int i = 0; i < _playerHealthImages.Length; i++)
        {
            if (_playerHealthImages[i].sprite == _playerHealthSprite)
            {
                AnimateDamageSprite(_playerHealthImages[i], _playerHealthTransforms[i]);
                break;  // Sadece ilk sa�l�kl� kalpte animasyon oynat�l�r, d�ng�den ��k�l�r
            }
        }
    }

    public void AnimateDamageForAll()
    {
        // T�m kalpler i�in hasar animasyonunu ba�lat
        for (int i = 0; i < _playerHealthImages.Length; i++)
        {
            AnimateDamageSprite(_playerHealthImages[i], _playerHealthTransforms[i]);
        }
    }

    private void AnimateDamageSprite(Image activeImage, RectTransform activeImageTransform)
    {
        // Hasar animasyonu: �nce �l�ek k���lt�l�r (0�a)
        activeImageTransform.DOScale(0f, _scaleDuration).SetEase(Ease.InBack).OnComplete(() =>
        {
            // �l�ek k���l�nce sprite bo� kalp olarak de�i�tirilir
            activeImage.sprite = _playerUnhealthySprite;

            // Sonra tekrar eski boyutuna �l�eklendirilir (1f)
            activeImageTransform.DOScale(1f, _scaleDuration).SetEase(Ease.OutBack);
        });
    }
}
