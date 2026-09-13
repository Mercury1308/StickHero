using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarManager : MonoBehaviour
{
    [SerializeField] private PillarController pillarPrefab;

    [SerializeField] private AnimationController animationController;
    [SerializeField] private StickManager stickManager;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform TargetMin; [SerializeField] private Transform TargetMax;

    [SerializeField] private Transform camera;

    [SerializeField] private PillarController current;


    public Vector3 currentPillarPosition => current.transform.position;
    private PillarController targetPillar;

    private Vector3 _offsetCamera;

    private void Start()
    {
        _offsetCamera = camera.transform.position - current.transform.position;
        Create();
    }

    public void Create()
    {
        var pillar = Instantiate(pillarPrefab);

        //SetPosition
        ChangePositionX(pillar.transform, spawnPoint.position.x);

        //SetScale
        pillar.SetRandomSize();

        //SetTarget
        var targetX = Random.Range(TargetMin.position.x, TargetMax.position.x);
        var targetPosition = pillar.transform.position;
        targetPosition.x = targetX;
        var move = animationController.Move(pillar.transform, targetPosition);
        //Animation
        StartCoroutine(move);
        //SetTarget
        targetPillar = pillar;
    }

    private void ChangePositionX(Transform current, float x)
    {
        var position = current.transform.position;
        position.x = x;
        current.transform.position = position;
    }

    public void NextLevel()
    {
        current = targetPillar;

        var targetPosition = current.transform.position + _offsetCamera;
        //Camera animation
        var move = animationController.Move(camera, targetPosition, .2f);
        StartCoroutine(move);
        IEnumerator Do()
        {
            yield return new WaitForSeconds(.3f);
            Create();

            stickManager.Create();
        }
        StartCoroutine(Do());


    }
}
