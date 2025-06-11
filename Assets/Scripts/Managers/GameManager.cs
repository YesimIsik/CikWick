using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }//Singleton desenini uygular. Böylece diðer sýnýflar GameManager.Instance ile bu nesneye eriþebilir.

    public event Action<GameState> OnGameStateChanged;//Oyun durumu deðiþtiðinde dinleyenlere bilgi veren bir olay (event).

    [Header("References")]

    [SerializeField] private CatController _catController;
    [SerializeField] private EggCounterUI _eggCounterUI;
    [SerializeField] private WinLoseUI _winLoseUI;
    [SerializeField] private PlayerHealthUI _playerHealthUI;
    //Kedi kontrolü, yumurta sayacý, kazandý/kaybetti arayüzü ve oyuncu caný UI’si gibi bileþenleri tutar.

    [Header("Settings")]
    [SerializeField] private int _maxEggCount = 5; //Toplanmasý gereken maksimum yumurta sayýsý.
    [SerializeField] private float _delay;//Oyun bittiðinde bekleme süresi (kaybetme/kazanma sahnesine geçmeden önce).

    private GameState _currentGameState;

    private int _currentEggCount;

    private bool _isCatCatched;
    //Þu anki oyun durumu, toplanan yumurta sayýsý ve kedinin oyuncuyu yakalayýp yakalamadýðý bilgisi.



    private void Awake()
    {
        Instance = this;//Singleton nesnesi olarak kendini atar.

    }

    private void Start()
    {
        HealthManager.Instance.OnPlayerDeath += HealthManager_OnPlayerDeath;
        _catController.OnCatCatched += CatController_OnCatCatched;//Oyuncu ölünce ve kedi oyuncuyu yakalayýnca çaðrýlacak event'ler atanýr.


    }

    private void CatController_OnCatCatched()
    {
        if (!_isCatCatched)
        {
            _playerHealthUI.AnimateDamageForAll();//Can UI’sine hasar animasyonu verilir.
            StartCoroutine(OnGameOver(true));//Oyun kaybetme süreci baþlatýlýr.
            CameraShake.Instance.ShakeCamera(1.5f, 2f, 0.5f);//Kamera sarsýlýr.
            _isCatCatched = true;
            //_isCatCatched = true yapýlarak tekrar tetiklenmesi engellenir.
        }
    }

    private void HealthManager_OnPlayerDeath()
    {
        StartCoroutine(OnGameOver(false));//Oyuncunun caný sýfýrlanýrsa oyunu kaybeder.


    }

    private void OnEnable()
    {
        ChangeGameState(GameState.CutScene);//Oyun baþladýðýnda oyun durumu CutScene olarak ayarlanýr.
        BackgroundMusic.Instance.PlayBackgroundMusic(true);//Arka plan müziði baþlatýlýr.


    }

    public void ChangeGameState(GameState gameState)
    {
        OnGameStateChanged?.Invoke(gameState);//Yeni bir oyun durumu atanýr.
        _currentGameState = gameState;
        Debug.Log("Game State:" + gameState);//Event tetiklenir, debug’a yazýlýr.


    }

    public void OnEggCollected()
    {
        _currentEggCount++;
        _eggCounterUI.SetEggCounterText(_currentEggCount, _maxEggCount);//Yumurta sayýsý artýrýlýr ve UI güncellenir.

        if (_currentEggCount == _maxEggCount)//Maksimum yumurta sayýsýna ulaþýlýrsa:


        {

            //WÝN
            _eggCounterUI.SetEggCompleted();
            ChangeGameState(GameState.GameOver);
            _winLoseUI.OnGameWin();//Win UI gösterilir.
        }
       
    }

    private IEnumerator OnGameOver(bool isCatCatched)
    {
        yield return new WaitForSeconds(_delay);//Belirli bir süre beklenir.
        ChangeGameState(GameState.GameOver);//Kaybetme ekraný gösterilir.
        _winLoseUI.OnGameLose();
       if (isCatCatched) { AudioManager.Instance.Play(SoundType.CatSound); }//Eðer kedi yakaladýysa kedi sesi çalýnýr.

    }

   
    public GameState GetCurrentGameState()
    {
        return _currentGameState;//Þu anda oyunun hangi durumda olduðunu döner (CutScene, Pause, Play, GameOver vs.)
    }
}
