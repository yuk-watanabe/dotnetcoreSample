﻿using System;

using System.IO;
using System.Runtime.Serialization.Json;

namespace JsonSerializationApp001
{
    class Program
    {
        static void Main(string[] args)
        {
            //System.Text.Encoding.CodePages を NuGet で追加
            //コンソールの文字コードの設定
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            Person person = new Person { Name = "板部岡 江雪斎", Age = 72 };

            //シリアライザーのインスタンスを生成
            DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(Person));

            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "person.json");
                
            //出力ファイル ストリームの生成
            using (FileStream stream = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                //シリアライズ
                jsonSerializer.WriteObject(stream, person);
            }

            Console.WriteLine($"object was serialized. file path is {filePath}");

            Person deSerializedPerson = null;

            //入力ファイル ストリームの生成
            //using (FileStream stream = new FileStream(filePath, FileMode.Open))
            using (Stream stream = File.Open(filePath, FileMode.Open))
            {
                //逆シリアライズ
                deSerializedPerson = jsonSerializer.ReadObject(stream) as Person;
            }

            Console.WriteLine($"deserialized an instance of the object.");

            Console.WriteLine($"Person.Name={deSerializedPerson.Name}, Person.Age={deSerializedPerson.Age}");


            //Stream stream01 = File.Open(filePath, FileMode.Open);
            //deSerializedPerson = jsonSerializer.ReadObject(stream01) as Person;

            Console.WriteLine($"Person.Name={deSerializedPerson.Name}, Person.Age={deSerializedPerson.Age}");

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}