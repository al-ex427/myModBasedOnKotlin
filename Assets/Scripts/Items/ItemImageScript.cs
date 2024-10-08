using KOTLIN.Items;
using UnityEngine;
using UnityEngine.UI;

public class ItemImageScript : MonoBehaviour
{
    private void Update()
    {
        if (this.gs != null)
        {
            Sprite sprite = ItemManager.Instance.itemSlot[ItemManager.Instance.itemSelected].sprite;
            if (sprite == this.blankSprite)
            {
                this.sprite.sprite = this.noItemSprite;
            }
            else
            {
                this.sprite.sprite = sprite;
            }
        }
        else
        {
            this.sprite.sprite = this.noItemSprite;
        }
    }

    public Image sprite;

    [SerializeField]
    private Sprite noItemSprite;

    [SerializeField]
    private Sprite blankSprite;

    public GameControllerScript gs;
}