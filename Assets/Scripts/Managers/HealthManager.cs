using System;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public static HealthManager Instance { get; private set; }//HealthManager sýnýfýna tüm kodlardan kolay eriþim saðlamak için Singleton pattern kullanýlmýþ.

    public event Action OnPlayerDeath;//Oyuncunun caný bittiðinde tetiklenen olay.

    [Header("References")]

    [SerializeField] private PlayerHealthUI _playerHealthUI;//UI bileþeni: oyuncunun canýný gösteren görsel arayüz.

    [Header("Settings")]

    [SerializeField] private int _maxHealth = 3;//Maksimum can deðeri.


    private int _currentHealth;//oyun içindeki güncel can miktarý.



    private void Awake()
    {
        Instance = this;//Singleton nesnesi olarak kendini ayarlýyor.
    }

    private void Start()
    {
        _currentHealth = _maxHealth;//Oyun baþladýðýnda caný maksimum yapar.
    }

    public void Damage (int damageAmount)//Oyuncuya dýþarýdan hasar verilmesini saðlar.
    {
        if (_currentHealth > 0)//Oyuncunun zaten ölü olup olmadýðýný kontrol eder.
        {
            _currentHealth -= damageAmount;
            _playerHealthUI.AnimateDamage();
            //Can azaltýlýr ve UI üzerinde hasar animasyonu oynatýlýr.
            if (_currentHealth <= 0)
            {//Can 0 veya altýna düþtüðünde ölüm olayý tetiklenir.
                OnPlayerDeath?. Invoke();
            }
        }
    }

    public void Heal (int healAmount)//Oyuncuya iyileþtirme uygulanýr.
    {
        if (_currentHealth < _maxHealth)//Zaten tam cana sahipse iþlem yapýlmaz.
        {
            _currentHealth = Mathf.Min(_currentHealth + healAmount, _maxHealth);//Can iyileþtirilir, ama maksimumu aþmaz.


        }
    }
}
