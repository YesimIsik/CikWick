using UnityEngine;

public class StateController : MonoBehaviour
{
    private PlayerState _currentPlayerState = PlayerState.Idle;

    private void Start()
    {
        ChangesState(PlayerState.Idle);
    }

    public void ChangesState(PlayerState newPlayerState)
    {
        if(_currentPlayerState== newPlayerState)
        {
            return;
        }

        _currentPlayerState = newPlayerState;
    }
    public PlayerState GetCurrentState()
    {
        return _currentPlayerState;
    }
}
