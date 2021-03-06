using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    // public float interactionDistance;

    public TMPro.TextMeshProUGUI interactionText;
    public GameObject interactionHoldGO; // the ui parent to disable when not interacting
    public UnityEngine.UI.Image interactionHoldProgress; // the progress bar for hold interaction type

    public Camera cam;
    public LayerMask interactionLayers;
    public Transform interactionPoint;
    public float interactionRange = .7f;
    
    // Update is called once per frame
    void Update(){
        // Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width/2f, Screen.height/2f, 0f));
        // RaycastHit hit;

        // Detect interactables within range
        Collider[] interactables = Physics.OverlapSphere(interactionPoint.position, interactionRange, interactionLayers);

        bool successfulHit = false;

        if (interactables.Length != 0) {
            Interactable interactable = interactables[0].GetComponent<Interactable>();

            if (interactable != null) {
                HandleInteraction(interactable);
                interactionText.text = interactable.GetDescription();
                successfulHit = true;

                interactionHoldGO.SetActive(interactable.interactionType == Interactable.InteractionType.Hold);
            }
        }

      	// if we miss, hide the UI
        if (!successfulHit) {
            interactionText.text = "";
            interactionHoldGO.SetActive(false);
        }
    }

    void HandleInteraction(Interactable interactable) {
        KeyCode key = KeyCode.E;
        switch (interactable.interactionType) {
            case Interactable.InteractionType.Click:
            	// interaction type is click and we clicked the button -> interact
                if (Input.GetKeyDown(key)) {
                    interactable.Interact();
                }
                break;
            case Interactable.InteractionType.Hold:
                if (Input.GetKey(key)) {
                  	// we are holding the key, increase the timer until we reach 1f
                    interactable.IncreaseHoldTime();
                    if (interactable.GetHoldTime() > 1f) {
                        interactable.Interact();
                        interactable.ResetHoldTime();
                    }
                } else {
                    interactable.ResetHoldTime();
                }
                interactionHoldProgress.fillAmount = interactable.GetHoldTime();
                break;
            // here is started code for your custom interaction :)
            case Interactable.InteractionType.Minigame:
                // here you make ur minigame appear
                break;

           	// helpful error for us in the future
            default:
                throw new System.Exception("Unsupported type of interactable.");
        }
    }

    void OnDrawGizmosSelected()
    {
        if (interactionPoint == null) {
            return;
        }
        Gizmos.DrawWireSphere(interactionPoint.position, interactionRange);
    }
}
