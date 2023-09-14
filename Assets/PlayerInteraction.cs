using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<GridCell>())
        {
            other.GetComponent<GridCell>().CanClickable(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<GridCell>())
        {
            other.GetComponent<GridCell>().CanClickable(false);
        }
        
    }
}
