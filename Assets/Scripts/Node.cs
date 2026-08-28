using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Node : MonoBehaviour
{

    public Color hoverColor;

    private Color startColor;
    private Renderer rend;
    private bool isHovered;

    private Color currentColor;
    private bool initialized;


    void Awake()
    {
        InitializeRendererState();
    }

    void Update()
    {
        if (Camera.main == null || Mouse.current == null)
        {
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        bool isMouseOverNode = Physics.Raycast(ray, out RaycastHit hit) && (hit.transform == transform || hit.transform.IsChildOf(transform));

        if (isMouseOverNode)
        {
            // Mouse Enter
            if (!isHovered)
            {
                rend.material.color = hoverColor;
                isHovered = true;
            }

            // Click
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                NodeTagChanger.NodeTags selectedTag = NodeTagChanger.getNodeSectedType();
                SetTagAndColor(selectedTag);
            }
            return;
        }

        // Mouse Exit
        if (isHovered)
        {
            rend.material.color = currentColor;
            isHovered = false;
        }


    }

    public void SetTagAndColor(NodeTagChanger.NodeTags selectedTag)
    {
        InitializeRendererState();

        tag = selectedTag.ToString();
        switch (selectedTag.id)
        {
            case 1:
                rend.material.color = startColor;
                currentColor = startColor;
                break;
            case 2:
                rend.material.color = Color.green;
                currentColor = Color.green;
                break;
            case 3:
                rend.material.color = Color.red;
                currentColor = Color.red;
                break;
            case 4:
                rend.material.color = Color.brown;
                currentColor = Color.brown;
                break;
            default:
                break;

        }
    }

    private void InitializeRendererState()
    {
        if (initialized)
        {
            return;
        }

        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        currentColor = startColor;
        initialized = true;
    }



}
