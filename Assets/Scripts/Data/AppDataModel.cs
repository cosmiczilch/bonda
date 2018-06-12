using System;
using System.Collections.Generic;
using System.IO;
using ProtoBuf;
using TimiShared.Loading;
using TimiShared.Service;

public class AppDataModel : IService {

    public static AppDataModel Instance {
        get {
            return ServiceLocator.Service<AppDataModel>();
        }
    }

    // This is the root of the app data models inside streaming assets
    private const string APPDATAMODEL_ROOT = "AppDataModels/";
    private const string APPDATAMODEL_EXTENSION = ".pb";

    #region Data
    private Dictionary<DataModelType, Object> _dataModels = new Dictionary<DataModelType, object>();

    private enum DataModelType {
        STARS_DATA = 0,
        COUNT
    }

    public StarsData StarsData {
        get {
            // TODO: Get or default
            return this._dataModels[DataModelType.STARS_DATA] as StarsData;
        }
    }
    #endregion

    private int _numLoadedDataModels;
    private System.Action _onDataModelsLoadedCallback;

    public void LoadData(System.Action onLoadedCallback) {
        this._numLoadedDataModels = 0;
        this._onDataModelsLoadedCallback = onLoadedCallback;

        this.LoadDataModel<StarsData>(DataModelType.STARS_DATA, OnDataModelLoaded);
    }

    private void LoadDataModel<T>(DataModelType dataModelType, System.Action<bool> onLoadedCallback) {
        string filePath = Path.Combine(APPDATAMODEL_ROOT, typeof(T).ToString() + APPDATAMODEL_EXTENSION);

        AssetLoader.Instance.GetStreamFromStreamingAssets(filePath, (Stream stream) => {
            if (stream != null) {
                Object dataModel = Serializer.Deserialize<T>(stream);
                this._dataModels.Add(dataModelType, dataModel);
                AssetLoader.Instance.CloseStream(stream);
                onLoadedCallback.Invoke(true);
            } else {
                onLoadedCallback.Invoke(false);
            }
        });
    }

    private void OnDataModelLoaded(bool success) {
        if (success) {
            ++this._numLoadedDataModels;
            if (this._numLoadedDataModels >= (int)DataModelType.COUNT) {
                if (this._onDataModelsLoadedCallback != null) {
                    this._onDataModelsLoadedCallback.Invoke();
                    this._onDataModelsLoadedCallback = null;
                }
            }
        }
    }
}
