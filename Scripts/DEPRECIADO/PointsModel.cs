using UnityEngine;

public class PointsModel : MonoBehaviour
{
    private int _points = 0;
    public GameManager gameManager;
    
    public void increasePoint(int value)
    {
        _points += value;
    }

    public void decreasePoint(int value) {
        _points -= value;
    }

    public int GetCurrentPoints()
    {
        return _points;
    }

}
