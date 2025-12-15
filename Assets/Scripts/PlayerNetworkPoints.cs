using Unity.Netcode;
using UnityEngine.Events;

public class PlayerNetworkPoints : NetworkBehaviour
{
    private readonly NetworkVariable<int> _pointsVar = new();
    public UnityAction<int> onPointsChanged;

    public int Points => _pointsVar.Value;

    public void AddPoint()
    {
        if ( IsServer )
        {
            _pointsVar.Value += 1;
        }
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        if ( IsOwner )
        {
            if ( PointsUI.Instance != null )
            {
                PointsUI.Instance.SetTargetPlayer( this );
            }
        }

        _pointsVar.OnValueChanged += OnPointsValueChanged;
    }

    public override void OnNetworkDespawn()
    {
        base.OnNetworkDespawn();

        _pointsVar.OnValueChanged -= OnPointsValueChanged;
    }

    private void OnPointsValueChanged( int previousValue, int newValue )
    {
        onPointsChanged?.Invoke( newValue );
    }
}