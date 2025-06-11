using UnityEngine;

public class CatAnimationController : MonoBehaviour
{
    [SerializeField] private Animator _catAnimator;//Unity Editor �zerinden atanan Animator bile�enidir.

    private CatStateController _catStateController;//Kedi hangi durumda (Idle, Y�r�yor, Ko�uyor, vs.) bunu tutan kontrol bile�eni.

    private void Awake()
    {
        _catStateController = GetComponent<CatStateController>();//Ayn� GameObject �zerindeki CatStateController bile�eni bulunur ve referans al�n�r.
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

        SetCatAnimations();//Her frame�de kedinin animasyon durumunu g�nceller.Bu sayede anl�k olarak animasyon de�i�imleri ger�ekle�ir.
    }

    private void SetCatAnimations()
    {
        _catAnimator.enabled = true;
        var currentCatState = _catStateController.GetCurrentState();//Kedinin �u anki durumu al�n�r (Idle, Walking, Running, Attacking).

        switch (currentCatState)
        {

            case CatState.Idle://Kedi bo�ta bekliyorsa sadece "idling" animasyonu aktif edilir, di�erleri kapat�l�r.
                _catAnimator.SetBool(Consts.CatAnimations.IS_IDLING, true);
                _catAnimator.SetBool(Consts.CatAnimations.IS_WALKING, false);
                _catAnimator.SetBool(Consts.CatAnimations.IS_RUNNING, false);
                break;


            case CatState.Walking:
                _catAnimator.SetBool(Consts.CatAnimations.IS_IDLING, false);
                _catAnimator.SetBool(Consts.CatAnimations.IS_WALKING, true);
                _catAnimator.SetBool(Consts.CatAnimations.IS_RUNNING, false);
                break;
            //Kedi y�r�yorsa sadece "walking" animasyonu a��k kal�r.

            case CatState.Running:
              _catAnimator.SetBool(Consts.CatAnimations.IS_RUNNING, true);
                break;
            //Kedi ko�uyorsa sadece ko�ma animasyonu aktif edilir.
            case CatState.Attacking:
                _catAnimator.SetBool(Consts.CatAnimations.IS_ATTACKING, true);
                break;

                //Kedi sald�r�yorsa sald�r� animasyonu �al��t�r�l�r.
        }
    }
}
