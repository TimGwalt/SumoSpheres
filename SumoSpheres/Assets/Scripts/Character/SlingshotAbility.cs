using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingshotAbility : NetworkBasePlayerMovement
{
    public GameObject anchor;
    public GameObject anchorCopy;
    public LineRenderer lineRenderer;
    private bool slingshot = false;
    public int slingForce = 50;

    public override void CheckInput()
    {
        base.CheckInput();
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!slingshot)
            {
                anchorCopy = Instantiate(anchor, this.transform.position, Quaternion.identity);
                lineRenderer = anchorCopy.GetComponent<LineRenderer>();
                lineRenderer.SetPosition(0, this.transform.position);
                lineRenderer.SetPosition(0, anchorCopy.transform.position);
                slingshot = true;
            }
            else
            {
                Vector3 distance = anchor.transform.position - this.transform.position;
                m_PlayerRB.AddForce(distance * (Vector3.Distance(anchor.transform.position, this.transform.position) * slingForce));
                Destroy(anchorCopy);
                slingshot = false;
            }
        }
        else
        {
            if (lineRenderer != null)
            {
                lineRenderer.SetPosition(0, this.transform.position);
                lineRenderer.SetPosition(1, anchorCopy.transform.position);
            }
        }
    }
}