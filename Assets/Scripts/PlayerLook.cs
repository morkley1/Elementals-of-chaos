using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{

    public float mousesen = 100f;

    public Transform playerBody;

    float Xrotation = 0f;

    public float picDist;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mousesen * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mousesen * Time.deltaTime;

        Xrotation -= mouseY;
        Xrotation = Mathf.Clamp(Xrotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(Xrotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
        if (Input.GetKeyDown(KeyCode.E))
        {
            Picup();
        }
    }

    void Picup()
    {
        RaycastHit zapHit;
        Ray ray = new Ray(transform.position, transform.forward);
        bool zapIt = Physics.Raycast(ray, out zapHit, picDist);
        if (zapHit.transform.GetComponent<WorldItem>() != null && zapIt)
        {
            zapHit.transform.GetComponent<WorldItem>().picup(GameObject.Find("First person player").GetComponent<Inventory>());
        }
    }
}
