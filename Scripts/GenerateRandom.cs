using UnityEngine;
using UnityEngine.UI;

public class GenerateRandom : MonoBehaviour, IInteractable
{
    Outline outline;
    public bool isEnabled;

    private void Start()
    {
        outline = GetComponent<Outline>();
        DisableOutline();
    }
    public void DisableOutline()
    {
        isEnabled = false;
        outline.enabled = false;
    }

    public void EnableOutline()
    {
        isEnabled = true;
        outline.enabled = true;
    }

    public void Interact(GameObject gameObject)
    {
        Debug.Log("Numero aleatorio: " + Random.RandomRange(1,100));
    }

    public string TextInteraction(string text)
    {
        return text; 
    }
}
