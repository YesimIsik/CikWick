using System;
using UnityEngine;
using UnityEngine.Playables;

public class TimeLineManager : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;//GameManager a ula�mam�z� sa�lar.

    
    private PlayableDirector _playableDirector;//Playable director a ula�mam�z� sa�lar.


    private void Awake()
    {
        _playableDirector = GetComponent<PlayableDirector>(); //GameObject�te bulunan PlayableDirector bile�enini al�r ve _playableDirector de�i�kenine atar.Sahnedeki timel�n � kontrol etmemize yarar
    }

    private void OnEnable()//GameObject aktif hale geldi�inde SetActive(true) otomatik olarak �a�r�l�r.
    {
        _playableDirector.Play();//Timeline�� ba�lat�r, yani PlayableDirector'a ba�l� olan Timeline oynat�lmaya ba�lan�r.


        _playableDirector.stopped += OnTimelineFinished;//Timeline oynatmas� bitti�inde �a�r�lan metottur.

      //  PlayableDirector.stopped eventi taraf�ndan tetiklenir.
    }

    private void OnTimelineFinished(PlayableDirector director)
    {
        _gameManager.ChangeGameState(GameState.Play);//Timeline bitti�inde GameManager'a haber vererek oyun durumunu Play olarak de�i�tirir.
    }
}

