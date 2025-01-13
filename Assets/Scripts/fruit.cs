using UnityEngine;

public class fruit : MonoBehaviour
{
    private Plane dragPlane;
    private Vector3 offset;
    private bool isDragging = false;

    public GameObject dragEffectPrefab;
    private GameObject dragEffectInstance;

    public GameObject dropEffectPrefab;
    private bool droppedOnce = false;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (rb == null)
        {
            Debug.LogWarning("Rigidbody bileþeni eksik. Lütfen objeye Rigidbody ekleyin.");
        }
    }

    void OnMouseDown()
    {
        if (rb != null)
        {
            rb.isKinematic = true;
        }

        dragPlane = new Plane(Vector3.up, transform.position);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float distance;

        if (dragPlane.Raycast(ray, out distance))
        {
            offset = transform.position - ray.GetPoint(distance);
        }

        if (dragEffectPrefab != null)
        {
            dragEffectInstance = Instantiate(dragEffectPrefab, transform.position, Quaternion.identity);
        }

        isDragging = true;
        droppedOnce = false;
    }

    void OnMouseDrag()
    {
        if (isDragging)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float distance;

            if (dragPlane.Raycast(ray, out distance))
            {
                transform.position = ray.GetPoint(distance) + offset;

                if (dragEffectInstance != null)
                {
                    dragEffectInstance.transform.position = transform.position;
                }
            }
        }
    }

    void OnMouseUp()
    {
        isDragging = false;

        if (dragEffectInstance != null)
        {
            Destroy(dragEffectInstance);
        }

        if (!droppedOnce && dropEffectPrefab != null)
        {
            GameObject dropEffectInstance = Instantiate(dropEffectPrefab, transform.position, Quaternion.identity);
            Destroy(dropEffectInstance, 1f);

            droppedOnce = true;
        }

        if (rb != null)
        {
            rb.isKinematic = false;
        }
    }
}
