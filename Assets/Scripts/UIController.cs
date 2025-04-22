using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Slider alphaSlider;
    public Button redButton, greenButton, blueButton, toggleButton;

    private ControllableObject currentTarget;

    void Start()
    {
        alphaSlider.onValueChanged.AddListener(UpdateAlpha);
        redButton.onClick.AddListener(() => SetColor(Color.red));
        greenButton.onClick.AddListener(() => SetColor(Color.green));
        blueButton.onClick.AddListener(() => SetColor(Color.blue));
        toggleButton.onClick.AddListener(ToggleVisibility);
    }

    public void SetTarget(ControllableObject target)
    {
        currentTarget = target;
        alphaSlider.value = target.GetComponent<Renderer>().material.color.a;
    }

    void UpdateAlpha(float value)
    {
        currentTarget?.SetAlpha(value);
    }

    void SetColor(Color color)
    {
        currentTarget?.SetColor(color);
    }

    void ToggleVisibility()
    {
        currentTarget?.ToggleVisibility();
    }
}
