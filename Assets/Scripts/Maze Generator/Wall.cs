using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] private Meme meme;

    private bool hit = false;

    public void OnCollisionEnter(Collision collision)
    {
        if (!hit && collision.collider.CompareTag("Player"))
            EnalbeFeatures();
    }

    private void EnalbeFeatures()
    {
        GetComponent<MeshRenderer>().enabled = true;

        for(int i = transform.childCount - 1; i >= 0; i--) {
            transform.GetChild(i).gameObject.SetActive(true);
        }

        meme.ShowMeme();
        hit = true;
    }
}
