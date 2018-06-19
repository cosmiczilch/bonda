using System.Collections.Generic;
using TimiShared.Debug;

public class StarsDataWrapper {

    private StarsData _data;

    public StarsDataWrapper(StarsData data) {
        this._data = data;
        // TODO: Is it possible to check here if the data is null/bad and instead replace this instance with
        // one that will just throw exceptions in all methods?
        // That'll help in not having to error check everywhere
    }

    #region Public API
    public StarData GetStarDataByCommonName(string commonName) {
        if (this._data == null || this._data.stars == null) {
            return null;
        }

        commonName = commonName.ToLower();
        StarData result = null;
        var enumerator = this._data.stars.GetEnumerator();
        while (enumerator.MoveNext()) {
            if (enumerator.Current.common_name.ToLower() == commonName) {
                result = enumerator.Current;
                break;
            }
        }
        return result;
    }

    public List<StarData> GetAllStars() {
        if (this._data == null) {
            TimiDebug.LogWarningColor("No star data", LogColor.grey);
            return new List<StarData>();
        }
        return this._data.stars;
    }

    public List<StarData> GetStarsWithCommonNames() {
        List<StarData> result = new List<StarData>();
        if (this._data != null && this._data.stars != null) {
            var enumerator = this._data.stars.GetEnumerator();
            while (enumerator.MoveNext()) {
                if (!string.IsNullOrEmpty(enumerator.Current.common_name)) {
                    result.Add(enumerator.Current);
                }
            }
        }
        return result;
    }
    #endregion
}
