using System;
using UnityEngine;
using UnityEngine.Playables;

public class TimeLineManager : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;//GameManager a ulaþmamýzý saðlar.

    
    private PlayableDirector _playableDirector;//Playable director a ulaþmamýzý saðlar.


    private void Awake()
    {
        _playableDirector = GetComponent<PlayableDirector>(); //GameObject’te bulunan PlayableDirector bileþenini alýr ve _playableDirector deðiþkenine atar.Sahnedeki timelýn ý kontrol etmemize yarar
    }

    private void OnEnable()//GameObject aktif hale geldiðinde SetActive(true) otomatik olarak çaðrýlýr.
    {
        _playableDirector.Play();//Timeline’ý baþlatýr, yani PlayableDirector'a baðlý olan Timeline oynatýlmaya baþlanýr.


        _playableDirector.stopped += OnTimelineFinished;//Timeline oynatmasý bittiðinde çaðrýlan metottur.

      //  PlayableDirector.stopped eventi tarafýndan tetiklenir.
    }

    private void OnTimelineFinished(PlayableDirector director)
    {
        _gameManager.ChangeGameState(GameState.Play);//Timeline bittiðinde GameManager'a haber vererek oyun durumunu Play olarak deðiþtirir.
    }
}

