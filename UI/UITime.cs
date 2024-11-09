using UnityEngine;
using UnityEngine.UIElements;

public class UITime : MonoBehaviour
{
    private VisualElement root;
    private Label timeLabel;
    private Label pointsLabel;
    public ModelTime _time;


    private void OnEnable()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        timeLabel = root.Q<Label>("timeLabel");
        pointsLabel = root.Q<Label>("pointsLabel");
    }

    private void FixedUpdate()
    {
        timeLabel.text = _time.timeElapsed;
    }
}
