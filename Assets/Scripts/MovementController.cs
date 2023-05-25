using UnityEngine;

[RequireComponent(typeof(Animator))]
public class MovementController : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody _rigidbody;

    private float _angleVelocity;
    private readonly float _smoothTime = 0.1f;
    private string _runMotionKey;

    public bool Freeze { get; set; } = false;


    public void Init(Rigidbody rigidbody, Animator animator, params string[] movementMotionKeys)
    {
        _rigidbody = rigidbody;
        _animator = animator;

        if (movementMotionKeys.Length != 1)
            throw new System.InvalidOperationException();
        _runMotionKey = movementMotionKeys[0];
    }

    public enum State : int
    {
        None,
        Sneaking,
        Walking,
        Running
    }

    public void Move(Vector2 input, State state)
    {
        if (Freeze)
        {
            state = State.None;
            _animator.SetInteger(_runMotionKey, 0);
            return;
        }

        var camera = Camera.main.transform;
        var _direction = camera.right * input.x +
            camera.forward * input.y;

        var plane = new Vector3(_direction.x, 0f, _direction.z).normalized;

        if (plane == Vector3.zero)
        {
            _animator.SetInteger(_runMotionKey, (int)State.None);
            return;
        }

        _animator.SetInteger(_runMotionKey, (int) state);


        var targetAngle = Mathf.Atan2(plane.x, plane.z) * Mathf.Rad2Deg;
        var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y,
            targetAngle, ref _angleVelocity, _smoothTime);

        //var offset = plane * unitsPerSecond * Time.fixedDeltaTime;

        //_rigidbody.MovePosition(_rigidbody.position + offset);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
    }
}
