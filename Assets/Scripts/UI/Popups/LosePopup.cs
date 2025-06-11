using MaskTransitions;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LosePopup : MonoBehaviour

{
    [Header("References")]
    [SerializeField] private TimerUI _timerUI;                  // Oyun süresini gösteren TimerUI referansý
    [SerializeField] private Button _tryAgainButton;             // "Tekrar Dene" butonu
    [SerializeField] private Button _mainMenuButton;             // "Ana Menü" butonu
    [SerializeField] private TMP_Text _timerText;                // Oyun süresi bilgisinin gösterileceði metin alaný


    private void OnEnable()
    {
        // Arka plandaki müziði kapat
        BackgroundMusic.Instance.PlayBackgroundMusic(false);

        // Kaybetme sesi çal
        AudioManager.Instance.Play(SoundType.LoseSound);

        // TimerUI'den alýnan final süre bilgisini metin alanýna yaz
        _timerText.text = _timerUI.GetFinalTime();

        // "Tekrar Dene" butonuna týklama olayýný dinle ve ilgili metodu çaðýr
        _tryAgainButton.onClick.AddListener(OnTryAgainButtonClicked);

        // "Ana Menü" butonuna týklama olayýný dinle ve ana menü sahnesini yükle
        _mainMenuButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.Play(SoundType.TransitionSound);  // Buton geçiþ sesi
            TransitionManager.Instance.LoadLevel(Consts.SceneNames.MENU_SCENE);  // Ana menü sahnesini yükle
        });
    }


    // "Tekrar Dene" butonuna týklanýnca çaðrýlýr
    private void OnTryAgainButtonClicked()
    {
        // Buton geçiþ sesi çal
        AudioManager.Instance.Play(SoundType.TransitionSound);

        // Oyun sahnesini tekrar yükle, yani oyunu yeniden baþlat
        TransitionManager.Instance.LoadLevel(Consts.SceneNames.GAME_SCENE);
    }
}

