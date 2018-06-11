using System.Collections.Generic;
using System.IO;
using ProtoBuf;
using TimiShared.Debug;
using TimiShared.Utils;
using UnityEditor;
using UnityEngine;

public class StarDataImporterWindow : EditorWindow {

    private string _inputFilePath = "Assets/";
    private string _outputFilePath = "Assets/Resources/";
    private float _apparentMagnitudeCutoff = 6.0f;

    // REFERENCE: https://github.com/astronexus/HYG-Database
    private const string RIGHT_ASCENSION_KEY        = "ra";
    private const string DECLINATION_KEY            = "dec";
    private const string COMMON_NAME_KEY            = "proper";
    private const string DISTANCE_PARSEC_KEY        = "dist";
    private const string APPARENT_MAGNITUDE_KEY     = "mag";
    private const string ABSOLUTE_MAGNITUDE_KEY     = "absmag";
    private const string CONSTELLATION_KEY          = "con";
    private const string LUMINOSITY_KEY             = "lum";

    [MenuItem("Tools/Game/Star data importer")]
    public static void ShowWindow() {
        EditorWindow.GetWindow(typeof(StarDataImporterWindow));
    }

    void OnGUI() {
        GUILayout.Label("Input file path:", EditorStyles.label);
        this._inputFilePath = EditorGUILayout.TextField("", this._inputFilePath);
        this._outputFilePath = EditorGUILayout.TextField("", this._outputFilePath);
        this._apparentMagnitudeCutoff = float.Parse(EditorGUILayout.TextField("", this._apparentMagnitudeCutoff.ToString()));

        if (GUILayout.Button("Import")) {
            this.Import(this._inputFilePath, this._outputFilePath, this._apparentMagnitudeCutoff);
        }
    }

    private void Import(string inputFilePath, string outputFilePath, float apparentMagnitudeCutoff) {
        CSVReader.CSVResult csvResult = CSVReader.ReadCSVFile(inputFilePath);
        if (csvResult == null) {
            return;
        }

        int numStarsImported = 0;
        StarsData starsData = new StarsData();
        starsData.stars = new List<StarData>();

        var enumerator = csvResult.items.GetEnumerator();
        while (enumerator.MoveNext()) {
            CSVReader.CSVItem item = enumerator.Current;
            float apparentMagnitude = float.Parse(item.values[APPARENT_MAGNITUDE_KEY]);
            if (apparentMagnitude > apparentMagnitudeCutoff) {
                continue;
            }
            ++numStarsImported;

            StarData starData = new StarData();
            starData.common_name = item.values[COMMON_NAME_KEY];
            starData.right_ascension = float.Parse(item.values[RIGHT_ASCENSION_KEY]);
            starData.declination = float.Parse(item.values[DECLINATION_KEY]);
            starData.distance_in_parsecs = float.Parse(item.values[DISTANCE_PARSEC_KEY]);
            starData.apparent_magnitude = float.Parse(item.values[APPARENT_MAGNITUDE_KEY]);
            starData.absolute_magnitude = float.Parse(item.values[ABSOLUTE_MAGNITUDE_KEY]);
            starData.constellation_name = item.values[CONSTELLATION_KEY];
            starData.luminosity = float.Parse(item.values[LUMINOSITY_KEY]);
            starsData.stars.Add(starData);
        }

        FileStream fileStream = FileUtils.OpenFileStream(outputFilePath, FileMode.Create, FileAccess.Write);
        if (fileStream == null) {
            return;
        }

        Serializer.Serialize<StarsData>(fileStream, starsData);
        fileStream.Flush();
        FileUtils.CloseFileStream(fileStream);

        TimiDebug.LogColor("Success! Imported " + numStarsImported.ToString() + " stars", LogColor.green);
    }

}
