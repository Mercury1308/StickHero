using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class StickManager : MonoBehaviour
{
    [SerializeField] private StickController stickPrefab;
    [SerializeField] private PillarManager pillarManager;
    [SerializeField] private Transform targetRotate;
    [SerializeField] private AnimationController animationController;
    [SerializeField] private ColliderDetect colliderDetect;
    [SerializeField] private float offsetX;

    private StickController current;

    private void Start()
    {
        Create();
    }
    public void Create()
    {
        var position = pillarManager.currentPillarPosition;
        position.x += offsetX;
        var stick = Instantiate(stickPrefab, position, Quaternion.identity);
        current = stick;
    }

    public void OnPointerDown()
    {
        current.grow = true;
    }
    public void OnPointerUp()
    {
        current.grow = false;


        IEnumerator Do()
        {
            var rotate = animationController.Rotate(current.transform, targetRotate);
            yield return rotate;
            yield return null;
            colliderDetect.LevelController(current.colliderPosition.position);
            yield return new WaitForSeconds(.1f);
            if (colliderDetect.LevelPass)
            {
                pillarManager.NextLevel();
            }
            else
            {
                Debug.Log("GameOver");
            }
        }
        StartCoroutine(Do());
    }




}
