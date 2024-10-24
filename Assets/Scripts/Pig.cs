using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private AudioSource _audioSource;


    [Header("AudioClips")]
    [SerializeField] private AudioClip[] _gruntClips;
    private float _soundCountDown;
    private int _randomClip;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
        _randomClip = Random.Range(0, _gruntClips.Length);
        _audioSource.clip = _gruntClips[_randomClip];

        _soundCountDown = Random.Range(2, 5);
    }

    private void Update()
    {
        _soundCountDown -= Time.deltaTime;

        if(_soundCountDown <0) 
        {
            _soundCountDown = Random.Range(4, 8); 
            _audioSource.Play();

            _randomClip = Random.Range(0, _gruntClips.Length);
            _audioSource.clip = _gruntClips[_randomClip];
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Bird")
        {
           Rigidbody2D birdRB = collision.gameObject.GetComponent<Rigidbody2D>();

            if (birdRB.velocity.magnitude > 2)
            {
                Defeated();
            } 
        }

        if(collision.gameObject.tag == "Ground")
        {
            if (_rigidbody2D.velocity.magnitude > 1)
            {
                Defeated();
            }
        }

        if (_rigidbody2D.velocity.magnitude > 3)
        {
            Defeated();
        }
    }

    private void Defeated()
    {
        EventManager.UpdatePigsCount();
        gameObject.SetActive(false);
    }

    private IEnumerator PlaySound()
    {
        yield return new WaitForSeconds(Random.Range(2, 5));
        _audioSource.Play();
    }
}
