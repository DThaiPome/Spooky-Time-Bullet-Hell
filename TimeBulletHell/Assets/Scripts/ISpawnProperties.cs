using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpawnProperties<T>
{
    //Spawn the given object
    void spawn(T t);
}
