using UnityEngine;
using UnityEngine.EventSystems;

public class HighLightManager : MonoBehaviour
{
    private int enemyLayer;
    private int friendlyLayer;

    private GameObject highlightedObject;
    private GameObject selectedObject;
    public LayerMask selectableLayer;

    private Outline highlightOutline;
    private RaycastHit hit;

    private PlayerScript playerScript;

    void Start()
    {
        playerScript = GetComponent<PlayerScript>();
        enemyLayer = playerScript.enemyLayer;
        friendlyLayer = playerScript.friendlyLayer;
    }

    // Update is called once per frame
    void Update()
    {
        hoverHighlight();
    }

    public void hoverHighlight()
    {
        if (highlightedObject != null && highlightOutline != null)
        {
            highlightOutline.enabled = false;
            highlightedObject = null;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray,out hit, selectableLayer))
        {
            if (hit.transform.parent)
            {
                highlightedObject = hit.transform.parent.gameObject;
            }
            else
            {
                highlightedObject = hit.transform.gameObject;
            }

            if(highlightedObject.layer == enemyLayer && highlightedObject != selectedObject)
            {
                if(highlightedObject.GetComponent<Outline>())
                {
                    highlightOutline = highlightedObject.GetComponent<Outline>();
                    highlightOutline.enabled = true;
                }
            }
            else
            {
                highlightedObject = null;
            }
        }
    }

    public void selectedHighlight(GameObject newSelection)
    {
        if (highlightedObject.layer == enemyLayer)
        {
            if(selectedObject != null)
            {
                if(selectedObject.GetComponent<Outline>())
                {
                    selectedObject.GetComponent<Outline>().enabled = false;
                }
            }

            selectedObject = newSelection;

            if (selectedObject.GetComponent<Outline>())
            {
                selectedObject.GetComponent<Outline>().enabled = true;
            }

            highlightOutline.enabled = true;
            highlightedObject = null;
        }
    }

    public void deselectHighlight()
    {
        selectedObject.GetComponent<Outline>().enabled = false;
        selectedObject = null;
    }
}
