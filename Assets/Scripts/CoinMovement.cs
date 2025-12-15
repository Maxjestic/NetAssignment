using UnityEngine;

public class CoinMovement : MonoBehaviour
{
    [SerializeField]
    private float amplitude;

    [SerializeField]
    private float frequency;

    [SerializeField]
    private float rotationSpeed;

    private float _x;
    private float _initialY;

    private void Start()
    {
        _initialY = transform.position.y;
    }

    void Update()
    {
        transform.Rotate( Vector3.up, rotationSpeed );
        transform.position = new Vector3( transform.position.x, _initialY + CalculateY(), transform.position.z );
    }

    private float CalculateY()
    {
        _x += Time.deltaTime;
        return amplitude * Mathf.Sin( _x * frequency );
    }
}