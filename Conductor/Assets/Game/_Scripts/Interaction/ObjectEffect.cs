using UnityEngine;

public class ObjectEffect : MonoBehaviour
{
    public enum EffectType { DOOR_OPEN, DOOR_CLOSE, WIRE_ON, WIRE_OFF, PISTON_EXTEND, PISTON_RETRACT, LOGIC_GATE, BOUNCER, BOUNCER_INV }
    public EffectType effect;
}
