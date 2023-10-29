using System;
using UnityEngine;

public class AlertTrigger : MonoBehaviour
{
    public Action<Collider> callback;

    private void OnTriggerEnter(Collider collider)
    {
        callback.Invoke(collider);
    }
}
