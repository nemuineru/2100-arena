using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Dir : MonoBehaviour {	

	public int Rot2Num8Dir (float A) {
        if (A > 45 / 2&& A <= 45 * 3 / 2)
            return 9;
        else if (A > 45 * 3 / 2 && A <= 45 * 5 / 2)
            return 8;
        else if (A > 45 * 5 / 2 && A <= 45 * 7 / 2)
            return 7;
        else if (A > 45 * 7 / 2 && A <= 45 * 9 / 2)
            return 4;
        else if (A > 45 * 9 / 2 && A <= 45 * 11 / 2)
            return 1;
        else if (A > 45 * 11 / 2 && A <= 45 * 13 / 2)
            return 2;
        else if (A > 45 * 13 / 2 && A <= 45 * 15 / 2)
            return 3;
        else
            return 6;
    }

    public int Rot2Num4Dir(float A)
    {

        if (A > 45 && A <= 45 * 3)
            return 8;
        else if (A >= 45 * 3 && A < 45 * 5)
            return 4;
        else if (A > 45 * 5 && A <= 45 * 7)
            return 2;
        else
            return 6;
    }
}
