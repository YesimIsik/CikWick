using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStateUI : MonoBehaviour
{
    [Header("References")]

    [SerializeField] private RectTransform _playerWalkingTransform;
    [SerializeField] private RectTransform _playerSlidingTransform;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private RectTransform _boosterSpeedTransform;
    [SerializeField] private RectTransform _boosterJumpTransform;
    [SerializeField] private RectTransform _boosterSlowTransform;

    [Header("Images")]
    [SerializeField] private Image _RedAppleImage;
    [SerializeField] private Image _GreenAppleImage;
    [SerializeField] private Image _YellowAppleImage;

    [Header("Sprites")] 

    [SerializeField] private Sprite _playerWalkingActiveSprite;
    [SerializeField] private Sprite _playerWalkingPassiveSprite;
    [SerializeField] private Sprite _playerSlidingActiveSprite;
    [SerializeField] private Sprite _playerSlidingPassiveSprite;

    [Header("Settings")]
    [SerializeField] private float _moveDuration;
    [SerializeField] private Ease _moveEase;



    public RectTransform GetBoosterSpeedTransform => _boosterSpeedTransform;
    public RectTransform GetBoosterJumpTransform => _boosterJumpTransform;

    public RectTransform GetBoosterSlowTransform => _boosterSlowTransform;
     
    public Image GetRedAppleImage => _RedAppleImage;
    public Image GetGreenAppleImage => _GreenAppleImage;
    public Image GetYellowAppleImage => _YellowAppleImage;

    private Image _playerWalkingImage;
    private Image _playerSlidingImage;

    

   private void Awake()
    {
        _playerWalkingImage = _playerWalkingTransform.GetComponent<Image>();
        _playerSlidingImage = _playerSlidingTransform.GetComponent<Image>();
    }

    private void Start()
    {
        _playerController.OnPlayerStateChanged += PlayerController_OnPlayerStateChanged;
    }

    private void PlayerController_OnPlayerStateChanged(PlayerState playerState)
    {
        switch (playerState)
        {
            case PlayerState.Idle:
            case PlayerState.Move:
            SetStateUserInterfaces(_playerWalkingActiveSprite, _playerSlidingPassiveSprite, _playerWalkingTransform, _playerSlidingTransform);
                break;

            case PlayerState.SlideIdle:
            case PlayerState.Slide:
            SetStateUserInterfaces(_playerWalkingPassiveSprite, _playerSlidingActiveSprite, _playerSlidingTransform, _playerWalkingTransform);
                break;

        }
    }
    private void SetStateUserInterfaces(Sprite playerWalkingSprite,Sprite playerSlidingSprite,
        RectTransform activeTransform, RectTransform passiveTransform)
    {
        _playerWalkingImage.sprite = playerWalkingSprite;
        _playerSlidingImage.sprite = playerSlidingSprite;

        activeTransform.DOAnchorPosX(-25f, _moveDuration).SetEase(_moveEase);
        activeTransform.DOAnchorPosX(-90f, _moveDuration).SetEase(_moveEase);
    }

    private IEnumerator SetBoosterUserInterfaces(RectTransform activeTransform, Image boosterImage,
        Image AppleImage,Sprite activeSprite, Sprite passiveSprite,Sprite activeAppleSprite,
        Sprite passiveAppleSprite, float duration)
    {
        boosterImage.sprite = activeSprite;
        AppleImage.sprite = activeAppleSprite;
        activeTransform.DOAnchorPosX(25F, _moveDuration).SetEase(_moveEase);

        yield return new WaitForSeconds(duration);

        boosterImage.sprite = passiveSprite;
        AppleImage.sprite = passiveAppleSprite;
        activeTransform.DOAnchorPosX(90f, _moveDuration).SetEase(_moveEase);
      }

    public void PlayBoosterUIAnimations(RectTransform activeTransform, Image boosterImage,
        Image AppleImage, Sprite activeSprite, Sprite passiveSprite, Sprite activeAppleSprite,
        Sprite passiveAppleSprite, float duration)
    {
        StartCoroutine(SetBoosterUserInterfaces(activeTransform, boosterImage, AppleImage, activeSprite, passiveSprite, activeAppleSprite, passiveAppleSprite, duration));
    }
}
