using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Sock : MonoBehaviour
{
    public SockIdle SockIdle { get; private set; }
    public SockMove SockMove { get; private set; }
    public SockJump SockJump { get; private set; }
    public SockFall SockFall { get; private set; }

    protected SockState currentState;

    [field: SerializeField] public Rigidbody Rb { get; private set; } = null;
    [field: SerializeReference] public Animator Anim { get; private set; } = null;
    [field: SerializeReference] public AnimationCurve MoveCurve { get; private set; } = AnimationCurve.Linear(0, 0, 1, 1);
    [field: SerializeReference] public float MoveSpeed { get; private set; } = 5f;
    [field: SerializeReference] public float TurnSpeed { get; private set; } = 2f;
    [field: SerializeReference] public AnimationCurve JumpCurve { get; private set; } = AnimationCurve.EaseInOut(0, 0, 1, 1);
    [field: SerializeReference] public float JumpForce { get; private set; } = 5f;
    [field: SerializeReference] public float JumpTime { get; private set; } = 0.5f;
    [field: SerializeReference] public LayerMask ExcludeLayersForJump { get; private set; }
    [field: SerializeReference] public Transform GroundCheckTransform { get; private set; }
    [field: SerializeReference] public LayerMask GroundLayer { get; private set; }
    [field: SerializeReference] public float GroundCheckRadius { get; private set; } = 0.1f;
    [field: SerializeReference] public Transform WallCheckTransform { get; private set; }
    [field: SerializeReference] public LayerMask WallLayer { get; private set; }
    [field: SerializeReference] public float WallCheckRadius { get; private set; } = 0.1f;

    private bool isGrounded = false;
    public bool IsGrounded { get => isGrounded; }

    private bool isOnWall = false;
    public bool IsOnWall { get => isOnWall; }

    private float jumpModifier = 1f;
    public float JumpModifier { get => jumpModifier; }
    public void SetJumpModifier(float modifier)
    {
        jumpModifier = modifier;
    }

    void Awake()
    {
        SockIdle = new SockIdle(this);
        SockMove = new SockMove(this);
        SockJump = new SockJump(this);
        SockFall = new SockFall(this);
    }


    public void ChangeState(SockState newState)
    {
        currentState?.Exit();

        currentState = newState;

        currentState?.Enter();
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(GroundCheckTransform.position, GroundCheckRadius, GroundLayer);

        isOnWall = Physics.CheckSphere(WallCheckTransform.position, WallCheckRadius, WallLayer);


        currentState?.FrameUpdate();

        currentState?.CheckState();
    }

    void FixedUpdate()
    {
        currentState?.PhysicsUpdate();
    }

    public void MoveCharacter(Vector2 direction)
    {
        if (!currentState.CanChangeStateTo(SockMove) || Game.Manager.CurrentState != GameState.Playing)
            return;

        ChangeState(SockMove);
        SockMove.SetDirection(direction);
    }

    public void JumpCharacter()
    {
        if (!currentState.CanChangeStateTo(SockJump) || Game.Manager.CurrentState != GameState.Playing)
            return;

        ChangeState(SockJump);
    }

    private void OnDrawGizmos()
    {
        if (GroundCheckTransform != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(GroundCheckTransform.position, GroundCheckRadius);
        }

        if (WallCheckTransform != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(WallCheckTransform.position, WallCheckRadius);
        }
    }
}
