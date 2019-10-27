using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChomperKill : MonoBehaviour
{
    RaycastHit hitInfo;
    private void Update()
    {
        Debug.DrawLine(transform.position + (Vector3.up * 0.1f), transform.position + Vector3.down * 0.3f, Color.red);
        Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, 0.3f);
        if (hitInfo.collider !=  null && hitInfo.collider.tag == "Enemy")
        {
            Debug.Log("Muerto");
            Destroy(hitInfo.collider.transform.parent.gameObject);
        }
    }
}
