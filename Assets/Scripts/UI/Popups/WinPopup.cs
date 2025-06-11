using MaskTransitions;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinPopup : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TimerUI _timerUI;                 // Oyun süresini gösteren TimerUI referansý
    [SerializeField] private Button _oneMoreButton;             // "Bir Daha Oyna" butonu
    [SerializeField] private Button _mainMenuButton;            // "Ana Menü" butonu
    [SerializeField] private TMP_Text _timerText;               // Oyun süresi bilgisinin gösterileceði metin alaný


    private void OnEnable()
    {
        // Arka plandaki müziði kapat
        BackgroundMusic.Instance.PlayBackgroundMusic(false);

        // Kazanma sesi çal
        AudioManager.Instance.Play(SoundType.WinSound);

        // TimerUI'den alýnan final süre bilgisini metin alanýna yaz
        _timerText.text = _timerUI.GetFinalTime();

        // "Bir Daha Oyna" butonuna týklama olayýný dinle ve ilgili metodu çaðýr
        _oneMoreButton.onClick.AddListener(OnOneMoreButtonClicked);

        // "Ana Menü" butonuna týklama olayýný dinle ve ana menü sahnesini yükle
        _mainMenuButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.Play(SoundType.TransitionSound); // Buton geçiþ sesi
            TransitionManager.Instance.LoadLevel(Consts.SceneNames.MENU_SCENE); // Ana menü sahnesini yükle
        });
    }


    // "Bir Daha Oyna" butonuna týklanýnca çaðrýlýr
    private void OnOneMoreButtonClicked()
    {
        // Buton geçiþ sesi çal
        AudioManager.Instance.Play(SoundType.TransitionSound);

        // Oyun sahnesini tekrar yükle, yani oyunu yeniden baþlat
        TransitionManager.Instance.LoadLevel(Consts.SceneNames.GAME_SCENE);
    }
}