using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    Rigidbody rb;

    Vector3 ScreenPoint;
    Vector3 Offset;
    float initialY; // Nesnenin ilk y (yükseklik) deðeri

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialY = transform.position.y;
    }

    private void OnMouseDown()
    {
        rb.useGravity = false;
        ScreenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        Offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, ScreenPoint.z));
    }

    private void OnMouseDrag()
    {
        Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, ScreenPoint.z);
        Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint) + Offset;

        // Y pozisyonunu sabitliyoruz (ilk y pozisyonu ile)
        cursorPosition.y = initialY;
        rb.MovePosition(cursorPosition); // Nesneyi hareket ettiriyoruz
    }

    private void OnMouseUp()
    {
        rb.useGravity = true;
    }
}
