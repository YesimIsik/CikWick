using UnityEngine;

public class CatAnimationController : MonoBehaviour
{
    [SerializeField] private Animator _catAnimator;//Unity Editor üzerinden atanan Animator bileþenidir.

    private CatStateController _catStateController;//Kedi hangi durumda (Idle, Yürüyor, Koþuyor, vs.) bunu tutan kontrol bileþeni.

    private void Awake()
    {
        _catStateController = GetComponent<CatStateController>();//Ayný GameObject üzerindeki CatStateController bileþeni bulunur ve referans alýnýr.
    }

    private void Update()
    {
        if (GameManager.Instance.GetCurrentGameState() != GameState.Play
         && GameManager.Instance.GetCurrentGameState() != GameState.Resume
         && GameManager.Instance.GetCurrentGameState() != GameState.CutScene
           && GameManager.Instance.GetCurrentGameState() != GameState.GameOver)
        {
            _catAnimator.enabled = false;
            return;
        }

        SetCatAnimations();//Her frame’de kedinin animasyon durumunu günceller.Bu sayede anlýk olarak animasyon deðiþimleri gerçekleþir.
    }

    private void SetCatAnimations()
    {
        _catAnimator.enabled = true;
        var currentCatState = _catStateController.GetCurrentState();//Kedinin þu anki durumu alýnýr (Idle, Walking, Running, Attacking).

        switch (currentCatState)
        {

            case CatState.Idle://Kedi boþta bekliyorsa sadece "idling" animasyonu aktif edilir, diðerleri kapatýlýr.
                _catAnimator.SetBool(Consts.CatAnimations.IS_IDLING, true);
                _catAnimator.SetBool(Consts.CatAnimations.IS_WALKING, false);
                _catAnimator.SetBool(Consts.CatAnimations.IS_RUNNING, false);
                break;


            case CatState.Walking:
                _catAnimator.SetBool(Consts.CatAnimations.IS_IDLING, false);
                _catAnimator.SetBool(Consts.CatAnimations.IS_WALKING, true);
                _catAnimator.SetBool(Consts.CatAnimations.IS_RUNNING, false);
                break;
            //Kedi yürüyorsa sadece "walking" animasyonu açýk kalýr.

            case CatState.Running:
              _catAnimator.SetBool(Consts.CatAnimations.IS_RUNNING, true);
                break;
            //Kedi koþuyorsa sadece koþma animasyonu aktif edilir.
            case CatState.Attacking:
                _catAnimator.SetBool(Consts.CatAnimations.IS_ATTACKING, true);
                break;

                //Kedi saldýrýyorsa saldýrý animasyonu çalýþtýrýlýr.
        }
    }
}
