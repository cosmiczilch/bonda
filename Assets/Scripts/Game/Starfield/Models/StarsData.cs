using System.Collections.Generic;
using ProtoBuf;

[System.Serializable]
[ProtoContract]
public class StarsData {

    [ProtoMember(1)]
    public List<StarData> stars;
}
