using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject beamPrefab;
    public GameObject speedUpPrefab;
    public GameObject sheildArmedPrefab;
    public List<GameObject> spawners = new List<GameObject>();
    int beamRandom;
    int seedUpRandom;
    int sheildArmedRandom;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GenerateBeam());
    }

    public IEnumerator GenerateBeam()
    {
        while (true)
        {
            beamRandom = Random.Range(0, 19);
            seedUpRandom = Random.Range(0, 50);
            sheildArmedRandom = Random.Range(0, 90);
            Instantiate(beamPrefab, spawners[beamRandom].transform.position, Quaternion.identity);

            if (seedUpRandom == 0)
            {
                yield return new WaitForSeconds(0.25f);
                Instantiate(speedUpPrefab, spawners[beamRandom].transform.position, Quaternion.Euler(new Vector3(90, 0, 0)));
            }

            if (sheildArmedRandom == 0)
            {
                yield return new WaitForSeconds(0.25f);
                Instantiate(sheildArmedPrefab, spawners[beamRandom].transform.position, Quaternion.Euler(new Vector3(90, 0, 0)));
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
