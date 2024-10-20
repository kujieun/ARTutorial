using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using UnityEngine.Timeline;

public class UserLocationTracker : MonoBehaviour
{
    public RawImage mapRawImage;

    [Header("Map SET")]

    public string strBaseURL = "";

    public int zoom = 14;
    public int mapWidth;
    public int mapHeight;
    public string strAPIKey = "";

    public GPSlocation GPSlocation;
    private double latitude = 0;
    private double longitude = 0;

    private double save_latitude = 0;
    private double save_longitude = 0;

    // Start is called before the first frame update
    void Start()
    {
        mapRawImage = GetComponent<RawImage>();
        StartCoroutine(WaitForSecond());
    }

    private void Update()
    {
        latitude = GPSlocation.latitude;
        longitude = GPSlocation.longitude;
        //print("location" + latitude + " " + longitude);
    }

    IEnumerator WaitForSecond()
    {
        while (true)
        {

            if (save_latitude != latitude || save_longitude != longitude)
            {
                save_latitude = latitude;
                save_longitude = longitude;
                StartCoroutine(LoadMap());
            }
            print("3��");
            yield return new WaitForSeconds(3f);
        }
        yield return new WaitForSeconds(1f);
    }

    IEnumerator LoadMap()
    {
        string url = strBaseURL + "center=" + latitude + "," + longitude +
            "&zoom=" + zoom.ToString() +
            "&size=" + mapWidth.ToString() + "x" + mapHeight.ToString()
            + "&key=" + strAPIKey;

        Debug.Log("URL : " + url);

        url = UnityWebRequest.UnEscapeURL(url);
        UnityWebRequest req = UnityWebRequestTexture.GetTexture(url);

        yield return req.SendWebRequest(); //req�� ��ȯ!

        mapRawImage.texture = DownloadHandlerTexture.GetContent(req); // �� >> �̹����� ����
    }
}
