using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ObjectPool))]
public class TestSingleObjectPool : MonoBehaviour {

	IEnumerator Start () {
        var objectPool = GetComponent<ObjectPool>();
        while( true )
        {
            GameObject go = objectPool.GetObject(Vector3.zero);
            Vector2 randomDir = Random.insideUnitCircle;
            go.GetComponent<Rigidbody2D>().AddForce(randomDir * 100f);
            yield return new WaitForSeconds(0.2f);

        }
	}
}
