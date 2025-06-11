using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
public class WinLoseUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject _blackBackgroundObject;  // Siyah arka plan objesi (genellikle yarý saydam)
    [SerializeField] private GameObject _winPopup;               // Kazanma durumunda gösterilecek popup paneli
    [SerializeField] private GameObject _losePopup;              // Kaybetme durumunda gösterilecek popup paneli

    [Header("Settings")]
    [SerializeField] private float _animationDuration = 0.3f;    // Animasyonlarýn süresi (saniye cinsinden)


    private Image _blackBackgroundImage;                          // Siyah arka planýn Image bileþeni (þeffaflýk kontrolü için)
    private RectTransform _winPopupTransform;                     // Kazanma popup'ýnýn RectTransform bileþeni (ölçek animasyonu için)
    private RectTransform _losePopupTransform;                    // Kaybetme popup'ýnýn RectTransform bileþeni


    private void Awake()
    {
        // Arka plan objesinden Image bileþenini alýyoruz
        _blackBackgroundImage = _blackBackgroundObject.GetComponent<Image>();

        // Kazanma popup'ýnýn RectTransform bileþenini alýyoruz
        _winPopupTransform = _winPopup.GetComponent<RectTransform>();

        // Kaybetme popup'ýnýn RectTransform bileþenini alýyoruz
        _losePopupTransform = _losePopup.GetComponent<RectTransform>();
    }


    // Oyun kazanýldýðýnda çaðrýlýr
    public void OnGameWin()
    {
        _blackBackgroundObject.SetActive(true);                   // Siyah arka planý aktif et
        _winPopup.SetActive(true);                                 // Kazanma popup'ýný aktif et

        // Siyah arka planýn þeffaflýðýný 0'dan 0.8'e animasyonla ayarla
        _blackBackgroundImage.DOFade(0.8f, _animationDuration).SetEase(Ease.Linear);

        // Kazanma popup'ýný büyüterek animasyonla göster (1.5 katýna kadar)
        _winPopupTransform.DOScale(1.5f, _animationDuration).SetEase(Ease.OutBack);
    }

    // Oyun kaybedildiðinde çaðrýlýr
    public void OnGameLose()
    {
        _blackBackgroundObject.SetActive(true);                   // Siyah arka planý aktif et
        _losePopup.SetActive(true);                                // Kaybetme popup'ýný aktif et

        // Siyah arka planýn þeffaflýðýný 0'dan 0.8'e animasyonla ayarla
        _blackBackgroundImage.DOFade(0.8f, _animationDuration).SetEase(Ease.Linear);

        // Kaybetme popup'ýný büyüterek animasyonla göster (1.5 katýna kadar)
        _losePopupTransform.DOScale(1.5f, _animationDuration).SetEase(Ease.OutBack);
    }
}
