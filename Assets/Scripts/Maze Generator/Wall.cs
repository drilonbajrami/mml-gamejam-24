using UnityEngine;

public class Wall : MonoBehaviour
{
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
            GetComponent<MeshRenderer>().enabled = true;
    }
}
