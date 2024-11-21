using UnityEngine;

public class ItemFalling : MonoBehaviour
{
    Rigidbody rb;
    public bool shouldFall = false;
    public float timeToFall = 25f;
    private float timeElapsed = 0f;

    private void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.useGravity = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            if (shouldFall)
            {
                FallDown();
            }
            else
            {
                //while (timeElapsed < timeToFall)
                //{
                //    timeElapsed += Time.deltaTime * 0.1f;
                //    Debug.Log(timeElapsed);
                //}
                //FallDown();
            }
        }
    }

    private void FallDown()
    {
        Destroy(gameObject);
    }
}
