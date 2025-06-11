using System;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public static HealthManager Instance { get; private set; }//HealthManager s�n�f�na t�m kodlardan kolay eri�im sa�lamak i�in Singleton pattern kullan�lm��.

    public event Action OnPlayerDeath;//Oyuncunun can� bitti�inde tetiklenen olay.

    [Header("References")]

    [SerializeField] private PlayerHealthUI _playerHealthUI;//UI bile�eni: oyuncunun can�n� g�steren g�rsel aray�z.

    [Header("Settings")]

    [SerializeField] private int _maxHealth = 3;//Maksimum can de�eri.


    private int _currentHealth;//oyun i�indeki g�ncel can miktar�.



    private void Awake()
    {
        Instance = this;//Singleton nesnesi olarak kendini ayarl�yor.
    }

    private void Start()
    {
        _currentHealth = _maxHealth;//Oyun ba�lad���nda can� maksimum yapar.
    }

    public void Damage (int damageAmount)//Oyuncuya d��ar�dan hasar verilmesini sa�lar.
    {
        if (_currentHealth > 0)//Oyuncunun zaten �l� olup olmad���n� kontrol eder.
        {
            _currentHealth -= damageAmount;
            _playerHealthUI.AnimateDamage();
            //Can azalt�l�r ve UI �zerinde hasar animasyonu oynat�l�r.
            if (_currentHealth <= 0)
            {//Can 0 veya alt�na d��t���nde �l�m olay� tetiklenir.
                OnPlayerDeath?. Invoke();
            }
        }
    }

    public void Heal (int healAmount)//Oyuncuya iyile�tirme uygulan�r.
    {
        if (_currentHealth < _maxHealth)//Zaten tam cana sahipse i�lem yap�lmaz.
        {
            _currentHealth = Mathf.Min(_currentHealth + healAmount, _maxHealth);//Can iyile�tirilir, ama maksimumu a�maz.


        }
    }
}
