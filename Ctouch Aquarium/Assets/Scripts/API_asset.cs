using UnityEngine;

[CreateAssetMenu(fileName = "API", menuName = "API", order = 0)]
public class API_asset : ScriptableObject
{
    public string API_URL;
    public string hash;
    public string timestamp;
    public string port;
}