using UnityEngine;
using System.Collections;

abstract public class SteeringAbstract : MonoBehaviour
{
    [Range(0, 100)]
    public float weight = 0;
}
