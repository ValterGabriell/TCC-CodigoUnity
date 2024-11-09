using UnityEngine;
using UnityEngine.UIElements;

public class AdjustmentPlace : MonoBehaviour
{
    public Transform player; // Referência ao transform do Player
    private VisualElement popupContainer;
    private VisualElement rootVisualElement;
    private VisualElement tipButton; // Referência ao botão de interrogação (dica)

    private void OnEnable()
    {
        // Obtém o documento raiz
        rootVisualElement = GetComponent<UIDocument>().rootVisualElement;
        popupContainer = rootVisualElement.Q<VisualElement>("popupContainer");
        tipButton = rootVisualElement.Q<VisualElement>("tip");
    }

    private void Update()
    {
        if (player != null)
        {
            // Converte a posição do player do mundo para a posição da tela
            Vector3 playerScreenPosition = Camera.main.WorldToScreenPoint(player.position);

            // Ajusta a posição do popup com base na posição da tela
            popupContainer.style.left = playerScreenPosition.x;
            popupContainer.style.top = Screen.height - playerScreenPosition.y;

            // Ajusta a posição do botão de interrogação acima do personagem
            tipButton.style.left = playerScreenPosition.x;
            tipButton.style.top = Screen.height - playerScreenPosition.y + 50; // Ajuste a distância conforme necessário
        }
    }
}
