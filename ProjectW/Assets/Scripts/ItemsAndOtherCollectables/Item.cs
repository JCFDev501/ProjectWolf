using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

    // Create a variable of the enum type
    [SerializeField] private Enums.ItemType itemType;

    // Start is called before the first frame update
    void Start()
    {
        //itemType = Enums.ItemType.None;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPlayerCollect()
    {
        if (itemType == Enums.ItemType.None) { return; }

        if (itemType == Enums.ItemType.Collectable)
        {
            Destroy(gameObject);
            Debug.Log(gameObject.name + "Was Destroyed");
        }
        else if (itemType == Enums.ItemType.Key)
        {
            Destroy(gameObject);
            Debug.Log(gameObject.name + "Was Destroyed");
        }
        else if (itemType == Enums.ItemType.Tool)
        {
            Destroy(gameObject);
            Debug.Log(gameObject.name + "Was Destroyed");
        }
    }
}
