using UnityEngine;

namespace AlwaysEast
{
    public class GearSlots : MonoBehaviour
    {
        public GearSlot[] slots;

        public GearSlot GetEmpty(Item.Type itemType)
        {
            foreach (GearSlot slot in slots)
            {
                if (slot.type == itemType)
                {
                    if (slot.transform.childCount == 0)
                        return slot;
                }
            }
            return null;
        }
    }
}