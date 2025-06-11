using MaskTransitions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuControllerUI : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _quitButton;
    //Unity Edit�r� �zerinden atanan "Oyna" (Play) ve "��k��" (Quit) butonlar�d�r.
    private void Awake()//Oyun ba�larken sahnedeki bu bile�en aktif oldu�unda bir kez �al���r.
    {
        _playButton.onClick.AddListener(() =>//Oyna butonuna t�klan�nca:
        {
            AudioManager.Instance.Play(SoundType.TransitionSound);//Ge�i� sesi �al�n�r (TransitionSound).
            TransitionManager.Instance.LoadLevel(Consts.SceneNames.GAME_SCENE);//TransitionManager kullan�larak oyun sahnesine ge�i� yap�l�r (GAME_SCENE).

        });

        _quitButton.onClick.AddListener(() =>//��k�� butonuna t�klan�nca:
        {
            AudioManager.Instance.Play(SoundType.ButtonClickSound);//T�klama sesi �al�n�r.


            Debug.Log("Quiting the Game!");//Konsola "Quiting the Game!" yaz�l�r (sadece edit�rde g�r�n�r).
            Application.Quit();//Application.Quit() ile oyun kapat�l�r. Bu sadece build edilmi� oyunlarda �al���r; Unity edit�r�nde �al��maz.
        });
    }
}
