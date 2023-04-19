using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Buff/ParcelProtection", fileName = "B_PP")]
public class b_ParcelProtection : Buff
{
    public override void apply()
    {
        //shield  +=
        //maxshield +=
    }

    public override void remove()
    {
        //maxShield -=
        //hope your script auto clamps
    }

    public override void trigger()
    {}
}