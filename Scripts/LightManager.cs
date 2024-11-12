using UnityEngine;

public class LightManager : MonoBehaviour
{
    private Light luz;

    public float minIntensity = 2.0f; // Intensidade mínima
    public float maxIntensity = 12.0f; // Intensidade máxima
    public float pulseSpeed = 2.0f; // Velocidade do pulso

    public Level02 level02;

    void Start()
    {
        if (luz == null)
        {
            luz = gameObject.GetComponent<Light>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (luz != null)
        {
            if (!level02.hasTheKey)
            {
                luz.intensity = Mathf.Lerp(minIntensity, maxIntensity, (Mathf.Sin(Time.time * pulseSpeed) + 1) / 2);
            }
            
        }
    }
}
