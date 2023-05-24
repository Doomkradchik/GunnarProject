using UnityEngine;

[RequireComponent(typeof(Animator))]
public class MovementController : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody _rigidbody;

    private float _angleVelocity;
    private readonly float _smoothTime = 0.1f;
    private readonly string _runMotionKey = "moving";

    public bool Freeze { get; set; } = false;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        var shiftPressed = Input.GetKey(KeyCode.LeftShift);
        Move(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")), shiftPressed);
    }

    protected void Move(Vector2 input, bool running = false)
    {
        if (Freeze)
        {
            //SoundAudioManager.Instance
            // .StopSound(SoundAudioManager.AudioData.Kind.Movement);

            return;
        }

        var camera = Camera.main.transform;
        var _direction = camera.right * input.x +
            camera.forward * input.y;

        var plane = new Vector3(_direction.x, 0f, _direction.z).normalized;

        if (plane == Vector3.zero)
        {
            _animator.SetInteger(_runMotionKey, 0);

            //SoundAudioManager.Instance
            //  .StopSound(SoundAudioManager.AudioData.Kind.Movement);
            return;
        }

        _animator.SetInteger(_runMotionKey, running ? 2 : 1);

        var targetAngle = Mathf.Atan2(plane.x, plane.z) * Mathf.Rad2Deg;
        var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y,
            targetAngle, ref _angleVelocity, _smoothTime);

        //var offset = plane * unitsPerSecond * Time.fixedDeltaTime;

        //_rigidbody.MovePosition(_rigidbody.position + offset);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
    }

}
