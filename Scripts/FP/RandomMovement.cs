using System.Collections;
using UnityEngine;

public class RandomPlataformMovement : MonoBehaviour
{

    public LevelManager01 levelManager;

    public float moveSpeed = 2f; 
    private Vector3 targetPosition;  
    private float constZ; 

    private Vector3 initialPosition;

    private void Start()
    {
        constZ = transform.localPosition.z;
        initialPosition = transform.localPosition;
        SetRandomTargetPosition();
    }
    void Update()
    {
        if (levelManager.canNormalizePlataforms)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, initialPosition, moveSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.localPosition, initialPosition) < 0.1f)
            {
                SetInitialPosition();
            }
        }
        else
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.localPosition, targetPosition) < 0.1f)
            {
                SetRandomTargetPosition();
            }
        }
    }

    void SetRandomTargetPosition()
    {
        float randomX = Random.Range(-4f, 8f);
        float randomY = Random.Range(0f, 1.5f);
        targetPosition = new Vector3(randomX, randomY, constZ);
    }

    void SetInitialPosition()
    {
        transform.localPosition = initialPosition;
    }
}
