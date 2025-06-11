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
    [Header("References")]//Unity Inspector'da bu deðiþkenler "References" baþlýðý altýnda gruplandýrýlýr.


    [SerializeField] private GameObject _settingsPopupObject;//Ayar penceresinin GameObject’idir.
    [SerializeField] private GameObject _blackBackgroundObject;//Karanlýk arka plan efekti saðlayan objedir.


    [Header("Buttons")]

    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _soundButton;
    [SerializeField] private Button _musicButton;
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _mainMenuButton;
    //Unity UI sistemine ait Button nesneleri.Her biri bir UI butonunu temsil eder.
    //Ayarlar, Müzik, Ses, Devam Et, Ana Menü.
    [Header("Sprites")]
    [SerializeField] private Sprite _musicActiveSprite;
    [SerializeField] private Sprite _musicPassiveSprite;
    [SerializeField] private Sprite _soundActiveSprite;
    [SerializeField] private Sprite _soundPassiveSprite;



    [Header("Settings")]
    [SerializeField] private float _animationDuration;// Animasyonlarýn süresidir.DOFade ve DOScale gibi DOTween animasyonlarýna bu süre veriliyor.


    private Image _blackBackGroundImage;

    private bool _isMusicActive;
    private bool _isSoundActive;
    private void Awake()//Unity tarafýndan sahne yüklendiðinde ilk çalýþan fonksiyonlardan biridir.


    {
        _blackBackGroundImage = _blackBackgroundObject.GetComponent<Image>();//Siyah arka planýn Image bileþeni alýnýr.
        _settingsPopupObject.transform.localScale = Vector3.zero;//Ayarlar panelinin baþlangýçta görünmemesini saðlar.
        _settingsButton.onClick.AddListener(OnSettingsButtonClicked);//Butonlara týklanýnca çalýþacak metotlar atanýr.
        _resumeButton.onClick.AddListener(OnResumeButtonClicked);//Butonlara týklanýnca çalýþacak metotlar atanýr.
        _mainMenuButton.onClick.AddListener(() =>
        {
            TransitionManager.Instance.LoadLevel(Consts.SceneNames.MENU_SCENE);//Ana Menü butonuna basýldýðýnda sahne deðiþimi yapýlýr.
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

    private void OnSettingsButtonClicked()//Ayarlar butonuna basýldýðýnda çaðrýlýr.
    {
       GameManager.Instance.ChangeGameState(GameState.Pause);//Oyun duraklatýlýr (Pause).
        AudioManager.Instance.Play(SoundType.ButtonClickSound);//Týklama sesi çalýnýr.
        _blackBackgroundObject.SetActive(true);// Ayarlar menüsü ve arka plan görünür hale getirilir.
        _settingsPopupObject.SetActive(true);//Ayarlar menüsü ve arka plan görünür hale getirilir.

        _blackBackGroundImage.DOFade(0.8f, _animationDuration).SetEase(Ease.Linear);//Arka plan karartýlýr (DOFade: saydamlýk animasyonu).
        _settingsPopupObject.transform.DOScale(1.5f, _animationDuration).SetEase(Ease.OutBack);//Ayarlar paneli büyüyerek görünür hale gelir (DOScale: ölçek animasyonu).



    }

    private void OnResumeButtonClicked()//Devam Et butonuna basýldýðýnda çaðrýlýr.
    {
        AudioManager.Instance.Play(SoundType.ButtonClickSound);//Týklama sesi çalýnýr.

        _blackBackGroundImage.DOFade(0f, _animationDuration).SetEase(Ease.Linear);//Arka plan tekrar þeffaf hale getirilir.
        _settingsPopupObject.transform.DOScale(0f,_animationDuration).SetEase(Ease.OutExpo).OnComplete(() =>//Ayarlar paneli küçülerek kapanýr.
        {
            GameManager.Instance.ChangeGameState(GameState.Resume);

            _blackBackgroundObject.SetActive(false);
            _settingsPopupObject.SetActive(false);
        });
        //Oyun yeniden baþlatýlýr.
        //UI objeleri gizlenir.
    }
}
