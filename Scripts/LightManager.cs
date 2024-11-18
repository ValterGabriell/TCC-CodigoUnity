using UnityEngine;

public class LightManager : MonoBehaviour
{
    private Light luz;

    public float minIntensity = 2.0f; // Intensidade m�nima
    public float maxIntensity = 12.0f; // Intensidade m�xima
    public float pulseSpeed = 2.0f; // Velocidade do pulso

    



    void Start()
    {
        if (luz == null)
        {
            luz = gameObject.GetComponent<Light>();
        }
    }

}
