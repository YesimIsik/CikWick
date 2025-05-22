using UnityEngine;

public class SpatulaBooster : MonoBehaviour, IBoostable
//Bu s�n�f ayn� zamanda bir ICollectible aray�z�n� (interface) uyguluyor.Spatulabooster bu ara y�z� uygulayarak
//bir boost fonksiyonun �al���r hale getirebilece�ini ifade ediyor.
{
    [Header("References")]
    [SerializeField] private Animator _spatulaAnimator;// Spatulan�n animasyonunu ifade eder.

    [Header("Settings")]
    [SerializeField] private float _jumpForce;//Uygulanacak z�plama kuvvetini ifade eder.


    private bool _isActivated;// Boost fonksiyonunun tekrar tekrar �a�r�lmas�n� engeller.Sadece aktif de�ilse Boost uygulanabilir.
    public void Boost(PlayerController playerController)//Bu fonksiyon oyuncuya z�plama kuvveti uygular.
    {
        if (_isActivated) { return;}//Zaten aktif ise i�levi bitir anlam�na gelir.
        PlayBoostAnimation();//Z�plama animasyonunu ba�lat�r.
        Rigidbody playerRigidbody = playerController.GetPlayerRigidbody();//Player Controller �zerinden oyuncunun RigidBodysi al�n�r.

        playerRigidbody.linearVelocity = new Vector3(playerRigidbody.linearVelocity.x, 0f, playerRigidbody.linearVelocity.z);
        //Dikey h�z� s�f�rlan�r.B�ylece �nceki d��me/z�plama etkisiz hale getirilir.
        playerRigidbody.AddForce(transform.forward * _jumpForce, ForceMode.Impulse);//Spatulan�n y�n�nde JumpForce uygulan�r.
        _isActivated = true;
        Invoke(nameof(ResetActivation), 0.2f);
        //Aktif hale ge�er ve 0.2 saniye sonra tekrar devre d��� b�rak�l�r(ResetActivation ile) .
    }   

    private void PlayBoostAnimation()
    {
        _spatulaAnimator.SetTrigger(Consts.OtherAnimations.IS_SPATULA_JUMPING);//Animatore z�plama etkeni g�nderilmesini sa�lar.
            
    }

    private void ResetActivation()
    {
        _isActivated = false;//Boost tekrar kullan�labilir hale gelir.
    }
}
