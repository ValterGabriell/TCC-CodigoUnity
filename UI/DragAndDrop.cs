using UnityEngine;
using UnityEngine.UIElements;

public class DragAndDropController : MonoBehaviour
{
    private VisualElement root;
    private VisualElement draggedItem;
    private Vector2 offset;

    private void OnEnable()
    {
        root = GetComponent<UIDocument>().rootVisualElement;

        // Configura drag-and-drop em todos os botões das classes específicas
        var buttons = root.Query<VisualElement>(className: "test-button").ToList();

        foreach (var button in buttons)
        {
            button.RegisterCallback<MouseDownEvent>(OnMouseDown);
            button.RegisterCallback<MouseMoveEvent>(OnMouseMove);
            button.RegisterCallback<MouseUpEvent>(OnMouseUp);
        }
    }

    private void OnMouseDown(MouseDownEvent evt)
    {
        Debug.Log("AQUI");
        draggedItem = evt.target as VisualElement;
        offset = evt.localMousePosition;
        draggedItem.CaptureMouse();
    }

    private void OnMouseMove(MouseMoveEvent evt)
    {
        if (draggedItem == null || !draggedItem.HasMouseCapture())
            return;

        Vector2 newPosition = evt.mousePosition - offset;
        draggedItem.style.left = newPosition.x;
        draggedItem.style.top = newPosition.y;
    }

    private void OnMouseUp(MouseUpEvent evt)
    {
        if (draggedItem == null)
            return;

        draggedItem.ReleaseMouse();
        draggedItem = null;
    }
}
