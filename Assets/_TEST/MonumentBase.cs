using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MonumentBase : MonoBehaviour
{
    [SerializeField] private ParticleSystem fireworksParticle;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameEventsHandler.Instance.MonumentFound(gameObject);
            StartCoroutine(SpawnFireworks());
        }
    }

    IEnumerator SpawnFireworks()
    {
        fireworksParticle.Play();

        yield return new WaitForSeconds(2);

        fireworksParticle.Stop();
    }
}
