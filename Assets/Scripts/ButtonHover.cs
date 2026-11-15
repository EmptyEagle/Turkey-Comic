using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonHover : MonoBehaviour
{
    [SerializeField] Sprite defaultSprite;
    [SerializeField] Sprite hoverSprite;
    private Image buttonImage;

    void Start()
    {
        buttonImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == gameObject)
        {
            buttonImage.sprite = hoverSprite;
        }
        else
        {
            buttonImage.sprite = defaultSprite;
        }
    }
}
