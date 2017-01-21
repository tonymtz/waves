using UnityEngine;

public class Trash : MonoBehaviour
{
    private bool isSelected;

    void Start()
    {

    }

    void Update()
    {

    }

    public void ToggleShader() {
        if (isSelected) {
            //renderer.material.shader = Shader.Find("Self-Illumin/Outlined Diffuse");
        }
    }
}
