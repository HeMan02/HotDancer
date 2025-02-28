using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerInfo<T> where T : IEntity
{
    public string Name { get; set; }
    public T Entity { get; set; }
}
