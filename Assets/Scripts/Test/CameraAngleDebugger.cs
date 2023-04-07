

using UnityEngine;

public class CameraAngleDebugger : MonoBehaviour
{
    public float rotationSpeed = 10f;

    void Update()
    {
        // 左右のキーでカメラを左右に回転させる
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(Vector3.up * -rotationSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        }

        // 上下のキーでカメラを上下に回転させる
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Rotate(Vector3.left * -rotationSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Rotate(Vector3.left * rotationSpeed * Time.deltaTime);
        }
    }
}
