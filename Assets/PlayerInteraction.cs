using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Cell>())
        {
            other.GetComponent<Cell>().FadeInOutCanvas(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Cell>())
        {
            other.GetComponent<Cell>().FadeInOutCanvas(false);
        }
        
    }
}
