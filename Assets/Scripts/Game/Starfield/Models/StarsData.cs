using ProtoBuf;

[System.Serializable]
[ProtoContract]
public class StarsData {

    [ProtoMember(1)]
    public StarData[] stars;
}
