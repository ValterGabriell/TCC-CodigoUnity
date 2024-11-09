using UnityEngine;
using UnityEngine.UIElements;

public class AdjustmentPlace : MonoBehaviour
{
    public Transform player; // Refer�ncia ao transform do Player
    private VisualElement popupContainer;
    private VisualElement rootVisualElement;
    private VisualElement tipButton; // Refer�ncia ao bot�o de interroga��o (dica)

    private void OnEnable()
    {
        // Obt�m o documento raiz
        rootVisualElement = GetComponent<UIDocument>().rootVisualElement;
        popupContainer = rootVisualElement.Q<VisualElement>("popupContainer");
        tipButton = rootVisualElement.Q<VisualElement>("tip");
    }

    private void Update()
    {
        if (player != null)
        {
            // Converte a posi��o do player do mundo para a posi��o da tela
            Vector3 playerScreenPosition = Camera.main.WorldToScreenPoint(player.position);

            // Ajusta a posi��o do popup com base na posi��o da tela
            popupContainer.style.left = playerScreenPosition.x;
            popupContainer.style.top = Screen.height - playerScreenPosition.y;

            // Ajusta a posi��o do bot�o de interroga��o acima do personagem
            tipButton.style.left = playerScreenPosition.x;
            tipButton.style.top = Screen.height - playerScreenPosition.y + 50; // Ajuste a dist�ncia conforme necess�rio
        }
    }
}
