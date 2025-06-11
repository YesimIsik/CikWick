using MaskTransitions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuControllerUI : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _quitButton;
    //Unity Editörü üzerinden atanan "Oyna" (Play) ve "Çýkýþ" (Quit) butonlarýdýr.
    private void Awake()//Oyun baþlarken sahnedeki bu bileþen aktif olduðunda bir kez çalýþýr.
    {
        _playButton.onClick.AddListener(() =>//Oyna butonuna týklanýnca:
        {
            AudioManager.Instance.Play(SoundType.TransitionSound);//Geçiþ sesi çalýnýr (TransitionSound).
            TransitionManager.Instance.LoadLevel(Consts.SceneNames.GAME_SCENE);//TransitionManager kullanýlarak oyun sahnesine geçiþ yapýlýr (GAME_SCENE).

        });

        _quitButton.onClick.AddListener(() =>//Çýkýþ butonuna týklanýnca:
        {
            AudioManager.Instance.Play(SoundType.ButtonClickSound);//Týklama sesi çalýnýr.


            Debug.Log("Quiting the Game!");//Konsola "Quiting the Game!" yazýlýr (sadece editörde görünür).
            Application.Quit();//Application.Quit() ile oyun kapatýlýr. Bu sadece build edilmiþ oyunlarda çalýþýr; Unity editöründe çalýþmaz.
        });
    }
}
