using DG.Tweening;
using MaskTransitions;
using System;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using Unity.Properties;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    [Header("References")]//Unity Inspector'da bu de�i�kenler "References" ba�l��� alt�nda grupland�r�l�r.


    [SerializeField] private GameObject _settingsPopupObject;//Ayar penceresinin GameObject�idir.
    [SerializeField] private GameObject _blackBackgroundObject;//Karanl�k arka plan efekti sa�layan objedir.


    [Header("Buttons")]

    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _soundButton;
    [SerializeField] private Button _musicButton;
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _mainMenuButton;
    //Unity UI sistemine ait Button nesneleri.Her biri bir UI butonunu temsil eder.
    //Ayarlar, M�zik, Ses, Devam Et, Ana Men�.
    [Header("Sprites")]
    [SerializeField] private Sprite _musicActiveSprite;
    [SerializeField] private Sprite _musicPassiveSprite;
    [SerializeField] private Sprite _soundActiveSprite;
    [SerializeField] private Sprite _soundPassiveSprite;



    [Header("Settings")]
    [SerializeField] private float _animationDuration;// Animasyonlar�n s�residir.DOFade ve DOScale gibi DOTween animasyonlar�na bu s�re veriliyor.


    private Image _blackBackGroundImage;

    private bool _isMusicActive;
    private bool _isSoundActive;
    private void Awake()//Unity taraf�ndan sahne y�klendi�inde ilk �al��an fonksiyonlardan biridir.


    {
        _blackBackGroundImage = _blackBackgroundObject.GetComponent<Image>();//Siyah arka plan�n Image bile�eni al�n�r.
        _settingsPopupObject.transform.localScale = Vector3.zero;//Ayarlar panelinin ba�lang��ta g�r�nmemesini sa�lar.
        _settingsButton.onClick.AddListener(OnSettingsButtonClicked);//Butonlara t�klan�nca �al��acak metotlar atan�r.
        _resumeButton.onClick.AddListener(OnResumeButtonClicked);//Butonlara t�klan�nca �al��acak metotlar atan�r.
        _mainMenuButton.onClick.AddListener(() =>
        {
            TransitionManager.Instance.LoadLevel(Consts.SceneNames.MENU_SCENE);//Ana Men� butonuna bas�ld���nda sahne de�i�imi yap�l�r.
        });

        _musicButton.onClick.AddListener(OnMusicButtonClicked);
        _soundButton.onClick.AddListener(SoundButtonClicked);
    }

    private void OnMusicButtonClicked()
    {
        AudioManager.Instance.Play(SoundType.ButtonClickSound);
        _isMusicActive = !_isMusicActive;
        _musicButton.image.sprite = _isMusicActive ? _musicActiveSprite : _musicPassiveSprite;
        BackgroundMusic.Instance.SetMusicMute(!_isMusicActive);
       
    }
    


    private void SoundButtonClicked()
    {
        AudioManager.Instance.Play(SoundType.ButtonClickSound);
        _isSoundActive = !_isSoundActive;
        _soundButton.image.sprite = _isSoundActive ? _soundActiveSprite : _soundPassiveSprite;
        AudioManager.Instance.SetSoundEffectsMute(!_isSoundActive);
    }

    private void OnSettingsButtonClicked()//Ayarlar butonuna bas�ld���nda �a�r�l�r.
    {
       GameManager.Instance.ChangeGameState(GameState.Pause);//Oyun duraklat�l�r (Pause).
        AudioManager.Instance.Play(SoundType.ButtonClickSound);//T�klama sesi �al�n�r.
        _blackBackgroundObject.SetActive(true);// Ayarlar men�s� ve arka plan g�r�n�r hale getirilir.
        _settingsPopupObject.SetActive(true);//Ayarlar men�s� ve arka plan g�r�n�r hale getirilir.

        _blackBackGroundImage.DOFade(0.8f, _animationDuration).SetEase(Ease.Linear);//Arka plan karart�l�r (DOFade: saydaml�k animasyonu).
        _settingsPopupObject.transform.DOScale(1.5f, _animationDuration).SetEase(Ease.OutBack);//Ayarlar paneli b�y�yerek g�r�n�r hale gelir (DOScale: �l�ek animasyonu).



    }

    private void OnResumeButtonClicked()//Devam Et butonuna bas�ld���nda �a�r�l�r.
    {
        AudioManager.Instance.Play(SoundType.ButtonClickSound);//T�klama sesi �al�n�r.

        _blackBackGroundImage.DOFade(0f, _animationDuration).SetEase(Ease.Linear);//Arka plan tekrar �effaf hale getirilir.
        _settingsPopupObject.transform.DOScale(0f,_animationDuration).SetEase(Ease.OutExpo).OnComplete(() =>//Ayarlar paneli k���lerek kapan�r.
        {
            GameManager.Instance.ChangeGameState(GameState.Resume);

            _blackBackgroundObject.SetActive(false);
            _settingsPopupObject.SetActive(false);
        });
        //Oyun yeniden ba�lat�l�r.
        //UI objeleri gizlenir.
    }
}
