using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;


public class Interaction : MonoBehaviour
{ 

    public GameObject originalMenu;
    public GameObject newMenu;
    public Material outlineMaterial;

    private float enter;
    private RaycastHit hitInfo;
    private Ray ray;
    private Plane m_plane;
    private PointerEventData pointerEventData;
    /*
     * Plus besoin d'utiliser les OnMouseDown étant donné qu'ils ne marchent pas sur mobile on remplace par des events triggers sur les objets
     * Il faut juste rajouter un PhysicsRayCaster à la caméra et un EventSystem dans la scène
     */
    private void Start()
    {
        m_plane = new Plane(new Vector3(0, 1, 0), 10f);
        m_plane.Translate(new Vector3(0, 2, 0));
    }
    public void RemoveOutline()
    {
        foreach ( Renderer rd in gameObject.GetComponentsInChildren<Renderer>())
        {
            Material[] materials = new Material[1];
            materials.SetValue(rd.sharedMaterial, 0);
            rd.materials = materials;
        }
    }

    public void AddOutline()
    {
        foreach(Renderer rd in gameObject.GetComponentsInChildren<Renderer>())
        {

            Material[] materials = new Material[2];
            materials.SetValue(rd.sharedMaterial, 0);
            materials.SetValue(outlineMaterial, 1);
            rd.materials = materials;
        }
    }

    public void Rotate(BaseEventData eventData)
    {
        pointerEventData = eventData as PointerEventData;
        ray = Camera.main.ScreenPointToRay(pointerEventData.position);
        m_plane.Raycast(ray, out enter);
        transform.LookAt(ray.GetPoint(enter));
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }


    public void SetOriginalMenu()
    {
        originalMenu.SetActive(true);
        newMenu.SetActive(false);
    }

    public void SetNewMenu()
    {
        originalMenu.SetActive(false);
        newMenu.SetActive(true);
    }
    

    public void Dragging(BaseEventData data)
    {
        PointerEventData pointerData = data as PointerEventData;
        //Nécessaire de mettre le tag main camera sur la caméra
        ray = Camera.main.ScreenPointToRay(pointerData.position);
        Physics.Raycast(ray, out hitInfo);

        if(hitInfo.collider.gameObject.tag == "MirrorSpot")
        {
            transform.position = hitInfo.point;
        }
    }

}
