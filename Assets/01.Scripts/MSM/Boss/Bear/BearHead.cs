using System.Collections;
using UnityEngine;

public class BearHead : MonoBehaviour
{
    [SerializeField] Transform upEnd;
    [SerializeField] float upWaitTime;
    [SerializeField] Transform downEnd;
    [SerializeField] float headSpeed;
    [SerializeField] WeaknessPoint WeaknessTarget;
    private void Awake()
    {
        transform.position = downEnd.position;
    }
    public IEnumerator HeadSee()
    {
        float time = 0;
        while (Mathf.Abs(transform.position.y - upEnd.position.y) > 0.1f)
        {
            Vector3 targetPosition = new Vector3(transform.position.x, upEnd.position.y, transform.position.z);

            time += Time.deltaTime;

            transform.position = Vector3.Lerp(transform.position, targetPosition, time * headSpeed);

            yield return null;
        }
        time = 0;
        yield return new WaitForSeconds(upWaitTime);
        while (Mathf.Abs(transform.position.y - downEnd.position.y) > 0.1f)
        {
            Vector3 targetPosition = new Vector3(transform.position.x, downEnd.position.y, transform.position.z);

            time += Time.deltaTime;

            transform.position = Vector3.Lerp(transform.position, targetPosition, time * headSpeed * 2);

            yield return null;
        }
    }

    public IEnumerator Weakness()
    {
        WeaknessTarget.gameObject.SetActive(true);
        float time = 0;
        while (Mathf.Abs(transform.position.y - upEnd.position.y) > 0.1f)
        {
            Vector3 targetPosition = new Vector3(transform.position.x, upEnd.position.y, transform.position.z);

            time += Time.deltaTime;

            transform.position = Vector3.Lerp(transform.position, targetPosition, time * headSpeed);

            yield return null;
        }
        time = 0;
        WeaknessTarget.isRest = true;
        yield return new WaitForSeconds(upWaitTime + 1);
        WeaknessTarget.isRest = false;
        WeaknessTarget.gameObject.SetActive(false);
        while (Mathf.Abs(transform.position.y - downEnd.position.y) > 0.1f)
        {
            Vector3 targetPosition = new Vector3(transform.position.x, downEnd.position.y, transform.position.z);

            time += Time.deltaTime;

            transform.position = Vector3.Lerp(transform.position, targetPosition, time * headSpeed * 2);

            yield return null;
        }
    }
}
