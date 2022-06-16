using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffsetRoad : MonoBehaviour
{
    [SerializeField] private Material roadMaterial;
    [SerializeField] private float speed;

    private Vector2 offset;
    private int mainTextId;
    private float value;
    // Start is called before the first frame update
    void Start()
    {
        mainTextId = Shader.PropertyToID("_MainTex");
        roadMaterial.SetTextureOffset(mainTextId, Vector2.zero);
    }

    // Update is called once per frame
    void Update()
    {
        if (offset.x == 1000000)
            offset.x = 0;
           
        offset = roadMaterial.GetTextureOffset(mainTextId);
        value = speed * 0.1f * Time.deltaTime;
        offset -= new Vector2(value, 0);
        roadMaterial.SetTextureOffset(mainTextId, offset);
    }
    /// <summary>
    /// Function lấy giá trị để move
    /// </summary>
    /// <returns></returns>
    public float GetValue()
    {
        return value;
    }
}
