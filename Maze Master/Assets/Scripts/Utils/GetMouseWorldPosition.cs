using UnityEngine;

public class GetMouseWorldPosition
{
    public static Vector3 getMouseWorldPosition()
    {
        Vector3 vec = getMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return vec;
    }
    public static Vector3 getMouseWorldPositionWithZ()
    {
        return getMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
    }
    public static Vector3 getMouseWorldPositionWithZ(Camera worldCamera)
    {
        return getMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
    }
    public static Vector3 getMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
    {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }
}