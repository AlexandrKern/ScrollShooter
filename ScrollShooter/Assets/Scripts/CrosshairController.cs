using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairController : MonoBehaviour
{
    void Update()
    {
        // ѕолучение позиции мыши в мировых координатах
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        // ѕеремещение прицела к позиции мыши
        transform.position = mousePosition;
    }
}
