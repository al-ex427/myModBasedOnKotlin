using KOTLIN.Items;

public class PickupScript : KOTLIN.Interactions.Interactable
{
    public override void Interact()
    {
        gameObject.SetActive(false);
        ItemManager.Instance.CollectItem(ItemID);
    }

    [UnityEngine.SerializeField] private int ItemID; 
}