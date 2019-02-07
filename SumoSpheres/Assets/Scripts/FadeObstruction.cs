using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeObstruction : MonoBehaviour
{
    [SerializeField]
    private float fadeSpeed = 1;

    private Material material;
    private Color color;
    private float targetAlpha = 1;
    private float currentAlpha = 1;
    private bool fadeObstacle = false;
    void Start()
    {
        material = GetComponent<MeshRenderer>().material;
        color = material.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeObstacle) {
            targetAlpha = 0.3f;
        } else {
            targetAlpha = 1;
        }
        currentAlpha = Mathf.MoveTowards(currentAlpha, targetAlpha, Time.deltaTime * fadeSpeed);
        material.color = new Color(color.r, color.g, color.b, currentAlpha);
        fadeObstacle = false;
    }

    void FadeObstacle() {
        fadeObstacle = true;
    }
}
