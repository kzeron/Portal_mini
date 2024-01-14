using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField] private Portal[] portals;

    private Camera myCamera;

    private void Awake()
    {
        myCamera = GetComponent<Camera>();
    }

    private void OnPreRender()
    {
        for (int i = 0; i < portals.Length;  i++) 
        {
            portals[i].Render(myCamera);
        }
    }
}
