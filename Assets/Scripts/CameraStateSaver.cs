using UnityEngine;

[System.Serializable]
public class CameraTransformData
{
    public Vector3 position;
    public Quaternion rotation;
}

public class CameraStateSaver : MonoBehaviour
{
    private const string CameraSaveKey = "CameraState";

    void Start()
    {
        LoadCameraState();
    }

    void OnApplicationQuit()
    {
        SaveCameraState();
    }

    public void SaveCameraState()
    {
        CameraTransformData data = new CameraTransformData
        {
            position = transform.position,
            rotation = transform.rotation
        };

        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(CameraSaveKey, json);
        PlayerPrefs.Save();
    }

    public void LoadCameraState()
    {
        if (PlayerPrefs.HasKey(CameraSaveKey))
        {
            string json = PlayerPrefs.GetString(CameraSaveKey);
            CameraTransformData data = JsonUtility.FromJson<CameraTransformData>(json);

            transform.position = data.position;
            transform.rotation = data.rotation;
        }
    }
}
