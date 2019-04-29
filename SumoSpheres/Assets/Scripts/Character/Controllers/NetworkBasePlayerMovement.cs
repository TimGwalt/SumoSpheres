using UnityEngine;
using Photon.Pun;

public class NetworkBasePlayerMovement : MonoBehaviourPun, IPunObservable
{
    public float m_MoveSpeed = 2f;
    public float m_RotateSpeed = 1f;
    public float m_JumpSpeed = 2f;
    public int m_Lives;

    public Rigidbody m_PlayerRB;
    public SphereCollider m_PlayerCollider;
    public Transform m_CameraTransform;
    public float m_DistanceToGround;
    public Vector3 m_TargetPosition;
    public Quaternion m_TargetRotation;
    
    // Set player member variables and, if the Photon View is the local player's set the camera to follow the player.
    public void Start()
    {
        m_PlayerRB = GetComponent<Rigidbody>();
        m_PlayerCollider = GetComponent<SphereCollider>();
        m_DistanceToGround = m_PlayerCollider.bounds.extents.y;
        if (photonView.IsMine)
        {
            m_CameraTransform = Camera.main.transform;
            Camera.main.GetComponent<ThirdPersonCamera>().target = photonView.transform;
        }
    }

    public void Update()
    {
        CheckSling();
    }
    public void FixedUpdate()
    {
        if (photonView.IsMine)
        {
            CheckInput();
        }
        else
            SmoothMove();
    }

    public virtual void CheckSling()
    {
        return;
    }
    public void SmoothMove()
    {
        transform.position = Vector3.Lerp(transform.position, m_TargetPosition, 0.25f);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, m_TargetRotation, 500);
    }

    // Gathers input from the user and applies forces to the player rigidbody based on inputs
    public virtual void CheckInput()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 inputDirection = input.normalized;
        if(Input.GetKey(KeyCode.Space) & IsGrounded())
        {
            m_PlayerRB.AddForce(Vector3.up * m_JumpSpeed, ForceMode.Impulse);
        }
        if(inputDirection != Vector2.zero)
        {
            // Add force to the player dependent on input axes and camera direction.
            Vector3 movement = new Vector3(input.x, 0.0f, input.y);
            Vector3 actualMovement = m_CameraTransform.TransformDirection(movement);
            m_PlayerRB.AddForce(actualMovement * m_MoveSpeed);
        }
    }

    // Detects if the player is grounded
    public bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, m_DistanceToGround);
    }

    // Sends position and rotation data over the network
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting == true)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else
        {
            m_TargetPosition = (Vector3) stream.ReceiveNext();
            m_TargetRotation = (Quaternion) stream.ReceiveNext();
        }
    }
}
