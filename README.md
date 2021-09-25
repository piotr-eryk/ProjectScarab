# Portals
It is game using environmental puzzles based on the Unity engine. The game is divided into several separate levels.
## Screenshots
![Player View](Assets/Project/Screenshots/PlayerView.png)
![One of the levels](Assets/Project/Screenshots/Level.png)
## Technologies
* C#
* Unity 2019.4.4f1
## Code example
An example code would be creating a missile from object pull so that the prefab is correctly positioned regardless of the object that fires it.

```
    private void SpawnBullet()
    {
        GameObject bulletObject = ObjectPool.SharedInstance.GetPooledObject();

        if (bulletObject) // if bullet exist get him from ObjectPool and rotate using angle of gun
        {
            Rigidbody rigidbody = bulletObject.GetComponent<Rigidbody>();
            rigidbody.angularVelocity = Vector3.zero;

            bulletObject.transform.position = transform.position + transform.forward * 2f;
            bulletObject.transform.rotation = transform.rotation;
            bulletObject.transform.Rotate(90.0f, 0.0f, 0.0f, Space.Self);
            rigidbody.velocity = transform.forward * bulletSpeed;

            bulletObject.SetActive(true);
        }
    }
 ```   

## Inspiration
This game is based on Udemy "Make a 3D Portals clone in Unity® and Blender from scratch!"

Parts created by me from scratch include:
* Guns with missile (look at "Code example")
* Lasers, mirrors system and every puzzle that base on lasers
* Multiple versions of the original button with improvements of original one
* Every 3d object in game was made by my own hands

## changes in the near future

* move spawn platforms and spawn bullets to one function
* move spawn laser and reflect laser to one function
* make door destruction independent from color
* the destruction of the door from the laser should be a property of the door, not the laser
* as above but for the player and the mirror
