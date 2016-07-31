using UnityEngine;
using System.Collections;

public class TestMNIST : MonoBehaviour
{
    const int InputLength = 28 * 28;

    [Header("Common")]
    [SerializeField]
    int commonIndex = 0;

    [Header("Image")]
    [SerializeField]
    TextAsset imageData;
    [SerializeField]
    Color baseColor;
    [SerializeField]
    int imageStartByte;
    [SerializeField]
    byte[] pixelBytes;
    [SerializeField]
    SpriteRenderer[] pixelOjects;
    [SerializeField]
    SpriteRenderer pixelObjectPrototype;
    [SerializeField]
    float pixelSize = 0.1f;

    [Header("Label")]
    [SerializeField]
    TextAsset labelData;
    [SerializeField]
    int labelStartByte;
    [SerializeField]
    int labelValue;

    [ContextMenu("LoadPixels")]
    void LoadPixels()
    {
        if (pixelBytes == null)
        {
            pixelBytes = new byte[InputLength];
        }

        System.Buffer.BlockCopy(imageData.bytes, imageStartByte + InputLength * commonIndex, pixelBytes, 0, InputLength);
    }

    [ContextMenu("Load label")]
    void LoadLabel()
    {
        labelValue = labelData.bytes[labelStartByte + commonIndex];
    }

    [ContextMenu("ApplyPixels")]
    void ApplyPixels()
    {
        for (int i = 0; i < InputLength; i++)
        {
            var color = baseColor * pixelBytes[i];
            color.a = 1;
            pixelOjects[i].color = color;
        }
    }

    [ContextMenu("LoadAndApply")]
    void LoadAndApply()
    {
        LoadLabel();
        LoadPixels();
        ApplyPixels();
    }

    [ContextMenu("CreatePixelObjects")]
    void CreatePixelObjects()
    {
        pixelOjects = new SpriteRenderer[InputLength];
        float x = 0;
        float y = 0;
        int pxId = 0;
        for (int row = 0; row < 28; row++)
        {
            for (int col = 0; col < 28; col++)
            {
                var px = Instantiate(pixelObjectPrototype);
                px.transform.SetParent(transform);
                px.transform.position = new Vector3(x, y, transform.position.z);
                x += pixelSize / 2.0f;
                pixelOjects[pxId] = px;
                pxId++;
            }
            y -= pixelSize / 2.0f;
            x = 0;
        }
    }
}
