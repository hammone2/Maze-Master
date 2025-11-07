using UnityEngine;

public class QuaternionEuler : MonoBehaviour
{
    private static Quaternion[] cachedQuaternionEulerArr;

    private static void CacheQuaternionEuler() {
        if (cachedQuaternionEulerArr != null) return;
        cachedQuaternionEulerArr = new Quaternion[360];
        for (int i=0; i<360; i++) {
            cachedQuaternionEulerArr[i] = Quaternion.Euler(0,0,i);
        }
    }

    public static Quaternion GetQuaternionEuler(float rotFloat)
    {
        int rot = Mathf.RoundToInt(rotFloat);
        rot = rot % 360;
        if (rot < 0) rot += 360;
        if (cachedQuaternionEulerArr == null)
            CacheQuaternionEuler();
        return cachedQuaternionEulerArr[rot];
    }
}
