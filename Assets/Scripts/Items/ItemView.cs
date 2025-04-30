using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemView : MonoBehaviour
{
    [SerializeField] private Button itemButton;
    [SerializeField] public TextMeshProUGUI itemQuantity;
    [HideInInspector] public Sprite itemSprite;
    private ItemController itemController;

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
