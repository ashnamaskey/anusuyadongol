using UnityEngine;

public class TrafficLightClick : MonoBehaviour
{
    public float raycastDistance = 30f;
    public LayerMask trafficLightLayer;
    public LayerMask fountainLayer;
    public GameObject trafficLightPanel;
    public GameObject fountainPanel;

    private bool trafficPanelOpen = false;
    private bool fountainPanelOpen = false;
    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
        if (trafficLightPanel != null)
            trafficLightPanel.SetActive(false);
        if (fountainPanel != null)
            fountainPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(
                new Vector3(Screen.width / 2, Screen.height / 2, 0));

            RaycastHit[] hits = Physics.RaycastAll(ray, raycastDistance);

            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.CompareTag("Player")) continue;

                // Traffic light hit
                if (((1 << hit.collider.gameObject.layer) & trafficLightLayer) != 0)
                {
                    Debug.Log("Hit traffic light: " + hit.collider.gameObject.name);

                    fountainPanelOpen = false;
                    if (fountainPanel != null)
                        fountainPanel.SetActive(false);

                    trafficPanelOpen = !trafficPanelOpen;
                    trafficLightPanel.SetActive(trafficPanelOpen);

                    if (trafficPanelOpen)
                    {
                        trafficLightPanel.transform.position =
                            hit.collider.transform.position + new Vector3(2f, 5f, 0f);
                        trafficLightPanel.transform.LookAt(cam.transform);
                        trafficLightPanel.transform.Rotate(0, 180f, 0);
                    }
                    break;
                }

                // Fountain hit
                if (((1 << hit.collider.gameObject.layer) & fountainLayer) != 0)
                {
                    Debug.Log("Hit fountain: " + hit.collider.gameObject.name +
                        " Layer: " + hit.collider.gameObject.layer);

                    trafficPanelOpen = false;
                    if (trafficLightPanel != null)
                        trafficLightPanel.SetActive(false);

                    fountainPanelOpen = !fountainPanelOpen;
                    fountainPanel.SetActive(fountainPanelOpen);

                    if (fountainPanelOpen)
                    {
                        fountainPanel.transform.position =
                            hit.collider.transform.position + new Vector3(0f, 3f, 0f);
                        fountainPanel.transform.LookAt(cam.transform);
                        fountainPanel.transform.Rotate(0, 180f, 0);
                    }
                    break;
                }
            }
        }
    }
}