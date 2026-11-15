using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonHover : MonoBehaviour
{

    private Image buttonImage;

    void Start()
    {
        buttonImage = GetComponent<Image>();
    }
    // Update is called once per frame
    void Update()
    {
    }
}
