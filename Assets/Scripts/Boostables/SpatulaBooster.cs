using UnityEngine;

public class SpatulaBooster : MonoBehaviour, IBoostable
//Bu sýnýf ayný zamanda bir ICollectible arayüzünü (interface) uyguluyor.Spatulabooster bu ara yüzü uygulayarak
//bir boost fonksiyonun çalýþýr hale getirebileceðini ifade ediyor.
{
    [Header("References")]
    [SerializeField] private Animator _spatulaAnimator;// Spatulanýn animasyonunu ifade eder.

    [Header("Settings")]
    [SerializeField] private float _jumpForce;//Uygulanacak zýplama kuvvetini ifade eder.


    private bool _isActivated;// Boost fonksiyonunun tekrar tekrar çaðrýlmasýný engeller.Sadece aktif deðilse Boost uygulanabilir.
    public void Boost(PlayerController playerController)//Bu fonksiyon oyuncuya zýplama kuvveti uygular.
    {
        if (_isActivated) { return;}//Zaten aktif ise iþlevi bitir anlamýna gelir.
        PlayBoostAnimation();//Zýplama animasyonunu baþlatýr.
        Rigidbody playerRigidbody = playerController.GetPlayerRigidbody();//Player Controller üzerinden oyuncunun RigidBodysi alýnýr.

        playerRigidbody.linearVelocity = new Vector3(playerRigidbody.linearVelocity.x, 0f, playerRigidbody.linearVelocity.z);
        //Dikey hýzý sýfýrlanýr.Böylece önceki düþme/zýplama etkisiz hale getirilir.
        playerRigidbody.AddForce(transform.forward * _jumpForce, ForceMode.Impulse);//Spatulanýn yönünde JumpForce uygulanýr.
        _isActivated = true;
        Invoke(nameof(ResetActivation), 0.2f);
        //Aktif hale geçer ve 0.2 saniye sonra tekrar devre dýþý býrakýlýr(ResetActivation ile) .
    }   

    private void PlayBoostAnimation()
    {
        _spatulaAnimator.SetTrigger(Consts.OtherAnimations.IS_SPATULA_JUMPING);//Animatore zýplama etkeni gönderilmesini saðlar.
            
    }

    private void ResetActivation()
    {
        _isActivated = false;//Boost tekrar kullanýlabilir hale gelir.
    }
}
