using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private Portal other;

    private PortalRenderer renderer;

    private void Awake()
    {
        renderer = GetComponent<PortalRenderer>();
    }

    public void Render(Camera mainCamera)
    {
        renderer.Render(mainCamera, other.transform);
    }
}
