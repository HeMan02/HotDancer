using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public interface IMovement
    {
        public void DoMovement(int value);
        public void SetValuesPower(PowerInfo<IEntity> obj);
}
