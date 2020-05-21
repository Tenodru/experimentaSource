
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 3.0f;
    public Transform interactionTransform;

    bool isFocus = false;
    Transform player;
    bool hasInteracted = false;

    /// <summary>
    /// Overridable method that manages interaction.
    /// </summary>
    public virtual void Interact()
    {
        Debug.Log("Interacting with " + interactionTransform.name);
    }

    private void Update()
    {
        Interacted();
    }

    public virtual void Interacted()
    {
        if (isFocus && !hasInteracted)
        {
            //Vector3 newPos = new Vector3(interactionTransform.position.x, interactionTransform.position.y + 3f, interactionTransform.position.z);

            float distance = Vector3.Distance(player.position, interactionTransform.position);
            if (distance <= radius)
            {
                Interact();
                Debug.Log("Interacted");
                hasInteracted = true;
            }
        }
    }

    public void OnFocused(Transform playerTransform)
    {
        isFocus = true;
        player = playerTransform;
        hasInteracted = false;
    }

    public void OnDefocused()
    {
        isFocus = false;
        player = null;
        hasInteracted = false;
    }

    private void OnDrawGizmosSelected()
    {
        if (interactionTransform == null)
            interactionTransform = transform;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }
}
