using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    [SerializeField] RectTransform spinner;
    [Tooltip("degree per second")]
    [SerializeField] float spinSpeed;

    private void Update()
    {
        spinner.Rotate(new Vector3(0, 0, spinSpeed * Time.deltaTime));
    }
}
