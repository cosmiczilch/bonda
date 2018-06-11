using ProtoBuf;
using UnityEngine;

[System.Serializable]
[ProtoContract]
public class StarData {

    [ProtoMember(1)]
    public string common_name;

    [ProtoMember(2)]
    public float right_ascension;

    [ProtoMember(3)]
    public float declination;

    [ProtoMember(4)]
    public float distance_in_parsecs;

    [ProtoMember(5)]
    public float apparent_magnitude;

    [ProtoMember(6)]
    public float absolute_magnitude;

    [ProtoMember(7)]
    public string constellation_name;

    [ProtoMember(8)]
    public float luminosity;
}
