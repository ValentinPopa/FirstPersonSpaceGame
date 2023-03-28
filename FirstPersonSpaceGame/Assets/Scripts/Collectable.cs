using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField]
    private Vector3 SpinAmount = new Vector3(0, 20, 0);
    public CollectableSpawner Spawner;

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + (SpinAmount * Time.deltaTime));
    }
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        Spawner.SpawnNewObject();
    }
}
