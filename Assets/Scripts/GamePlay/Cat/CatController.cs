using System;
using UnityEngine;
using UnityEngine.AI;

public class CatController : MonoBehaviour
{
    public event Action OnCatCatched;//Kedi oyuncuya (civciv) ula�t���nda tetiklenecek olay

    [Header("References")]
    [SerializeField] PlayerController _playerController;
    [SerializeField] private Transform _playerTransform;
    //Oyuncunun kontrol ve konum referanslar�. Kedi, oyuncunun nerede oldu�unu bilmek i�in bunlara ihtiya� duyar.



    [Header("Settings")]
    [SerializeField] private float _defaultSpeed = 5f;//Normal h�z� 



    [SerializeField] private float _chaseSpeed = 7f;//Kovalama h�z�

    [Header("Navigation Settings")]
    [SerializeField] private float _patrolRadius;// maksimum yar��ap.

    [SerializeField] private float _waitTime = 2f;//Belirli bir noktaya ula�t���nda bekleme s�resi.

    [SerializeField] private int _maxDestinationAttempts = 10;//Rastgele bir hedef bulmak i�in maksimum deneme say�s�.

    [SerializeField] private float _chaseDistanceThreshold = 1.5f;//Takip s�ras�nda oyuncuya �ok yakla�mamas� i�in mesafe e�i�i.

    [SerializeField] private float _chaseDistance = 2f;//Oyuncuya ne kadar yakla��rsa "yakaland�" say�l�r.


    private NavMeshAgent _catAgent;

    private CatStateController _catStateController;


    private float _timer;

    private bool _isWaiting;

    private bool _isChasing;

    private Vector3 _initialPosition;

    //AI i�in kullan�lan bile�enler.





    private void Awake()
    {
        _catAgent = GetComponent<NavMeshAgent>();
        _catStateController = GetComponent<CatStateController>();//NavMeshAgent ve CatStateController bile�enleri al�n�r.
    }


    private void Start()
    {
        _initialPosition = transform.position;
        SetRandomDestination();//Ba�lang�� pozisyonu saklan�r ve rastgele bir hedef se�ilir.
    }

    private void Update()
    {
        if (_playerController.CanCatChase())//E�er kedi oyuncuyu takip edebilecek durumdaysa (CanCatChase()), oyuncuyu kovalar.
        {
            SetChaseMovement();
        }
        else
        {
            SetPatrolMovement();//Aksi halde gezme moduna ge�er.
        }

          
    }


    private void SetChaseMovement()
    {
        _isChasing = true;
        Vector3 directiontoPlayer = (_playerTransform.position - transform.position).normalized;
        Vector3 offsetPosition = _playerTransform.position - directiontoPlayer * _chaseDistanceThreshold;
        _catAgent.SetDestination(offsetPosition);
        _catAgent.speed = _chaseSpeed;
        _catStateController.ChangeState(CatState.Running);
        //Kedinin hedefi, oyuncunun biraz gerisi olacak �ekilde ayarlan�r.
        //H�z artt�r�l�r, animasyon durumu Running yap�l�r.



        if (Vector3.Distance(transform.position,_playerTransform.position)<=_chaseDistance&& _isChasing)//E�er oyuncuya yeterince yak�nsa:
        {
            //CATCHED THE CH�CK
            OnCatCatched?.Invoke();//OnCatCatched olay� tetiklenir.
            _catStateController.ChangeState(CatState.Attacking);//Kedi "sald�r�yor" durumuna ge�er.
            _isChasing = false;
        }
    }


    private void SetPatrolMovement()
    {
        _catAgent.speed = _defaultSpeed;//Kedi normal h�z�na d�ner.


        if (!_catAgent.pathPending && _catAgent.remainingDistance <= _catAgent.stoppingDistance)
        {
            if (!_isWaiting)
            {
                _isWaiting = true;
                _timer = _waitTime;
                _catStateController.ChangeState(CatState.Idle);
            }
        }//Hedefe ula��ld�ysa, bekleme s�reci ba�lar.

        if (_isWaiting)
        {
            _timer -= Time.deltaTime;
            if(_timer <= 0f)
            {
                _isWaiting = false;
                SetRandomDestination();
                _catStateController.ChangeState(CatState.Walking);
            }
        }//Bekleme s�resi bitti�inde yeni bir rastgele hedef se�ilir.
    }

    private void SetRandomDestination()
    {
        int attempts = 0;
        bool destinationSet = false;

        while(attempts<_maxDestinationAttempts&& !destinationSet)//Belirli say�da denemeyle rastgele bir hedef pozisyon bulunmaya �al���l�r.
        {
            Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * _patrolRadius;
            randomDirection += _initialPosition;//Ba�lang�� pozisyonuna yak�n rastgele bir y�n belirlenir.

              if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, _patrolRadius, NavMesh.AllAreas))
            {//Belirlenen pozisyon NavMesh �zerinde ge�erli mi kontrol edilir.
                Vector3 finalPosition = hit.position;

                if (IsPositionBlocked(finalPosition))//E�er pozisyon engelli de�ilse, hedef olarak ayarlan�r.
                {
                    _catAgent.SetDestination(finalPosition);
                    destinationSet = true;
                }
                else
                {
                    attempts++;
                }
            }
            else
            {
                attempts++;
            }


        }

        if (!destinationSet)//Ge�erli bir hedef bulunamazsa, 2 kat� s�rede beklenir.
        {
            Debug.LogWarning("Failed to find a valid destination");
            _isWaiting = true;
            _timer = _waitTime * 2;
        }

    }

    private bool IsPositionBlocked(Vector3 position)
    {
        if(NavMesh.Raycast(transform.position,position,out NavMeshHit hit, NavMesh.AllAreas))//Kediden hedefe do�ru olan �izgide engel varsa, pozisyon ge�ersiz kabul edilir.
        {
            return true;
        }
        return false;
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 pos = (_initialPosition != Vector3.zero) ? _initialPosition : transform.position;//Edit�rde, gezme alan� g�rsel olarak �izilir (ye�il daire).

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(pos, _patrolRadius);
    }

}
