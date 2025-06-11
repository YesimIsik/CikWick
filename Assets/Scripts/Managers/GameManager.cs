using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }//Singleton desenini uygular. B�ylece di�er s�n�flar GameManager.Instance ile bu nesneye eri�ebilir.

    public event Action<GameState> OnGameStateChanged;//Oyun durumu de�i�ti�inde dinleyenlere bilgi veren bir olay (event).

    [Header("References")]

    [SerializeField] private CatController _catController;
    [SerializeField] private EggCounterUI _eggCounterUI;
    [SerializeField] private WinLoseUI _winLoseUI;
    [SerializeField] private PlayerHealthUI _playerHealthUI;
    //Kedi kontrol�, yumurta sayac�, kazand�/kaybetti aray�z� ve oyuncu can� UI�si gibi bile�enleri tutar.

    [Header("Settings")]
    [SerializeField] private int _maxEggCount = 5; //Toplanmas� gereken maksimum yumurta say�s�.
    [SerializeField] private float _delay;//Oyun bitti�inde bekleme s�resi (kaybetme/kazanma sahnesine ge�meden �nce).

    private GameState _currentGameState;

    private int _currentEggCount;

    private bool _isCatCatched;
    //�u anki oyun durumu, toplanan yumurta say�s� ve kedinin oyuncuyu yakalay�p yakalamad��� bilgisi.



    private void Awake()
    {
        Instance = this;//Singleton nesnesi olarak kendini atar.

    }

    private void Start()
    {
        HealthManager.Instance.OnPlayerDeath += HealthManager_OnPlayerDeath;
        _catController.OnCatCatched += CatController_OnCatCatched;//Oyuncu �l�nce ve kedi oyuncuyu yakalay�nca �a�r�lacak event'ler atan�r.


    }

    private void CatController_OnCatCatched()
    {
        if (!_isCatCatched)
        {
            _playerHealthUI.AnimateDamageForAll();//Can UI�sine hasar animasyonu verilir.
            StartCoroutine(OnGameOver(true));//Oyun kaybetme s�reci ba�lat�l�r.
            CameraShake.Instance.ShakeCamera(1.5f, 2f, 0.5f);//Kamera sars�l�r.
            _isCatCatched = true;
            //_isCatCatched = true yap�larak tekrar tetiklenmesi engellenir.
        }
    }

    private void HealthManager_OnPlayerDeath()
    {
        StartCoroutine(OnGameOver(false));//Oyuncunun can� s�f�rlan�rsa oyunu kaybeder.


    }

    private void OnEnable()
    {
        ChangeGameState(GameState.CutScene);//Oyun ba�lad���nda oyun durumu CutScene olarak ayarlan�r.
        BackgroundMusic.Instance.PlayBackgroundMusic(true);//Arka plan m�zi�i ba�lat�l�r.


    }

    public void ChangeGameState(GameState gameState)
    {
        OnGameStateChanged?.Invoke(gameState);//Yeni bir oyun durumu atan�r.
        _currentGameState = gameState;
        Debug.Log("Game State:" + gameState);//Event tetiklenir, debug�a yaz�l�r.


    }

    public void OnEggCollected()
    {
        _currentEggCount++;
        _eggCounterUI.SetEggCounterText(_currentEggCount, _maxEggCount);//Yumurta say�s� art�r�l�r ve UI g�ncellenir.

        if (_currentEggCount == _maxEggCount)//Maksimum yumurta say�s�na ula��l�rsa:


        {

            //W�N
            _eggCounterUI.SetEggCompleted();
            ChangeGameState(GameState.GameOver);
            _winLoseUI.OnGameWin();//Win UI g�sterilir.
        }
       
    }

    private IEnumerator OnGameOver(bool isCatCatched)
    {
        yield return new WaitForSeconds(_delay);//Belirli bir s�re beklenir.
        ChangeGameState(GameState.GameOver);//Kaybetme ekran� g�sterilir.
        _winLoseUI.OnGameLose();
       if (isCatCatched) { AudioManager.Instance.Play(SoundType.CatSound); }//E�er kedi yakalad�ysa kedi sesi �al�n�r.

    }

   
    public GameState GetCurrentGameState()
    {
        return _currentGameState;//�u anda oyunun hangi durumda oldu�unu d�ner (CutScene, Pause, Play, GameOver vs.)
    }
}
