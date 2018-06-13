using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using ProtoBuf;
using TimiShared.Extensions;
using TimiShared.Loading;
using TimiShared.Service;
using UnityEngine;

// TODO: Change this to not be a MonoBehaviour; right now the only reason this is a MonoBehaviour is for this.StartCoroutine
public class AppDataModel : MonoBehaviour, IService {

    public static AppDataModel Instance {
        get {
            return ServiceLocator.Service<AppDataModel>();
        }
    }

    // This is the root of the app data models inside streaming assets
    private const string APPDATAMODEL_ROOT = "AppDataModels/";
    private const string APPDATAMODEL_EXTENSION = ".pb";

    #region Data
    private Dictionary<DataModelType, object> _dataModels = new Dictionary<DataModelType, object>();

    private enum DataModelType {
        STARS_DATA = 0,
        COUNT
    }

    public StarsData StarsData {
        get {
            return this._dataModels.GetOrDefault(DataModelType.STARS_DATA) as StarsData;
        }
    }
    #endregion

    private int _numLoadedDataModels;

    public Coroutine LoadDataAsync() {
        return this.StartCoroutine(this.LoadDataInternal());
    }

    private IEnumerator LoadDataInternal() {
        this._numLoadedDataModels = 0;

        this.LoadDataModel<StarsData>(DataModelType.STARS_DATA);
        // Add more data models here

        while (this._numLoadedDataModels < (int)DataModelType.COUNT) {
            yield return null;
        }
    }

    private void LoadDataModel<T>(DataModelType dataModelType) {
        string filePath = Path.Combine(APPDATAMODEL_ROOT, typeof(T).ToString() + APPDATAMODEL_EXTENSION);

        AssetLoader.Instance.GetStreamFromStreamingAssets(filePath, (Stream stream) => {
            if (stream != null) {
                object dataModel = Serializer.Deserialize<T>(stream);
                this._dataModels.Add(dataModelType, dataModel);
                AssetLoader.Instance.CloseStream(stream);

                ++this._numLoadedDataModels;
            }
        });
    }
}
