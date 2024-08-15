using KOTLIN.Items;
using UnityEngine;
using UnityEngine.UI;

public class ItemImageScript : MonoBehaviour
{
    private void Update()
    {
        if (this.gs != null)
        {
            Texture texture = ItemManager.Instance.itemSlot[ItemManager.Instance.itemSelected].texture;
            if (texture == this.blankSprite)
            {
                this.sprite.texture = this.noItemSprite;
            }
            else
            {
                this.sprite.texture = texture;
            }
        }
        else
        {
            this.sprite.texture = this.noItemSprite;
        }
    }

    public RawImage sprite;

    [SerializeField]
    private Texture noItemSprite;

    [SerializeField]
    private Texture blankSprite;

    public GameControllerScript gs;
}