
## How to use?

The class you want to pool must inherit from: PooledMonoBehaviour

    ``` csharp
    class SampleObjectToPool : PooledMonoBehaviour{

    }
    ```

Setup a pool size on the object in the unity inspector

Instantiate a new object from the pool

    ``` csharp
    SampleObjectToPool newObject = prefabReference.Get<SampleObjectToPool>();
    ```


Caution: You need to have OnEnable to reset the object