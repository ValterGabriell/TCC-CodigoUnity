using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    public Transform currentPlayer;
    public Vector3 offset = new(0, 0, 0);

    void Start()
    {
        transform.rotation = Quaternion.Euler(30, -45, 0);
    }
    void LateUpdate()
    {
        transform.position = currentPlayer.localPosition + offset;
    }
}
