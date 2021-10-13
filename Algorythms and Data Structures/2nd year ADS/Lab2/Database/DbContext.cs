using System.Threading;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Lab2.Database.Models;
using Lab2.Models;

namespace Lab2.Database
{
    public class DbContext<T> where T : IDataModel
    {
        private readonly string _connectionString;
        private List<List<T>> _data;
        private List<T> _overflow;
        private List<Models.Index> _index;

        public DbContext(string connectionString)
        {
            this._connectionString = connectionString;
            this._data = new List<List<T>>();
            this._overflow = new List<T>();
            this._index = new List<Models.Index>();

            SetIndex(); // _index
            SetData();  // _data
            SetOverflow();
        }


        private void SetIndex() // sets current index
        {
            try
            {
                var path = _connectionString + @"/index.json";

                if (!File.Exists(path))
                {
                    var fs = File.Create(path);
                    fs.Close();
                }

                using (var sr = new StreamReader(path))
                {
                    var index = sr.ReadToEnd();
                    try
                    {
                        _index = JsonSerializer.Deserialize<List<Models.Index>>(index);
                    }
                    catch { }
                }
            }
            catch
            {
                throw new Exception("Failed retrieving index");
            }
        }
        private void SetData() // sets current data
        {
            try
            {
                var blocks = Directory.GetFiles(_connectionString, @"*data.json");

                if (blocks.Length < 12)
                {
                    for (int i = 0; i < 12; i++)
                    {
                        var tmp = "";
                        if (i < 10)
                        {
                            tmp = "0" + i;
                        }
                        else
                        {
                            tmp = "" + i;
                        }
                        var fs = File.Create(_connectionString + $"/{tmp}data.json");
                        fs.Close();
                    }
                }

                foreach (var dataBlock in blocks)
                {
                    using (var sr = new StreamReader(dataBlock))
                    {
                        try
                        {
                            var data = sr.ReadToEnd();
                            _data.Add(JsonSerializer.Deserialize<List<T>>(data));
                        }
                        catch
                        {
                            _data.Add(new List<T>());
                        }
                    }
                }
            }
            catch
            {
                throw new Exception("Failed retrieveing data");
            }
        }
        private void SetOverflow() // sets current oveflow data
        {
            try
            {
                var path = _connectionString + @"/overflow.json";

                if (!File.Exists(path))
                {
                    var fs = File.Create(path);
                    fs.Close();
                }

                using (var sr = new StreamReader(path))
                {
                    try
                    {
                        var data = sr.ReadToEnd();
                        _overflow = JsonSerializer.Deserialize<List<T>>(data);
                    }
                    catch { }
                }
            }
            catch
            {
                throw new Exception("Failed retrieving overflow data");
            }
        }


        public bool Insert(T item) // add data to db & dbindex
        {
            if (this.Select(item.Key) != null) return false;

            int blockIndex = -1;
            bool isOverflow = false;

            foreach (var pair in _index) // find where key should be stored
            {
                if (pair.Key < item.Key)
                {
                    blockIndex = pair.Location;
                }
            }

            if (blockIndex == -1) // if key is out of boundries of index
            {
                isOverflow = true;
            }
            else if (_data[blockIndex].Count >= 450) // if datablock is full
            {
                isOverflow = true;
            }

            if (!isOverflow) // if its not in overflow
            {
                if (_data[blockIndex].Count == 0)
                {
                    _data[blockIndex].Add(item);
                    return SaveData(blockIndex);
                }

                for (int i = 0; i < _data[blockIndex].Count; i++) 
                {
                    var dataItem = _data[blockIndex][i];

                    if (dataItem.Key > item.Key)
                    {
                        _data[blockIndex].Insert(i, item);
                        return SaveData(blockIndex);
                    }

                    if (i == _data[blockIndex].Count - 1)
                    {
                        _data[blockIndex].Insert(i, item);
                        return SaveData(blockIndex);
                    }
                }
            }
        
            if (isOverflow) // if its in overflow
            {
                if (_overflow.Count == 0)
                {
                    _overflow.Add(item);
                    return SaveOverflow();
                }

                for (int i = 0; i < _overflow.Count; i++)
                {
                    var dataItem = _overflow[i];

                    if (dataItem.Key > item.Key)
                    {
                        _overflow.Insert(i, item);
                        return SaveOverflow();
                    }

                    if (i == _overflow.Count - 1)
                    {
                        _overflow.Insert(i, item);
                        return SaveOverflow();
                    }
                }
            }
        
            return false;
        }   

        public T Select(int key) // retrieve data from db using dbindex
        {
            int blockIndex = -1;
            bool isOverflow = false;
            T result = default(T);

            foreach (var pair in _index) // find where key could be stored
            {
                if (pair.Key < key)
                {
                    blockIndex = pair.Location;
                }
            }

            if (blockIndex == -1) // if key is out of boundries of index
            {
                isOverflow = true;
            }
            
            if (!isOverflow) // if its not in overflow
            {
                result = (T)UniformBinarySearch(_data[blockIndex], key);
                if (result == null)
                {
                    isOverflow = true;
                }
            }

            if (isOverflow) // if its in overflow
            {
                result = (T)UniformBinarySearch(_overflow, key);
            }

            return result;
        }

        public bool Delete(int key) // delete data from db and dbindex
        {
            throw new NotImplementedException();
        }


        private object UniformBinarySearch(dynamic data, int key) // search in dbindex
        {
            int n = data.Count;

            // if (key > data[n - 1].Key) return null;

            int average;
            int first = 0;
            int last = n - 1;
            object result = null;
            
            while (first < last)
            {
                average = first + (last - first) / 2;
                if (key == data[first].Key)
                {
                    result = data[first];
                }
                else if (key == data[average].Key) 
                {
                    result = data[average];
                    break;
                }
                else if (key <= data[average].Key) 
                {
                    last = average;
                }
                else 
                {
                    if (first != average)
                    {
                        first = average;
                    }
                    else 
                    {
                        first++;
                    }
                }
            }

            return result;
        }


        public bool SaveIndex() // edit data & dbindex data
        {
            var path = _connectionString + $"/index.json";

            using (StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.UTF8))
            {
                var index = JsonSerializer.Serialize<List<Models.Index>>(_index);
                sw.WriteLine(index);
            }

            return true;
        }
        public bool SaveData(int blockIndex) // edit data & dbindex data
        {
            var dataBlock = _data[blockIndex];
            
            var tmp = "";
            if (blockIndex < 10)
            {
                tmp = "0" + blockIndex;
            }
            else
            {
                tmp = "" + blockIndex;
            }

            var path = _connectionString + $"/{tmp}data.json";

            using (StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.UTF8))
            {
                var data = JsonSerializer.Serialize<List<T>>(dataBlock);
                sw.WriteLine(data);
            }

            return true;
        }
        public bool SaveOverflow() // edit data & dbindex data
        {
            var path = _connectionString + $"/overflow.json";

            using (StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.UTF8))
            {
                var data = JsonSerializer.Serialize<List<T>>(_overflow);
                sw.WriteLine(data);
            }

            return true;
        }
    }
}