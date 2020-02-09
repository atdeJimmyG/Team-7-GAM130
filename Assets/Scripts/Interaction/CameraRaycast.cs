using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRaycast : MonoBehaviour
{
    [SerializeField]private float range;

    private IIntractable currentTarget;
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        RaycastForInteractable();

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentTarget != null)
            {
                currentTarget.OnIntract();
            }
        }
    }

    private void RaycastForInteractable()
    {
        RaycastHit result;

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out result, range))
        {
            IIntractable intractable = result.collider.GetComponent<IIntractable>();

            if(intractable != null)
            {
                if(result.distance <= intractable.MaxRange)
                {
                    if (intractable == currentTarget)
                    {
                        return;
                    }
                    else if(currentTarget != null)
                    {
                        currentTarget.OnEndHover();
                        currentTarget = intractable;
                        currentTarget.OnStartHover();
                        return;
                    }
                    else
                    {
                        currentTarget = intractable;
                        currentTarget.OnStartHover();
                        return;
                    }
                }
                else
                {
                    if(currentTarget != null)
                    {
                        currentTarget.OnEndHover();
                        currentTarget = null;
                        return;
                    }
                }
            }
            else
            {
                if (currentTarget != null)
                {
                    currentTarget.OnEndHover();
                    currentTarget = null;
                    return;
                }
            }
        }
        else
        {
            if (currentTarget != null)
            {
                currentTarget.OnEndHover();
                currentTarget = null;
                return;
            }
        }
    }
}
