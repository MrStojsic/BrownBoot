using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Npcs.Occupation
{
    public interface IOccupation
    {
        string BusinessTitle
        {
            get;
        }
        Sprite Icon
        {
            get;
        }
    }
}
