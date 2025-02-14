using UnityEngine;

[CreateAssetMenu(fileName = "TransformProfile", menuName = "Transform Data/Profile")]
public class TransformProfile : ScriptableObject
{
    // Start is called before the first frame update
    public Vector3 position;
    public Vector3 rotation;
    public Vector3 scale = Vector3.one;
}
