using UnityEngine;

public class LevelManager02 : GenericLevel
{
    public bool hasRedKey = false;
    public bool hasBlueKey = false;
    public bool hasWhiteKey = false;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

    }
}
