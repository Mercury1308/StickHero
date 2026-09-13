using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderDetect : MonoBehaviour
{
    public bool LevelPass;
    [SerializeField] private Transform parent;
    public void LevelController(Vector3 position)
    {
        LevelPass = false;
        parent.position = position;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Pillar"))
        {
            LevelPass = true;
        }
    }
}