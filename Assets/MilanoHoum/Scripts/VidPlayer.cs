using UnityEngine;
using UnityEngine.Video;


public class VidPlayer : MonoBehaviour
{
    [SerializeField] string Video_name; 

    // Start is called before the first frame update
    void Start()
    {
        PlayVideo();            
    }

   

    public void PlayVideo()
    {
        VideoPlayer videoPlayer = GetComponent<VideoPlayer>();
        if (videoPlayer)
        {
            string videoPath = System.IO.Path.Combine(Application.streamingAssetsPath,Video_name);
            Debug.Log(videoPath);
            videoPlayer.url = videoPath;
            videoPlayer.Play();
        }

    }
}
