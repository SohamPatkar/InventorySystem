using UnityEngine;
using UnityEngine.UI;

public class ItemView : MonoBehaviour
{
    [SerializeField] private Button itemButton;
    private ItemController itemController;
    public Sprite itemSprite;

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
