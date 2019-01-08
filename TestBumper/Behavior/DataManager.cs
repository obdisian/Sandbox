using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
//using System.Diagnostics;

namespace Wavy
{
    #region 基底
    public abstract class CsvDataBase
    {
        public class DataUnit
        {
            public enum Type
            {
                Int,
                Float,
                String,
                Max,
            }
            public void Init(string str)
            {
                switch (ThisType)
                {
                    case Type.Int: _value = ParseInt(str); break;
                    case Type.Float: _value = ParseFloat(str); break;
                    case Type.String: _value = str; break;
                }
            }
            private int ParseInt(string str)
            {
                int result = 0;
                if (!int.TryParse(str, out result)) { Debug.LogError($"int型のデータのパースに失敗しました"); }
                return result;
            }
            private float ParseFloat(string str)
            {
                float result = 0;
                if (!float.TryParse(str, out result)) { Debug.LogError($"float型のデータのパースに失敗しました"); }
                return result;
            }

            public Type ThisType { get; set; } = Type.Int;
            private object _value;
            //public T GetValue<T>() { return (T)_value; }
            public int Int { get { return (int)_value; } }
            public float Float { get { return (float)_value; } }
            public string Str { get { return (string)_value; } }
        }

        protected abstract DataUnit[] DataTable();



        /// <summary>
        /// csvの読み込み、データのパース
        /// </summary>
        /// <returns>The data list.</returns>
        public static List<T> GetDataList<T>(string fileName) where T : CsvDataBase, new()
        {
            var textData = new List<string[]>();
            var textAsset = Resources.Load($"Csv/{fileName}") as TextAsset;

            var rows = textAsset.text.Split('\n');
            int ignoreCount = 1;
            for (int i = 0; i < rows.Length; i++)
            {
                if (i < ignoreCount) { continue; }

                var row = rows[i];
                var columns = row.Split(',');
                textData.Add(columns);
            }

            var list = new List<T>();
            for (int i = 0; i < textData.Count; i++)
            {
                var text = textData[i];
                var data = new T();

                var dataList = data.DataTable();
                for (int index = 0; index < Mathf.Min(text.Length, dataList.Length); index++)
                {
                    dataList[index].Init(text[index]);
                }
                list.Add(data);
            }
            return list;
        }
    }
    #endregion



    #region 使用するデータ(CsvDataBaseを継承して定義する)
    /// <summary>
    /// キャラクターのレベルデータ
    /// </summary>
    public class CharacterLevelData : CsvDataBase
    {
        protected override DataUnit[] DataTable()
        {
            return new DataUnit[]
            {
                _level,
                _exPoint,
                _scale,
                _speed,
                _cameraDistance,
            };
        }

        private DataUnit _level = new DataUnit { ThisType = DataUnit.Type.Int };
        private DataUnit _exPoint = new DataUnit { ThisType = DataUnit.Type.Float };
        private DataUnit _scale = new DataUnit { ThisType = DataUnit.Type.Float };
        private DataUnit _speed = new DataUnit { ThisType = DataUnit.Type.Float };
        private DataUnit _cameraDistance = new DataUnit { ThisType = DataUnit.Type.Float };

        public int Level { get { return _level.Int; }}
        public float ExPoint { get { return _exPoint.Float; } }
        public float Scale { get { return _scale.Float; } }
        public float Speed { get { return _speed.Float; } }
        public float CameraDistance { get { return _cameraDistance.Float; } }
    }

    /// <summary>
    /// 各国のユーザーの名前データ
    /// </summary>
    public class CountryNameData : CsvDataBase
    {
        protected override DataUnit[] DataTable()
        {
            List<DataUnit> ret = new List<DataUnit>();
            ret.Add(_no);
            ret.Add(_country);
            ret.AddRange(_nameList);
            return ret.ToArray();
        }

        private DataUnit _no = new DataUnit { ThisType = DataUnit.Type.Int };
        private DataUnit _country = new DataUnit { ThisType = DataUnit.Type.String };
        private const int NameCount = 10;
        private DataUnit[] _nameList = new DataUnit[NameCount]
        {
            new DataUnit { ThisType = DataUnit.Type.String },
            new DataUnit { ThisType = DataUnit.Type.String },
            new DataUnit { ThisType = DataUnit.Type.String },
            new DataUnit { ThisType = DataUnit.Type.String },
            new DataUnit { ThisType = DataUnit.Type.String },
            new DataUnit { ThisType = DataUnit.Type.String },
            new DataUnit { ThisType = DataUnit.Type.String },
            new DataUnit { ThisType = DataUnit.Type.String },
            new DataUnit { ThisType = DataUnit.Type.String },
            new DataUnit { ThisType = DataUnit.Type.String },
        };

        public int No { get { return _no.Int; } }
        public string Country { get { return _country.Str; } }
        public string GetName(int index)
        {
            if (index < 0 && _nameList.Length <= index) { return string.Empty; }
            return _nameList[index].Str;
        }
        public string GetRandomName()
        {
            if (_nameList.Length <= 0) { return string.Empty; }
            return _nameList[Random.Range(0, _nameList.Length)].Str;
        }
    }
    #endregion



    /// <summary>
    /// データ管理クラス
    /// </summary>
    public class DataManager : SingletonMonoBehaviour<DataManager>
	{
        public List<CharacterLevelData> CharacterLevelDataList = new List<CharacterLevelData>();
        public List<CountryNameData> CountryNameDataList = new List<CountryNameData>();


        // initialize
        protected override void Initialize()
		{
            // 読み込むファイル
            CharacterLevelDataList = CsvDataBase.GetDataList<CharacterLevelData>("LevelTable");
            CountryNameDataList = CsvDataBase.GetDataList<CountryNameData>("CountryNameTable");
        }
    }
}
