using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider))]
public class DestroyOnExit : MonoBehaviour {

    Collider _collider;

	void Awake()
    {
        _collider = GetComponent<Collider>();
        _collider.isTrigger = true;
    }

    void OnTriggerExit (Collider other)
    {
        Destroy(other.gameObject);
    }
    
}
