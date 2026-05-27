using UnityEngine;

public class WebGLFocus : MonoBehaviour
{
    void Start()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        FocusCanvas();
#endif
    }

    [System.Runtime.InteropServices.DllImport("__Internal")]
    private static extern void FocusCanvas();
}