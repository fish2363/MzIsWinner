using UnityEngine;
using System.Collections;

public class FishMovement : MonoBehaviour
{
    public Transform startPoint;
    public Transform targetPoint;
    public float heightMin = 2f;
    public float heightMax = 5f;
    public float waitTime = 2f;

    private bool isMovingToTarget = true;

    private void Start()
    {
        StartCoroutine(MoveFish());
    }

    private IEnumerator MoveFish()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(0, 3));
            Transform from = isMovingToTarget ? startPoint : targetPoint;
            Transform to = isMovingToTarget ? targetPoint : startPoint;
        
            float height = Random.Range(heightMin, heightMax);
            yield return StartCoroutine(ParabolicMove(from.position, to.position, height, 1.5f));
            isMovingToTarget = !isMovingToTarget;
            yield return new WaitForSeconds(Random.Range(1, 5));
        }
    }

    private IEnumerator ParabolicMove(Vector3 start, Vector3 end, float height, float duration)
    {
        float time = 0;
        while (time < duration)
        {
            float t = time / duration;
            Vector3 linearPosition = Vector3.Lerp(start, end, t);
            float parabolicHeight = Mathf.Sin(Mathf.PI * t) * height;
            transform.position = new Vector3(linearPosition.x, linearPosition.y + parabolicHeight, linearPosition.z);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = end;
    }
}
    