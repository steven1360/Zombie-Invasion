using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    public string CollectedItemInfo { get; private set; }

    void OnTriggerEnter2D(Collider2D col)
    {
        SupplyBox box = col.GetComponent<SupplyBox>();
        if (box != null)
        {
            CollectedItemInfo = box.SupplyBoxItem.UseItem();
            Destroy(box.gameObject);
        }
    }
}
