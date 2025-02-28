using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using UnityEngine;

public class InfoSpawnPoin<T> where T : ISpawnPoint
{
    public string Name { get; set; }
    public T Entity { get; set; }
}
