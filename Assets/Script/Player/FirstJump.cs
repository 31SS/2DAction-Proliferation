using UnityEngine;

public class FirstJump : MonoBehaviour
{
    private PlayerMover _playerMover;
    private UnityChan2DController _unityChan2DController;

    private void Awake()
    {
        _playerMover = GetComponent<PlayerMover>();
        _unityChan2DController = GetComponent<UnityChan2DController>();
    }
    void Start()
    {
        if (Input.GetButton("Jump"))
        {
            _playerMover.Jumpping(_unityChan2DController.M_Animator);
        }
    }
}
