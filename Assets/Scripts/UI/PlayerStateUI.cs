using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class PlayerStateUI : MonoBehaviour//Unity de UI y�netimi i�in kullan�lan bir s�n�f.
{
    [Header("References")]//Inspector'da bu alan� �References� ba�l��� alt�nda gruplayarak daha okunabilir yapar.

    [SerializeField] private RectTransform _playerWalkingTransform;
    [SerializeField] private RectTransform _playerSlidingTransform;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private RectTransform _boosterSpeedTransform;
    [SerializeField] private RectTransform _boosterJumpTransform;
    [SerializeField] private RectTransform _boosterSlowTransform;
    [SerializeField] private PlayableDirector _playableDirector;
    //Bu de�i�kenler UI �gelerine kar��l�k gelir.
    //RectTransform:UI nesnesinin pozisyon, boyut ve hizalanmas�n� temsil eder.
    [Header("Images")]
    [SerializeField] private Image _RedAppleImage;
    [SerializeField] private Image _GreenAppleImage;
    [SerializeField] private Image _YellowAppleImage;
    //Oyuncunun UI'de g�rece�i elma g�rsellerini ifade eder.

    [Header("Sprites")] 

    [SerializeField] private Sprite _playerWalkingActiveSprite;
    [SerializeField] private Sprite _playerWalkingPassiveSprite;
    [SerializeField] private Sprite _playerSlidingActiveSprite;
    [SerializeField] private Sprite _playerSlidingPassiveSprite;
    //Oyuncunun durumunu (y�r�me ve kayman�n aktif veya pasif oldu�unu) g�rselleyen spritelard�r.

    [Header("Settings")]
    [SerializeField] private float _moveDuration;//UI ��elerinin ne kadar s�rede hareket edece�ini belirler.
    [SerializeField] private Ease _moveEase;// Tweening (yumu�ak ge�i�) animasyonlar� i�in Ease tipi
                                            // (Ease tipi genellikle DOTween k�t�phanesinden gelir).



    public RectTransform GetBoosterSpeedTransform => _boosterSpeedTransform;
    public RectTransform GetBoosterJumpTransform => _boosterJumpTransform;
    public RectTransform GetBoosterSlowTransform => _boosterSlowTransform;
    //Bu �� get �zelli�i, d�� s�n�flar�n _boosterSpeedTransform , _boosterJumpTransform ve _boosterSlowTransform referanslar�na eri�mesini sa�lar. 
    public Image GetRedAppleImage => _RedAppleImage;
    public Image GetGreenAppleImage => _GreenAppleImage;
    public Image GetYellowAppleImage => _YellowAppleImage;
    //Her biri ilgili UI Image nesnesine eri�im sa�lar. 

    private Image _playerWalkingImage;//Y�r�me UI eleman�n�n Image bile�inini ifade eder.
    private Image _playerSlidingImage;//Kayma UI eleman�n�n Image bile�enini ifade eder.

    

   private void Awake()//�lk �al��an metottur.
    {
        _playerWalkingImage = _playerWalkingTransform.GetComponent<Image>();//RectTransform objelerinin i�indeki Image bile�inini al�r.
        _playerSlidingImage = _playerSlidingTransform.GetComponent<Image>();//RectTransform objelerinin i�indeki Image bile�inini al�r.
    }

    private void Start()//Unity de start metodu awake �al��t�ktan sonra ve sahne ba�lat�ld���nda �a�r�l�r.
    {
        _playerController.OnPlayerStateChanged += PlayerController_OnPlayerStateChanged;
        // Oyuncunun UI'de y�r�y�p y�r�medi�ini, kay�p kaymad���n� vs. g�ncelleyen bir fonksiyondur.
        _playableDirector.stopped += OnTimelineFineshed;


    }

    private void OnTimelineFineshed(PlayableDirector director)
    {
        SetStateUserInterfaces(_playerWalkingActiveSprite, _playerSlidingPassiveSprite, _playerWalkingTransform, _playerSlidingTransform);
    }

    private void PlayerController_OnPlayerStateChanged(PlayerState playerState)//Bu metod, PlayerController s�n�f�ndan gelen OnPlayerStateChanged event'ine ba�l�d�r.
         //Parametre olarak yeni oyuncu durumu (playerState) gelir.
    {
        switch (playerState)//Oyuncunun durumu kontrol edilir.
        {
            case PlayerState.Idle:
            case PlayerState.Move://E�er oyuncu Idle (bo�ta durma) ya da Move (y�r�me) durumundaysa bu blok �al���r.
                SetStateUserInterfaces(_playerWalkingActiveSprite, _playerSlidingPassiveSprite, _playerWalkingTransform, _playerSlidingTransform);
                //UI�de y�r�me aktif, kayma pasif g�sterilir.
                break;//Bu durumun i�lemleri bitmi�tir, switch�ten ��k�l�r.

                


            case PlayerState.SlideIdle:
            case PlayerState.Slide://E�er oyuncu SlideIdle (kayma duraksamas�) veya Slide (kayma hareketi) durumundaysa bu blok �al���r.
             SetStateUserInterfaces(_playerWalkingPassiveSprite, _playerSlidingActiveSprite, _playerSlidingTransform, _playerWalkingTransform);
                //UI�de kayma aktif, y�r�me pasif g�sterilir.
                break;//switch yap�s�ndan ��k�l�r.

        }
    }
    private void SetStateUserInterfaces(Sprite playerWalkingSprite,Sprite playerSlidingSprite,
        RectTransform activeTransform, RectTransform passiveTransform)//Oyuncunun durumuna g�re UI�de hangi sprite'lar�n ve animasyonlar�n g�sterilece�ini ayarlar.
    {
        _playerWalkingImage.sprite = playerWalkingSprite;//Y�r�me resmini verilen sprite ile de�i�tirir.
        _playerSlidingImage.sprite = playerSlidingSprite;//Kayma resmini verilen  sprite ile de�i�tirir.

        activeTransform.DOAnchorPosX(-25f, _moveDuration).SetEase(_moveEase);//Aktif olan UI eleman�, X ekseninde -25f pozisyonuna animasyonla ta��n�r.
        activeTransform.DOAnchorPosX(-90f, _moveDuration).SetEase(_moveEase);
    }

    private IEnumerator SetBoosterUserInterfaces(RectTransform activeTransform, Image boosterImage,
        Image AppleImage,Sprite activeSprite, Sprite passiveSprite,Sprite activeAppleSprite,
        Sprite passiveAppleSprite, float duration)
    //Belirli s�re boyunca booster (g��lendirici) UI animasyonu oynat�r.
    {
        boosterImage.sprite = activeSprite;//Booster ikonuna aktif g�r�n�m atan�r.

        AppleImage.sprite = activeAppleSprite;//Elma (apple) ikonuna aktif g�r�n�m atan�r.
        activeTransform.DOAnchorPosX(25F, _moveDuration).SetEase(_moveEase);//Booster UI eleman� sola do�ru animasyonla kayd�r�l�r.

        yield return new WaitForSeconds(duration);//Belirtilen s�re boyunca bekletilir.

        boosterImage.sprite = passiveSprite;//Booster ikonu pasif g�r�n�me d�ner.
        AppleImage.sprite = passiveAppleSprite;//Elma ikonu pasif g�r�n�me d�ner.
        activeTransform.DOAnchorPosX(90f, _moveDuration).SetEase(_moveEase);//UI elementi tekrar eski yerine animasyonla d�ner.
    }

    public void PlayBoosterUIAnimations(RectTransform activeTransform, Image boosterImage,
        Image AppleImage, Sprite activeSprite, Sprite passiveSprite, Sprite activeAppleSprite,
        Sprite passiveAppleSprite, float duration)//Bu metot d��ar�dan �a�r�ld���nda booster UI animasyonunu ba�lat�r.
    {
        StartCoroutine(SetBoosterUserInterfaces(activeTransform, boosterImage, AppleImage, activeSprite, passiveSprite, activeAppleSprite, passiveAppleSprite, duration));
        //Coroutine ba�lat�l�r ve booster animasyonu oynat�lmaya ba�lan�r.
    }
}
