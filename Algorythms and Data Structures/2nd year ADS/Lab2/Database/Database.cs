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
        private List<Models.Index> _index;

        public DbContext(string connectionString)
        {
            this._connectionString = connectionString;
            SetData();  // _data
            SetIndex(); // _index
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

                using (StreamReader sr = new StreamReader(path))
                {
                    var index = sr.ReadToEnd();
                    try
                    {
                        _index = JsonSerializer.Deserialize<List<Models.Index>>(index);
                    }
                    catch
                    {
                        _index = new List<Models.Index>();
                    }
                }
            }
            catch
            {
                throw new Exception("Failed retrieving index");
            }
        }

        private void SetData() // sets current data
        {
            if (_data == null) _data = new List<List<T>>();

            var dataFiles = Directory.GetFiles(_connectionString, @"*ata.json");

            foreach (var file in dataFiles)
            {
                try
                {
                    using (StreamReader sr = new StreamReader(file))
                    {
                        var data = sr.ReadToEnd();
                        try
                        {
                            _data.Add(JsonSerializer.Deserialize<List<T>>(data));
                        }
                        catch
                        {
                            _data.Add(new List<T>());
                        }
                    }
                }
                catch
                {
                    throw new Exception("Failed retrieving data");
                }
            }
        }

        private void SplitBlock(List<T> block, int pos)
        {
            var newBlocks = new List<List<T>>();

            for (int i = 0; i < block.Count; i += block.Count / 2)
            {
                newBlocks.Add(block.GetRange(i, Math.Min(block.Count / 2, block.Count - i)));
            }

            _data[pos] = newBlocks[0];
            _data.Insert(pos + 1, newBlocks[1]);

            for (int i = 0; i < _data[pos].Count; i++)
            {
                var index = _index.FirstOrDefault(_i => _i.Key == _data[pos][i].Key);
                index.Location = pos;
            }

            for (int i = 0; i < _data[pos + 1].Count; i++)
            {
                var index = _index.FirstOrDefault(_i => _i.Key == _data[pos + 1][i].Key);
                index.Location = pos + 1;
            }
        }


        public bool Insert(T item) // add data to db & dbindex
        {
            if (this.Select(item.Key) != null) return false;

            if (_data.Count == 0)
            {
                _data.Add(new List<T>());
                _data[0].Add(item);
                _index.Add(new Models.Index(item.Key, 0));
                return Save();
            }

            for (int i = 0; i < _data.Count; i++)
            {
                // System.Console.WriteLine(_data[i].Count);
                // System.Console.WriteLine(_index.Count);

                var block = _data[i];

                if (_data[i].Count >= 1000)
                {
                    SplitBlock(_data[i], i);
                }
                
                bool condition;

                try 
                {
                    condition = item.Key > _data[i][0].Key && item.Key < _data[i + 1][0].Key;
                }
                catch 
                { 
                    condition = true;
                }

                if (condition)
                {
                    if (_data[i].Count == 0)
                    {
                        _data[i].Add(item);
                    } 
                    else
                    {
                        for (int j = 0; j < _data[i].Count; j++)
                        {
                            if (_data[i][j].Key > item.Key || j == _data[i].Count - 1)
                            {
                                _data[i].Insert(j, item);
                                break;
                            }
                        }
                    }
                    
                    _index.Add(new Models.Index(item.Key, i));
                    
                    return Save();
                }
            }

            return Save();
        }   

        public T Select(int key) // retrieve data from db using dbindex
        {
            var blockIndex = (Models.Index)UniformBinarySearch(_index, key);

            if (blockIndex == null) return default(T);

            var dataBlock = _data[blockIndex.Location];

            var data = (T)UniformBinarySearch(dataBlock, key);

            return data;
        }

        public bool Delete(int key) // delete data from db and dbindex
        {
            var data = this.Select(key);
            if (data == null) return false;

            var blockIndex = (Models.Index)UniformBinarySearch(_index, key);

            _data[blockIndex.Location].Remove(data);
            _index.Remove(blockIndex);


            return Save();
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

        public bool Save() // edit data & dbindex data
        {
            for (int i = 0; i < _data.Count; i++)
            {
                _data = _data.OrderBy(i => i[0].Key).ToList();
                var dataBlock = _data[i];
                var path = _connectionString + $"/{i}data.json";

                if (!File.Exists(path))
                {
                    var fs = File.Create(path);
                    fs.Close();
                }

                using (StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.UTF8))
                {
                    dataBlock = dataBlock.OrderBy(b => b.Key).ToList();
                    var data = JsonSerializer.Serialize<List<T>>(dataBlock);
                    sw.WriteLine(data);
                }
            }
            
            

            using (StreamWriter sw = new StreamWriter(
                _connectionString + @"/index.json", false, System.Text.Encoding.UTF8))
            {
                _index.Sort(new IndexSorter());
                var index = JsonSerializer.Serialize<List<Models.Index>>(_index);
                sw.WriteLine(index);
            }

                return true;
        }
    }
}