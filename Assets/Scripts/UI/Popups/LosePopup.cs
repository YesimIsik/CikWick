using MaskTransitions;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LosePopup : MonoBehaviour

{
    [Header("References")]
    [SerializeField] private TimerUI _timerUI;                  // Oyun s�resini g�steren TimerUI referans�
    [SerializeField] private Button _tryAgainButton;             // "Tekrar Dene" butonu
    [SerializeField] private Button _mainMenuButton;             // "Ana Men�" butonu
    [SerializeField] private TMP_Text _timerText;                // Oyun s�resi bilgisinin g�sterilece�i metin alan�


    private void OnEnable()
    {
        // Arka plandaki m�zi�i kapat
        BackgroundMusic.Instance.PlayBackgroundMusic(false);

        // Kaybetme sesi �al
        AudioManager.Instance.Play(SoundType.LoseSound);

        // TimerUI'den al�nan final s�re bilgisini metin alan�na yaz
        _timerText.text = _timerUI.GetFinalTime();

        // "Tekrar Dene" butonuna t�klama olay�n� dinle ve ilgili metodu �a��r
        _tryAgainButton.onClick.AddListener(OnTryAgainButtonClicked);

        // "Ana Men�" butonuna t�klama olay�n� dinle ve ana men� sahnesini y�kle
        _mainMenuButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.Play(SoundType.TransitionSound);  // Buton ge�i� sesi
            TransitionManager.Instance.LoadLevel(Consts.SceneNames.MENU_SCENE);  // Ana men� sahnesini y�kle
        });
    }


    // "Tekrar Dene" butonuna t�klan�nca �a�r�l�r
    private void OnTryAgainButtonClicked()
    {
        // Buton ge�i� sesi �al
        AudioManager.Instance.Play(SoundType.TransitionSound);

        // Oyun sahnesini tekrar y�kle, yani oyunu yeniden ba�lat
        TransitionManager.Instance.LoadLevel(Consts.SceneNames.GAME_SCENE);
    }
}

