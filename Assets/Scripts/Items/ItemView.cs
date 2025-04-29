using UnityEngine;
using UnityEngine.UI;

public class ItemView : MonoBehaviour
{
    public Sprite itemSprite;
    public ItemController itemController;
    private Button itemButton;

    void Awake()
    {
        itemButton = GetComponent<Button>();
    }

    void Start()
    {
        itemButton.image.sprite = itemSprite;
        itemButton.onClick.AddListener(OnItemClicked);
    }

    private void OnItemClicked()
    {
        itemController?.OnInteract();
    }

    public void SetItemController(ItemController controller)
    {
        itemController = controller;
    }
}
