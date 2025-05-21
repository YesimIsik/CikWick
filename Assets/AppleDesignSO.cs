using UnityEngine;


[CreateAssetMenu(fileName = "AppleDesignSO", menuName = "ScriptableObjects/AppleDesignSO")]
//Bu attribute (öznitelik), Unity editöründe bu ScriptableObject'ten yeni bir varlık (asset) oluşturulmasını sağlar.
public class AppleDesignSO : ScriptableObject
//Bu sınıf bir ScriptableObject'tir, yani sahneye eklenmeden Unity editöründe veri depolamak için kullanılır.
{
    [SerializeField] private float _increaseDecreaseMultiplier;
    //Güçlendirme etkisinin çarpan değeridir.Örneğin: 1.5 → hız/zıplama %50 artar, 0.5 → %50 azalır.
    [SerializeField] private float _resetBoostDuration;
    //Güçlendirme etkisinin ne kadar süreyle geçerli olacağını belirtir.Örneğin: 3 saniye boyunca oyuncunun hızı artar.
    [SerializeField] private Sprite _activeSprite;
    [SerializeField] private Sprite _passiveSprite;
    [SerializeField] private Sprite _activeAppleSprite;
    [SerializeField] private Sprite _passiveAppleSprite;

    public float IncreaseDecreaseMultiplier => _increaseDecreaseMultiplier;
    //Read-only olarak sadece değeri döner. Setter yoktur, yani değiştirilemez.
    public float ResetBoostDuration => _resetBoostDuration;
    //Bu da benzer şekilde değişkeni okuma amaçlı dışarıya açar.Güçlendirme süresini almak için diğer sınıflar bunu kullanır.
    public Sprite ActiveSprite => _activeSprite;
    public Sprite PassiveSprite => _passiveSprite;
    public Sprite ActiveAppleSprite => _activeAppleSprite;
    public Sprite PassiveAppleSprite => _passiveAppleSprite;

}
