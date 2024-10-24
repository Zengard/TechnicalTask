using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private CircleCollider2D _circleCollider2D;
    private AudioSource _audioSource;

    [Header("AudioClips")]
    [SerializeField] private AudioClip _flyingClip;
    [SerializeField] private AudioClip _collisionClip;

    [Space]
    private bool _isGetHit = false;
    [SerializeField]private float _TimeToDisable = 3f;

    //getters and setters
    public Rigidbody2D Rigidbody2D { get { return _rigidbody2D; } set { _rigidbody2D = value; } }
    public CircleCollider2D CircleCollider2D { get { return _circleCollider2D; } }

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _circleCollider2D = GetComponent<CircleCollider2D>();
        _audioSource = GetComponent<AudioSource>();
        _rigidbody2D.isKinematic = true;

        _circleCollider2D.isTrigger = true;
    }

    private void Update()
    {
        if(_isGetHit == true)
        {
            _TimeToDisable -= Time.deltaTime;
        }

        if(_TimeToDisable <= 0)
        {
            EventManager.UpdateBirdsQueue();
            gameObject.SetActive(false);
        }
    }

    public void ReleaseBird()
    {
        PathPoint.instance.Clear();
        StartCoroutine(CreathPathPoints());
        PlayFlyingSound();
    }

    private void PlayFlyingSound()
    {
        _audioSource.PlayOneShot(_flyingClip);
    }

    private void PlayCollisionSound()
    {
        _audioSource.PlayOneShot(_collisionClip);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(_isGetHit == false)
        {
            PlayCollisionSound();
            _isGetHit = true;
        }
    }

    private IEnumerator CreathPathPoints()
    {
        while (true)
        {
            if(_isGetHit == true)
            {
                break;
            }

            PathPoint.instance.CreatePathPoint(transform.position);
            yield return new WaitForSeconds(PathPoint.instance.TimeInterval);
        }
    }
}
