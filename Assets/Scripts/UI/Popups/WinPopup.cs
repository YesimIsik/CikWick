using MaskTransitions;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinPopup : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TimerUI _timerUI;                 // Oyun s�resini g�steren TimerUI referans�
    [SerializeField] private Button _oneMoreButton;             // "Bir Daha Oyna" butonu
    [SerializeField] private Button _mainMenuButton;            // "Ana Men�" butonu
    [SerializeField] private TMP_Text _timerText;               // Oyun s�resi bilgisinin g�sterilece�i metin alan�


    private void OnEnable()
    {
        // Arka plandaki m�zi�i kapat
        BackgroundMusic.Instance.PlayBackgroundMusic(false);

        // Kazanma sesi �al
        AudioManager.Instance.Play(SoundType.WinSound);

        // TimerUI'den al�nan final s�re bilgisini metin alan�na yaz
        _timerText.text = _timerUI.GetFinalTime();

        // "Bir Daha Oyna" butonuna t�klama olay�n� dinle ve ilgili metodu �a��r
        _oneMoreButton.onClick.AddListener(OnOneMoreButtonClicked);

        // "Ana Men�" butonuna t�klama olay�n� dinle ve ana men� sahnesini y�kle
        _mainMenuButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.Play(SoundType.TransitionSound); // Buton ge�i� sesi
            TransitionManager.Instance.LoadLevel(Consts.SceneNames.MENU_SCENE); // Ana men� sahnesini y�kle
        });
    }


    // "Bir Daha Oyna" butonuna t�klan�nca �a�r�l�r
    private void OnOneMoreButtonClicked()
    {
        // Buton ge�i� sesi �al
        AudioManager.Instance.Play(SoundType.TransitionSound);

        // Oyun sahnesini tekrar y�kle, yani oyunu yeniden ba�lat
        TransitionManager.Instance.LoadLevel(Consts.SceneNames.GAME_SCENE);
    }
}