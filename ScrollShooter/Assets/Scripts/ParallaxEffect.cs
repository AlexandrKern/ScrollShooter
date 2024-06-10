using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ParallaxLayer
{
    public Transform layer;  // Ссылка на слой фона
    public float parallaxFactor;  // Коэффициент параллакса для этого слоя
}

public class ParallaxEffect : MonoBehaviour
{
    public ParallaxLayer[] layers;  // Массив слоев с их коэффициентами параллакса
    public float smoothing = 1f;  // Коэффициент сглаживания для плавности

    private Transform cam;  // Ссылка на камеру
    private Vector3 previousCamPos;  // Позиция камеры в предыдущем кадре

    void Awake()
    {
        // Установка ссылки на камеру
        cam = Camera.main.transform;
    }

    void Start()
    {
        // Сохранение начальной позиции камеры
        previousCamPos = cam.position;
    }

    void Update()
    {
        // Для каждого слоя фона
        foreach (ParallaxLayer layer in layers)
        {
            // Параллакс - противоположное движение камеры по оси x, умноженное на масштаб
            float parallax = (previousCamPos.x - cam.position.x) * layer.parallaxFactor;

            // Определение конечной позиции слоя
            float backgroundTargetPosX = layer.layer.position.x + parallax;

            // Создание позиции слоя с текущей позицией по оси Y и Z
            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, layer.layer.position.y, layer.layer.position.z);

            // Плавное смещение между текущей позицией и целевой позицией
            layer.layer.position = Vector3.Lerp(layer.layer.position, backgroundTargetPos, smoothing * Time.deltaTime);
        }

        // Обновление предыдущей позиции камеры
        previousCamPos = cam.position;
    }
}