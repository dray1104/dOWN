using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Slot : MonoBehaviour
{
    public ItemSO holdItem;
    public float lastUsedTime = -999f;

    [Header("UI")]
    public Image cooldownOverlay;
    public Image iconImage;
    public TextMeshProUGUI amountTxt;

    private int itemAmount;
    private string defaultText;

    private void Awake()
    {
        // Safer fallback if not assigned in inspector
        if (iconImage == null)
            iconImage = transform.GetChild(0).GetComponent<Image>();

        if (amountTxt == null)
            amountTxt = transform.GetChild(1).GetComponent<TextMeshProUGUI>();

        defaultText = amountTxt.text;
    }

    // 🔥 Call this when using an item
    public void TriggerCooldown()
    {
        if (holdItem == null) return;

        lastUsedTime = Time.time;
    }

    public void SetItem(ItemSO item, int amount)
    {
        holdItem = item;
        itemAmount = amount;
        UpdateSlot();
    }

    public void UpdateSlot()
    {
        if (holdItem != null)
        {
            iconImage.enabled = true;
            iconImage.sprite = holdItem.icon;

            if (holdItem.itemType == ItemType.Consumable)
                amountTxt.text = itemAmount.ToString();
            else
                amountTxt.text = "";
        }
        else
        {
            iconImage.enabled = false;
            amountTxt.text = defaultText;
        }
    }

    public void UpdateCooldown()
    {
        if (holdItem == null || holdItem.cooldown <= 0f)
        {
            cooldownOverlay.fillAmount = 0f;
            return;
        }

        float timeLeft = (lastUsedTime + holdItem.cooldown) - Time.time;

        if (timeLeft > 0)
        {
            cooldownOverlay.fillAmount = timeLeft / holdItem.cooldown;
        }
        else
        {
            cooldownOverlay.fillAmount = 0f;
        }
    }

    public bool IsOnCooldown()
    {
        if (holdItem == null) return false;
        return Time.time < lastUsedTime + holdItem.cooldown;
    }

    public int AddAmount(int amountToAdd)
    {
        itemAmount += amountToAdd;
        UpdateSlot();
        return itemAmount;
    }

    public int RemoveAmount(int amountToRemove)
    {
        itemAmount -= amountToRemove;

        if (itemAmount <= 0)
            ClearSlot();
        else
            UpdateSlot();

        return itemAmount;
    }

    public void ClearSlot()
    {
        holdItem = null;
        itemAmount = 0;
        UpdateSlot();
    }

    public bool HasItem()
    {
        return holdItem != null;
    }

    private void Update()
    {
        // Only update if actually needed
        if (holdItem != null && holdItem.cooldown > 0f)
        {
            UpdateCooldown();
        }
    }
}