using System;
using TimiShared.Service;

// TODO: Change this to not be a MonoBehaviour; right now the only reason this is a MonoBehaviour is for this.StartCoroutine
public class AppDataModel : DataModelBase, IService {

    public static AppDataModel Instance {
        get {
            return ServiceLocator.Service<AppDataModel>();
        }
    }

    #region DataModelBase
    protected override string GetDataModelRootPath {
        get {
            return "AppDataModels/";
        }
    }

    protected override Type[] DataModelTypes {
        get {
            return this._dataModelTypes;
        }
    }
    #endregion

    private Type[] _dataModelTypes = {
        typeof(StarsData)
        // Add more data types here
    };


    #region Public API
    public StarsData StarsData {
        get {
            return this.GetDataModelForType(typeof(StarsData)) as StarsData;
        }
    }
    #endregion
}
