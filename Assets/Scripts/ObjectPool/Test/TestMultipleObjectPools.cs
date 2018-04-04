using System.Collections;
using UnityEngine;

public class TestMultipleObjectPools : MonoBehaviour {

    private Vector3 spawnPointPink = new Vector3(2, 0, 0);
    private Vector3 spawnPointYellow = new Vector3(-2, 0, 0);

    IEnumerator Start () {
        var objectPool = GetComponent<ObjectPool>();
        while( true )
        {
            Spawn("SpaceShipPink", spawnPointPink);   
            yield return new WaitForSeconds(0.2f);
            Spawn("SpaceShipYellow", spawnPointYellow);
            yield return new WaitForSeconds(0.2f);

        }
    }

    private void Spawn(string poolName, Vector3 pos)
    {
        GameObject go = PoolObjectManager.Instance.GetPool(poolName).GetObject(pos);
        go.transform.position = pos;
        Vector2 randomDir = Random.insideUnitCircle;
        go.GetComponent<Rigidbody2D>().AddForce(randomDir * 100f);
    }
}
