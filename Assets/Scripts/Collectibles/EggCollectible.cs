using UnityEngine;

public class EggCollectible : MonoBehaviour, ICollectible
{
    // Bu method, yumurtaya dokunulduðunda veya toplandýðýnda çaðrýlýr
    public void Collect()
    {
        // GameManager'daki yumurta toplama fonksiyonunu tetikler
        GameManager.Instance.OnEggCollected();

        // Kamerayý sarsarak toplama hissi yaratýr (0.5 saniye þiddet ve süre)
        CameraShake.Instance.ShakeCamera(0.5f, 0.5f);

        // Yumurtayý toplama sesini oynatýr
        AudioManager.Instance.Play(SoundType.PickupGoodSound);

        // Yumurtanýn oyun sahnesinden silinmesini saðlar
        Destroy(gameObject);
    }
}
