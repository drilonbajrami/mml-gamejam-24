
using UnityEngine;
using UnityEngine.Video;

public class Meme : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private VideoPlayer videoPlayer;

    public MeshRenderer MeshRenderer => meshRenderer;
    public VideoPlayer VideoPlayer => videoPlayer;

    public void ShowMeme()
    {
        MemeDealer.Instance.ShowNext(this);
    }    
}
