using System.IO;
using ProtoBuf;
using TimiShared.Utils;
using UnityEngine;

public class ExampleSerializer : MonoBehaviour {

    [SerializeField]
    private StarsData _starsData;

    [SerializeField]
    private string filePath = "Assets/Resources/starsData";

    private void Start() {
        //// Pick one of the two below:
//         this.SerializeStars();
         this._starsData = this.DeserializeStars();
    }

    private void SerializeStars() {
        StarsData starsData = new StarsData();
        starsData.stars = new StarData[3];
        {
            StarData star = new StarData();
            star.star_name = "Sirius";
            star.declination = 10.2f;
            star.right_ascension = 90.2f;
            star.position = new Vector3(0, 0, 0);
            starsData.stars[0] = star;
        }
        {
            StarData star = new StarData();
            star.star_name = "Betelgeuse";
            star.declination = 42.2f;
            star.right_ascension = 190.2f;
            star.position = new Vector3(1, 1, 1);
            starsData.stars[1] = star;
        }
        {
            StarData star = new StarData();
            star.star_name = "Bellatrix";
            star.declination = 1.5f;
            star.right_ascension = 9.8f;
            star.position = new Vector3(2, 3, 4);
            starsData.stars[2] = star;
        }

        FileStream fileStream = FileUtils.OpenFileStream(filePath, FileMode.Create, FileAccess.Write);
        if (fileStream != null) {
            Serializer.Serialize<StarsData>(fileStream, starsData);
            fileStream.Flush();
            FileUtils.CloseFileStream(fileStream);
        }
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
