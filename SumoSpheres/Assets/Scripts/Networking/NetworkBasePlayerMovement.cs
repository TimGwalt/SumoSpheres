using UnityEngine;
using Photon.Pun;

public class NetworkBasePlayerMovement : MonoBehaviourPun, IPunObservable
{
    public float m_MoveSpeed = 2f;
    public float m_RotateSpeed = 1f;
    public float m_JumpSpeed = 2f;
    public int m_Lives;

    private Rigidbody m_PlayerRB;
    private SphereCollider m_PlayerCollider;
    private Transform m_CameraTransform;
    private float m_DistanceToGround;
    private Vector3 m_TargetPosition;
    private Quaternion m_TargetRotation;
    
    private void Start()
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

    private void FixedUpdate()
    {
        if (photonView.IsMine)
        {
            CheckInput();
        }
        else
            SmoothMove();
    }

    private void SmoothMove()
    {
        transform.position = Vector3.Lerp(transform.position, m_TargetPosition, 0.25f);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, m_TargetRotation, 500);
    }

    private void CheckInput()
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

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, m_DistanceToGround);
    }

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
