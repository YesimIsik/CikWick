using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class PlayerStateUI : MonoBehaviour//Unity de UI yönetimi için kullanýlan bir sýnýf.
{
    [Header("References")]//Inspector'da bu alaný “References” baþlýðý altýnda gruplayarak daha okunabilir yapar.

    [SerializeField] private RectTransform _playerWalkingTransform;
    [SerializeField] private RectTransform _playerSlidingTransform;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private RectTransform _boosterSpeedTransform;
    [SerializeField] private RectTransform _boosterJumpTransform;
    [SerializeField] private RectTransform _boosterSlowTransform;
    [SerializeField] private PlayableDirector _playableDirector;
    //Bu deðiþkenler UI ögelerine karþýlýk gelir.
    //RectTransform:UI nesnesinin pozisyon, boyut ve hizalanmasýný temsil eder.
    [Header("Images")]
    [SerializeField] private Image _RedAppleImage;
    [SerializeField] private Image _GreenAppleImage;
    [SerializeField] private Image _YellowAppleImage;
    //Oyuncunun UI'de göreceði elma görsellerini ifade eder.

    [Header("Sprites")] 

    [SerializeField] private Sprite _playerWalkingActiveSprite;
    [SerializeField] private Sprite _playerWalkingPassiveSprite;
    [SerializeField] private Sprite _playerSlidingActiveSprite;
    [SerializeField] private Sprite _playerSlidingPassiveSprite;
    //Oyuncunun durumunu (yürüme ve kaymanýn aktif veya pasif olduðunu) görselleyen spritelardýr.

    [Header("Settings")]
    [SerializeField] private float _moveDuration;//UI öðelerinin ne kadar sürede hareket edeceðini belirler.
    [SerializeField] private Ease _moveEase;// Tweening (yumuþak geçiþ) animasyonlarý için Ease tipi
                                            // (Ease tipi genellikle DOTween kütüphanesinden gelir).



    public RectTransform GetBoosterSpeedTransform => _boosterSpeedTransform;
    public RectTransform GetBoosterJumpTransform => _boosterJumpTransform;
    public RectTransform GetBoosterSlowTransform => _boosterSlowTransform;
    //Bu üç get özelliði, dýþ sýnýflarýn _boosterSpeedTransform , _boosterJumpTransform ve _boosterSlowTransform referanslarýna eriþmesini saðlar. 
    public Image GetRedAppleImage => _RedAppleImage;
    public Image GetGreenAppleImage => _GreenAppleImage;
    public Image GetYellowAppleImage => _YellowAppleImage;
    //Her biri ilgili UI Image nesnesine eriþim saðlar. 

    private Image _playerWalkingImage;//Yürüme UI elemanýnýn Image bileþinini ifade eder.
    private Image _playerSlidingImage;//Kayma UI elemanýnýn Image bileþenini ifade eder.

    

   private void Awake()//Ýlk çalýþan metottur.
    {
        _playerWalkingImage = _playerWalkingTransform.GetComponent<Image>();//RectTransform objelerinin içindeki Image bileþinini alýr.
        _playerSlidingImage = _playerSlidingTransform.GetComponent<Image>();//RectTransform objelerinin içindeki Image bileþinini alýr.
    }

    private void Start()//Unity de start metodu awake çalýþtýktan sonra ve sahne baþlatýldýðýnda çaðrýlýr.
    {
        _playerController.OnPlayerStateChanged += PlayerController_OnPlayerStateChanged;
        // Oyuncunun UI'de yürüyüp yürümediðini, kayýp kaymadýðýný vs. güncelleyen bir fonksiyondur.
        _playableDirector.stopped += OnTimelineFineshed;


    }

    private void OnTimelineFineshed(PlayableDirector director)
    {
        SetStateUserInterfaces(_playerWalkingActiveSprite, _playerSlidingPassiveSprite, _playerWalkingTransform, _playerSlidingTransform);
    }

    private void PlayerController_OnPlayerStateChanged(PlayerState playerState)//Bu metod, PlayerController sýnýfýndan gelen OnPlayerStateChanged event'ine baðlýdýr.
         //Parametre olarak yeni oyuncu durumu (playerState) gelir.
    {
        switch (playerState)//Oyuncunun durumu kontrol edilir.
        {
            case PlayerState.Idle:
            case PlayerState.Move://Eðer oyuncu Idle (boþta durma) ya da Move (yürüme) durumundaysa bu blok çalýþýr.
                SetStateUserInterfaces(_playerWalkingActiveSprite, _playerSlidingPassiveSprite, _playerWalkingTransform, _playerSlidingTransform);
                //UI’de yürüme aktif, kayma pasif gösterilir.
                break;//Bu durumun iþlemleri bitmiþtir, switch’ten çýkýlýr.

                


            case PlayerState.SlideIdle:
            case PlayerState.Slide://Eðer oyuncu SlideIdle (kayma duraksamasý) veya Slide (kayma hareketi) durumundaysa bu blok çalýþýr.
             SetStateUserInterfaces(_playerWalkingPassiveSprite, _playerSlidingActiveSprite, _playerSlidingTransform, _playerWalkingTransform);
                //UI’de kayma aktif, yürüme pasif gösterilir.
                break;//switch yapýsýndan çýkýlýr.

        }
    }
    private void SetStateUserInterfaces(Sprite playerWalkingSprite,Sprite playerSlidingSprite,
        RectTransform activeTransform, RectTransform passiveTransform)//Oyuncunun durumuna göre UI’de hangi sprite'larýn ve animasyonlarýn gösterileceðini ayarlar.
    {
        _playerWalkingImage.sprite = playerWalkingSprite;//Yürüme resmini verilen sprite ile deðiþtirir.
        _playerSlidingImage.sprite = playerSlidingSprite;//Kayma resmini verilen  sprite ile deðiþtirir.

        activeTransform.DOAnchorPosX(-25f, _moveDuration).SetEase(_moveEase);//Aktif olan UI elemaný, X ekseninde -25f pozisyonuna animasyonla taþýnýr.
        activeTransform.DOAnchorPosX(-90f, _moveDuration).SetEase(_moveEase);
    }

    private IEnumerator SetBoosterUserInterfaces(RectTransform activeTransform, Image boosterImage,
        Image AppleImage,Sprite activeSprite, Sprite passiveSprite,Sprite activeAppleSprite,
        Sprite passiveAppleSprite, float duration)
    //Belirli süre boyunca booster (güçlendirici) UI animasyonu oynatýr.
    {
        boosterImage.sprite = activeSprite;//Booster ikonuna aktif görünüm atanýr.

        AppleImage.sprite = activeAppleSprite;//Elma (apple) ikonuna aktif görünüm atanýr.
        activeTransform.DOAnchorPosX(25F, _moveDuration).SetEase(_moveEase);//Booster UI elemaný sola doðru animasyonla kaydýrýlýr.

        yield return new WaitForSeconds(duration);//Belirtilen süre boyunca bekletilir.

        boosterImage.sprite = passiveSprite;//Booster ikonu pasif görünüme döner.
        AppleImage.sprite = passiveAppleSprite;//Elma ikonu pasif görünüme döner.
        activeTransform.DOAnchorPosX(90f, _moveDuration).SetEase(_moveEase);//UI elementi tekrar eski yerine animasyonla döner.
    }

    public void PlayBoosterUIAnimations(RectTransform activeTransform, Image boosterImage,
        Image AppleImage, Sprite activeSprite, Sprite passiveSprite, Sprite activeAppleSprite,
        Sprite passiveAppleSprite, float duration)//Bu metot dýþarýdan çaðrýldýðýnda booster UI animasyonunu baþlatýr.
    {
        StartCoroutine(SetBoosterUserInterfaces(activeTransform, boosterImage, AppleImage, activeSprite, passiveSprite, activeAppleSprite, passiveAppleSprite, duration));
        //Coroutine baþlatýlýr ve booster animasyonu oynatýlmaya baþlanýr.
    }
}
