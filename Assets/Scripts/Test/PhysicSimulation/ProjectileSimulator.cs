using UnityEngine;
using UnityEngine.Serialization;

public class ProjectileSimulator : MonoBehaviour
{
    public Transform target; // 衝突判定を持つオブジェクトのTransform

    private Rigidbody rb;
    private Vector3 initialPosition;
    private Vector3 initialVelocity;
    private bool isSimulating = false;

    [FormerlySerializedAs("PinballController")] public PinballGenerator _pinballGenerator;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialPosition = transform.position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && !isSimulating)
        {
            isSimulating = true;

            // AddForceで与える力
            Vector3 force = _pinballGenerator.launchVelocity;

            // velocityに力を加える
            rb.velocity += force / rb.mass;

            // Rigidbodyのgravityを有効にする
            rb.isKinematic = false;

            // シミュレーション開始時の初速度を保存
            initialVelocity = rb.velocity;
        }
    }

    private void FixedUpdate()
    {
        if (isSimulating)
        {
            // 現在の位置と速度から、時間をかけて移動する
            rb.MovePosition(transform.position + initialVelocity * Time.fixedDeltaTime);
            initialVelocity += Physics.gravity * Time.fixedDeltaTime;

            // 衝突判定を持つオブジェクトに対して、Raycastを飛ばして衝突位置を求める
            RaycastHit hit;
            if (Physics.Raycast(transform.position, rb.velocity.normalized, out hit, rb.velocity.magnitude * Time.fixedDeltaTime))
            {
                Debug.Log("Hit position: " + hit.point);
                isSimulating = false;
                rb.isKinematic = true;
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                transform.position = initialPosition;
            }
        }
    }
}