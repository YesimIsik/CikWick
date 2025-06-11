using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
public class WinLoseUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject _blackBackgroundObject;  // Siyah arka plan objesi (genellikle yar� saydam)
    [SerializeField] private GameObject _winPopup;               // Kazanma durumunda g�sterilecek popup paneli
    [SerializeField] private GameObject _losePopup;              // Kaybetme durumunda g�sterilecek popup paneli

    [Header("Settings")]
    [SerializeField] private float _animationDuration = 0.3f;    // Animasyonlar�n s�resi (saniye cinsinden)


    private Image _blackBackgroundImage;                          // Siyah arka plan�n Image bile�eni (�effafl�k kontrol� i�in)
    private RectTransform _winPopupTransform;                     // Kazanma popup'�n�n RectTransform bile�eni (�l�ek animasyonu i�in)
    private RectTransform _losePopupTransform;                    // Kaybetme popup'�n�n RectTransform bile�eni


    private void Awake()
    {
        // Arka plan objesinden Image bile�enini al�yoruz
        _blackBackgroundImage = _blackBackgroundObject.GetComponent<Image>();

        // Kazanma popup'�n�n RectTransform bile�enini al�yoruz
        _winPopupTransform = _winPopup.GetComponent<RectTransform>();

        // Kaybetme popup'�n�n RectTransform bile�enini al�yoruz
        _losePopupTransform = _losePopup.GetComponent<RectTransform>();
    }


    // Oyun kazan�ld���nda �a�r�l�r
    public void OnGameWin()
    {
        _blackBackgroundObject.SetActive(true);                   // Siyah arka plan� aktif et
        _winPopup.SetActive(true);                                 // Kazanma popup'�n� aktif et

        // Siyah arka plan�n �effafl���n� 0'dan 0.8'e animasyonla ayarla
        _blackBackgroundImage.DOFade(0.8f, _animationDuration).SetEase(Ease.Linear);

        // Kazanma popup'�n� b�y�terek animasyonla g�ster (1.5 kat�na kadar)
        _winPopupTransform.DOScale(1.5f, _animationDuration).SetEase(Ease.OutBack);
    }

    // Oyun kaybedildi�inde �a�r�l�r
    public void OnGameLose()
    {
        _blackBackgroundObject.SetActive(true);                   // Siyah arka plan� aktif et
        _losePopup.SetActive(true);                                // Kaybetme popup'�n� aktif et

        // Siyah arka plan�n �effafl���n� 0'dan 0.8'e animasyonla ayarla
        _blackBackgroundImage.DOFade(0.8f, _animationDuration).SetEase(Ease.Linear);

        // Kaybetme popup'�n� b�y�terek animasyonla g�ster (1.5 kat�na kadar)
        _losePopupTransform.DOScale(1.5f, _animationDuration).SetEase(Ease.OutBack);
    }
}
