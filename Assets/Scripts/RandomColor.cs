using Unity.Netcode;
using UnityEngine;

public class RandomColor : NetworkBehaviour
{
    private readonly NetworkVariable<Color> _netColor = new();

    private void Awake()
    {
        _netColor.OnValueChanged += OnColorChanged;
    }

    public override void OnNetworkSpawn()
    {
        if ( IsServer )
        {
            _netColor.Value = Random.ColorHSV( 0f, 1f, 1f, 1f, 0.5f, 1f );
        }

        ApplyColor( _netColor.Value );
    }

    public override void OnNetworkDespawn()
    {
        _netColor.OnValueChanged -= OnColorChanged;
    }

    private void OnColorChanged( Color previous, Color current )
    {
        ApplyColor( current );
    }

    private void ApplyColor( Color color )
    {
        var r = GetComponentInChildren<Renderer>();
        if ( r != null )
        {
            r.material.color = color;
        }
    }
}