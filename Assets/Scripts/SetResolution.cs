using UnityEngine;

public class SetResolution : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Screen.SetResolution(160, 144, true);
    }
}
