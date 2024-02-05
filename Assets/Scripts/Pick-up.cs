using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PickUp : MonoBehaviour
{
    private bool isPickedUp = false;
    private Transform pickedObject;
    public string message;
    private Vector3 offset;

    private void OnMouseEnter()
    {
        UpdateTooltipMessage();
    }

    private void OnMouseExit()
    {
        TooltipManager._instance.HideToolTip();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isPickedUp)
            {
                PickUpObject();
            }
            else
            {
                DropObject();
            }

            UpdateTooltipMessage();
        }

        if (isPickedUp)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pickedObject.position = new Vector3(mousePosition.x + offset.x, mousePosition.y + offset.y, pickedObject.position.z);
        }
    }

    private void UpdateTooltipMessage()
    {
        if (isPickedUp)
        {
            TooltipManager._instance.SetAndShowToolTip("Press SPACE to drop");
        }
        else
        {
            TooltipManager._instance.SetAndShowToolTip("Press SPACE to pick-up");
        }
    }

    public void PickUpObject()
    {
        //Raycast used for detecting objects in the same line of the cursor
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("PickableObject"))
            {
                pickedObject = hit.collider.transform;
                isPickedUp = true;
                offset = pickedObject.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }
    }

    public void DropObject()
    {
        isPickedUp = false;
        pickedObject = null;
    }
}
