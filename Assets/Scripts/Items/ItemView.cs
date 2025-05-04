using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemView : MonoBehaviour
{
    [SerializeField] private Button itemButton;
    [SerializeField] public TextMeshProUGUI itemQuantity;
    [HideInInspector] public Sprite itemSprite;

    private ItemModel itemModel;

    void OnEnable()
    {
        itemButton.onClick.AddListener(OnItemButtonClick);
    }

    public void SetImage(ItemModel item)
    {
        itemModel = item;
        itemButton.image.sprite = item.icon;
    }

    public void SetQuantity(ItemModel item)
    {
        itemQuantity.text = item.quantity.ToString();
    }

    private void OnItemButtonClick()
    {
        EventService.Instance.OpenBuyPanel.InvokeEvent(itemModel);
    }

    public void SetItemController()
    {

    }
}
