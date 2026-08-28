using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    void Update()
    {
        if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed)
        {
            transform.position += 5f * Time.deltaTime * transform.up;
        }
        if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed)
        {
            transform.position -= 5f * Time.deltaTime * transform.up;
        }
        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
        {
            transform.position -= 5f * Time.deltaTime * transform.right;
        }
        if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
        {
            transform.position += 5f * Time.deltaTime * transform.right;
        }

        if (Keyboard.current.qKey.isPressed || Keyboard.current.leftShiftKey.isPressed)
        {
            Camera.main.orthographicSize += Time.deltaTime * 5f;
        }
        if (Keyboard.current.eKey.isPressed || Keyboard.current.spaceKey.isPressed)
        {
            Camera.main.orthographicSize -= Time.deltaTime * 5f;
        }
    }
}
