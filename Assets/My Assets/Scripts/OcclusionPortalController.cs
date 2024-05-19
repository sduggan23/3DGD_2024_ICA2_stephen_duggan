using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class OcclusionPortalController : MonoBehaviour
{
    private OcclusionPortal occlusionPortal;
    private bool isOpen = true;
    private bool isMoving = false;
    public float moveDuration = 1.0f;  // Duration over which to move down

    void Start()
    {
        occlusionPortal = GetComponent<OcclusionPortal>();
        if (occlusionPortal == null)
        {
            Debug.Log("OcclusionPortal component not found");
        }
    }

    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            TogglePortal();
            if (!isMoving)
            {
                StartCoroutine(MoveDownOverTime());
            }
        }
    }

    void TogglePortal()
    {
        isOpen = !isOpen;
        if (occlusionPortal != null)
        {
            occlusionPortal.open = isOpen;
            Debug.Log("Portal is now " + (isOpen ? "open" : "closed"));
        }
    }

    IEnumerator MoveDownOverTime()
    {
        isMoving = true;
        Vector3 startPosition = transform.position;
        Vector3 endPosition = startPosition - new Vector3(0, 5, 0);
        float elapsedTime = 0;

        while (elapsedTime < moveDuration)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = endPosition; 
        isMoving = false;
    }
}
