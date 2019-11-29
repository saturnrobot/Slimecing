using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatScript : MonoBehaviour {

    // Update is called once per frame
    public GameObject Owner;
    private Rigidbody rb;
    private bool moved = false;
   
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        transform.position = Owner.transform.position;
    }
    
    void Update()
    {
        Vector3 slimeHead = Owner.transform.position;
        slimeHead.y = (Owner.transform.position.y + Owner.GetComponent<Collider>().bounds.size.y) + transform.lossyScale.y;

        Vector3 offset = rb.transform.position - slimeHead;
        if (Vector3.Distance(transform.position, slimeHead) > 0.2f)
        {
            Vector3 resultForce = Vector3.zero;
            resultForce += 20 * offset.normalized;
            rb.AddForce(-resultForce);
            moved = true;
        }

        if (Vector3.Distance(transform.position, slimeHead) < 0.2f && moved)
        {
            rb.velocity = Vector3.zero;
            moved = false;
        }

        /*Vector3 hatRotation = transform.eulerAngles;
        hatRotation.y = Mathf.Clamp(hatRotation.y, 0, 20);
        hatRotation.z = Mathf.Clamp(hatRotation.z, 0, 20);
        transform.eulerAngles = hatRotation;*/
        Vector3 clampPos = slimeHead + Vector3.ClampMagnitude(offset, 0.3f);
        clampPos.y = Mathf.Clamp(transform.position.y, slimeHead.y + transform.lossyScale.y + 0.25f, slimeHead.y + transform.lossyScale.y + 1);
        transform.position = clampPos;

        if (rb.angularVelocity.magnitude < 3)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Owner.transform.forward), 0.5f);
        }
    }
}
