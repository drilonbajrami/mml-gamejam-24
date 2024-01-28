using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class MemeDealer : Singleton<MemeDealer>
{
    public List<Material> Memes = new();
    public List<VideoClip> MemeGIFs = new();

    private int memeIndex = 0;
    private int gifIndex = 0;

    private static System.Random rng = new System.Random();

    public void ShuffleMemes()
    {
        Memes.Shuffle(rng);
        MemeGIFs.Shuffle(rng);
        gifIndex = 0;
        memeIndex = 0;
    }

    public void ShowNext(Meme meme)
    {
        int random = Random.Range(0, 2);

        // GIF
        if (random < 1) {
            meme.VideoPlayer.clip = MemeGIFs[gifIndex];
            meme.VideoPlayer.enabled = true;
            gifIndex++;
            if(gifIndex >= MemeGIFs.Count) gifIndex = 0;
            
        }
        // MEME
        else {
            meme.MeshRenderer.material = Memes[memeIndex];
            memeIndex++;
            if (memeIndex >= Memes.Count) memeIndex = 0;
        }
    }

    
}

public static class ListExtension
{
    public static void Shuffle<T>(this List<T> list, System.Random rng)
    {
        int n = list.Count;
        while (n > 1) {
            n--;
            int k = rng.Next(n + 1);
            (list[n], list[k]) = (list[k], list[n]);
        }
    }
}