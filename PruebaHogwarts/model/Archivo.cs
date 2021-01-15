using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using PruebaHogwarts.Entity;
using System.Text.Json;
using Newtonsoft.Json;

namespace PruebaHogwarts.model
{
    public static class Archivo
    {
        static string path = "estudiantes.json";
        public static void crear(IEnumerable<Estudiante> estudiantes)
        {
            try
            {
                // Create the file, or overwrite if the file exists.
                using (FileStream fs = File.Create(path))
                {
                    var json = JsonConvert.SerializeObject(estudiantes);
                    byte[] info = new UTF8Encoding(true).GetBytes(json);
                    // Add some information to the file.
                    fs.Write(info, 0, info.Length);
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static List<Estudiante> leer()
        {
            try
            {
                // Open the stream and read it back.
                using (StreamReader sr = File.OpenText(path))
                {
                    string jsonString = "", s="";
                    while ((s = sr.ReadLine()) != null)
                    {
                        jsonString = s;
                    }
                    if (jsonString != "")
                        {
                        List<Estudiante> json = JsonConvert.DeserializeObject<List<Estudiante>>(jsonString);
                        return json;
                    }

                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return null;
        }
    }
}
