using UnityEngine;

public class PortalRenderer : MonoBehaviour
{
    [SerializeField] private Color outlineColor;
    [SerializeField] private Renderer outline;
    [SerializeField] private Camera portalCamera;

    private Material material;
    private Renderer renderer;
    private RenderTexture renderTexture;

    private void Awake()
    {
        renderer = GetComponent<Renderer>();
        material = GetComponent<Material>();
        renderTexture = new RenderTexture(Screen.width, Screen.height, 0);
        material.mainTexture = renderTexture;
        portalCamera.targetTexture = renderTexture;
        outline.material.color = outlineColor;
    }

    public void Render(Camera mainCamera, Transform otherPortal)
    {
        if (!renderer.isVisible)
        {
            return;
        }
        RenderInternal(mainCamera, otherPortal);
    }
    public void RenderInternal(Camera mainCamera, Transform otherPortal)
    {
        Transform enterPoint = transform;
        Transform exitPoint = otherPortal;

        Transform portalCamTransform = mainCamera.transform;
        portalCamTransform.position = mainCamera.transform.position;
        portalCamTransform.rotation = mainCamera.transform.rotation;

        portalCamTransform.MirrorPosition(enterPoint, exitPoint);
        portalCamTransform.MirrorRotation(enterPoint, exitPoint);

        portalCamera.Render();
    }
}
