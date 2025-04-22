using UnityEngine;

public class ControllableObject : MonoBehaviour
{
    private Renderer objRenderer;
    private Color currentColor;

    void Awake()
    {
        objRenderer = GetComponent<Renderer>();
        currentColor = objRenderer.material.color;
    }

    public void SetAlpha(float alpha)
    {
        Color color = objRenderer.material.color;
        color.a = alpha;
        objRenderer.material.color = color;

        objRenderer.material.SetFloat("_Mode", 2); // Для прозрачности
        objRenderer.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        objRenderer.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        objRenderer.material.SetInt("_ZWrite", 0);
        objRenderer.material.DisableKeyword("_ALPHATEST_ON");
        objRenderer.material.EnableKeyword("_ALPHABLEND_ON");
        objRenderer.material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        objRenderer.material.renderQueue = 3000;
    }

    public void SetColor(Color color)
    {
        currentColor = color;
        color.a = objRenderer.material.color.a; // сохраняем прозрачность
        objRenderer.material.color = color;
    }

    public void ToggleVisibility()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
