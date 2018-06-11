using ProtoBuf;
using UnityEngine;

[System.Serializable]
[ProtoContract]
public class StarData {

    [ProtoMember(1)]
    public string star_name;

    [ProtoMember(2)]
    public float declination;

    [ProtoMember(3)]
    public float right_ascension;

    [ProtoMember(4)]
    public Vector3 position;
}
