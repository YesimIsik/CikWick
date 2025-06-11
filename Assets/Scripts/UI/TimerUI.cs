using DG.Tweening;
using System;
using TMPro;
using UnityEngine;

public class TimerUI : MonoBehaviour

{
    [Header("References")]
    [SerializeField] private RectTransform _timerRotatableTransform;
    // Timer ikonunun d�nece�i UI elementinin RectTransform referans�

    [SerializeField] private TMP_Text _timerText;
    // Timer'�n g�sterilece�i TextMeshPro metin alan�

    [Header("Settings")]
    [SerializeField] private float _rotationDuration;
    // D�nd�rme animasyonunun s�resi (saniye)

    [SerializeField] private Ease _rotationEase;
    // DOTween d�nd�rme animasyonunun kolayla�t�rma (ease) t�r�

    private float _elapsedTime;
    // Ge�en s�reyi saniye cinsinden tutan de�i�ken

    private bool _isTimerRunnig;
    // Timer'�n �al���p �al��mad���n� tutan bayrak

    private Tween _rotationTween;
    // DOTween d�nd�rme animasyonunun referans�

    private string _finaltime;
    // Timer durdu�unda g�sterilecek son zaman metni

    private void Start()
    {
        // Oyun durumlar� de�i�ti�inde tetiklenen event'e abone oluyoruz
        GameManager.Instance.OnGameStateChanged += GameManager_OnGameStateChanged;
    }

    private void GameManager_OnGameStateChanged(GameState gameState)
    {
        // Oyun durumu de�i�ti�inde �al��acak metot
        switch (gameState)
        {
            case GameState.Play:
                PlayRotationAnimation();  // Timer ikonunu d�nd�rmeye ba�la
                StartTimer();             // Timer'� s�f�rla ve ba�lat
                break;
            case GameState.Pause:
                StopTimer();              // Timer'� durdur
                break;
            case GameState.Resume:
                ResumeTimer();            // Timer'� devam ettir
                break;
            case GameState.GameOver:
                FinishTimer();            // Timer'� durdur ve son zaman� kaydet
                break;
        }
    }

    private void PlayRotationAnimation()
    {
        // Timer ikonunu s�rekli d�nd�rmek i�in DOTween animasyonu ba�lat�l�r
        _rotationTween = _timerRotatableTransform.DORotate(
            new Vector3(0f, 0f, -360f), // Z ekseninde tam tur d�n��
            _rotationDuration,           // Animasyonun s�resi
            RotateMode.FastBeyond360     // H�zl� d�nd�rme modu
        )
        .SetLoops(-1, LoopType.Restart)   // Sonsuz d�ng� yapar
        .SetEase(_rotationEase);           // Belirlenen easing t�r�n� uygular
    }

    private void StartTimer()
    {
        _isTimerRunnig = true;           // Timer aktif
        _elapsedTime = 0;                // S�re s�f�rlan�r
        InvokeRepeating(nameof(UpdateTimerUI), 0f, 1f);
        // UpdateTimerUI metodunu hemen ve her saniye tekrarla
    }

    private void StopTimer()
    {
        _isTimerRunnig = false;          // Timer pasif
        CancelInvoke(nameof(UpdateTimerUI)); // UpdateTimerUI �a�r�lar� iptal edilir
        _rotationTween.Pause();          // D�nd�rme animasyonu durdurulur
    }

    private void ResumeTimer()
    {
        if (!_isTimerRunnig)
        {
            _isTimerRunnig = true;             // Timer aktif yap�l�r
            InvokeRepeating(nameof(UpdateTimerUI), 0f, 1f);
            // UpdateTimerUI �a�r�lar� tekrar ba�lat�l�r
            _rotationTween.Play();              // D�nd�rme animasyonu devam eder
        }
    }

    private void FinishTimer()
    {
        StopTimer();                        // Timer durdurulur
        _finaltime = GetFormattedlapsedTime();
        // Son ge�en s�re formatlan�p kaydedilir
    }

    private string GetFormattedlapsedTime()
    {
        int minutes = Mathf.FloorToInt(_elapsedTime / 60f);
        // Ge�en s�reden dakika hesaplan�r (tam say�)

        int seconds = Mathf.FloorToInt(_elapsedTime % 60f);
        // Ge�en s�reden saniye hesaplan�r

        return string.Format("{0:00}:{1:00}", minutes, seconds);
        // "mm:ss" format�nda metin d�nd�r�l�r
    }

    private void UpdateTimerUI()
    {
        if (!_isTimerRunnig) return;  // Timer aktif de�ilse i�lemi durdur

        _elapsedTime += 1f;           // Ge�en s�reyi 1 saniye artt�r

        int minutes = Mathf.FloorToInt(_elapsedTime / 60f);
        // Dakika hesapla

        int seconds = Mathf.FloorToInt(_elapsedTime % 60f);
        // Saniye hesapla

        _timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        // Timer UI'�nda g�ncellenen s�reyi g�ster
    }

    public string GetFinalTime()
    {
        return _finaltime;           // Oyun bitti�inde kaydedilen son zaman� d�nd�r�r
    }
}
