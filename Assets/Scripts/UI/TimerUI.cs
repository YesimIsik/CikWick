using DG.Tweening;
using System;
using TMPro;
using UnityEngine;

public class TimerUI : MonoBehaviour

{
    [Header("References")]
    [SerializeField] private RectTransform _timerRotatableTransform;
    // Timer ikonunun döneceði UI elementinin RectTransform referansý

    [SerializeField] private TMP_Text _timerText;
    // Timer'ýn gösterileceði TextMeshPro metin alaný

    [Header("Settings")]
    [SerializeField] private float _rotationDuration;
    // Döndürme animasyonunun süresi (saniye)

    [SerializeField] private Ease _rotationEase;
    // DOTween döndürme animasyonunun kolaylaþtýrma (ease) türü

    private float _elapsedTime;
    // Geçen süreyi saniye cinsinden tutan deðiþken

    private bool _isTimerRunnig;
    // Timer'ýn çalýþýp çalýþmadýðýný tutan bayrak

    private Tween _rotationTween;
    // DOTween döndürme animasyonunun referansý

    private string _finaltime;
    // Timer durduðunda gösterilecek son zaman metni

    private void Start()
    {
        // Oyun durumlarý deðiþtiðinde tetiklenen event'e abone oluyoruz
        GameManager.Instance.OnGameStateChanged += GameManager_OnGameStateChanged;
    }

    private void GameManager_OnGameStateChanged(GameState gameState)
    {
        // Oyun durumu deðiþtiðinde çalýþacak metot
        switch (gameState)
        {
            case GameState.Play:
                PlayRotationAnimation();  // Timer ikonunu döndürmeye baþla
                StartTimer();             // Timer'ý sýfýrla ve baþlat
                break;
            case GameState.Pause:
                StopTimer();              // Timer'ý durdur
                break;
            case GameState.Resume:
                ResumeTimer();            // Timer'ý devam ettir
                break;
            case GameState.GameOver:
                FinishTimer();            // Timer'ý durdur ve son zamaný kaydet
                break;
        }
    }

    private void PlayRotationAnimation()
    {
        // Timer ikonunu sürekli döndürmek için DOTween animasyonu baþlatýlýr
        _rotationTween = _timerRotatableTransform.DORotate(
            new Vector3(0f, 0f, -360f), // Z ekseninde tam tur dönüþ
            _rotationDuration,           // Animasyonun süresi
            RotateMode.FastBeyond360     // Hýzlý döndürme modu
        )
        .SetLoops(-1, LoopType.Restart)   // Sonsuz döngü yapar
        .SetEase(_rotationEase);           // Belirlenen easing türünü uygular
    }

    private void StartTimer()
    {
        _isTimerRunnig = true;           // Timer aktif
        _elapsedTime = 0;                // Süre sýfýrlanýr
        InvokeRepeating(nameof(UpdateTimerUI), 0f, 1f);
        // UpdateTimerUI metodunu hemen ve her saniye tekrarla
    }

    private void StopTimer()
    {
        _isTimerRunnig = false;          // Timer pasif
        CancelInvoke(nameof(UpdateTimerUI)); // UpdateTimerUI çaðrýlarý iptal edilir
        _rotationTween.Pause();          // Döndürme animasyonu durdurulur
    }

    private void ResumeTimer()
    {
        if (!_isTimerRunnig)
        {
            _isTimerRunnig = true;             // Timer aktif yapýlýr
            InvokeRepeating(nameof(UpdateTimerUI), 0f, 1f);
            // UpdateTimerUI çaðrýlarý tekrar baþlatýlýr
            _rotationTween.Play();              // Döndürme animasyonu devam eder
        }
    }

    private void FinishTimer()
    {
        StopTimer();                        // Timer durdurulur
        _finaltime = GetFormattedlapsedTime();
        // Son geçen süre formatlanýp kaydedilir
    }

    private string GetFormattedlapsedTime()
    {
        int minutes = Mathf.FloorToInt(_elapsedTime / 60f);
        // Geçen süreden dakika hesaplanýr (tam sayý)

        int seconds = Mathf.FloorToInt(_elapsedTime % 60f);
        // Geçen süreden saniye hesaplanýr

        return string.Format("{0:00}:{1:00}", minutes, seconds);
        // "mm:ss" formatýnda metin döndürülür
    }

    private void UpdateTimerUI()
    {
        if (!_isTimerRunnig) return;  // Timer aktif deðilse iþlemi durdur

        _elapsedTime += 1f;           // Geçen süreyi 1 saniye arttýr

        int minutes = Mathf.FloorToInt(_elapsedTime / 60f);
        // Dakika hesapla

        int seconds = Mathf.FloorToInt(_elapsedTime % 60f);
        // Saniye hesapla

        _timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        // Timer UI'ýnda güncellenen süreyi göster
    }

    public string GetFinalTime()
    {
        return _finaltime;           // Oyun bittiðinde kaydedilen son zamaný döndürür
    }
}
