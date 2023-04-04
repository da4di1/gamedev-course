using Player;
using UnityEngine;

[RequireComponent(typeof(PlayerHandler))]
public class InputReader : MonoBehaviour
{
    private PlayerHandler _playerEntity;

    private float _direction;

    private void Start()
    {
        _playerEntity = GetComponent<PlayerHandler>();
    }

    private void Update()
    {
        _direction = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            _playerEntity.Jump();
        }
    }

    private void FixedUpdate() 
    {
        _playerEntity.MoveHorizontally(_direction);
    }
}
