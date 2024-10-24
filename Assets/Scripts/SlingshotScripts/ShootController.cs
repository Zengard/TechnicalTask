using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController
{
    private Slingshot _slingshot;

    public ShootController(Slingshot slingshot)
    {
        _slingshot = slingshot;
    }

    public void Shoot()
    {
        _slingshot.CurrentBird.Rigidbody2D.isKinematic = false;
        _slingshot.CurrentBird.CircleCollider2D.isTrigger = false;
        Vector3 birdForce = (_slingshot.CurrentPosition - _slingshot.CenterPosition.position) * _slingshot.ShootForce * -1;
        _slingshot.CurrentBird.Rigidbody2D.velocity = birdForce;

        _slingshot.CurrentBird.GetComponent<Bird>().ReleaseBird();

        _slingshot.CurrentBird = null;
    }
}
