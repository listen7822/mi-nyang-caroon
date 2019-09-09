using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;

public class CSVReader
{
	static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
	static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
	static char[] TRIM_CHARS = { '\"' };
	
	public static List<Dictionary<string, object>> Read(string file , DialogueManager dir)
	{
		var list = new List<Dictionary<string, object>>();
        TextAsset data = Resources.Load(file) as TextAsset;
		
        var lines = Regex.Split(data.text, LINE_SPLIT_RE);
        if (lines.Length <= 1) return list;

        var header = Regex.Split(lines[0], SPLIT_RE);

		for(var i=1; i < lines.Length; i++) {
			
			var values = Regex.Split(lines[i], SPLIT_RE);
			if(values.Length == 0 ||values[0] == "") continue;
			
			var entry = new Dictionary<string, object>();
			for(var j=0; j < header.Length && j < values.Length; j++ ) {
				string value = values[j];
				value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\n", "\n");
				object finalvalue = value;
				int n;
				float f;
				if(int.TryParse(value, out n)) {
					finalvalue = n;
				} else if (float.TryParse(value, out f)) {
					finalvalue = f;
				}
				entry[header[j]] = finalvalue;
			}
			list.Add (entry);
		}
        dir.IsReady = true;
        return list;
	}


    public static Dictionary<string, List<object>> CSVLeader(string FilePath)
    {
        Dictionary<string, List<object>> mapData = null;

		TextAsset data = Resources.Load(FilePath) as TextAsset;

		string tempString = data.text;
        //유니코드를 사용하기 위한 조건 System.Text.Encoding.GetEncoding("euc-kr") 없다면 한글 못읽어드림
        System.IO.StreamReader sr = new System.IO.StreamReader(tempString, System.Text.Encoding.GetEncoding("euc-kr"));
        string[] line = System.Text.RegularExpressions.Regex.Split(sr.ReadToEnd(), "\r\n");
		foreach (var item in line)
		{
			Debug.Log(item);
		}
        if (line.Length > 0)
        {
            mapData = new Dictionary<string, List<object>>();
            int l = line.Length - 1; //마지막 라인은 비어있는("" 만 들어가있다.) 곳이라 -1를 해준다.
            for (int i = 1; i < l; ++i)
            {
                string[] values = System.Text.RegularExpressions.Regex.Split(line[i], ","); //열의 위치값을 분리한다.
                if (values.Length == 0) continue; //값이 없다면 다음으로
                int valuel = values.Length - 1;
                List<object> newObjs = new List<object>();
                for (int j = 1; j < valuel; ++j) //내가 정한기준이 0번쨰는 Header(mapData의 키값으로 사용) 이므로 건너 뛴다. 
                {
                    if (values[j] == "") continue;
                    string newObj = values[j];
                    object finishValue = newObj;
                    int n;
                    float f;
                    if (int.TryParse(newObj, out n))
                    {
                        finishValue = n;
                    }
                    else if (float.TryParse(newObj, out f))
                    {
                        finishValue = f;
                    }
                    newObjs.Add(finishValue);
                }
                mapData.Add(values[0], newObjs);
            }
        }
		Debug.Log(mapData);
        return mapData;
    }


	public static List<Dictionary<string, object>> Read1(string file)
	{
		var list = new List<Dictionary<string, object>>();
		System.IO.StreamReader sr =  new System.IO.StreamReader(Application.streamingAssetsPath + "/" + file + ".CSV", System.Text.Encoding.GetEncoding("euc-kr"));
        string[] line = System.Text.RegularExpressions.Regex.Split(sr.ReadToEnd(), "\r\n");

		if(line.Length <= 1) return list;
		
		var header = Regex.Split(line[0], SPLIT_RE);
		for(var i=1; i < line.Length; i++) {
			
			var values = Regex.Split(line[i], SPLIT_RE);
			if(values.Length == 0 ||values[0] == "") continue;
			
			var entry = new Dictionary<string, object>();
			for(var j=0; j < header.Length && j < values.Length; j++ ) {
				string value = values[j];
				value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "");
				object finalvalue = value;
				int n;
				float f;
				if(int.TryParse(value, out n)) {
					finalvalue = n;
				} else if (float.TryParse(value, out f)) {
					finalvalue = f;
				}
				entry[header[j]] = finalvalue;
			}
			list.Add (entry);
		}
		return list;
	}
}
