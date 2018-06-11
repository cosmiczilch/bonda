using System.Collections.Generic;
using System.IO;
using ProtoBuf;
using TimiShared.Utils;
using UnityEngine;

public class ExampleSerializer : MonoBehaviour {

    [SerializeField]
    private StarsData _starsData;

    [SerializeField]
    private string filePath = "Assets/Resources/Data/starData";

    private void Start() {
         this._starsData = this.DeserializeStars();
    }

    private StarsData DeserializeStars() {
        StarsData starsData = null;
        FileStream fileStream = FileUtils.OpenFileStream(filePath, FileMode.Open, FileAccess.Read);
        if (fileStream != null) {
            starsData = Serializer.Deserialize<StarsData>(fileStream);
            FileUtils.CloseFileStream(fileStream);
        }
        return starsData;
    }


}
