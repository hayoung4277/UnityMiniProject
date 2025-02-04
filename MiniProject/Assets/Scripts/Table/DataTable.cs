using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System.IO;
using System.Globalization;
using System.Linq;

public abstract class DataTable
{
    public static readonly string FormatPath = "tables/{0}";

    public abstract void Load(string filename);

    public static List<T> LoadCSV<T>(string csv)
    {
        using (var reader = new StringReader(csv))
        using (var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            csvReader.Context.TypeConverterCache.AddConverter<List<int>>(new IntListConverter());
            csvReader.Context.TypeConverterCache.AddConverter<List<string>>(new StringListConverter());

            return csvReader.GetRecords<T>().ToList();
        }
    }
}

//정수 리스트 변환기 (예: "1,2,3" → List<int> {1, 2, 3})
public class IntListConverter : DefaultTypeConverter
{
    public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
    {
        if (string.IsNullOrWhiteSpace(text)) return new List<int>();
        return text.Split(',').Select(int.Parse).ToList();
    }

    public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
    {
        return value is List<int> list ? string.Join(",", list) : "";
    }
}

//문자열 리스트 변환기 (예: "Fire,Water,Wind" → List<string> {"Fire", "Water", "Wind"})
public class StringListConverter : DefaultTypeConverter
{
    public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
    {
        if (string.IsNullOrWhiteSpace(text)) return new List<string>();
        return text.Split(',').ToList();
    }

    public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
    {
        return value is List<string> list ? string.Join(",", list) : "";
    }
}