using UnityEngine;

public class DeathArea : MonoBehaviour
{
    public LevelManager02 levelManager;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            levelManager.RestartLevel();
        }
    }
}
