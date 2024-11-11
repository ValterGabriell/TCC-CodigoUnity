using UnityEngine;
using UnityEngine.UIElements;

public class PlayerPopup : MonoBehaviour
{
    public Transform player; // Referência ao transform do Player
    private VisualElement popupContainer;
    private VisualElement hintsContainer;
    private Button initialButton;
    private Button tip;
    public GameManager gameManager;

    private VisualElement rootVisualElement;

    private void OnEnable()
    {
        Time.timeScale = 0;
        rootVisualElement = GetComponent<UIDocument>().rootVisualElement;
        popupContainer = rootVisualElement.Q<VisualElement>("popupContainer");
        hintsContainer = rootVisualElement.Q<VisualElement>("hintsContainer");
        initialButton = rootVisualElement.Q<Button>("initialButton");
        tip = rootVisualElement.Q<Button>("tip");

        tip.visible = false;

        tip.clicked += () => {
            popupContainer.visible = true;
        };

        initialButton.clicked += OnInitialButtonClicked;
    }

    private void Update()
    {
        if (gameManager.isLevelCompleted)
        {
            tip.visible = false;
            popupContainer.visible = false;
        }
        else
        {
            tip.visible = false;
            //popupContainer.visible = true;
        }
        if (player != null)
        {
            // Converte a posição do player do mundo para a posição da tela
            Vector3 playerScreenPosition = Camera.main.WorldToScreenPoint(player.position);

            // Ajusta a posição do popup com base na posição da tela
            popupContainer.style.left = playerScreenPosition.x;
            popupContainer.style.top = Screen.height - playerScreenPosition.y;
        }
    }

    private void OnInitialButtonClicked()
    {
        Time.timeScale = 1;
        initialButton.text = "Fechar";
        popupContainer.visible = false;
        tip.visible = true; 
    }
}
