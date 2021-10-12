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
        private List<T> _data;
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
                using (StreamReader sr = new StreamReader(path))
                {
                    var index = sr.ReadToEnd();
                    _index = JsonSerializer.Deserialize<List<Models.Index>>(index);
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
                var path = _connectionString + @"/data.json";
                using (StreamReader sr = new StreamReader(path))
                {
                    var data = sr.ReadToEnd();
                    _data = JsonSerializer.Deserialize<List<T>>(data);
                }
            }
            catch 
            {
                throw new Exception("Failed retrieving data");
            }
        }


        public bool Insert(T item) // add data to db & dbindex
        {
            if (this.Select(item.Key) != null) return false;

            _data.Add(item);

            var newIndex = new Models.Index(item.Key, _data.Count - 1);
            _index.Add(newIndex);

            return Save();
        }

        public T Select(int key) // retrieve data from db using dbindex
        {
            var dataIndex = UniformBinarySearch(key);

            return dataIndex == null ? default(T) : _data[dataIndex.Location];
        }

        public bool Delete(int key) // delete data from db and dbindex
        {
            var dataIndex = UniformBinarySearch(key);

            if (dataIndex == null) return false;

            _data.RemoveAt(dataIndex.Location);
            _index.Remove(dataIndex);

            return Save();
        }


        private Models.Index UniformBinarySearch(int key) // search in dbindex
        {
            int n = _index.Count;

            if (key > _index[n - 1].Key) return null;

            int average;
            int first = 0;
            int last = n - 1;
            Models.Index result = null;

            while (first < last)
            {
                average = first + (last - first) / 2;
                if (key == _index[average].Key) 
                {
                    result = _index[average];
                    break;
                }
                else if (key <= _index[average].Key) 
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
            try
            {
                using (StreamWriter sw = new StreamWriter(
                    _connectionString + @"/data.json", false, System.Text.Encoding.UTF8))
                {
                    var data = JsonSerializer.Serialize<List<T>>(_data);
                    sw.WriteLine(data);
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
            catch
            {
                return false;
            }
        }
    }
}