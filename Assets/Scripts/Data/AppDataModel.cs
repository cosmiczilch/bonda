using System.IO;
using ProtoBuf;
using TimiShared.Debug;
using TimiShared.Service;
using TimiShared.Utils;

public class AppDataModel : IService {

    public static AppDataModel Instance {
        get {
            return ServiceLocator.Service<AppDataModel>();
        }
    }

    #region Data
    private StarsData _starsData;
    public StarsData Stars {
        get {
            return this._starsData;
        }
    }
    #endregion

    public void LoadData(string filePath) {
        FileStream fileStream = FileUtils.OpenFileStream(filePath, FileMode.Open, FileAccess.Read);
        if (fileStream != null) {
            this._starsData = Serializer.Deserialize<StarsData>(fileStream);
            FileUtils.CloseFileStream(fileStream);
        }

        TimiDebug.LogColor("AppDataModel loaded ", LogColor.green);
        TimiDebug.LogColor("Loaded " + this.Stars.stars.Count + " stars", LogColor.green);
    }
}
