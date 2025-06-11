using UnityEngine;

public class EggCollectible : MonoBehaviour, ICollectible
{
    // Bu method, yumurtaya dokunuldu�unda veya topland���nda �a�r�l�r
    public void Collect()
    {
        // GameManager'daki yumurta toplama fonksiyonunu tetikler
        GameManager.Instance.OnEggCollected();

        // Kameray� sarsarak toplama hissi yarat�r (0.5 saniye �iddet ve s�re)
        CameraShake.Instance.ShakeCamera(0.5f, 0.5f);

        // Yumurtay� toplama sesini oynat�r
        AudioManager.Instance.Play(SoundType.PickupGoodSound);

        // Yumurtan�n oyun sahnesinden silinmesini sa�lar
        Destroy(gameObject);
    }
}
