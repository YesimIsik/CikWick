using System;
using UnityEngine;
using UnityEngine.AI;

public class CatController : MonoBehaviour
{
    public event Action OnCatCatched;//Kedi oyuncuya (civciv) ulaþtýðýnda tetiklenecek olay

    [Header("References")]
    [SerializeField] PlayerController _playerController;
    [SerializeField] private Transform _playerTransform;
    //Oyuncunun kontrol ve konum referanslarý. Kedi, oyuncunun nerede olduðunu bilmek için bunlara ihtiyaç duyar.



    [Header("Settings")]
    [SerializeField] private float _defaultSpeed = 5f;//Normal hýzý 



    [SerializeField] private float _chaseSpeed = 7f;//Kovalama hýzý

    [Header("Navigation Settings")]
    [SerializeField] private float _patrolRadius;// maksimum yarýçap.

    [SerializeField] private float _waitTime = 2f;//Belirli bir noktaya ulaþtýðýnda bekleme süresi.

    [SerializeField] private int _maxDestinationAttempts = 10;//Rastgele bir hedef bulmak için maksimum deneme sayýsý.

    [SerializeField] private float _chaseDistanceThreshold = 1.5f;//Takip sýrasýnda oyuncuya çok yaklaþmamasý için mesafe eþiði.

    [SerializeField] private float _chaseDistance = 2f;//Oyuncuya ne kadar yaklaþýrsa "yakalandý" sayýlýr.


    private NavMeshAgent _catAgent;

    private CatStateController _catStateController;


    private float _timer;

    private bool _isWaiting;

    private bool _isChasing;

    private Vector3 _initialPosition;

    //AI için kullanýlan bileþenler.





    private void Awake()
    {
        _catAgent = GetComponent<NavMeshAgent>();
        _catStateController = GetComponent<CatStateController>();//NavMeshAgent ve CatStateController bileþenleri alýnýr.
    }


    private void Start()
    {
        _initialPosition = transform.position;
        SetRandomDestination();//Baþlangýç pozisyonu saklanýr ve rastgele bir hedef seçilir.
    }

    private void Update()
    {
        if (_playerController.CanCatChase())//Eðer kedi oyuncuyu takip edebilecek durumdaysa (CanCatChase()), oyuncuyu kovalar.
        {
            SetChaseMovement();
        }
        else
        {
            SetPatrolMovement();//Aksi halde gezme moduna geçer.
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
        //Kedinin hedefi, oyuncunun biraz gerisi olacak þekilde ayarlanýr.
        //Hýz arttýrýlýr, animasyon durumu Running yapýlýr.



        if (Vector3.Distance(transform.position,_playerTransform.position)<=_chaseDistance&& _isChasing)//Eðer oyuncuya yeterince yakýnsa:
        {
            //CATCHED THE CHÝCK
            OnCatCatched?.Invoke();//OnCatCatched olayý tetiklenir.
            _catStateController.ChangeState(CatState.Attacking);//Kedi "saldýrýyor" durumuna geçer.
            _isChasing = false;
        }
    }


    private void SetPatrolMovement()
    {
        _catAgent.speed = _defaultSpeed;//Kedi normal hýzýna döner.


        if (!_catAgent.pathPending && _catAgent.remainingDistance <= _catAgent.stoppingDistance)
        {
            if (!_isWaiting)
            {
                _isWaiting = true;
                _timer = _waitTime;
                _catStateController.ChangeState(CatState.Idle);
            }
        }//Hedefe ulaþýldýysa, bekleme süreci baþlar.

        if (_isWaiting)
        {
            _timer -= Time.deltaTime;
            if(_timer <= 0f)
            {
                _isWaiting = false;
                SetRandomDestination();
                _catStateController.ChangeState(CatState.Walking);
            }
        }//Bekleme süresi bittiðinde yeni bir rastgele hedef seçilir.
    }

    private void SetRandomDestination()
    {
        int attempts = 0;
        bool destinationSet = false;

        while(attempts<_maxDestinationAttempts&& !destinationSet)//Belirli sayýda denemeyle rastgele bir hedef pozisyon bulunmaya çalýþýlýr.
        {
            Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * _patrolRadius;
            randomDirection += _initialPosition;//Baþlangýç pozisyonuna yakýn rastgele bir yön belirlenir.

              if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, _patrolRadius, NavMesh.AllAreas))
            {//Belirlenen pozisyon NavMesh üzerinde geçerli mi kontrol edilir.
                Vector3 finalPosition = hit.position;

                if (IsPositionBlocked(finalPosition))//Eðer pozisyon engelli deðilse, hedef olarak ayarlanýr.
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

        if (!destinationSet)//Geçerli bir hedef bulunamazsa, 2 katý sürede beklenir.
        {
            Debug.LogWarning("Failed to find a valid destination");
            _isWaiting = true;
            _timer = _waitTime * 2;
        }

    }

    private bool IsPositionBlocked(Vector3 position)
    {
        if(NavMesh.Raycast(transform.position,position,out NavMeshHit hit, NavMesh.AllAreas))//Kediden hedefe doðru olan çizgide engel varsa, pozisyon geçersiz kabul edilir.
        {
            return true;
        }
        return false;
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 pos = (_initialPosition != Vector3.zero) ? _initialPosition : transform.position;//Editörde, gezme alaný görsel olarak çizilir (yeþil daire).

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(pos, _patrolRadius);
    }

}
