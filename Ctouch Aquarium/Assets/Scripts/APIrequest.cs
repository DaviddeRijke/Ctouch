using UnityEngine;

public class APIrequest : MonoBehaviour
{
    public API API_Asset;
    public RequestType type;
}

public enum RequestType
{
    GET,SET,EXECUTE
}