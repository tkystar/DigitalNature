using UnityEngine;

public class ShooterTest : MonoBehaviour
{
    public GameObject leftBullet;
    public GameObject rightBullet;

    private GameObject currentBullet;
    private Vector3 leftStartPosition;
    private Vector3 rightStartPosition;

    private bool canShoot = false;

    void Start()
    {
        leftStartPosition = leftBullet.transform.position;
        rightStartPosition = rightBullet.transform.position;
        currentBullet = leftBullet;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canShoot)
        {
            Shoot();
        }
    }

    void OnMouseDown()
    {
        if (canShoot)
        {
            SwapBullets();
        }
    }

    void Shoot()
    {
        //発射された弾と同じ弾が発射前の位置に生成される
        Instantiate(currentBullet, currentBullet.transform.position, Quaternion.identity);

        //選択した弾が中央に来るように2つの玉がスライドする
        if (currentBullet == leftBullet)
        {
            currentBullet = rightBullet;
            rightBullet.transform.position = transform.position;
        }
        else
        {
            currentBullet = leftBullet;
            leftBullet.transform.position = transform.position;
        }

        canShoot = false;
    }

    void SwapBullets()
    {
        //選択した弾が中央に来るように2つの玉がスライドする
        if (currentBullet == leftBullet)
        {
            currentBullet = rightBullet;
            rightBullet.transform.position = transform.position;
        }
        else
        {
            currentBullet = leftBullet;
            leftBullet.transform.position = transform.position;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == currentBullet)
        {
            canShoot = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == currentBullet)
        {
            canShoot = false;
        }
    }
}