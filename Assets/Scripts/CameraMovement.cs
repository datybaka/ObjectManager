using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float zoomSpeed = 10f;
    public float rotationSpeed = 5f;
    public float moveSpeed = 10f;

    public Transform focusTarget;
    public LayerMask selectableLayers;
    public UIController uiController;

    private Vector3 offset;
    private float distanceToTarget;
    private bool isRotating;

    void Start()
    {
        if (focusTarget != null)
        {
            distanceToTarget = Vector3.Distance(transform.position, focusTarget.position);
            offset = transform.position - focusTarget.position;
        }
    }

    void Update()
    {
        HandleZoom();
        HandleVerticalMovement();
        HandleRotation();
        HandleSelection();
    }

    void HandleSelection()
    {
        if (Input.GetMouseButtonDown(0)) // Левая кнопка мыши
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, selectableLayers))
            {
                focusTarget = hit.transform;
                var controllable = hit.transform.GetComponent<ControllableObject>();
                if (controllable != null)
                {
                    uiController.SetTarget(controllable);
                }
                UpdateFocusOffset(); // обновим смещение и дистанцию
                UpdateFocus();
            }
        }
    }

    void UpdateFocusOffset()
    {
        distanceToTarget = Vector3.Distance(transform.position, focusTarget.position);
        offset = transform.position - focusTarget.position;
    }

    void UpdateFocus()
    {
        // Smoothly move to focus on the target
        transform.position = focusTarget.position + offset.normalized * distanceToTarget;
        transform.LookAt(focusTarget);
    }


    void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0f)
        {
            Vector3 direction = transform.forward * scroll * zoomSpeed;
            transform.position += direction;
        }
    }



    void HandleVerticalMovement()
    {
        if (Input.GetMouseButton(2)) // Middle mouse button held
        {
            float vertical = Input.GetAxis("Mouse Y");
            transform.position += Vector3.up * vertical * moveSpeed * Time.deltaTime * 3;
        }
    }

    void HandleRotation()
    {
        if (Input.GetMouseButtonDown(1) && focusTarget != null)
        {
            isRotating = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            isRotating = false;
        }

        if (isRotating && focusTarget != null)
        {
            float h = Input.GetAxis("Mouse X") * rotationSpeed;
            float v = -Input.GetAxis("Mouse Y") * rotationSpeed;

            transform.RotateAround(focusTarget.position, Vector3.up, h);
            transform.RotateAround(focusTarget.position, transform.right, v);

            offset = transform.position - focusTarget.position;
        }
    }
}
