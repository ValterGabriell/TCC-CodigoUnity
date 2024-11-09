using UnityEngine;

public class EndTrigger : MonoBehaviour
{
    public GameManager Manager;
    private void OnTriggerEnter(Collider other)
    {
        Manager.isLevelCompleted = true;
    }
}
